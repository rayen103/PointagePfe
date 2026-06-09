
import joblib
import numpy as np

# Load model and scaler
model = joblib.load('bus_eta_model.pkl')
scaler = joblib.load('bus_eta_scaler.pkl')

print("Model:", model)
print("Scaler:", scaler)
print("\nScaler attributes:")
print("  n_features_in_:", scaler.n_features_in_)
if hasattr(scaler, 'feature_names_in_'):
    print("  feature_names_in_:", scaler.feature_names_in_)
print("  mean_:", scaler.mean_)
print("  var_:", scaler.var_)
print("  scale_:", scaler.scale_)

# Let's try some test inputs!
# Test 1: small distance, midday, weekday
# All features in ~0-1 range
test_features_1 = np.array([
    0.2,   # DistanceToNextStop
    0.3,   # log_distance
    0.0,   # distance_over_300
    0.5,   # hour
    0.5,   # hour_sin
    0.5,   # hour_cos
    0.0,   # is_rush_hour
    0.3    # day_of_week
]).reshape(1, -1)

print("\nTest 1 features (0-1 range):", test_features_1)
scaled_1 = scaler.transform(test_features_1)
print("Test 1 scaled:", scaled_1)
pred_1 = model.predict(scaled_1)
print("Test 1 prediction:", pred_1, "seconds? minutes?")

# Test 2: longer distance, rush hour
test_features_2 = np.array([
    0.8,   # DistanceToNextStop
    0.7,   # log_distance
    1.0,   # distance_over_300
    0.3,   # hour
    0.8,   # hour_sin
    0.2,   # hour_cos
    1.0,   # is_rush_hour
    0.2    # day_of_week
]).reshape(1, -1)

print("\nTest 2 features (0-1 range):", test_features_2)
scaled_2 = scaler.transform(test_features_2)
print("Test 2 scaled:", scaled_2)
pred_2 = model.predict(scaled_2)
print("Test 2 prediction:", pred_2, "seconds? minutes?")
