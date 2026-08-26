#!/bin/bash

echo "========================================"
echo "Starting ETA Prediction ML Service"
echo "========================================"
echo ""

if [ ! -d "ml-service" ]; then
    echo "ERROR: ml-service directory not found!"
    echo "Please make sure the ML service folder exists!"
    exit 1
fi

cd ml-service
echo "Starting FastAPI on http://localhost:8000"
python3 -m uvicorn main:app --host 0.0.0.0 --port 8000 --reload
