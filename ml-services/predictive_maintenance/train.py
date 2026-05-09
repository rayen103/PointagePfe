from __future__ import annotations

import argparse
import json
from pathlib import Path

import joblib
import mlflow
import mlflow.sklearn
import numpy as np
import shap
from sklearn.impute import KNNImputer
from sklearn.metrics import (
    f1_score,
    mean_absolute_error,
    mean_squared_error,
    precision_score,
    r2_score,
    recall_score,
    roc_auc_score,
)
from sklearn.model_selection import train_test_split

from data_loader import FEATURES, load_dataframe
from model import make_models


def train(args):
    out = Path(args.model_dir)
    out.mkdir(parents=True, exist_ok=True)

    df = load_dataframe(args.dataset)
    y_cls = df["needs_maintenance_7d"].astype(int).values
    y_reg = df["days_to_failure"].astype(float).values
    X = df.drop(columns=["needs_maintenance_7d", "days_to_failure"]).select_dtypes(include=["number"]).values

    X_train, X_test, y_train_c, y_test_c, y_train_r, y_test_r = train_test_split(
        X, y_cls, y_reg, test_size=0.2, random_state=42, stratify=y_cls
    )

    imputer = KNNImputer(n_neighbors=5)
    X_train = imputer.fit_transform(X_train)
    X_test = imputer.transform(X_test)

    clf, reg = make_models()
    clf.fit(X_train, y_train_c)
    reg.fit(X_train, y_train_r)

    prob = clf.predict_proba(X_test)[:, 1]
    threshold = 0.35
    pred_c = (prob >= threshold).astype(int)
    pred_r = reg.predict(X_test)

    cls_metrics = {
        "precision": float(precision_score(y_test_c, pred_c, zero_division=0)),
        "recall": float(recall_score(y_test_c, pred_c, zero_division=0)),
        "f1": float(f1_score(y_test_c, pred_c, zero_division=0)),
        "roc_auc": float(roc_auc_score(y_test_c, prob)),
    }
    reg_metrics = {
        "mae": float(mean_absolute_error(y_test_r, pred_r)),
        "rmse": float(np.sqrt(mean_squared_error(y_test_r, pred_r))),
        "r2": float(r2_score(y_test_r, pred_r)),
    }

    explainer = shap.Explainer(clf)
    shap_vals = explainer(X_test[:200])
    top = np.argsort(np.abs(shap_vals.values).mean(axis=0))[-3:]
    top_risk = [str(i) for i in top.tolist()]

    mlflow.set_experiment("predictive_maintenance")
    with mlflow.start_run():
        mlflow.log_params({"classifier": "XGBoost", "regressor": "GradientBoosting", "imputer": "KNN", "threshold": threshold})
        mlflow.log_metrics({**cls_metrics, **reg_metrics})
        mlflow.sklearn.log_model(clf, "classifier")
        mlflow.sklearn.log_model(reg, "regressor")

    joblib.dump({"clf": clf, "reg": reg, "imputer": imputer, "top_risk": top_risk, "threshold": threshold}, out / "model.joblib")
    (out / "metadata.json").write_text(
        json.dumps({"model_version": "1.0.0", "last_trained": now(), "cls_metrics": cls_metrics, "reg_metrics": reg_metrics}, indent=2),
        encoding="utf-8",
    )


def now() -> str:
    import pandas as pd

    return pd.Timestamp.utcnow().isoformat()


if __name__ == "__main__":
    p = argparse.ArgumentParser()
    p.add_argument("--dataset", required=True)
    p.add_argument("--model-dir", default="artifacts")
    train(p.parse_args())
