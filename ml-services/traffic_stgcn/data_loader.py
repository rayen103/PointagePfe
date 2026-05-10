from __future__ import annotations

import numpy as np
import pandas as pd
import torch
from torch_geometric.data import Data


def build_graph_from_csv(path: str):
    df = pd.read_csv(path)
    required = ["source", "target", "distance_km", "speed_last_15min", "time_sin", "time_cos", "day_of_week_enc", "weather", "travel_time_t15", "travel_time_t30", "travel_time_t60"]
    missing = set(required) - set(df.columns)
    if missing:
        raise ValueError(f"Missing columns: {sorted(missing)}")

    nodes = sorted(set(df["source"]).union(set(df["target"])))
    node_idx = {n: i for i, n in enumerate(nodes)}

    edge_index = torch.tensor([[node_idx[s], node_idx[t]] for s, t in zip(df["source"], df["target"])], dtype=torch.long).t().contiguous()
    edge_weight = torch.tensor(1 / np.clip(df["distance_km"].values.astype(float), 1e-3, None), dtype=torch.float32)

    x = torch.tensor(df[["speed_last_15min", "time_sin", "time_cos", "day_of_week_enc", "weather"]].values, dtype=torch.float32)
    y = torch.tensor(df[["travel_time_t15", "travel_time_t30", "travel_time_t60"]].values, dtype=torch.float32)

    return Data(x=x, edge_index=edge_index, edge_weight=edge_weight, y=y)
