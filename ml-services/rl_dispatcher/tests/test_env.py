from model import BusDispatchEnv


def test_env_step_reset():
    env = BusDispatchEnv()
    obs, _ = env.reset(seed=42)
    assert obs is not None
    action = env.action_space.sample()
    next_obs, reward, terminated, truncated, _ = env.step(action)
    assert next_obs.shape == obs.shape
    assert isinstance(float(reward), float)
    assert terminated is False
    assert truncated is False
