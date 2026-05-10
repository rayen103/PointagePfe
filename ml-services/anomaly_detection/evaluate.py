from __future__ import annotations

import argparse
import json
from pathlib import Path

import joblib
import numpy as np
import torch
from sklearn.metrics import confusion_matrix, f1_score, precision_score, recall_score, roc_auc_score

from data_loader import FEATURES, load_dataframe, preprocess
from model import LSTMAutoencoder


def evaluate(dataset: str, model_dir: str):
    out = Path(model_dir)
    bundle = joblib.load(out / "detector.joblib")
    df = load_dataframe(dataset)
    seqs, _ = preprocess(df, seq_len=20, scaler=bundle["scaler"])

    ae = LSTMAutoencoder(input_dim=len(FEATURES))
    ae.load_state_dict(torch.load(out / "autoencoder.pt", map_location="cpu"))
    ae.eval()

    with torch.no_grad():
        x = torch.from_numpy(seqs)
        rec = ae(x)
        err = ((x - rec) ** 2).mean(dim=(1, 2)).numpy()

    iso = bundle["iso"]
    iso_scores = -iso.score_samples(seqs.reshape(len(seqs), -1))
    score = 0.5 * (err / max(bundle["threshold"], 1e-6)) + 0.5 * (iso_scores / np.max(iso_scores))
    pred = (score > 1.0).astype(int)

    if "label" in df.columns and len(df["label"]) >= len(pred) + 20:
        y_true = df["label"].astype(int).values[-len(pred) :]
        result = {
            "precision": float(precision_score(y_true, pred, zero_division=0)),
            "recall": float(recall_score(y_true, pred, zero_division=0)),
            "f1": float(f1_score(y_true, pred, zero_division=0)),
            "roc_auc": float(roc_auc_score(y_true, score)),
            "confusion_matrix": confusion_matrix(y_true, pred).tolist(),
        }
    else:
        result = {"precision": 0.0, "recall": 0.0, "f1": 0.0, "roc_auc": 0.0, "confusion_matrix": [[0, 0], [0, 0]]}

    print(json.dumps(result, indent=2))


if __name__ == "__main__":
    p = argparse.ArgumentParser()
    p.add_argument("--dataset", required=True)
    p.add_argument("--model-dir", default="artifacts")
    a = p.parse_args()
    evaluate(a.dataset, a.model_dir)
