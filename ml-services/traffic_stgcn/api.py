from __future__ import annotations

import json
import math
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
        if len(req.segment_ids) == 0:
            raise HTTPException(status_code=422, detail="segment_ids cannot be empty")
        if len(req.segment_ids) != len(req.current_speeds):
            raise HTTPException(status_code=422, detail="segment_ids and current_speeds length mismatch")

        hour = 12.0
        day_of_week_enc = 3.0
        weather = 0.0
        time_sin = math.sin(2 * math.pi * hour / 24.0)
        time_cos = math.cos(2 * math.pi * hour / 24.0)

        x = torch.tensor(
            [[float(speed), float(time_sin), float(time_cos), float(day_of_week_enc), float(weather)] for speed in req.current_speeds],
            dtype=torch.float32,
        )

        n = len(req.segment_ids)
        if n == 1:
            edge_index = torch.tensor([[0], [0]], dtype=torch.long)
        else:
            edges = []
            for i in range(n - 1):
                edges.append([i, i + 1])
                edges.append([i + 1, i])
            edge_index = torch.tensor(edges, dtype=torch.long).t().contiguous()

        with torch.no_grad():
            raw_pred = model(x, edge_index).cpu().numpy()

        horizon_options = [15, 30, 60]
        horizon_idx = int(min(range(len(horizon_options)), key=lambda i: abs(horizon_options[i] - req.horizon_minutes)))
        preds = []
        for idx, seg in enumerate(req.segment_ids):
            all_horizons = raw_pred[idx]
            selected = float(max(0.0, all_horizons[horizon_idx]))
            spread = float(abs(all_horizons.max() - all_horizons.min()))
            confidence = float(max(0.1, min(0.99, 1.0 / (1.0 + spread))))
            preds.append(SegmentPrediction(segment_id=seg, predicted_travel_time=selected, confidence=confidence))
        return TrafficPredictResponse(predictions=preds)
    except HTTPException:
        raise
    except Exception as exc:
        raise HTTPException(status_code=500, detail=f"Traffic prediction failed: {exc}") from exc
