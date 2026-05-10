@echo off
setlocal enabledelayedexpansion

set ROOT_DIR=%~dp0

echo Starting all ML services + backend + frontend...
echo Use Ctrl+C in each window to stop services.

call :start_ml eta_prediction 8001
call :start_ml anomaly_detection 8002
call :start_ml demand_forecasting 8003
call :start_ml passenger_counting_cv 8004
call :start_ml driver_behavior_scoring 8005
call :start_ml predictive_maintenance 8006
call :start_ml rl_dispatcher 8007
call :start_ml traffic_stgcn 8008

start "Backend" cmd /k "cd /d ""%ROOT_DIR%backend"" && dotnet restore && dotnet run --project src/CollectManagement.ms/CollectManagement.WebAPI/CollectManagement.WebAPI.csproj"
start "Frontend" cmd /k "cd /d ""%ROOT_DIR%frontend"" && npm install && npm start"

goto :eof

:start_ml
set SERVICE_DIR=%1
set SERVICE_PORT=%2
start "ML %SERVICE_DIR%" cmd /k "cd /d ""%ROOT_DIR%ml-services\%SERVICE_DIR%"" && python -m pip install -r requirements.txt && python -m uvicorn api:app --host 0.0.0.0 --port %SERVICE_PORT%"
goto :eof
