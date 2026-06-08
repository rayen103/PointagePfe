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
class ETAInput(BaseModel):
    # Legacy compatibility payload
    DistanceFromStop: Optional[float] = None
    log_distance: Optional[float] = None
    distance_over_300m: Optional[int] = None
    hour: Optional[int] = None
    hour_sin: Optional[float] = None
    hour_cos: Optional[float] = None
    is_rush_hour: Optional[int] = None
    day_of_week: Optional[int] = None

    # New database payload
    Latitude: Optional[float] = None
    Longitude: Optional[float] = None
    CodeCircuit: Optional[str] = None
    ModelBus: Optional[str] = None
    Capacite: Optional[float] = None
    CurrentOccupancy: Optional[float] = None
    LastPositionAt: Optional[datetime] = None

    @model_validator(mode="after")
    def validate_input_shape(self):
        has_legacy = all(getattr(self, field) is not None for field in BASE_FEATURE_NAMES)
        raw_required = [
            "Latitude",
            "Longitude",
            "CodeCircuit",
            "ModelBus",
            "Capacite",
            "CurrentOccupancy",
            "LastPositionAt",
        ]
        has_raw = all(getattr(self, field) is not None for field in raw_required)

        if not has_legacy and not has_raw:
            raise ValueError(
                "Payload must contain either legacy ETA features or raw DB fields "
                "(Latitude, Longitude, CodeCircuit, ModelBus, Capacite, CurrentOccupancy, LastPositionAt)."
            )
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
    if stops_df is None or not route_code:
        return DEFAULT_DISTANCE_FALLBACK_METERS, True

    route_stops = stops_df[stops_df["RouteCode"] == route_code.strip().lower()].copy()
    if route_stops.empty:
        return DEFAULT_DISTANCE_FALLBACK_METERS, True

    route_stops = route_stops.sort_values("StopOrder", kind="stable").reset_index(drop=True)
    route_stops["Latitude"] = pd.to_numeric(route_stops["Latitude"], errors="coerce")
    route_stops["Longitude"] = pd.to_numeric(route_stops["Longitude"], errors="coerce")
    route_stops = route_stops.dropna(subset=["Latitude", "Longitude"])
    if route_stops.empty:
        return DEFAULT_DISTANCE_FALLBACK_METERS, True
    current = (latitude, longitude)

    route_stops["distance_to_bus"] = route_stops.apply(
        lambda row: haversine(current, (row["Latitude"], row["Longitude"]), unit=Unit.METERS),
        axis=1,
    )

    if route_stops["distance_to_bus"].isna().all():
        return DEFAULT_DISTANCE_FALLBACK_METERS, True

    nearest_idx = int(route_stops["distance_to_bus"].idxmin())
    nearest_order = route_stops.loc[nearest_idx, "StopOrder"]

    if pd.isna(nearest_order):
        next_stop = route_stops.loc[nearest_idx]
    else:
        candidates = route_stops[route_stops["StopOrder"] > nearest_order]
        next_stop = candidates.iloc[0] if not candidates.empty else route_stops.iloc[0]

    distance_meters = haversine(
        current,
        (float(next_stop["Latitude"]), float(next_stop["Longitude"])),
        unit=Unit.METERS,
    )
    return max(distance_meters, MIN_DISTANCE_METERS), False


def _parse_day_of_week(dt: datetime) -> int:
    # CHANGED: convert Python weekday (Mon=0..Sun=6) to .NET style (Sun=0..Sat=6).
    return (dt.weekday() + 1) % 7


# CHANGED: central feature engineering shared by prediction + training.
def engineer_features_from_raw(
    latitude: float,
    longitude: float,
    code_circuit: str,
    model_bus: str,
    capacite: float,
    current_occupancy: float,
    last_position_at: datetime,
    stops_csv_path: str = DEFAULT_STOPS_CSV_PATH,
) -> dict:
    stops_df = load_stops_csv(stops_csv_path)
    distance_meters, used_fallback = calculate_distance_to_next_stop(latitude, longitude, code_circuit, stops_df)

    safe_capacity = max(float(capacite or 0), MIN_CAPACITY)
    occupancy_ratio = float(current_occupancy or 0) / safe_capacity

    hour = int(last_position_at.hour)
    day_of_week = _parse_day_of_week(last_position_at)

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
        # CHANGED: support both legacy feature payload and new raw DB payload.
        has_legacy_payload = all(getattr(input_data, field) is not None for field in BASE_FEATURE_NAMES)

        if has_legacy_payload:
            hour = int(input_data.hour)
            feature_map = {
                "DistanceFromStop": float(input_data.DistanceFromStop),
                "log_distance": float(input_data.log_distance),
                "distance_over_300m": int(input_data.distance_over_300m),
                "hour": hour,
                "hour_sin": float(input_data.hour_sin) if input_data.hour_sin is not None else float(math.sin(2 * math.pi * hour / 24)),
                "hour_cos": float(input_data.hour_cos) if input_data.hour_cos is not None else float(math.cos(2 * math.pi * hour / 24)),
                "is_rush_hour": int(input_data.is_rush_hour),
                "day_of_week": int(input_data.day_of_week),
                "occupancy_ratio": 0.0,
                "route_encoded": 0.0,
                "model_encoded": 0.0,
                "DirectionRef": 0.0,
                "is_weekend": int(int(input_data.day_of_week) in (0, 6)),
                "used_fallback_stop": False,
            }
        else:
            timestamp = input_data.LastPositionAt
            feature_map = engineer_features_from_raw(
                latitude=float(input_data.Latitude),
                longitude=float(input_data.Longitude),
                code_circuit=str(input_data.CodeCircuit),
                model_bus=str(input_data.ModelBus),
                capacite=float(input_data.Capacite),
                current_occupancy=float(input_data.CurrentOccupancy),
                last_position_at=timestamp,
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
