from __future__ import annotations

import json
from pathlib import Path

import joblib
import numpy as np
from fastapi import FastAPI, HTTPException, Query
from pydantic import BaseModel

MODEL_DIR = Path("artifacts")
app = FastAPI(title="Predictive Maintenance Service")
bundle = None
meta = {"model_version": "untrained", "last_trained": "never"}


class MaintenanceResponse(BaseModel):
    needs_maintenance: bool
    probability: float
    days_to_failure: float
    top_risk_factors: list[str]


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


@app.get("/maintenance/predict", response_model=MaintenanceResponse)
def predict(bus_id: str = Query(...)):
    try:
        if bundle is None:
            raise HTTPException(status_code=500, detail="Model not loaded")

        sample = np.array([[85, 32, 12.4, 12000, 0.62, -0.08, 2600, 4] + [0] * 16], dtype=float)
        sample = bundle["imputer"].transform(sample)
        prob = float(bundle["clf"].predict_proba(sample)[0, 1])
        days = float(bundle["reg"].predict(sample)[0])
        return MaintenanceResponse(
            needs_maintenance=bool(prob >= bundle["threshold"]),
            probability=prob,
            days_to_failure=days,
            top_risk_factors=bundle["top_risk"],
        )
    except HTTPException:
        raise
    except Exception as exc:
        raise HTTPException(status_code=500, detail=f"Prediction failed: {exc}") from exc
