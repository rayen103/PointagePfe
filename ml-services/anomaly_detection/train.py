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
from sklearn.ensemble import IsolationForest
from sklearn.metrics import f1_score, precision_score, recall_score, roc_auc_score
from torch import nn
from torch.utils.data import DataLoader, TensorDataset

from data_loader import FEATURES, load_dataframe, preprocess
from model import LSTMAutoencoder


def train(args):
    out = Path(args.model_dir)
    out.mkdir(parents=True, exist_ok=True)

    df = load_dataframe(args.dataset)
    seqs, scaler = preprocess(df, seq_len=20)
    n = len(seqs)
    tr_end, val_end = int(n * 0.7), int(n * 0.85)
    tr, va, te = seqs[:tr_end], seqs[tr_end:val_end], seqs[val_end:]

    iso = IsolationForest(contamination=0.05, random_state=42)
    iso.fit(tr.reshape(len(tr), -1))

    device = torch.device("cuda" if torch.cuda.is_available() else "cpu")
    ae = LSTMAutoencoder(input_dim=len(FEATURES)).to(device)
    opt = torch.optim.AdamW(ae.parameters(), lr=1e-3)
    loss_fn = nn.MSELoss()

    loader = DataLoader(TensorDataset(torch.from_numpy(tr)), batch_size=64, shuffle=True)

    mlflow.set_experiment("anomaly_detection")
    with mlflow.start_run():
        mlflow.log_params({"seq_len": 20, "contamination": 0.05, "model": "IsolationForest+LSTM-AE"})
        for e in range(args.epochs):
            ae.train()
            losses = []
            for (xb,) in loader:
                xb = xb.to(device)
                opt.zero_grad()
                rec = ae(xb)
                loss = loss_fn(rec, xb)
                loss.backward()
                opt.step()
                losses.append(loss.item())
            mlflow.log_metric("train_loss", float(np.mean(losses)), step=e)

        ae.eval()
        with torch.no_grad():
            va_t = torch.from_numpy(va).to(device)
            va_rec = ae(va_t)
            va_err = ((va_t - va_rec) ** 2).mean(dim=(1, 2)).cpu().numpy()
        threshold = float(np.percentile(va_err, 95))

        with torch.no_grad():
            te_t = torch.from_numpy(te).to(device)
            te_rec = ae(te_t)
            te_err = ((te_t - te_rec) ** 2).mean(dim=(1, 2)).cpu().numpy()

        iso_scores = -iso.score_samples(te.reshape(len(te), -1))
        score = 0.5 * (te_err / max(threshold, 1e-6)) + 0.5 * (iso_scores / np.max(iso_scores))
        y_pred = (score > 1.0).astype(int)

        if "label" in df.columns and len(df["label"]) >= len(score) + 20:
            y_true = df["label"].astype(int).values[-len(score) :]
            metrics = {
                "precision": float(precision_score(y_true, y_pred, zero_division=0)),
                "recall": float(recall_score(y_true, y_pred, zero_division=0)),
                "f1": float(f1_score(y_true, y_pred, zero_division=0)),
                "roc_auc": float(roc_auc_score(y_true, score)),
            }
        else:
            metrics = {"precision": 0.0, "recall": 0.0, "f1": 0.0, "roc_auc": 0.0}

        mlflow.log_metrics(metrics)
        mlflow.pytorch.log_model(ae, "autoencoder")

    joblib.dump({"iso": iso, "scaler": scaler, "threshold": threshold}, out / "detector.joblib")
    torch.save(ae.state_dict(), out / "autoencoder.pt")

    plt.figure(figsize=(8, 4))
    plt.hist(score, bins=40)
    plt.axvline(1.0, color="r")
    plt.title("Threshold sensitivity")
    plt.tight_layout()
    plt.savefig(out / "threshold_sensitivity.png")
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
    p.add_argument("--epochs", type=int, default=30)
    train(p.parse_args())
