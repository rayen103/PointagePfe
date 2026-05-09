from __future__ import annotations

import argparse
import json
from pathlib import Path

import joblib
import numpy as np
import torch

from data_loader import FEATURE_COLUMNS, load_eta_dataframe, make_sliding_windows, split_dataset
from model import LSTMRegressor


def evaluate(dataset: str, model_dir: str):
    model_path = Path(model_dir)
    scaler = joblib.load(model_path / "scaler.joblib")

    df = load_eta_dataframe(dataset)
    seq_data, _ = make_sliding_windows(df, window=10, scaler=scaler)
    _, _, test_set = split_dataset(seq_data, 0.7, 0.15)

    device = torch.device("cuda" if torch.cuda.is_available() else "cpu")
    model = LSTMRegressor(input_size=len(FEATURE_COLUMNS))
    model.load_state_dict(torch.load(model_path / "best_model.pt", map_location=device))
    model.to(device).eval()

    with torch.no_grad():
        preds = model(torch.from_numpy(test_set.X).to(device)).cpu().numpy()

    actual = test_set.y
    metrics = {
        "mae": float(np.mean(np.abs(preds - actual))),
        "rmse": float(np.sqrt(np.mean((preds - actual) ** 2))),
        "mape": float(np.mean(np.abs((actual - preds) / np.clip(np.abs(actual), 1e-6, None))) * 100),
    }
    print(json.dumps(metrics, indent=2))


if __name__ == "__main__":
    parser = argparse.ArgumentParser()
    parser.add_argument("--dataset", required=True)
    parser.add_argument("--model-dir", default="artifacts")
    args = parser.parse_args()
    evaluate(args.dataset, args.model_dir)
