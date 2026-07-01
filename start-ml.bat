@echo off
echo ========================================
echo Starting ETA Prediction ML Service
echo ========================================
echo.

if not exist "ml-services\eta_prediction" (
    echo ERROR: ml-services\eta_prediction directory not found!
    echo Please make sure you have the ML service folder exists!
    echo.
    pause
    exit /b 1
)

cd /d "%~dp0ml-services\eta_prediction
echo Starting FastAPI on http://localhost:8001
echo.
python -m uvicorn main:app --host 0.0.0.0 --port 8001 --reload

pause
