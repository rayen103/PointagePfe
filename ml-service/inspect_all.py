
import joblib
import os

files_to_inspect = [
    "bus_eta_model.pkl",
    "bus_eta_scaler.pkl",
    "feature_info.pkl",
    "route_encoder.pkl",
    "model_encoder.pkl"
]

for filename in files_to_inspect:
    print(f"\n{'='*60}")
    print(f"Inspecting {filename}")
    print(f"{'='*60}")
    if os.path.exists(filename):
        try:
            obj = joblib.load(filename)
            print(f"Type: {type(obj)}")
            print(f"Contents: {obj}")
            
            if hasattr(obj, "__dict__"):
                print("\nAttributes:")
                for k, v in obj.__dict__.items():
                    if not k.startswith('_'):
                        print(f"  {k}: {v}")
        except Exception as e:
            print(f"Error loading: {e}")
    else:
        print("File not found!")

