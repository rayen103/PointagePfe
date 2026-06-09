
import joblib
import numpy as np
import os
from sklearn.linear_model import LinearRegression
from sklearn.preprocessing import StandardScaler

# Get current script directory
script_dir = os.path.dirname(os.path.abspath(__file__))

# Create dummy training data
np.random.seed(42)
X = np.random.rand(100, 8)  # 8 base features
y = np.random.rand(100) * 10  # ETA in minutes

# Train a dummy model
model = LinearRegression()
model.fit(X, y)

# Create a dummy scaler
scaler = StandardScaler()
scaler.fit(X)

# Save them to the script directory
model_path = os.path.join(script_dir, "bus_eta_model.pkl")
scaler_path = os.path.join(script_dir, "bus_eta_scaler.pkl")

joblib.dump(model, model_path)
joblib.dump(scaler, scaler_path)

print(f"Dummy model and scaler created successfully!")
print(f"Model: {model_path}")
print(f"Scaler: {scaler_path}")
