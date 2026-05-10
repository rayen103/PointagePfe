from __future__ import annotations

import json
from pathlib import Path

import numpy as np
from fastapi import FastAPI, HTTPException
from pydantic import BaseModel
from stable_baselines3 import PPO

MODEL_DIR = Path("artifacts")
app = FastAPI(title="RL Bus Dispatcher Service")
model = None
meta = {"model_version": "untrained", "last_trained": "never"}


class DispatchState(BaseModel):
    queue_sizes_per_stop: list[float]
    bus_positions: list[float]
    bus_occupancies: list[float]
    time_of_day: float
    traffic_level: float


class DispatchResponse(BaseModel):
    action: int
    decision: str


class HealthResponse(BaseModel):
    status: str
    model_version: str
    last_trained: str


@app.on_event("startup")
def startup():
    global model, meta
    model_zip = MODEL_DIR / "ppo_dispatcher.zip"
    if model_zip.exists():
        model = PPO.load(str(model_zip))
    if (MODEL_DIR / "metadata.json").exists():
        meta = json.loads((MODEL_DIR / "metadata.json").read_text(encoding="utf-8"))


@app.get("/health", response_model=HealthResponse)
def health():
    return HealthResponse(status="ok", model_version=meta.get("model_version", "untrained"), last_trained=meta.get("last_trained", "never"))


@app.post("/dispatch/recommend", response_model=DispatchResponse)
def recommend(state: DispatchState):
    try:
        if model is None:
            raise HTTPException(status_code=500, detail="Model not loaded")
        obs = np.array(
            state.queue_sizes_per_stop + state.bus_positions + state.bus_occupancies + [state.time_of_day, state.traffic_level],
            dtype=np.float32,
        )
        action, _ = model.predict(obs, deterministic=True)
        action = int(action)
        decision = "dispatch_bus" if action < len(state.bus_positions) else "hold"
        return DispatchResponse(action=action, decision=decision)
    except HTTPException:
        raise
    except Exception as exc:
        raise HTTPException(status_code=500, detail=f"Dispatch recommendation failed: {exc}") from exc
