# Bus ETA ML Service

## Setup Instructions

1. **Place your model files** in this directory:
   - `bus_eta_model.pkl`
   - `bus_eta_scaler.pkl`

2. **Install dependencies**:
   ```bash
   pip install -r requirements.txt
   ```

3. **Run the service**:
   ```bash
   python main.py
   # Or with uvicorn:
   uvicorn main:app --host 0.0.0.0 --port 8000 --reload
   ```

4. **Update `main.py`** to match your actual model's input features! Look for the `ETAInput` class and the feature array creation.

5. **Visit API documentation**: http://localhost:8000/docs

## API Endpoints

- `GET /`: Health check
- `GET /health`: Detailed health status
- `POST /predict`: Predict ETA

## Example Request
```json
{
  "distance": 5.2,
  "hour": 14,
  "day_of_week": 2,
  "is_weekend": 0,
  "weather_condition": 0.5,
  "traffic_level": 0.3
}
```
