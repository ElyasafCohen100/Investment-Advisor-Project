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

# ╔═════════════════════════════════════════════════════╗
# ║                  📡 API Service Layer               ║
# ║     Handles communication with the backend server   ║
# ║              Used by all application windows        ║
# ╚═════════════════════════════════════════════════════╝

import requests

class APIService:
    # ======== Base URL and current user ID holder ========= #
    BASE_URL = "http://localhost:5025/api"
    current_user_id = None

    @staticmethod
    def login(username, password):
        # ======== Sends login request with credentials ========= #
        try:
            response = requests.post(f"{APIService.BASE_URL}/User/login", json={
                "username": username,
                "password": password
            })
            response.raise_for_status()

            if response.status_code == 200:
                data = response.json()
                APIService.current_user_id = data.get("id")
                return {"success": True, "message": data.get("message", "Login successful")}

        except Exception as e:
            return {"success": False, "message": str(e)}

    @staticmethod
    def get_stock_by_symbol(symbol):
        # ======== Retrieves stock info using stock symbol ========= #
        try:
            response = requests.get(f"{APIService.BASE_URL}/Stock/symbol/{symbol}")
            response.raise_for_status()
            return {"success": True, "data": response.json()}
        except Exception as e:
            return {"success": False, "message": str(e)}

    @staticmethod
    def buy_stock(symbol, amount):
        # ======== Sends a request to purchase a stock ========= #
        if APIService.current_user_id is None:
            return {"success": False, "message": "Please login before making a transaction"}
        try:
            stock_response = APIService.get_stock_by_symbol(symbol)
            if not stock_response["success"]:
                return {"success": False, "message": stock_response["message"]}

            stock_data = stock_response["data"]
            stock_id = stock_data["id"]
            price = stock_data.get("currentPrice", 100)

            payload = {
                "userId": APIService.current_user_id,
                "stockId": stock_id,
                "transactionAmount": int(amount),
                "priceAtTransaction": price,
                "transactionType": "buy"
            }

            response = requests.post(f"{APIService.BASE_URL}/Transaction", json=payload)
            response.raise_for_status()
            return {"success": True, "message": response.text}
        except Exception as e:
            return {"success": False, "message": str(e)}

    @staticmethod
    def sell_stock(symbol, amount):
        # ======== Sends a request to sell a stock ========= #
        try:
            stock_response = APIService.get_stock_by_symbol(symbol)
            if not stock_response["success"]:
                return {"success": False, "message": stock_response["message"]}

            stock_data = stock_response["data"]
            stock_id = stock_data["id"]
            price = stock_data.get("currentPrice", 100)

            payload = {
                "userId": APIService.current_user_id,
                "stockId": stock_id,
                "transactionAmount": int(amount),
                "priceAtTransaction": price,
                "transactionType": "sell"
            }

            response = requests.post(f"{APIService.BASE_URL}/Transaction", json=payload)
            response.raise_for_status()
            return {"success": True, "message": response.text}
        except Exception as e:
            return {"success": False, "message": str(e)}

    @staticmethod
    def get_portfolio():
        # ======== Retrieves portfolio data for current user ========= #
        try:
            url = f"{APIService.BASE_URL}/Portfolio/user/{APIService.current_user_id}"
            print(f"📡 Sending GET request to: {url}")

            response = requests.get(url)
            print(f"🔁 Raw response status: {response.status_code}")
            print(f"📄 Raw response body: {response.text[:1000]}")

            response.raise_for_status()
            return {"success": True, "data": response.json()}
        except Exception as e:
            print(f"❌ EXCEPTION in get_portfolio(): {e}")
            return {"success": False, "message": str(e)}

    @staticmethod
    def get_all_transactions():
        # ======== Retrieves all transactions (admin/debug) ========= #
        try:
            response = requests.get(f"{APIService.BASE_URL}/Transaction")
            response.raise_for_status()
            return {"success": True, "data": response.json()}
        except Exception as e:
            return {"success": False, "message": str(e)}

    @staticmethod
    def get_user_transactions():
        # ======== Retrieves current user's transactions ========= #
        try:
            response = requests.get(f"{APIService.BASE_URL}/Transaction/user/{APIService.current_user_id}")
            response.raise_for_status()
            return {"success": True, "data": response.json()}
        except Exception as e:
            return {"success": False, "message": str(e)}

    @staticmethod
    def create_user(username, password):
        # ======== Registers a new user account ========= #
        try:
            response = requests.post(f"{APIService.BASE_URL}/User/register", json={
                "username": username,
                "password": password
            })
            response.raise_for_status()
            return response.json()
        except Exception as e:
            return {"success": False, "message": str(e)}
