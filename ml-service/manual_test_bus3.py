
import joblib
import math
import numpy as np
from datetime import datetime

# Load model and scaler
model = joblib.load("bus_eta_model.pkl")
scaler = joblib.load("bus_eta_scaler.pkl")

# Min/Max values
FEATURE_MIN_MAX = {
    "DistanceToNextStop": (0.0, 2500.0),
    "log_distance": (0.0, 8.0),
    "distance_over_300": (0.0, 1.0),
    "hour": (0.0, 23.0),
    "hour_sin": (-1.0, 1.0),
    "hour_cos": (-1.0, 1.0),
    "is_rush_hour": (0.0, 1.0),
    "day_of_week": (0.0, 6.0),
}

def _parse_day_of_week(dt):
    # Convert Python weekday (Mon=0..Sun=6) to .NET style (Sun=0..Sat=6)
    return (dt.weekday() + 1) % 7

def normalize_feature(col, val):
    min_val, max_val = FEATURE_MIN_MAX[col]
    clamped = max(min_val, min(max_val, val))
    return (clamped - min_val) / (max_val - min_val)

# Test Bus 3 exactly as in test_different_buses
print("=== Test Bus 3: distance=1000, time=2026-06-09T18:00:00 ===")
dt3 = datetime(2026, 6, 9, 18, 0, 0)
distance3 = 1000
hour3 = dt3.hour
log_dist3 = math.log(max(distance3, 1))
dist_over_300_3 = 1 if distance3 > 300 else 0
hour_sin3 = math.sin(2 * math.pi * hour3 / 24)
hour_cos3 = math.cos(2 * math.pi * hour3 / 24)
rush_hour3 = 1 if (7 <= hour3 <=9 or 17 <= hour3 <=19) else 0
dow3 = _parse_day_of_week(dt3)

print("Date time:", dt3)
print("Python weekday(dt.weekday()):", dt3.weekday())
print("day_of_week (after _parse):", dow3)

features_list3 = [
    distance3,
    log_dist3,
    dist_over_300_3,
    hour3,
    hour_sin3,
    hour_cos3,
    rush_hour3,
    dow3
]

print("\nRaw features:", features_list3)

feature_names = ["DistanceToNextStop", "log_distance", "distance_over_300", "hour", "hour_sin", "hour_cos", "is_rush_hour", "day_of_week"]
normalized3 = []
for name, val in zip(feature_names, features_list3):
    norm_val = normalize_feature(name, val)
    normalized3.append(norm_val)
    print(f"  {name:20} raw={val:.4f}, normalized={norm_val:.4f}")

print("\nNormalized features array:", np.array(normalized3))

scaled3 = scaler.transform(np.array(normalized3).reshape(1, -1))
print("\nScaled features:", scaled3)

# Manually calculate prediction using coefficients
intercept = model.intercept_
coef = model.coef_
print("\nModel intercept:", intercept)
print("Model coefficients:", coef)

manual_pred = intercept
for i in range(len(coef)):
    contribution = coef[i] * scaled3[0][i]
    manual_pred += contribution
    print(f"  Feature {i} ({feature_names[i]}): scaled={scaled3[0][i]:.4f}, coef={coef[i]:.4f}, contribution={contribution:.4f}")

print("\nManual prediction:", manual_pred, "minutes")

pred3 = model.predict(scaled3)
print("Model prediction:", pred3, "minutes")

