
import joblib
import numpy as np

model = joblib.load("bus_eta_model.pkl")
scaler = joblib.load("bus_eta_scaler.pkl")
feature_info = joblib.load("feature_info.pkl")

print("Model type:", type(model))
print()
print("Model coefficients:", model.coef_)
print("Model intercept:", model.intercept_)
print()
print("Feature info:", feature_info)
print()
print("Scaler attributes:")
print("  n_features_in_:", scaler.n_features_in_)
if hasattr(scaler, "feature_names_in_"):
    print("  feature_names_in_:", scaler.feature_names_in_)
print("  mean_:", scaler.mean_)
print("  var_:", scaler.var_)
print("  scale_:", scaler.scale_)

feature_names = feature_info["original_features"]
print()
print("Feature names and coefficients:")
for name, coef in zip(feature_names, model.coef_):
    print(f"  {name:20}: {coef:10.4f} {'(positive: increases ETA)' if coef > 0 else '(negative: decreases ETA)'}")

