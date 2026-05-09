from __future__ import annotations

import argparse
import json
from pathlib import Path

import numpy as np
from ultralytics import YOLO


def evaluate(model_path: str, data_yaml: str):
    model = YOLO(model_path)
    res = model.val(data=data_yaml)
    out = {
        "map50": float(res.results_dict.get("metrics/mAP50(B)", 0.0)),
        "map5095": float(res.results_dict.get("metrics/mAP50-95(B)", 0.0)),
        "count_mae": float(np.nan),
    }
    print(json.dumps(out, indent=2))


if __name__ == "__main__":
    p = argparse.ArgumentParser()
    p.add_argument("--model-path", required=True)
    p.add_argument("--data-yaml", required=True)
    a = p.parse_args()
    evaluate(a.model_path, a.data_yaml)
