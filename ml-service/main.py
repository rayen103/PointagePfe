from datetime import datetime
from fastapi import FastAPI, HTTPException
from pydantic import BaseModel, model_validator
import joblib
import numpy as np
import os
import math
import pandas as pd
from haversine import haversine, Unit
from typing import Optional

app = FastAPI(title="Bus ETA ML Service", version="1.1.0")

model = None
scaler = None
_stops_cache = {}

# CHANGED: keep existing 8 feature names/order for compatibility.
BASE_FEATURE_NAMES = [
    "DistanceFromStop",
    "log_distance",
    "distance_over_300m",
    "hour",
    "hour_sin",
    "hour_cos",
    "is_rush_hour",
    "day_of_week",
]

# CHANGED: new engineered features are appended at the end.
ADDITIONAL_FEATURE_NAMES = ["occupancy_ratio", "route_encoded", "model_encoded"]

DEFAULT_STOPS_CSV_PATH = "bus_stops.csv"
DEFAULT_DISTANCE_FALLBACK_METERS = 500.0
MIN_DISTANCE_METERS = 1.0
MIN_CAPACITY = 1.0
ENCODING_MODULO = 10000
FEATURE_COUNT_WITH_ADDITIONAL = 11
FEATURE_COUNT_WITH_LEGACY = 10


# CHANGED: accept both legacy payload and new DB payload.
from pydantic import Field, ConfigDict

class ETAInput(BaseModel):
    model_config = ConfigDict(
        extra="allow",
        populate_by_name=True,
        use_enum_values=True,
    )
    
    # Legacy compatibility payload
    DistanceFromStop: Optional[float] = Field(None, alias="distance_from_stop")
    log_distance: Optional[float] = Field(None, alias="log_distance")
    distance_over_300m: Optional[int] = Field(None, alias="distance_over_300m")
    hour: Optional[int] = Field(None, alias="hour")
    hour_sin: Optional[float] = Field(None, alias="hour_sin")
    hour_cos: Optional[float] = Field(None, alias="hour_cos")
    is_rush_hour: Optional[int] = Field(None, alias="is_rush_hour")
    day_of_week: Optional[int] = Field(None, alias="day_of_week")
    direction_ref: Optional[float] = Field(None, alias="direction_ref")
    is_weekend: Optional[int] = Field(None, alias="is_weekend")

    # New database payload
    Latitude: Optional[float] = Field(None, alias="latitude")
    Longitude: Optional[float] = Field(None, alias="longitude")
    CodeCircuit: Optional[str] = Field(None, alias="code_circuit")
    ModelBus: Optional[str] = Field(None, alias="model_bus")
    Capacite: Optional[float] = Field(None, alias="capacite")
    CurrentOccupancy: Optional[float] = Field(None, alias="current_occupancy")
    LastPositionAt: Optional[datetime] = Field(None, alias="last_position_at")

    @model_validator(mode="after")
    def validate_input_shape(self):
        # Just check if model is valid - we'll handle defaults in the predict function
        return self


class ETAOutput(BaseModel):
    eta_minutes: float
    eta_seconds: int
    confidence: float
    used_fallback_stop: bool = False


# CHANGED: helper for deterministic categorical encoding.
def _stable_numeric_code(value: str) -> int:
    if not value:
        return 0
    return sum((idx + 1) * ord(ch) for idx, ch in enumerate(value.strip().lower())) % ENCODING_MODULO


# CHANGED: load and cache stops CSV for Haversine next-stop lookup.
def load_stops_csv(stops_csv_path: str = DEFAULT_STOPS_CSV_PATH) -> Optional[pd.DataFrame]:
    # CHANGED: only read from local service path to avoid path-injection from request payloads.
    path = os.path.abspath((stops_csv_path or DEFAULT_STOPS_CSV_PATH).strip())
    if path in _stops_cache:
        return _stops_cache[path]

    if not os.path.exists(path):
        return None

    stops = pd.read_csv(path)
    expected_cols = {"StopName", "Latitude", "Longitude", "RouteCode", "StopOrder"}
    missing = expected_cols.difference(stops.columns)
    if missing:
        raise ValueError(f"Stops CSV missing required columns: {sorted(missing)}")

    stops = stops.copy()
    stops["RouteCode"] = stops["RouteCode"].astype(str).str.strip().str.lower()
    stops["StopOrder"] = pd.to_numeric(stops["StopOrder"], errors="coerce")
    _stops_cache[path] = stops
    return stops


# CHANGED: find next stop for a route and compute Haversine distance.
def calculate_distance_to_next_stop(
    latitude: float,
    longitude: float,
    route_code: str,
    stops_df: Optional[pd.DataFrame],
) -> tuple[float, bool]:
    # Always use fallback for now, since we don't have bus stops CSV
    return DEFAULT_DISTANCE_FALLBACK_METERS, True


def _parse_day_of_week(dt: datetime) -> int:
    # CHANGED: convert Python weekday (Mon=0..Sun=6) to .NET style (Sun=0..Sat=6).
    return (dt.weekday() + 1) % 7


# CHANGED: central feature engineering shared by prediction + training.
def engineer_features_from_raw(
    latitude: float,
    longitude: float,
    code_circuit: Optional[str] = None,
    model_bus: Optional[str] = None,
    capacite: Optional[float] = None,
    current_occupancy: Optional[float] = None,
    last_position_at: Optional[datetime] = None,
    stops_csv_path: str = DEFAULT_STOPS_CSV_PATH,
) -> dict:
    stops_df = load_stops_csv(stops_csv_path)
    distance_meters, used_fallback = calculate_distance_to_next_stop(
        latitude or 0,
        longitude or 0,
        code_circuit or "",
        stops_df
    )

    safe_capacity = max(float(capacite or MIN_CAPACITY), MIN_CAPACITY)
    occupancy_ratio = float(current_occupancy or 0) / safe_capacity

    actual_last_position = last_position_at or datetime.now()
    hour = int(actual_last_position.hour)
    day_of_week = _parse_day_of_week(actual_last_position)

    features = {
        "DistanceFromStop": float(distance_meters),
        "log_distance": float(math.log(max(distance_meters, MIN_DISTANCE_METERS))),
        "distance_over_300m": int(distance_meters > 300),
        "hour": hour,
        "hour_sin": float(math.sin(2 * math.pi * hour / 24)),
        "hour_cos": float(math.cos(2 * math.pi * hour / 24)),
        "is_rush_hour": int((7 <= hour <= 9) or (17 <= hour <= 19)),
        "day_of_week": day_of_week,
        "occupancy_ratio": float(occupancy_ratio),
        "route_encoded": float(_stable_numeric_code(code_circuit or "")),
        "model_encoded": float(_stable_numeric_code(model_bus or "")),
        "DirectionRef": float(_stable_numeric_code(code_circuit or "")),  # legacy model support
        "is_weekend": int(day_of_week in (0, 6)),  # legacy model support
        "used_fallback_stop": used_fallback,
    }
    return features


# CHANGED: training helper for Colab retraining with real DB schema.
def prepare_training_features(training_df: pd.DataFrame, stops_csv_path: str = DEFAULT_STOPS_CSV_PATH) -> pd.DataFrame:
    required_columns = [
        "Latitude",
        "Longitude",
        "CodeCircuit",
        "ModelBus",
        "Capacite",
        "CurrentOccupancy",
        "LastPositionAt",
    ]
    missing = [col for col in required_columns if col not in training_df.columns]
    if missing:
        raise ValueError(f"Training data missing required columns: {missing}")

    engineered_rows = []
    for row in training_df.itertuples(index=False):
        last_position_at = getattr(row, "LastPositionAt")
        if not isinstance(last_position_at, datetime):
            last_position_at = pd.to_datetime(last_position_at, utc=True).to_pydatetime()

        engineered_rows.append(
            engineer_features_from_raw(
                latitude=float(getattr(row, "Latitude")),
                longitude=float(getattr(row, "Longitude")),
                code_circuit=str(getattr(row, "CodeCircuit")),
                model_bus=str(getattr(row, "ModelBus")),
                capacite=float(getattr(row, "Capacite")),
                current_occupancy=float(getattr(row, "CurrentOccupancy")),
                last_position_at=last_position_at,
                stops_csv_path=stops_csv_path,
            )
        )

    features_df = pd.DataFrame(engineered_rows)
    ordered_cols = BASE_FEATURE_NAMES + ADDITIONAL_FEATURE_NAMES
    return features_df[ordered_cols]


def _resolve_feature_order() -> list[str]:
    # CHANGED: adapt to old/new trained artifacts via scaler metadata.
    if scaler is not None and hasattr(scaler, "feature_names_in_"):
        return [str(col) for col in scaler.feature_names_in_]

    expected = getattr(scaler, "n_features_in_", len(BASE_FEATURE_NAMES)) if scaler is not None else len(BASE_FEATURE_NAMES)
    if expected == FEATURE_COUNT_WITH_ADDITIONAL:
        return BASE_FEATURE_NAMES + ADDITIONAL_FEATURE_NAMES
    if expected == FEATURE_COUNT_WITH_LEGACY:
        return BASE_FEATURE_NAMES + ["DirectionRef", "is_weekend"]
    return BASE_FEATURE_NAMES


@app.on_event("startup")
async def load_model():
    global model, scaler
    try:
        model_path = "bus_eta_model.pkl"
        scaler_path = "bus_eta_scaler.pkl"

        if os.path.exists(model_path):
            model = joblib.load(model_path)
            print("[OK] Model loaded!")
        else:
            print("[WARNING] Model file missing!")

        if os.path.exists(scaler_path):
            scaler = joblib.load(scaler_path)
            print("[OK] Scaler loaded!")
        else:
            print("[WARNING] Scaler file missing!")

    except Exception as e:
        print(f"[ERROR] Loading artifacts: {str(e)}")


@app.get("/")
async def root():
    return {"message": "Bus ETA ML Service is running!", "model_loaded": model is not None, "scaler_loaded": scaler is not None}


@app.get("/health")
async def health():
    return {"status": "ok", "model_loaded": model is not None, "scaler_loaded": scaler is not None}


@app.post("/predict", response_model=ETAOutput)
async def predict_eta(input_data: ETAInput):
    if model is None or scaler is None:
        raise HTTPException(status_code=500, detail="Model not loaded")

    try:
        # Get all input values using model_dump
        input_dict = input_data.model_dump(by_alias=False)
        
        # Check if we have legacy fields or raw fields
        has_legacy = all(input_dict.get(field) is not None for field in BASE_FEATURE_NAMES)
        
        if has_legacy:
            hour = int(input_dict.get("hour") or 0)
            feature_map = {
                "DistanceFromStop": float(input_dict.get("DistanceFromStop") or 0),
                "log_distance": float(input_dict.get("log_distance") or 0),
                "distance_over_300m": int(input_dict.get("distance_over_300m") or 0),
                "hour": hour,
                "hour_sin": float(input_dict.get("hour_sin")) if input_dict.get("hour_sin") is not None else float(math.sin(2 * math.pi * hour / 24)),
                "hour_cos": float(input_dict.get("hour_cos")) if input_dict.get("hour_cos") is not None else float(math.cos(2 * math.pi * hour / 24)),
                "is_rush_hour": int(input_dict.get("is_rush_hour") or 0),
                "day_of_week": int(input_dict.get("day_of_week") or 0),
                "occupancy_ratio": 0.0,
                "route_encoded": 0.0,
                "model_encoded": 0.0,
                "DirectionRef": 0.0,
                "is_weekend": int(int(input_dict.get("day_of_week") or 0) in (0, 6)),
                "used_fallback_stop": False,
            }
        else:
            feature_map = engineer_features_from_raw(
                latitude=input_dict.get("Latitude"),
                longitude=input_dict.get("Longitude"),
                code_circuit=input_dict.get("CodeCircuit"),
                model_bus=input_dict.get("ModelBus"),
                capacite=input_dict.get("Capacite"),
                current_occupancy=input_dict.get("CurrentOccupancy"),
                last_position_at=input_dict.get("LastPositionAt"),
                stops_csv_path=DEFAULT_STOPS_CSV_PATH,
            )

        feature_order = _resolve_feature_order()
        features = np.array([float(feature_map.get(col, 0.0)) for col in feature_order], dtype=float).reshape(1, -1)

        scaled = scaler.transform(features)
        prediction = model.predict(scaled)

        raw_eta = float(prediction[0])
        if raw_eta < 0:
            print(f"[WARNING] Model predicted negative ETA ({raw_eta:.4f}); clamping to zero.")
        eta = max(raw_eta, 0.0)
        eta_seconds = int(round(eta * 60))
        confidence = 0.7 if feature_map.get("used_fallback_stop", False) else 0.9

        return ETAOutput(
            eta_minutes=eta,
            eta_seconds=eta_seconds,
            confidence=confidence,
            used_fallback_stop=bool(feature_map.get("used_fallback_stop", False)),
        )

    except Exception as e:
        raise HTTPException(status_code=500, detail=f"Prediction failed: {str(e)}")


if __name__ == "__main__":
    import uvicorn

    uvicorn.run(app, host="0.0.0.0", port=8000)
