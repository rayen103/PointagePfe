
import joblib

print("=== feature_info.pkl ===")
feature_info = joblib.load("feature_info.pkl")
print(feature_info)

print("\n=== route_encoder.pkl ===")
route_encoder = joblib.load("route_encoder.pkl")
print(route_encoder)
print("Classes:", route_encoder.classes_)

print("\n=== model_encoder.pkl ===")
model_encoder = joblib.load("model_encoder.pkl")
print(model_encoder)
print("Classes:", model_encoder.classes_)
