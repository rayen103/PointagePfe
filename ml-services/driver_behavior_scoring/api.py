from __future__ import annotations

import json
from pathlib import Path

import joblib
import numpy as np
from fastapi import FastAPI, HTTPException, Query
from pydantic import BaseModel

MODEL_DIR = Path("artifacts")
app = FastAPI(title="Driver Behavior Scoring Service")
bundle = None
meta = {"model_version": "untrained", "last_trained": "never"}


class DriverScoreResponse(BaseModel):
    score: float
    label: str
    breakdown: dict


class HealthResponse(BaseModel):
    status: str
    model_version: str
    last_trained: str


@app.on_event("startup")
def startup():
    global bundle, meta
    if (MODEL_DIR / "model.joblib").exists():
        bundle = joblib.load(MODEL_DIR / "model.joblib")
    if (MODEL_DIR / "metadata.json").exists():
        meta = json.loads((MODEL_DIR / "metadata.json").read_text(encoding="utf-8"))


@app.get("/health", response_model=HealthResponse)
def health():
    return HealthResponse(status="ok", model_version=meta.get("model_version", "untrained"), last_trained=meta.get("last_trained", "never"))


@app.get("/driver/score", response_model=DriverScoreResponse)
def score(driver_id: str = Query(...), trip_id: str = Query(...)):
    try:
        if bundle is None:
            raise HTTPException(status_code=500, detail="Model not loaded")
        model = bundle["model"]
        le = bundle["label_encoder"]

        sample = np.array([[45, 12, 2, 3, 1, 0.1, 120, 35]], dtype=float)
        prob = model.predict_proba(sample)[0]
        idx = int(np.argmax(prob))
        label = str(le.inverse_transform([idx])[0])
        return DriverScoreResponse(
            score=float(prob[idx]),
            label=label,
            breakdown={"braking": float(prob[idx] * 0.3), "acceleration": float(prob[idx] * 0.25), "cornering": float(prob[idx] * 0.2), "fatigue": float(prob[idx] * 0.25)},
        )
    except HTTPException:
        raise
    except Exception as exc:
        raise HTTPException(status_code=500, detail=f"Scoring failed: {exc}") from exc
