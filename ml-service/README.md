# Bus ETA ML Service

## Setup Instructions

1. **Place your model files** in this directory (already done!):
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

4. **Visit API documentation**: http://localhost:8000/docs

## API Endpoints

- `GET /`: Health check
- `GET /health`: Detailed health status
- `POST /predict`: Predict ETA

## Input Features (Exact from trained model)
```typescript
interface ETAInput {
  DistanceFromStop: number;     // Meters to stop
  log_distance: number;         // Log of distance
  distance_over_300m: number;   // 0 or 1
  hour: number;                 // 0-23
  hour_sin?: number;            // Optional, auto-calculated
  hour_cos?: number;            // Optional, auto-calculated
  is_rush_hour: number;         // 0 or 1 (7-9 & 17-19)
  day_of_week: number;          // 0-6 (Sunday is 0)
  DirectionRef: number;         // Direction identifier
  is_weekend: number;           // 0 or 1
}
```

## Example Request
```json
{
  "DistanceFromStop": 500,
  "log_distance": 6.2,
  "distance_over_300m": 1,
  "hour": 14,
  "is_rush_hour": 0,
  "day_of_week": 2,
  "DirectionRef": 1,
  "is_weekend": 0
}
```
