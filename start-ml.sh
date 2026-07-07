#!/bin/bash

echo "========================================"
echo "Starting ETA Prediction ML Service"
echo "========================================"
echo ""

if [ ! -d "ml-services/eta_prediction" ]; then
    echo "ERROR: ml-services/eta_prediction directory not found!"
    echo "Please make sure you have the ML service folder exists!"
    exit 1
fi

cd ml-services/eta_prediction
echo "Starting FastAPI on http://localhost:8001"
python3 -m uvicorn main:app --host 0.0.0.0 --port 8001 --reload
