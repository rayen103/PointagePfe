#!/usr/bin/env bash
set -euo pipefail

ROOT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

echo "Starting all ML services + backend + frontend..."
echo "Press Ctrl+C to stop everything."

cleanup() {
  echo
  echo "Stopping all services..."
  kill 0
}
trap cleanup INT TERM EXIT

start_service() {
  local dir="$1"
  local port="$2"
  echo "[$dir] installing requirements..."
  (cd "$ROOT_DIR/ml-services/$dir" && python -m pip install -r requirements.txt >/dev/null)
  echo "[$dir] starting on http://localhost:$port ..."
  (cd "$ROOT_DIR/ml-services/$dir" && python -m uvicorn api:app --host 0.0.0.0 --port "$port") &
}

start_service "eta_prediction" 8001
start_service "anomaly_detection" 8002
start_service "demand_forecasting" 8003
start_service "passenger_counting_cv" 8004
start_service "driver_behavior_scoring" 8005
start_service "predictive_maintenance" 8006
start_service "rl_dispatcher" 8007
start_service "traffic_stgcn" 8008

echo "[backend] restoring and starting on http://localhost:6064 ..."
(cd "$ROOT_DIR/backend" && dotnet restore && dotnet run --project src/CollectManagement.ms/CollectManagement.WebAPI/CollectManagement.WebAPI.csproj) &

echo "[frontend] installing dependencies and starting on http://localhost:4200 ..."
(cd "$ROOT_DIR/frontend" && npm install && npm start) &

wait
