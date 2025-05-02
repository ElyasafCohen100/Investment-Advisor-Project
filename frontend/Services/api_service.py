import requests


class APIService:
    BASE_URL = "http://localhost:5025/api"  # שנה לכתובת שלך ב-Somee או בענן
    current_user_id = None  # נשמר לאחר login

    @staticmethod
    def login(username, password):
        try:
            response = requests.post(f"{APIService.BASE_URL}/User/login", json={
                "username": username,
                "password": password
            })
            response.raise_for_status()

            # נניח שהשרת מחזיר גם userId
            if response.status_code == 200:
                data = response.json()
                APIService.current_user_id = data.get("id")
                return {"success": True, "message": data.get("message", "Login successful")}

        except Exception as e:
            return {"success": False, "message": str(e)}

    @staticmethod
    def get_stock_by_symbol(symbol):
        try:
            response = requests.get(f"{APIService.BASE_URL}/Stock/symbol/{symbol}")
            response.raise_for_status()
            return {"success": True, "data": response.json()}
        except Exception as e:
            return {"success": False, "message": str(e)}

    @staticmethod
    def buy_stock(symbol, amount):

        if APIService.current_user_id is None:
            return {"success": False, "message": "Please login before making a transaction"}
        try:
            stock_response = APIService.get_stock_by_symbol(symbol)
            if not stock_response["success"]:
                return {"success": False, "message": stock_response["message"]}

            stock_data = stock_response["data"]
            stock_id = stock_data["id"]
            price = stock_data.get("currentPrice", 100)  # מחיר מדומה אם אין

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
        try:
            url = f"{APIService.BASE_URL}/Portfolio/user/{APIService.current_user_id}"
            print(f"📡 Sending GET request to: {url}")

            response = requests.get(url)
            print(f"🔁 Raw response status: {response.status_code}")
            print(f"📄 Raw response body: {response.text[:1000]}")  # כדי לא להציף – חותך ל־1000 תווים

            response.raise_for_status()

            return {"success": True, "data": response.json()}
        except Exception as e:
            print(f"❌ EXCEPTION in get_portfolio(): {e}")
            return {"success": False, "message": str(e)}

    @staticmethod
    def get_all_transactions():
        try:
            response = requests.get(f"{APIService.BASE_URL}/Transaction")
            response.raise_for_status()
            return {"success": True, "data": response.json()}
        except Exception as e:
            return {"success": False, "message": str(e)}

    @staticmethod
    def get_user_transactions():
        try:
            response = requests.get(f"{APIService.BASE_URL}/Transaction/user/{APIService.current_user_id}")
            response.raise_for_status()
            return {"success": True, "data": response.json()}
        except Exception as e:
            return {"success": False, "message": str(e)}

    @staticmethod
    def create_user(username, password):
        try:
            response = requests.post(f"{APIService.BASE_URL}/User/register", json={
                "username": username,
                "password": password
            })
            response.raise_for_status()
            return response.json()  # ← מחזיר את מה שהשרת החזיר בפועל, כולל userId
        except Exception as e:
            return {"success": False, "message": str(e)}
