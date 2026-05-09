from __future__ import annotations

from sklearn.ensemble import GradientBoostingRegressor
from xgboost import XGBClassifier


def make_models():
    clf = XGBClassifier(
        n_estimators=350,
        max_depth=5,
        learning_rate=0.05,
        subsample=0.9,
        colsample_bytree=0.9,
        objective="binary:logistic",
        eval_metric="logloss",
        random_state=42,
    )
    reg = GradientBoostingRegressor(random_state=42)
    return clf, reg
