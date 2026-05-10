from __future__ import annotations

import json
from datetime import datetime
from pathlib import Path
from typing import List

import joblib
import pandas as pd
from fastapi import FastAPI, HTTPException, Query
from pydantic import BaseModel

MODEL_DIR = Path("artifacts")
app = FastAPI(title="Demand Forecasting Service")
bundle = None
meta = {"model_version": "untrained", "last_trained": "never"}


class ForecastPoint(BaseModel):
    hour: int
    predicted_count: int
    lower: int
    upper: int


class ForecastResponse(BaseModel):
    predicted_count: int
    lower: int
    upper: int
    horizon_hours: List[int]


class HealthResponse(BaseModel):
    status: str
    model_version: str
    last_trained: str


@app.on_event("startup")
def startup():
    global bundle, meta
    if (MODEL_DIR / "ensemble.joblib").exists():
        bundle = joblib.load(MODEL_DIR / "ensemble.joblib")
    if (MODEL_DIR / "metadata.json").exists():
        meta = json.loads((MODEL_DIR / "metadata.json").read_text(encoding="utf-8"))


@app.get("/health", response_model=HealthResponse)
def health():
    return HealthResponse(status="ok", model_version=meta.get("model_version", "untrained"), last_trained=meta.get("last_trained", "never"))


@app.get("/demand/forecast", response_model=ForecastResponse)
def forecast(stop_id: str = Query(...), datetime_value: str = Query(..., alias="datetime")):
    try:
        if bundle is None:
            raise HTTPException(status_code=500, detail="Model not loaded")
        dt = datetime.fromisoformat(datetime_value)
        model, enc, scl = bundle["model"], bundle["encoder"], bundle["scaler"]

        rows = []
        horizon = list(range(1, 25))
        for h in horizon:
            ts = dt + pd.Timedelta(hours=h)
            rows.append(
                {
                    "datetime": ts,
                    "stop_id": stop_id,
                    "hour": ts.hour,
                    "weekday": ts.weekday(),
                    "is_holiday": 0,
                    "weather_temp": 20.0,
                    "weather_rain": 0.0,
                    "event_nearby": 0,
                    "lag_1h": 10,
                    "lag_24h": 15,
                    "lag_168h": 12,
                    "passenger_count": 0,
                }
            )

        feature_df = pd.DataFrame(rows)
        from data_loader import preprocess

        X, _, _, _ = preprocess(feature_df, fit=False, encoder=enc, scaler=scl)
        pred = model.predict(X, feature_df)
        p0 = int(max(0, round(pred[0])))
        return ForecastResponse(predicted_count=p0, lower=max(0, int(p0 * 0.85)), upper=int(p0 * 1.15), horizon_hours=horizon)
    except HTTPException:
        raise
    except ValueError as exc:
        raise HTTPException(status_code=422, detail=str(exc)) from exc
    except Exception as exc:
        raise HTTPException(status_code=500, detail=f"Forecast failed: {exc}") from exc
