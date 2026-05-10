from __future__ import annotations

from dataclasses import dataclass
from typing import List, Tuple

import numpy as np
import pandas as pd
from sklearn.preprocessing import MinMaxScaler

FEATURE_COLUMNS: List[str] = [
    "gps_lat",
    "gps_lon",
    "speed",
    "heading",
    "stop_sequence",
    "hour",
    "day_of_week",
    "weather_code",
    "historical_delay_avg",
]
TARGET_COLUMN = "minutes_to_arrival"


@dataclass
class SequenceDataset:
    X: np.ndarray
    y: np.ndarray


def load_eta_dataframe(csv_path: str) -> pd.DataFrame:
    df = pd.read_csv(csv_path)
    missing = set(FEATURE_COLUMNS + [TARGET_COLUMN]) - set(df.columns)
    if missing:
        raise ValueError(f"Missing required columns: {sorted(missing)}")

    if "timestamp" in df.columns:
        df = df.sort_values([c for c in ["route_id", "stop_id", "timestamp"] if c in df.columns])

    return df.dropna(subset=FEATURE_COLUMNS + [TARGET_COLUMN]).reset_index(drop=True)


def make_sliding_windows(
    df: pd.DataFrame,
    window: int = 10,
    scaler: MinMaxScaler | None = None,
) -> Tuple[SequenceDataset, MinMaxScaler]:
    scaler = scaler or MinMaxScaler()
    features = df[FEATURE_COLUMNS].astype(float).values
    target = df[TARGET_COLUMN].astype(float).values

    scaled = scaler.fit_transform(features) if not hasattr(scaler, "scale_") else scaler.transform(features)

    X, y = [], []
    for i in range(window, len(df)):
        X.append(scaled[i - window : i])
        y.append(target[i])

    return SequenceDataset(np.asarray(X, dtype=np.float32), np.asarray(y, dtype=np.float32)), scaler


def split_dataset(seq_data: SequenceDataset, train_ratio: float = 0.7, val_ratio: float = 0.15):
    n = len(seq_data.X)
    train_end = int(n * train_ratio)
    val_end = train_end + int(n * val_ratio)

    train = SequenceDataset(seq_data.X[:train_end], seq_data.y[:train_end])
    val = SequenceDataset(seq_data.X[train_end:val_end], seq_data.y[train_end:val_end])
    test = SequenceDataset(seq_data.X[val_end:], seq_data.y[val_end:])
    return train, val, test
