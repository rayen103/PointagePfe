
import joblib
import math
import numpy as np

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

def normalize_feature(col, val):
    min_val, max_val = FEATURE_MIN_MAX[col]
    clamped = max(min_val, min(max_val, val))
    return (clamped - min_val) / (max_val - min_val)

# Test Bus 1: distance 200, time 10:00
print("=== Test Bus 1: distance=200, time=10:00 ===")
distance1 = 200
hour1 = 10
log_dist1 = math.log(max(distance1, 1))
dist_over_300_1 = 1 if distance1 > 300 else 0
hour_sin1 = math.sin(2 * math.pi * hour1 / 24)
hour_cos1 = math.cos(2 * math.pi * hour1 / 24)
rush_hour1 = 1 if (7 <= hour1 <=9 or 17 <= hour1 <=19) else 0
dow1 = 2 # Tuesday (since test date is 2026-06-09, let's check what _parse_day_of_week gives us)

features_list1 = [
    distance1,
    log_dist1,
    dist_over_300_1,
    hour1,
    hour_sin1,
    hour_cos1,
    rush_hour1,
    dow1
]

print("Raw features:", features_list1)

feature_names = ["DistanceToNextStop", "log_distance", "distance_over_300", "hour", "hour_sin", "hour_cos", "is_rush_hour", "day_of_week"]
normalized1 = []
for name, val in zip(feature_names, features_list1):
    norm_val = normalize_feature(name, val)
    normalized1.append(norm_val)
    print(f"  {name:20} raw={val:.4f}, normalized={norm_val:.4f}")

print("\nNormalized features array:", np.array(normalized1))

scaled1 = scaler.transform(np.array(normalized1).reshape(1, -1))
print("Scaled features:", scaled1)

pred1 = model.predict(scaled1)
print("Prediction:", pred1, "minutes?")

print("\n=== Test Bus 2: distance=2000, time=08:00 ===")
distance2 = 2000
hour2 = 8
log_dist2 = math.log(max(distance2, 1))
dist_over_300_2 = 1 if distance2 > 300 else 0
hour_sin2 = math.sin(2 * math.pi * hour2 / 24)
hour_cos2 = math.cos(2 * math.pi * hour2 / 24)
rush_hour2 = 1 if (7 <= hour2 <=9 or 17 <= hour2 <=19) else 0
dow2 = 2

features_list2 = [
    distance2,
    log_dist2,
    dist_over_300_2,
    hour2,
    hour_sin2,
    hour_cos2,
    rush_hour2,
    dow2
]

print("Raw features:", features_list2)

normalized2 = []
for name, val in zip(feature_names, features_list2):
    norm_val = normalize_feature(name, val)
    normalized2.append(norm_val)
    print(f"  {name:20} raw={val:.4f}, normalized={norm_val:.4f}")

print("\nNormalized features array:", np.array(normalized2))

scaled2 = scaler.transform(np.array(normalized2).reshape(1, -1))
print("Scaled features:", scaled2)

pred2 = model.predict(scaled2)
print("Prediction:", pred2, "minutes?")

