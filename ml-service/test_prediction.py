
import requests
import json

url = "http://localhost:8000/predict"

# Test request with new DB fields
payload = {
    "latitude": 36.8,
    "longitude": 10.2,
    "code_circuit": "100",
    "model_bus": "mercedes",
    "capacite": 50,
    "current_occupancy": 25,
    "last_position_at": "2026-06-09T14:30:00"
}

print("Sending request:", json.dumps(payload, indent=2))
try:
    response = requests.post(url, json=payload, timeout=10)
    print(f"Response status: {response.status_code}")
    print(f"Response body: {json.dumps(response.json(), indent=2)}")
except Exception as e:
    print(f"Error: {e}")

