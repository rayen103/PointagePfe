from __future__ import annotations

import numpy as np


def sample_state(num_stops: int = 5, num_buses: int = 3):
    return {
        "queue_sizes_per_stop": np.random.randint(0, 50, size=num_stops).tolist(),
        "bus_positions": np.random.randint(0, num_stops, size=num_buses).tolist(),
        "bus_occupancies": np.random.rand(num_buses).tolist(),
        "time_of_day": float(np.random.uniform(0, 23.99)),
        "traffic_level": float(np.random.uniform(0, 1)),
    }
