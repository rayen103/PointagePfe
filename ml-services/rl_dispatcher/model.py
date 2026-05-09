from __future__ import annotations

import gymnasium as gym
import numpy as np
from gymnasium import spaces


class BusDispatchEnv(gym.Env):
    metadata = {"render_modes": []}

    def __init__(self, num_stops: int = 5, num_buses: int = 3, hold_actions: int = 3):
        super().__init__()
        self.num_stops = num_stops
        self.num_buses = num_buses
        self.hold_actions = hold_actions
        self.action_space = spaces.Discrete(num_buses + hold_actions)
        obs_dim = num_stops + num_buses + num_buses + 2
        self.observation_space = spaces.Box(low=0, high=1e3, shape=(obs_dim,), dtype=np.float32)
        self.state = None

    def reset(self, *, seed=None, options=None):
        super().reset(seed=seed)
        queue = self.np_random.integers(0, 50, size=self.num_stops)
        positions = self.np_random.integers(0, self.num_stops, size=self.num_buses)
        occ = self.np_random.random(self.num_buses)
        tod = self.np_random.uniform(0, 24)
        traffic = self.np_random.uniform(0, 1)
        self.state = np.concatenate([queue, positions, occ, [tod, traffic]]).astype(np.float32)
        return self.state, {}

    def step(self, action):
        queue = self.state[: self.num_stops]
        occ = self.state[self.num_stops + self.num_buses : self.num_stops + 2 * self.num_buses]
        traffic = self.state[-1]

        avg_wait = float(np.mean(queue) * (1 + 0.5 * traffic))
        fuel_cost = float(1.5 if action < self.num_buses else 0.4 * (action - self.num_buses + 1))
        occupancy_eff = float(np.mean(occ))
        reward = -avg_wait - 0.1 * fuel_cost + 0.2 * occupancy_eff

        queue = np.maximum(0, queue + self.np_random.normal(1.0, 2.0, size=self.num_stops))
        if action < self.num_buses:
            queue[self.np_random.integers(0, self.num_stops)] = max(0, queue.min() - 5)

        self.state[: self.num_stops] = queue
        self.state[-2] = (self.state[-2] + 1 / 60.0) % 24
        terminated = False
        truncated = False
        return self.state.astype(np.float32), float(reward), terminated, truncated, {}
