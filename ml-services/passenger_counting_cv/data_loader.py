from __future__ import annotations

from pathlib import Path


def verify_dataset(dataset_yaml: str) -> Path:
    path = Path(dataset_yaml)
    if not path.exists():
        raise FileNotFoundError(f"Dataset config not found: {path}")
    return path
