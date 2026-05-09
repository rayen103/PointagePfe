from __future__ import annotations

import json
from pathlib import Path
from typing import List

import joblib
import numpy as np
import torch
from fastapi import FastAPI, HTTPException
from pydantic import BaseModel, Field

from data_loader import FEATURE_COLUMNS
from model import LSTMRegressor

MODEL_DIR = Path("artifacts")


class StatePoint(BaseModel):
    gps_lat: float
    gps_lon: float
    speed: float
    heading: float
    stop_sequence: float
    hour: float
    day_of_week: float
    weather_code: float
    historical_delay_avg: float


class ETAPredictRequest(BaseModel):
    route_id: str
    stop_id: str
    current_state: List[StatePoint] = Field(min_length=10)


class ETAPredictResponse(BaseModel):
    eta_minutes: float
    confidence_interval: List[float]


class HealthResponse(BaseModel):
    status: str
    model_version: str
    last_trained: str


app = FastAPI(title="ETA Prediction Service")
_model = None
_scaler = None
_metadata = {"model_version": "untrained", "last_trained": "never"}


def _load_artifacts():
    global _model, _scaler, _metadata
    metadata_path = MODEL_DIR / "metadata.json"
    if metadata_path.exists():
        _metadata = json.loads(metadata_path.read_text(encoding="utf-8"))
    if (MODEL_DIR / "best_model.pt").exists() and (MODEL_DIR / "scaler.joblib").exists():
        _scaler = joblib.load(MODEL_DIR / "scaler.joblib")
        _model = LSTMRegressor(input_size=len(FEATURE_COLUMNS))
        _model.load_state_dict(torch.load(MODEL_DIR / "best_model.pt", map_location="cpu"))
        _model.eval()


@app.on_event("startup")
def startup_event():
    _load_artifacts()


@app.get("/health", response_model=HealthResponse)
def health():
    return HealthResponse(status="ok", model_version=_metadata.get("model_version", "untrained"), last_trained=_metadata.get("last_trained", "never"))


@app.post("/eta/predict", response_model=ETAPredictResponse)
def predict_eta(payload: ETAPredictRequest):
    try:
        if _model is None or _scaler is None:
            raise HTTPException(status_code=500, detail="Model artifacts are not loaded")

        seq = np.array([[getattr(row, col) for col in FEATURE_COLUMNS] for row in payload.current_state], dtype=np.float32)
        if seq.shape[0] < 10:
            raise HTTPException(status_code=422, detail="current_state must contain at least 10 timesteps")
        seq = seq[-10:]
        scaled = _scaler.transform(seq)
        x = torch.from_numpy(scaled.reshape(1, 10, len(FEATURE_COLUMNS))).float()
        with torch.no_grad():
            pred = float(_model(x).item())

        spread = max(0.1 * abs(pred), 1.0)
        return ETAPredictResponse(eta_minutes=pred, confidence_interval=[pred - 1.96 * spread, pred + 1.96 * spread])
    except HTTPException:
        raise
    except Exception as exc:
        raise HTTPException(status_code=500, detail=f"Prediction failed: {exc}") from exc
