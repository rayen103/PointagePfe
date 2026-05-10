from __future__ import annotations

import argparse
import json
from pathlib import Path

import joblib
import numpy as np
from sklearn.metrics import mean_absolute_error, mean_squared_error, r2_score

from data_loader import load_dataframe, preprocess


def mape(y_true, y_pred):
    return float(np.mean(np.abs((y_true - y_pred) / np.clip(np.abs(y_true), 1e-6, None))) * 100)


def evaluate(dataset: str, model_dir: str):
    df = load_dataframe(dataset)
    split_idx = int(len(df) * 0.85)
    test_df = df.iloc[split_idx:]

    bundle = joblib.load(Path(model_dir) / "ensemble.joblib")
    model, enc, scl = bundle["model"], bundle["encoder"], bundle["scaler"]
    Xte, yte, _, _ = preprocess(test_df, fit=False, encoder=enc, scaler=scl)
    pred = model.predict(Xte, test_df)

    print(
        json.dumps(
            {
                "rmse": float(np.sqrt(mean_squared_error(yte, pred))),
                "mae": float(mean_absolute_error(yte, pred)),
                "mape": mape(yte, pred),
                "r2": float(r2_score(yte, pred)),
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
