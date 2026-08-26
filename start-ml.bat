@echo off
echo ========================================
echo Starting ETA Prediction ML Service
echo ========================================
echo.

if not exist "ml-service" (
    echo ERROR: ml-service directory not found!
    echo Please make sure the ML service folder exists!
    echo.
    pause
    exit /b 1
)

cd /d "%~dp0ml-service"
echo Starting FastAPI on http://localhost:8000
echo.
python -m uvicorn main:app --host 0.0.0.0 --port 8000 --reload

pause
