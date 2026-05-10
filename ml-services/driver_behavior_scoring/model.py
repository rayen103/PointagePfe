from __future__ import annotations

from xgboost import XGBClassifier
from sklearn.calibration import CalibratedClassifierCV


def make_model(num_classes: int = 3):
    base = XGBClassifier(
        objective="multi:softprob",
        num_class=num_classes,
        n_estimators=300,
        max_depth=5,
        learning_rate=0.05,
        subsample=0.9,
        colsample_bytree=0.9,
        eval_metric="mlogloss",
        random_state=42,
    )
    return CalibratedClassifierCV(base, method="sigmoid", cv=3)
