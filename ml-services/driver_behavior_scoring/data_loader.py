from __future__ import annotations

import pandas as pd
from sklearn.model_selection import GroupShuffleSplit

FEATURES = [
    "avg_speed",
    "speed_variance",
    "harsh_brake_count",
    "harsh_accel_count",
    "sharp_turn_count",
    "idle_time_pct",
    "time_since_break",
    "trip_duration",
]
TARGET = "label"


def load_trip_dataframe(path: str) -> pd.DataFrame:
    df = pd.read_csv(path)
    required = FEATURES + [TARGET, "driver_id", "trip_id"]
    missing = set(required) - set(df.columns)
    if missing:
        raise ValueError(f"Missing columns: {sorted(missing)}")
    return df.dropna(subset=required).reset_index(drop=True)


def split_by_driver(df: pd.DataFrame, test_size: float = 0.2):
    gss = GroupShuffleSplit(n_splits=1, test_size=test_size, random_state=42)
    train_idx, test_idx = next(gss.split(df, groups=df["driver_id"]))
    return df.iloc[train_idx], df.iloc[test_idx]
