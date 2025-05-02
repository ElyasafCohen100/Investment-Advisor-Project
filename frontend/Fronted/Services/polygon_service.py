from datetime import datetime, timedelta

import requests


class PolygonService:
    API_KEY = "B3oUsO0EkvpF9xzR8vq2ob4XDP4zcx80"
    BASE_URL = "https://api.polygon.io"

    @staticmethod
    def get_last_3_months_history(symbol):
        today = datetime.today()
        from_date = (today - timedelta(days=90)).strftime("%Y-%m-%d")
        to_date = today.strftime("%Y-%m-%d")

        url = f"{PolygonService.BASE_URL}/v2/aggs/ticker/{symbol}/range/1/day/{from_date}/{to_date}?adjusted=true&sort=asc&apiKey={PolygonService.API_KEY}"
        try:
            response = requests.get(url)
            response.raise_for_status()
            results = response.json().get("results", [])
            return [
                {
                    "date": datetime.fromtimestamp(item["t"] / 1000).strftime("%Y-%m-%d"),
                    "price": item["c"]
                }
                for item in results
            ]
        except Exception as e:
            return []
