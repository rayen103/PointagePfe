from fastapi import FastAPI, HTTPException
from pydantic import BaseModel
import joblib
import numpy as np
import os
from typing import List, Optional

app = FastAPI(title="Bus ETA ML Service", version="1.0.0")

# Global variables to hold model and scaler
model = None
scaler = None

# Expected input features (you should adjust these to match your actual model!)
# Replace with your actual feature names
class ETAInput(BaseModel):
    # Example features (customize these based on your model!)
    distance: float = 0.0
    hour: int = 0
    day_of_week: int = 0
    is_weekend: int = 0
    weather_condition: float = 0.0
    traffic_level: float = 0.0


class ETAOutput(BaseModel):
    eta_minutes: float
    confidence: Optional[float] = None


# Load model and scaler on startup
@app.on_event("startup")
async def load_model():
    global model, scaler
    try:
        # Load the model and scaler from pickle files
        model_path = "bus_eta_model.pkl"
        scaler_path = "bus_eta_scaler.pkl"
        
        if os.path.exists(model_path):
            model = joblib.load(model_path)
            print("✅ Model loaded successfully!")
        else:
            print(f"⚠️ Warning: Model file {model_path} not found!")
        
        if os.path.exists(scaler_path):
            scaler = joblib.load(scaler_path)
            print("✅ Scaler loaded successfully!")
        else:
            print(f"⚠️ Warning: Scaler file {scaler_path} not found!")
            
    except Exception as e:
        print(f"❌ Error loading model/scaler: {str(e)}")


@app.get("/")
async def root():
    return {"message": "Bus ETA ML Service is running!"}


@app.get("/health")
async def health():
    return {"status": "healthy", "model_loaded": model is not None, "scaler_loaded": scaler is not None}


@app.post("/predict", response_model=ETAOutput)
async def predict_eta(input_data: ETAInput):
    if model is None or scaler is None:
        raise HTTPException(status_code=500, detail="Model or scaler not loaded!")
    
    try:
        # Convert input data to the right format for your model
        # Adjust the order of features to match your model's expected input!
        features = np.array([
            input_data.distance,
            input_data.hour,
            input_data.day_of_week,
            input_data.is_weekend,
            input_data.weather_condition,
            input_data.traffic_level
        ]).reshape(1, -1)
        
        # Scale the features
        scaled_features = scaler.transform(features)
        
        # Predict
        prediction = model.predict(scaled_features)
        
        eta = float(prediction[0])
        
        return ETAOutput(
            eta_minutes=eta,
            confidence=0.95
        )
        
    except Exception as e:
        raise HTTPException(status_code=500, detail=f"Prediction error: {str(e)}")


if __name__ == "__main__":
    import uvicorn
    uvicorn.run(app, host="0.0.0.0", port=8000)
