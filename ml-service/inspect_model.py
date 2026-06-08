
import joblib

print("=== INSPECTING MODEL AND SCALER ===")

try:
    scaler = joblib.load("bus_eta_scaler.pkl")
    print("[OK] Scaler loaded!")
    print(f"Scaler type: {type(scaler)}")
    if hasattr(scaler, 'n_features_in_'):
        print(f"Number of features expected: {scaler.n_features_in_}")
    if hasattr(scaler, 'feature_names_in_'):
        print(f"Feature names (in order!): {list(scaler.feature_names_in_)}")
    print("Scaler attributes:")
    for attr in dir(scaler):
        if not attr.startswith('_'):
            print(f"  - {attr}")
except Exception as e:
    print(f"[ERROR] Error loading scaler: {e}")

print("\n" + "="*50)

try:
    model = joblib.load("bus_eta_model.pkl")
    print("[OK] Model loaded!")
    print(f"Model type: {type(model)}")
    if hasattr(model, 'get_params'):
        print(f"Model params: {model.get_params()}")
except Exception as e:
    print(f"[ERROR] Error loading model: {e}")
