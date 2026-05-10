from __future__ import annotations

import argparse
import json
from pathlib import Path

import mlflow
import numpy as np
from ultralytics import YOLO

from data_loader import verify_dataset


def train(args):
    out = Path(args.model_dir)
    out.mkdir(parents=True, exist_ok=True)
    dataset_yaml = verify_dataset(args.dataset_yaml)

    mlflow.set_experiment("passenger_counting_cv")
    with mlflow.start_run():
        mlflow.log_params(
            {
                "epochs": 50,
                "img_size": 640,
                "batch": 16,
                "freeze": 10,
                "augment": "flip+mosaic+hsv",
                "model": "yolov8n",
            }
        )

        model = YOLO("yolov8n.pt")
        results = model.train(
            data=str(dataset_yaml),
            epochs=50,
            imgsz=640,
            batch=16,
            freeze=10,
            hsv_h=0.015,
            hsv_s=0.7,
            hsv_v=0.4,
            fliplr=0.5,
            mosaic=1.0,
            project=str(out),
            name="train",
        )

        metrics = {
            "map50": float(results.results_dict.get("metrics/mAP50(B)", 0.0)),
            "map5095": float(results.results_dict.get("metrics/mAP50-95(B)", 0.0)),
        }
        mlflow.log_metrics(metrics)
        mlflow.log_artifact(str(out / "train" / "weights" / "best.pt"))

    (out / "metadata.json").write_text(
        json.dumps({"model_version": "1.0.0", "last_trained": now(), "metrics": metrics}, indent=2),
        encoding="utf-8",
    )


def now() -> str:
    import pandas as pd

    return pd.Timestamp.utcnow().isoformat()


if __name__ == "__main__":
    p = argparse.ArgumentParser()
    p.add_argument("--dataset-yaml", required=True)
    p.add_argument("--model-dir", default="artifacts")
    train(p.parse_args())
