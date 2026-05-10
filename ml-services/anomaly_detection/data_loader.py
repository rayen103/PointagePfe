from __future__ import annotations

import numpy as np
import pandas as pd
from sklearn.preprocessing import StandardScaler

FEATURES = ["speed", "acceleration", "jerk", "heading_change_rate", "dwell_time", "distance_from_route"]


def load_dataframe(path: str) -> pd.DataFrame:
    df = pd.read_csv(path)
    missing = set(FEATURES) - set(df.columns)
    if missing:
        raise ValueError(f"Missing columns: {sorted(missing)}")
    return df.dropna(subset=FEATURES).reset_index(drop=True)


def preprocess(df: pd.DataFrame, seq_len: int = 20, scaler: StandardScaler | None = None):
    x = df[FEATURES].astype(float).values
    x = np.clip(x, x.mean(axis=0) - 3 * x.std(axis=0), x.mean(axis=0) + 3 * x.std(axis=0))
    scaler = scaler or StandardScaler()
    x = scaler.fit_transform(x) if not hasattr(scaler, "mean_") else scaler.transform(x)

    seqs = np.array([x[i - seq_len : i] for i in range(seq_len, len(x))], dtype=np.float32)
    return seqs, scaler
