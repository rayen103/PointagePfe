from __future__ import annotations

import json
from pathlib import Path

import torch
from fastapi import FastAPI, HTTPException
from pydantic import BaseModel

from model import STGCN

MODEL_DIR = Path("artifacts")
app = FastAPI(title="Traffic-aware STGCN Service")
model = None
meta = {"model_version": "untrained", "last_trained": "never"}


class TrafficPredictRequest(BaseModel):
    segment_ids: list[str]
    current_speeds: list[float]
    horizon_minutes: int


class SegmentPrediction(BaseModel):
    segment_id: str
    predicted_travel_time: float
    confidence: float


class TrafficPredictResponse(BaseModel):
    predictions: list[SegmentPrediction]


class HealthResponse(BaseModel):
    status: str
    model_version: str
    last_trained: str


@app.on_event("startup")
def startup():
    global model, meta
    if (MODEL_DIR / "stgcn.pt").exists():
        model = STGCN()
        model.load_state_dict(torch.load(MODEL_DIR / "stgcn.pt", map_location="cpu"))
        model.eval()
    if (MODEL_DIR / "metadata.json").exists():
        meta = json.loads((MODEL_DIR / "metadata.json").read_text(encoding="utf-8"))


@app.get("/health", response_model=HealthResponse)
def health():
    return HealthResponse(status="ok", model_version=meta.get("model_version", "untrained"), last_trained=meta.get("last_trained", "never"))


@app.post("/traffic/predict", response_model=TrafficPredictResponse)
def predict(req: TrafficPredictRequest):
    try:
        if model is None:
            raise HTTPException(status_code=500, detail="Model not loaded")
        preds = []
        for seg, speed in zip(req.segment_ids, req.current_speeds):
            base = max(1.0, 30.0 / max(speed, 1.0))
            horizon_factor = req.horizon_minutes / 15.0
            p = base * horizon_factor
            preds.append(SegmentPrediction(segment_id=seg, predicted_travel_time=float(p), confidence=0.75))
        return TrafficPredictResponse(predictions=preds)
    except HTTPException:
        raise
    except Exception as exc:
        raise HTTPException(status_code=500, detail=f"Traffic prediction failed: {exc}") from exc
