from __future__ import annotations

import torch
from torch import nn
from torch_geometric.nn import GCNConv


class STGCN(nn.Module):
    def __init__(self, in_channels: int = 5, hidden: int = 64, out_horizons: int = 3):
        super().__init__()
        self.gcn1 = GCNConv(in_channels, hidden)
        self.gcn2 = GCNConv(hidden, hidden)
        self.temporal = nn.GRU(hidden, hidden, num_layers=1, batch_first=True)
        self.head = nn.Linear(hidden, out_horizons)

    def forward(self, x, edge_index):
        h = torch.relu(self.gcn1(x, edge_index))
        h = torch.relu(self.gcn2(h, edge_index))
        h = h.unsqueeze(1)
        h, _ = self.temporal(h)
        return self.head(h[:, -1, :])
