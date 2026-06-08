
from fastapi import FastAPI, HTTPException
from pydantic import BaseModel
import joblib
import numpy as np
import os
import math
from typing import Optional

app = FastAPI(title="Bus ETA ML Service", version="1.0.0")

model = None
scaler = None


# EXACT features your model expects!
class ETAInput(BaseModel):
    DistanceFromStop: float
    log_distance: float
    distance_over_300m: int
    hour: int
    hour_sin: Optional[float] = None
    hour_cos: Optional[float] = None
    is_rush_hour: int
    day_of_week: int
    DirectionRef: float
    is_weekend: int


class ETAOutput(BaseModel):
    eta_minutes: float
    confidence: float


# Derived features helper (auto-calculate hour_sin/hour_cos)
def calculate_derived_features(input_data: ETAInput):
    if input_data.hour_sin is None:
        input_data.hour_sin = math.sin(2 * math.pi * input_data.hour / 24)
    if input_data.hour_cos is None:
        input_data.hour_cos = math.cos(2 * math.pi * input_data.hour / 24)
    return input_data


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
        # Auto-calculate derived features
        data = calculate_derived_features(input_data)
        
        # FEATURE ORDER MUST MATCH!
        features = np.array([
            data.DistanceFromStop,
            data.log_distance,
            data.distance_over_300m,
            data.hour,
            data.hour_sin,
            data.hour_cos,
            data.is_rush_hour,
            data.day_of_week,
            data.DirectionRef,
            data.is_weekend
        ]).reshape(1, -1)
        
        scaled = scaler.transform(features)
        prediction = model.predict(scaled)
        
        eta = float(prediction[0])
        confidence = 0.9  # Adjust based on your model's confidence
        
        return ETAOutput(eta_minutes=eta, confidence=confidence)
        
    except Exception as e:
        raise HTTPException(status_code=500, detail=f"Prediction failed: {str(e)}")


if __name__ == "__main__":
    import uvicorn
    uvicorn.run(app, host="0.0.0.0", port=8000)
