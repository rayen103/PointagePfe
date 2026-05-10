from __future__ import annotations

import json
from pathlib import Path
from typing import List, Optional

import joblib
import numpy as np
import torch
from fastapi import FastAPI, HTTPException, WebSocket
from pydantic import BaseModel, Field

from data_loader import FEATURES
from model import LSTMAutoencoder

MODEL_DIR = Path("artifacts")
app = FastAPI(title="Anomaly Detection Service")
bundle = None
ae = None
meta = {"model_version": "untrained", "last_trained": "never"}


class TelemetryPoint(BaseModel):
    speed: float
    acceleration: float
    jerk: float
    heading_change_rate: float
    dwell_time: float
    distance_from_route: float


class DetectRequest(BaseModel):
    bus_id: str
    telemetry_window: List[TelemetryPoint] = Field(min_length=20, max_length=20)


class DetectResponse(BaseModel):
    is_anomaly: bool
    score: float
    anomaly_type: Optional[str]


class HealthResponse(BaseModel):
    status: str
    model_version: str
    last_trained: str


@app.on_event("startup")
def startup():
    global bundle, ae, meta
    if (MODEL_DIR / "detector.joblib").exists():
        bundle = joblib.load(MODEL_DIR / "detector.joblib")
    if (MODEL_DIR / "autoencoder.pt").exists():
        ae = LSTMAutoencoder(input_dim=len(FEATURES))
        ae.load_state_dict(torch.load(MODEL_DIR / "autoencoder.pt", map_location="cpu"))
        ae.eval()
    if (MODEL_DIR / "metadata.json").exists():
        meta = json.loads((MODEL_DIR / "metadata.json").read_text(encoding="utf-8"))


@app.get("/health", response_model=HealthResponse)
def health():
    return HealthResponse(status="ok", model_version=meta.get("model_version", "untrained"), last_trained=meta.get("last_trained", "never"))


def infer(window: List[TelemetryPoint]):
    x = np.array([[getattr(p, f) for f in FEATURES] for p in window], dtype=np.float32)
    x = np.clip(x, -3, 3)
    x = bundle["scaler"].transform(x).reshape(1, 20, len(FEATURES))
    t = torch.from_numpy(x)
    with torch.no_grad():
        rec = ae(t)
        err = float(((t - rec) ** 2).mean().item())
    iso_score = float(-bundle["iso"].score_samples(x.reshape(1, -1))[0])
    score = 0.5 * (err / max(bundle["threshold"], 1e-6)) + 0.5 * iso_score
    is_anomaly = score > 1.0
    anomaly_type = "route_deviation" if is_anomaly else None
    return is_anomaly, float(score), anomaly_type


@app.post("/anomaly/detect", response_model=DetectResponse)
def detect(req: DetectRequest):
    try:
        if bundle is None or ae is None:
            raise HTTPException(status_code=500, detail="Model not loaded")
        is_anomaly, score, anomaly_type = infer(req.telemetry_window)
        return DetectResponse(is_anomaly=is_anomaly, score=score, anomaly_type=anomaly_type)
    except HTTPException:
        raise
    except Exception as exc:
        raise HTTPException(status_code=500, detail=f"Detection failed: {exc}") from exc


@app.websocket("/anomaly/stream")
async def stream(websocket: WebSocket):
    await websocket.accept()
    try:
        while True:
            payload = await websocket.receive_json()
            req = DetectRequest(**payload)
            is_anomaly, score, anomaly_type = infer(req.telemetry_window)
            await websocket.send_json({"is_anomaly": is_anomaly, "score": score, "anomaly_type": anomaly_type})
    except Exception:
        await websocket.close()
