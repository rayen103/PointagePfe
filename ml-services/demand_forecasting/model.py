from __future__ import annotations

import numpy as np
import pandas as pd
from prophet import Prophet
from xgboost import XGBRegressor


class DemandEnsemble:
    def __init__(self):
        self.xgb = XGBRegressor(
            n_estimators=400,
            max_depth=6,
            learning_rate=0.05,
            subsample=0.9,
            colsample_bytree=0.9,
            objective="reg:squarederror",
            random_state=42,
        )
        self.prophet = Prophet(changepoint_prior_scale=0.05, seasonality_mode="multiplicative")

    def fit(self, X: np.ndarray, y: np.ndarray, df_time: pd.DataFrame):
        self.xgb.fit(X, y)
        prophet_df = df_time[["datetime"]].copy().rename(columns={"datetime": "ds"})
        prophet_df["y"] = y
        self.prophet.fit(prophet_df)

    def predict(self, X: np.ndarray, df_time: pd.DataFrame) -> np.ndarray:
        pred_xgb = self.xgb.predict(X)
        future = df_time[["datetime"]].copy().rename(columns={"datetime": "ds"})
        pred_prophet = self.prophet.predict(future)["yhat"].values
        return 0.65 * pred_xgb + 0.35 * pred_prophet
