from __future__ import annotations

import argparse
import json
from pathlib import Path

import joblib
import matplotlib.pyplot as plt
import mlflow
import mlflow.pytorch
import numpy as np
import torch
from torch import nn
from torch.optim import AdamW
from torch.optim.lr_scheduler import CosineAnnealingLR
from torch.utils.data import DataLoader, TensorDataset

from data_loader import FEATURE_COLUMNS, load_eta_dataframe, make_sliding_windows, split_dataset
from model import LSTMRegressor


def train(args):
    model_dir = Path(args.model_dir)
    model_dir.mkdir(parents=True, exist_ok=True)

    df = load_eta_dataframe(args.dataset)
    seq_data, scaler = make_sliding_windows(df, window=args.window)
    train_set, val_set, test_set = split_dataset(seq_data, 0.7, 0.15)

    train_loader = DataLoader(
        TensorDataset(torch.from_numpy(train_set.X), torch.from_numpy(train_set.y)),
        batch_size=args.batch_size,
        shuffle=True,
    )
    val_loader = DataLoader(
        TensorDataset(torch.from_numpy(val_set.X), torch.from_numpy(val_set.y)),
        batch_size=args.batch_size,
        shuffle=False,
    )

    device = torch.device("cuda" if torch.cuda.is_available() else "cpu")
    model = LSTMRegressor(input_size=len(FEATURE_COLUMNS), hidden_size=args.hidden_size).to(device)
    criterion = nn.HuberLoss()
    optimizer = AdamW(model.parameters(), lr=args.lr, weight_decay=args.weight_decay)
    scheduler = CosineAnnealingLR(optimizer, T_max=args.epochs)

    best_val = float("inf")
    patience_counter = 0
    train_losses, val_losses = [], []

    mlflow.set_experiment("eta_prediction")
    with mlflow.start_run():
        mlflow.log_params(
            {
                "window": args.window,
                "hidden_size": args.hidden_size,
                "epochs": args.epochs,
                "batch_size": args.batch_size,
                "lr": args.lr,
                "optimizer": "AdamW",
                "loss": "HuberLoss",
                "scheduler": "CosineAnnealingLR",
            }
        )

        for epoch in range(args.epochs):
            model.train()
            epoch_train = []
            for xb, yb in train_loader:
                xb, yb = xb.to(device), yb.to(device)
                optimizer.zero_grad()
                pred = model(xb)
                loss = criterion(pred, yb)
                loss.backward()
                optimizer.step()
                epoch_train.append(loss.item())

            model.eval()
            epoch_val = []
            with torch.no_grad():
                for xb, yb in val_loader:
                    xb, yb = xb.to(device), yb.to(device)
                    loss = criterion(model(xb), yb)
                    epoch_val.append(loss.item())

            tr, vl = float(np.mean(epoch_train)), float(np.mean(epoch_val))
            train_losses.append(tr)
            val_losses.append(vl)
            scheduler.step()

            mlflow.log_metric("train_loss", tr, step=epoch)
            mlflow.log_metric("val_loss", vl, step=epoch)

            if vl < best_val:
                best_val = vl
                patience_counter = 0
                torch.save(model.state_dict(), model_dir / "best_model.pt")
            else:
                patience_counter += 1
                if patience_counter >= args.patience:
                    break

        model.load_state_dict(torch.load(model_dir / "best_model.pt", map_location=device))
        model.eval()

        X_test = torch.from_numpy(test_set.X).to(device)
        y_test = torch.from_numpy(test_set.y).to(device)
        with torch.no_grad():
            preds = model(X_test).cpu().numpy()
            actual = y_test.cpu().numpy()

        mae = float(np.mean(np.abs(preds - actual)))
        rmse = float(np.sqrt(np.mean((preds - actual) ** 2)))
        mape = float(np.mean(np.abs((actual - preds) / np.clip(np.abs(actual), 1e-6, None))) * 100)

        mlflow.log_metrics({"mae": mae, "rmse": rmse, "mape": mape})
        mlflow.pytorch.log_model(model, "model")

        plt.figure(figsize=(8, 4))
        plt.plot(train_losses, label="train")
        plt.plot(val_losses, label="val")
        plt.title("ETA LSTM Loss")
        plt.legend()
        plot_path = model_dir / "loss_curve.png"
        plt.tight_layout()
        plt.savefig(plot_path)
        plt.close()
        mlflow.log_artifact(str(plot_path))

        joblib.dump(scaler, model_dir / "scaler.joblib")
        metadata = {
            "model_version": "1.0.0",
            "last_trained": pd_timestamp(),
            "window": args.window,
            "test_metrics": {"mae": mae, "rmse": rmse, "mape": mape},
        }
        (model_dir / "metadata.json").write_text(json.dumps(metadata, indent=2), encoding="utf-8")


def pd_timestamp() -> str:
    import pandas as pd

    return pd.Timestamp.utcnow().isoformat()


if __name__ == "__main__":
    parser = argparse.ArgumentParser()
    parser.add_argument("--dataset", required=True)
    parser.add_argument("--model-dir", default="artifacts")
    parser.add_argument("--window", type=int, default=10)
    parser.add_argument("--epochs", type=int, default=100)
    parser.add_argument("--patience", type=int, default=10)
    parser.add_argument("--batch-size", type=int, default=64)
    parser.add_argument("--hidden-size", type=int, default=64)
    parser.add_argument("--lr", type=float, default=1e-3)
    parser.add_argument("--weight-decay", type=float, default=1e-4)
    train(parser.parse_args())
