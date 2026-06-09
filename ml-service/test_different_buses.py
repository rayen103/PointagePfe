
import requests
import json
from datetime import datetime, timedelta

url = "http://localhost:8000/predict"

# Test different buses with different distances and times
test_buses = [
    {
        "name": "Bus 1 - Short distance, non-rush hour",
        "data": {
            "distance_to_next_stop": 200,
            "last_position_at": "2026-06-09T10:00:00"
        }
    },
    {
        "name": "Bus 2 - Long distance, rush hour",
        "data": {
            "distance_to_next_stop": 2000,
            "last_position_at": "2026-06-09T08:00:00"
        }
    },
    {
        "name": "Bus 3 - Medium distance, evening rush",
        "data": {
            "distance_to_next_stop": 1000,
            "last_position_at": "2026-06-09T18:00:00"
        }
    }
]

print("Testing different bus scenarios:")
print("=" * 80)
all_predictions = []
for bus in test_buses:
    print(f"\n--- {bus['name']} ---")
    try:
        response = requests.post(url, json=bus["data"], timeout=10)
        print(f"Response status: {response.status_code}")
        result = response.json()
        print(f"Response: {json.dumps(result, indent=2)}")
        all_predictions.append(result["eta_minutes"])
    except Exception as e:
        print(f"Error: {e}")

print("\n" + "="*80)
print("All predictions:", all_predictions)

if len(all_predictions) > 1:
    first = all_predictions[0]
    all_same = True
    for p in all_predictions[1:]:
        if abs(p - first) > 0.01:
            all_same = False
            break
    if all_same:
        print("\nAll predictions are identical - that's not good.")
    else:
        print("\nPredictions are different - that's good!")
