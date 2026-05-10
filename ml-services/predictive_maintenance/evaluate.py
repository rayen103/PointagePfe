from __future__ import annotations

import argparse
import json
from pathlib import Path

import joblib
import numpy as np
from sklearn.metrics import (
    f1_score,
    mean_absolute_error,
    mean_squared_error,
    precision_score,
    r2_score,
    recall_score,
    roc_auc_score,
)

from data_loader import load_dataframe


def evaluate(dataset: str, model_dir: str):
    bundle = joblib.load(Path(model_dir) / "model.joblib")
    df = load_dataframe(dataset)

    y_cls = df["needs_maintenance_7d"].astype(int).values
    y_reg = df["days_to_failure"].astype(float).values
    X = df.drop(columns=["needs_maintenance_7d", "days_to_failure"]).select_dtypes(include=["number"]).values
    X = bundle["imputer"].transform(X)

    prob = bundle["clf"].predict_proba(X)[:, 1]
    pred_cls = (prob >= bundle["threshold"]).astype(int)
    pred_reg = bundle["reg"].predict(X)

    print(
        json.dumps(
            {
                "precision": float(precision_score(y_cls, pred_cls, zero_division=0)),
                "recall": float(recall_score(y_cls, pred_cls, zero_division=0)),
                "f1": float(f1_score(y_cls, pred_cls, zero_division=0)),
                "roc_auc": float(roc_auc_score(y_cls, prob)),
                "mae": float(mean_absolute_error(y_reg, pred_reg)),
                "rmse": float(np.sqrt(mean_squared_error(y_reg, pred_reg))),
                "r2": float(r2_score(y_reg, pred_reg)),
            },
            indent=2,
        )
    )


if __name__ == "__main__":
    p = argparse.ArgumentParser()
    p.add_argument("--dataset", required=True)
    p.add_argument("--model-dir", default="artifacts")
    a = p.parse_args()
    evaluate(a.dataset, a.model_dir)
