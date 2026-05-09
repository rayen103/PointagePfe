from __future__ import annotations

import numpy as np
import pandas as pd
from sklearn.model_selection import TimeSeriesSplit
from sklearn.preprocessing import OneHotEncoder, StandardScaler

FEATURES = [
    "stop_id",
    "hour",
    "weekday",
    "is_holiday",
    "weather_temp",
    "weather_rain",
    "event_nearby",
    "lag_1h",
    "lag_24h",
    "lag_168h",
]
TARGET = "passenger_count"


def load_dataframe(path: str) -> pd.DataFrame:
    df = pd.read_csv(path, parse_dates=["datetime"])
    for lag in [1, 24, 168]:
        col = f"lag_{lag}h"
        if col not in df.columns:
            df[col] = df.groupby("stop_id")[TARGET].shift(lag)
    return df.dropna(subset=FEATURES + [TARGET]).sort_values("datetime").reset_index(drop=True)


def preprocess(df: pd.DataFrame, fit: bool = True, encoder=None, scaler=None):
    cat = df[["stop_id"]]
    num_cols = [c for c in FEATURES if c != "stop_id"]
    num = df[num_cols].astype(float)

    if fit:
        encoder = OneHotEncoder(handle_unknown="ignore", sparse_output=False)
        scaler = StandardScaler()
        cat_x = encoder.fit_transform(cat)
        num_x = scaler.fit_transform(num)
    else:
        cat_x = encoder.transform(cat)
        num_x = scaler.transform(num)

    X = np.hstack([cat_x, num_x])
    y = df[TARGET].astype(float).values
    return X, y, encoder, scaler


def rolling_origin_splits(df: pd.DataFrame, n_splits: int = 5):
    tscv = TimeSeriesSplit(n_splits=n_splits)
    idx = np.arange(len(df))
    return list(tscv.split(idx))
