from __future__ import annotations

import argparse
import json
from pathlib import Path

import mlflow
import mlflow.pytorch
import numpy as np
import torch
from sklearn.metrics import mean_absolute_error, mean_squared_error

from data_loader import build_graph_from_csv
from model import STGCN


def train(args):
    out = Path(args.model_dir)
    out.mkdir(parents=True, exist_ok=True)

    data = build_graph_from_csv(args.dataset)
    n = data.x.shape[0]
    idx = np.arange(n)
    tr_end, va_end = int(0.7 * n), int(0.85 * n)
    tr, va, te = idx[:tr_end], idx[tr_end:va_end], idx[va_end:]

    model = STGCN(in_channels=data.x.shape[1])
    opt = torch.optim.AdamW(model.parameters(), lr=1e-3)
    loss_fn = torch.nn.SmoothL1Loss()

    mlflow.set_experiment("traffic_stgcn")
    with mlflow.start_run():
        mlflow.log_params({"model": "STGCN", "loss": "SmoothL1", "optimizer": "AdamW"})
        best_val = float("inf")
        for epoch in range(args.epochs):
            model.train()
            opt.zero_grad()
            pred = model(data.x, data.edge_index)
            loss = loss_fn(pred[tr], data.y[tr])
            loss.backward()
            opt.step()

            model.eval()
            with torch.no_grad():
                val_pred = model(data.x, data.edge_index)
                val_loss = loss_fn(val_pred[va], data.y[va]).item()
            mlflow.log_metric("train_loss", float(loss.item()), step=epoch)
            mlflow.log_metric("val_loss", float(val_loss), step=epoch)

            if val_loss < best_val:
                best_val = val_loss
                torch.save(model.state_dict(), out / "stgcn.pt")

        model.load_state_dict(torch.load(out / "stgcn.pt", map_location="cpu"))
        model.eval()
        with torch.no_grad():
            pred = model(data.x, data.edge_index)[te].numpy()
            actual = data.y[te].numpy()

        mae = float(mean_absolute_error(actual, pred))
        rmse = float(np.sqrt(mean_squared_error(actual, pred)))
        hist_baseline = np.repeat(actual.mean(axis=0, keepdims=True), len(actual), axis=0)
        baseline_mae = float(mean_absolute_error(actual, hist_baseline))
        improvement = float((baseline_mae - mae) / max(baseline_mae, 1e-6) * 100)

        mlflow.log_metrics({"mae": mae, "rmse": rmse, "improvement_over_historical_avg_pct": improvement})
        mlflow.pytorch.log_model(model, "model")

    (out / "metadata.json").write_text(
        json.dumps({"model_version": "1.0.0", "last_trained": now(), "mae": mae, "rmse": rmse}, indent=2),
        encoding="utf-8",
    )


def now() -> str:
    import pandas as pd

    return pd.Timestamp.utcnow().isoformat()


if __name__ == "__main__":
    p = argparse.ArgumentParser()
    p.add_argument("--dataset", required=True)
    p.add_argument("--model-dir", default="artifacts")
    p.add_argument("--epochs", type=int, default=100)
    train(p.parse_args())
