from __future__ import annotations

import pandas as pd

FEATURES = [
    "engine_temp",
    "oil_pressure",
    "battery_voltage",
    "mileage_since_service",
    "vibration_rms",
    "fuel_efficiency_delta",
    "bus_age_days",
    "error_code_count_7d",
]


def load_dataframe(path: str) -> pd.DataFrame:
    df = pd.read_csv(path)
    missing = set(FEATURES + ["needs_maintenance_7d", "days_to_failure"]) - set(df.columns)
    if missing:
        raise ValueError(f"Missing columns: {sorted(missing)}")

    for col in FEATURES:
        df[f"rolling_mean_7d_{col}"] = df[col].rolling(window=7, min_periods=1).mean()
        df[f"rate_of_change_{col}"] = df[col].diff().fillna(0)

    return df
