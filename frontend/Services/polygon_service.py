# ╔═════════════════════════════════╗
# ║         📁 Python Project 📁
# ║
# ║  ✨ Team Members ✨
# ║
# ║  🧑‍💻 Elyasaf Cohen 311557227 🧑‍💻
# ║  🧑‍💻 Eldad Cohen   207920711 🧑‍💻
# ║  🧑‍💻 Israel Shlomo 315130344 🧑‍💻
# ║
# ╚══════════════════════════════════╝

# ╔══════════════════════════════════════════════════════════════════════╗
# ║       📊 Stock History Fetcher – PolygonService.py                   ║
# ║  Step-by-step data retriever from the Polygon.io financial API      ║
# ║  Used to get historical prices for the last 3 months for a stock    ║
# ║  Enables AI assistant or portfolio window to visualize market trend ║
# ╚══════════════════════════════════════════════════════════════════════╝

# ------------------------------------------------------------- #
# Step 1: Import required modules                               #
#         datetime → for date range creation                    #
#         requests → to fetch data from Polygon API             #
# ------------------------------------------------------------- #

# ═════════════════════════════════════════════════════════════ #
# 📦 PolygonService – handles API calls to Polygon.io           #
# Use this to get 3 months of daily historical data for stocks  #
# ═════════════════════════════════════════════════════════════ #

# ======== Imports for date handling and API requests ========= #
import requests
from datetime import datetime, timedelta


# ======== Service class to interact with Polygon API ========= #
class PolygonService:
    # ======== Your Polygon.io API key and base URL ========= #
    API_KEY = "B3oUsO0EkvpF9xzR8vq2ob4XDP4zcx80"
    BASE_URL = "https://api.polygon.io"

    @staticmethod
    def get_last_3_months_history(symbol):
        # ======== Define date range: from 90 days ago until today ========= #
        today = datetime.today()
        from_date = (today - timedelta(days=90)).strftime("%Y-%m-%d")
        to_date = today.strftime("%Y-%m-%d")

        # ======== Construct API request URL with query params ========= #
        url = (
            f"{PolygonService.BASE_URL}/v2/aggs/ticker/{symbol}/range/1/day/"
            f"{from_date}/{to_date}?adjusted=true&sort=asc&apiKey={PolygonService.API_KEY}"
        )

        try:
            # ======== Make the GET request to Polygon API ========= #
            response = requests.get(url)
            response.raise_for_status()

            # ======== Extract results and convert to date + price format ========= #
            results = response.json().get("results", [])
            return [
                {
                    "date": datetime.fromtimestamp(item["t"] / 1000).strftime("%Y-%m-%d"),  # UNIX timestamp ➡️ readable date
                    "price": item["c"]  # Closing price
                }
                for item in results
            ]

        except Exception as e:
            # ======== In case of failure, return empty list ========= #
            return []
