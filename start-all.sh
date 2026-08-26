#!/bin/bash

echo "========================================"
echo "Starting PFE Project - All Services"
echo "========================================"

# Function to kill background processes on exit
cleanup() {
    echo ""
    echo "Stopping all services..."
    kill $BACKEND_PID 2>/dev/null
    kill $FRONTEND_PID 2>/dev/null
    kill $ML_PID 2>/dev/null
    wait 2>/dev/null
    echo "All services stopped!"
    exit 0
}

# Trap SIGINT (Ctrl+C) to trigger cleanup
trap cleanup SIGINT

echo ""
echo "[1/3] Starting Backend..."
cd backend
dotnet run --project src/CollectManagement.ms/CollectManagement.WebAPI/CollectManagement.WebAPI.csproj &
BACKEND_PID=$!
cd ..
echo "Backend started with PID: $BACKEND_PID"

echo ""
echo "[2/3] Starting Frontend..."
cd frontend
npm start &
FRONTEND_PID=$!
cd ..
echo "Frontend started with PID: $FRONTEND_PID"

echo ""
echo "[3/3] Starting ETA Prediction ML Service..."
if [ -d "ml-service" ]; then
    cd ml-service
    python3 -m uvicorn main:app --host 0.0.0.0 --port 8000 --reload &
    ML_PID=$!
    cd ..
    echo "ETA Prediction Service started with PID: $ML_PID"
else
    echo "Warning: ml-service directory not found!"
fi

echo ""
echo "========================================"
echo "All services are starting!"
echo "- Backend: http://localhost:6064"
echo "- Frontend: http://localhost:4200"
echo "- ETA Prediction: http://localhost:8000"
echo ""
echo "Press Ctrl+C to stop all services"
echo "========================================"

# Wait for all background processes
wait
