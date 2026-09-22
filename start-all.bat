@echo off
echo ========================================
echo Starting PFE Project - All Services
echo ========================================

echo.
echo [1/3] Starting Backend...
start "Backend Service" cmd /k "cd /d %~dp0backend && dotnet run --project src/CollectManagement.ms/CollectManagement.WebAPI/CollectManagement.WebAPI.csproj"

echo.
echo [2/3] Starting Frontend...
start "Frontend Service" cmd /k "cd /d %~dp0frontend && npm start"

echo.
echo [3/3] Starting ETA Prediction ML Service...
if exist "%~dp0ml-service" (
    start "ETA Prediction Service" cmd /k "cd /d %~dp0ml-service && python -m uvicorn main:app --host 0.0.0.0 --port 8000 --reload"
) else (
    echo Warning: ml-service directory not found!
)

echo.
echo ========================================
echo All services are starting!
echo - Backend: http://localhost:6064
echo - Frontend: http://localhost:4200
echo - ETA Prediction: http://localhost:8000
echo ========================================
pause
