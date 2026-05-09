from __future__ import annotations

import argparse
import json
from pathlib import Path

import numpy as np
import torch
from sklearn.metrics import mean_absolute_error, mean_squared_error

from data_loader import build_graph_from_csv
from model import STGCN


def evaluate(dataset: str, model_dir: str):
    data = build_graph_from_csv(dataset)
    model = STGCN(in_channels=data.x.shape[1])
    model.load_state_dict(torch.load(Path(model_dir) / "stgcn.pt", map_location="cpu"))
    model.eval()

    with torch.no_grad():
        pred = model(data.x, data.edge_index).numpy()
    actual = data.y.numpy()

    out = {
        "mae": float(mean_absolute_error(actual, pred)),
        "rmse": float(np.sqrt(mean_squared_error(actual, pred))),
        "mae_t15": float(np.mean(np.abs(actual[:, 0] - pred[:, 0]))),
        "mae_t30": float(np.mean(np.abs(actual[:, 1] - pred[:, 1]))),
        "mae_t60": float(np.mean(np.abs(actual[:, 2] - pred[:, 2]))),
    }
    print(json.dumps(out, indent=2))


if __name__ == "__main__":
    p = argparse.ArgumentParser()
    p.add_argument("--dataset", required=True)
    p.add_argument("--model-dir", default="artifacts")
    a = p.parse_args()
    evaluate(a.dataset, a.model_dir)
