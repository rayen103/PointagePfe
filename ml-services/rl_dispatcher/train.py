from __future__ import annotations

import argparse
import json
from pathlib import Path

import mlflow
from stable_baselines3 import PPO
from stable_baselines3.common.vec_env import DummyVecEnv

from model import BusDispatchEnv


def train(args):
    out = Path(args.model_dir)
    out.mkdir(parents=True, exist_ok=True)

    vec_env = DummyVecEnv([lambda: BusDispatchEnv() for _ in range(8)])
    model = PPO(
        "MlpPolicy",
        vec_env,
        verbose=0,
        ent_coef=0.01,
        policy_kwargs={"net_arch": [256, 256]},
    )

    mlflow.set_experiment("rl_dispatcher")
    with mlflow.start_run():
        mlflow.log_params({"algo": "PPO", "timesteps": args.timesteps, "vec_env": 8, "ent_coef": 0.01})
        model.learn(total_timesteps=args.timesteps)
        model.save(str(out / "ppo_dispatcher"))
        mlflow.log_artifact(str(out / "ppo_dispatcher.zip"))

        rewards = []
        env = BusDispatchEnv()
        obs, _ = env.reset()
        for _ in range(200):
            action, _ = model.predict(obs, deterministic=True)
            obs, reward, *_ = env.step(int(action))
            rewards.append(float(reward))
        mlflow.log_metric("mean_episode_reward", sum(rewards) / len(rewards))

    (out / "metadata.json").write_text(
        json.dumps({"model_version": "1.0.0", "last_trained": now(), "timesteps": args.timesteps}, indent=2),
        encoding="utf-8",
    )


def now() -> str:
    import pandas as pd

    return pd.Timestamp.utcnow().isoformat()


if __name__ == "__main__":
    p = argparse.ArgumentParser()
    p.add_argument("--model-dir", default="artifacts")
    p.add_argument("--timesteps", type=int, default=2000000)
    train(p.parse_args())
