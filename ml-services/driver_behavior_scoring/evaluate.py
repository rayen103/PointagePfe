from __future__ import annotations

import argparse
import json
from pathlib import Path

import joblib
import numpy as np
from sklearn.metrics import accuracy_score, classification_report, confusion_matrix, f1_score

from data_loader import FEATURES, TARGET, load_trip_dataframe


def evaluate(dataset: str, model_dir: str):
    bundle = joblib.load(Path(model_dir) / "model.joblib")
    model, le = bundle["model"], bundle["label_encoder"]

    df = load_trip_dataframe(dataset)
    y = le.transform(df[TARGET])
    X = df[FEATURES].astype(float).values
    pred = model.predict(X)

    res = {
        "accuracy": float(accuracy_score(y, pred)),
        "macro_f1": float(f1_score(y, pred, average="macro")),
        "confusion_matrix": confusion_matrix(y, pred).tolist(),
        "report": classification_report(y, pred, target_names=le.classes_, output_dict=True, zero_division=0),
    }
    print(json.dumps(res, indent=2))


if __name__ == "__main__":
    p = argparse.ArgumentParser()
    p.add_argument("--dataset", required=True)
    p.add_argument("--model-dir", default="artifacts")
    a = p.parse_args()
    evaluate(a.dataset, a.model_dir)
