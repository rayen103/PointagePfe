from __future__ import annotations

import argparse
import json
from pathlib import Path

import joblib
import matplotlib.pyplot as plt
import mlflow
import mlflow.sklearn
import numpy as np
import shap
from sklearn.metrics import accuracy_score, classification_report, confusion_matrix, f1_score
from sklearn.preprocessing import LabelEncoder

from data_loader import FEATURES, TARGET, load_trip_dataframe, split_by_driver
from model import make_model


def train(args):
    out = Path(args.model_dir)
    out.mkdir(parents=True, exist_ok=True)

    df = load_trip_dataframe(args.dataset)
    train_df, test_df = split_by_driver(df)

    le = LabelEncoder()
    y_train = le.fit_transform(train_df[TARGET])
    y_test = le.transform(test_df[TARGET])
    X_train = train_df[FEATURES].astype(float).values
    X_test = test_df[FEATURES].astype(float).values

    model = make_model(num_classes=len(le.classes_))
    model.fit(X_train, y_train)

    pred = model.predict(X_test)
    proba = model.predict_proba(X_test)

    metrics = {
        "accuracy": float(accuracy_score(y_test, pred)),
        "macro_f1": float(f1_score(y_test, pred, average="macro")),
    }

    report = classification_report(y_test, pred, target_names=le.classes_, output_dict=True, zero_division=0)
    cm = confusion_matrix(y_test, pred)

    explainer = shap.Explainer(model.predict_proba, X_train[: min(len(X_train), 500)])
    shap_values = explainer(X_test[: min(len(X_test), 100)])

    plt.figure()
    shap.summary_plot(shap_values, X_test[: min(len(X_test), 100)], feature_names=FEATURES, show=False)
    plt.tight_layout()
    plt.savefig(out / "shap_summary.png")
    plt.close()

    mlflow.set_experiment("driver_behavior_scoring")
    with mlflow.start_run():
        mlflow.log_params({"model": "XGBoostClassifier", "calibration": "sigmoid", "split": "stratified_by_driver"})
        mlflow.log_metrics(metrics)
        mlflow.sklearn.log_model(model, "model")
        mlflow.log_dict(report, "classification_report.json")
        mlflow.log_artifact(str(out / "shap_summary.png"))

    joblib.dump({"model": model, "label_encoder": le, "feature_names": FEATURES}, out / "model.joblib")
    (out / "confusion_matrix.json").write_text(json.dumps(cm.tolist()), encoding="utf-8")
    (out / "metadata.json").write_text(
        json.dumps({"model_version": "1.0.0", "last_trained": now(), "metrics": metrics}, indent=2), encoding="utf-8"
    )


def now() -> str:
    import pandas as pd

    return pd.Timestamp.utcnow().isoformat()


if __name__ == "__main__":
    p = argparse.ArgumentParser()
    p.add_argument("--dataset", required=True)
    p.add_argument("--model-dir", default="artifacts")
    train(p.parse_args())
