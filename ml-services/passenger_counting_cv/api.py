from __future__ import annotations

import base64
import json
from pathlib import Path
from typing import List

import cv2
import numpy as np
from fastapi import FastAPI, HTTPException
from pydantic import BaseModel

from model import DoorCounter

MODEL_DIR = Path("artifacts")
app = FastAPI(title="Passenger Counting CV Service")
counter = None
meta = {"model_version": "untrained", "last_trained": "never"}
occupancy = {}


class CountRequest(BaseModel):
    bus_id: str
    frame_batch: List[str]


class CountResponse(BaseModel):
    boarded: int
    alighted: int
    occupancy: int
    frame_annotated: str


class HealthResponse(BaseModel):
    status: str
    model_version: str
    last_trained: str


@app.on_event("startup")
def startup():
    global counter, meta
    weights = MODEL_DIR / "train" / "weights" / "best.pt"
    if weights.exists():
        counter = DoorCounter(str(weights))
    if (MODEL_DIR / "metadata.json").exists():
        meta = json.loads((MODEL_DIR / "metadata.json").read_text(encoding="utf-8"))


@app.get("/health", response_model=HealthResponse)
def health():
    return HealthResponse(status="ok", model_version=meta.get("model_version", "untrained"), last_trained=meta.get("last_trained", "never"))


@app.post("/cv/count", response_model=CountResponse)
def count(req: CountRequest):
    try:
        if counter is None:
            raise HTTPException(status_code=500, detail="Model not loaded")
        annotated = None
        in_c, out_c = 0, 0
        for b64_frame in req.frame_batch:
            raw = base64.b64decode(b64_frame)
            frame = cv2.imdecode(np.frombuffer(raw, dtype=np.uint8), cv2.IMREAD_COLOR)
            if frame is None:
                raise HTTPException(status_code=422, detail="Invalid frame encoding")
            annotated, in_c, out_c = counter.process_frame(frame)

        current = occupancy.get(req.bus_id, 0)
        current = max(0, current + in_c - out_c)
        occupancy[req.bus_id] = current

        _, buf = cv2.imencode(".jpg", annotated)
        return CountResponse(
            boarded=int(in_c),
            alighted=int(out_c),
            occupancy=int(current),
            frame_annotated=base64.b64encode(buf.tobytes()).decode("utf-8"),
        )
    except HTTPException:
        raise
    except Exception as exc:
        raise HTTPException(status_code=500, detail=f"Counting failed: {exc}") from exc
