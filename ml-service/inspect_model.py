import joblib
import numpy as np

# CHANGED: reference schema expected from your real database input.
EXPECTED_DB_INPUT = [
    "Latitude",
    "Longitude",
    "CodeCircuit",
    "ModelBus",
    "Capacite",
    "CurrentOccupancy",
    "LastPositionAt",
]

# CHANGED: expected engineered order for retrained model.
ENGINEERED_FEATURE_ORDER = [
    "DistanceFromStop",
    "log_distance",
    "distance_over_300m",
    "hour",
    "hour_sin",
    "hour_cos",
    "is_rush_hour",
    "day_of_week",
    "occupancy_ratio",
    "route_encoded",
    "model_encoded",
]

print("=== INSPECTING MODEL AND SCALER ===")
print("\nExpected raw DB input fields:")
for field in EXPECTED_DB_INPUT:
    print(f"  - {field}")

print("\nExpected engineered features (order for retraining):")
for idx, feature in enumerate(ENGINEERED_FEATURE_ORDER, start=1):
    print(f"  {idx:02d}. {feature}")

scaler_feature_names = None

try:
    scaler = joblib.load("bus_eta_scaler.pkl")
    print("\n[OK] Scaler loaded!")
    print(f"Scaler type: {type(scaler)}")
    if hasattr(scaler, "n_features_in_"):
        print(f"Number of features expected: {scaler.n_features_in_}")
    if hasattr(scaler, "feature_names_in_"):
        scaler_feature_names = list(scaler.feature_names_in_)
        print(f"Scaler feature names: {scaler_feature_names}")
except Exception as e:
    print(f"\n[ERROR] Error loading scaler: {e}")

print("\n" + "=" * 60)

try:
    model = joblib.load("bus_eta_model.pkl")
    print("\n[OK] Model loaded!")
    print(f"Model type: {type(model)}")

    feature_names = scaler_feature_names or ENGINEERED_FEATURE_ORDER

    # CHANGED: display feature importance after retraining when available.
    if hasattr(model, "feature_importances_"):
        importances = np.asarray(model.feature_importances_, dtype=float)
        names = feature_names[: len(importances)]
        ranked = sorted(zip(names, importances), key=lambda x: x[1], reverse=True)
        print("\nFeature importances:")
        for name, score in ranked:
            print(f"  - {name}: {score:.6f}")
    elif hasattr(model, "coef_"):
        coef = np.asarray(model.coef_)
        if coef.ndim > 1:
            coef = coef[0]
        coef = np.abs(coef)
        names = feature_names[: len(coef)]
        ranked = sorted(zip(names, coef), key=lambda x: x[1], reverse=True)
        print("\nAbsolute coefficient importances:")
        for name, score in ranked:
            print(f"  - {name}: {score:.6f}")
    else:
        print("\nFeature importance not available for this model type.")

except Exception as e:
    print(f"\n[ERROR] Error loading model: {e}")
