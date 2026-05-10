from __future__ import annotations

import argparse
import json
from pathlib import Path

import joblib
import matplotlib.pyplot as plt
import mlflow
import mlflow.sklearn
import numpy as np
from sklearn.metrics import mean_absolute_error, mean_squared_error, r2_score
from statsmodels.tsa.statespace.sarimax import SARIMAX

from data_loader import load_dataframe, preprocess, rolling_origin_splits
from model import DemandEnsemble


def mape(y_true, y_pred):
    y_true = np.asarray(y_true)
    y_pred = np.asarray(y_pred)
    return float(np.mean(np.abs((y_true - y_pred) / np.clip(np.abs(y_true), 1e-6, None))) * 100)


def train(args):
    out = Path(args.model_dir)
    out.mkdir(parents=True, exist_ok=True)
    df = load_dataframe(args.dataset)
    splits = rolling_origin_splits(df, n_splits=5)

    fold_metrics = []
    for train_idx, test_idx in splits:
        tr_df, te_df = df.iloc[train_idx], df.iloc[test_idx]
        Xtr, ytr, enc, scl = preprocess(tr_df, fit=True)
        Xte, yte, _, _ = preprocess(te_df, fit=False, encoder=enc, scaler=scl)

        model = DemandEnsemble()
        model.fit(Xtr, ytr, tr_df)
        pred = model.predict(Xte, te_df)

        fold_metrics.append(
            {
                "rmse": float(np.sqrt(mean_squared_error(yte, pred))),
                "mae": float(mean_absolute_error(yte, pred)),
                "mape": mape(yte, pred),
                "r2": float(r2_score(yte, pred)),
            }
        )

    split_idx = int(len(df) * 0.85)
    train_df, test_df = df.iloc[:split_idx], df.iloc[split_idx:]
    Xtr, ytr, enc, scl = preprocess(train_df, fit=True)
    Xte, yte, _, _ = preprocess(test_df, fit=False, encoder=enc, scaler=scl)

    model = DemandEnsemble()
    model.fit(Xtr, ytr, train_df)
    pred = model.predict(Xte, test_df)

    sarima = SARIMAX(train_df["passenger_count"], order=(1, 1, 1), seasonal_order=(1, 1, 1, 24)).fit(disp=False)
    sarima_forecast = sarima.forecast(steps=len(test_df))
    baseline_mape = mape(yte, sarima_forecast)

    metrics = {
        "rmse": float(np.sqrt(mean_squared_error(yte, pred))),
        "mae": float(mean_absolute_error(yte, pred)),
        "mape": mape(yte, pred),
        "r2": float(r2_score(yte, pred)),
        "sarima_mape": baseline_mape,
    }

    mlflow.set_experiment("demand_forecasting")
    with mlflow.start_run():
        mlflow.log_params({"model": "XGBoost+Prophet", "baseline": "SARIMA", "splits": 5})
        mlflow.log_metrics(metrics)
        mlflow.sklearn.log_model(model.xgb, "xgb_model")
        mlflow.log_dict({"cv": fold_metrics}, "rolling_cv_metrics.json")

    joblib.dump({"model": model, "encoder": enc, "scaler": scl}, out / "ensemble.joblib")

    residuals = yte - pred
    plt.figure(figsize=(8, 4))
    plt.plot(residuals)
    plt.title("Residual diagnostics")
    plt.tight_layout()
    plt.savefig(out / "residual_diagnostics.png")
    plt.close()

    plt.figure(figsize=(8, 4))
    plt.bar(range(len(model.xgb.feature_importances_)), model.xgb.feature_importances_)
    plt.title("XGBoost Feature Importance")
    plt.tight_layout()
    plt.savefig(out / "feature_importance.png")
    plt.close()

    (out / "metadata.json").write_text(
        json.dumps({"model_version": "1.0.0", "last_trained": now(), "metrics": metrics}, indent=2),
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
