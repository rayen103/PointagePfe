from __future__ import annotations

import argparse
import json
from pathlib import Path

from stable_baselines3 import PPO

from model import BusDispatchEnv


def evaluate(model_dir: str):
    model = PPO.load(str(Path(model_dir) / "ppo_dispatcher.zip"))
    env = BusDispatchEnv()

    obs, _ = env.reset()
    rewards = []
    for _ in range(1000):
        action, _ = model.predict(obs, deterministic=True)
        obs, reward, *_ = env.step(int(action))
        rewards.append(reward)

    print(json.dumps({"mean_episode_reward": float(sum(rewards) / len(rewards))}, indent=2))


if __name__ == "__main__":
    p = argparse.ArgumentParser()
    p.add_argument("--model-dir", default="artifacts")
    a = p.parse_args()
    evaluate(a.model_dir)
