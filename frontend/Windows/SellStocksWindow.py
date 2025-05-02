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

from PySide6.QtWidgets import (
    QWidget, QLineEdit, QPushButton, QMessageBox,
    QVBoxLayout, QFormLayout, QFrame
)
from PySide6.QtGui import QPixmap, QPalette, QBrush
from PySide6.QtCore import Qt
from Frontend.Services.api_service import APIService


# ======================================== SELL STOCKS WINDOW ======================================== #

class SellStocksWindow(QWidget):
    def __init__(self):
        super().__init__()
        self.setWindowTitle("📉 Sell Stocks – Finance Mode")
        self.resize(800, 500)  # Same as Buy Window 📐

        # ===== Set Background Image ===== #
        palette = QPalette()
        background = QPixmap(
            "C:/Users/elyas/PycharmProjects/InvestmentAdvisor/Pictures/background_pic.jpeg")  # ✅ make sure this exists
        palette.setBrush(QPalette.Window, QBrush(background))
        self.setPalette(palette)

        # ======= Styling: white modern transparent theme ======= #
        self.setStyleSheet("""
            QLabel {
                color: white;
                font-size: 16px;
                font-weight: bold;
            }
            QLineEdit {
                background-color: rgba(255, 255, 255, 0.9);
                color: #333;
                border: 1px solid #ccc;
                border-radius: 6px;
                padding: 6px;
                font-size: 14px;
                min-width: 300px;
            }
            QPushButton {
                background-color: rgba(255, 255, 255, 0.8);
                color: #333;
                font-weight: bold;
                padding: 10px 24px;
                border-radius: 16px;
                font-size: 14px;
            }
            QPushButton:hover {
                background-color: rgba(255, 255, 255, 0.95);
            }
        """)

        # ======= Create input fields ======= #
        self.stock_input = QLineEdit()
        self.stock_input.setPlaceholderText("e.g., TSLA")
        self.amount_input = QLineEdit()
        self.amount_input.setPlaceholderText("e.g., 5")

        # ======= Create button ======= #
        self.sell_button = QPushButton("📉 Execute Sale 🧾")
        self.sell_button.setCursor(Qt.PointingHandCursor)
        self.sell_button.clicked.connect(self.on_execute_sale)

        # ======= Form Layout ======= #
        form_layout = QFormLayout()
        form_layout.addRow("Stock name:", self.stock_input)
        form_layout.addRow("Amount:", self.amount_input)

        form_container = QFrame()
        form_container.setLayout(form_layout)

        # ======= Main Layout ======= #
        main_layout = QVBoxLayout()
        main_layout.setAlignment(Qt.AlignmentFlag.AlignCenter)
        main_layout.addWidget(form_container, alignment=Qt.AlignmentFlag.AlignCenter)
        main_layout.addSpacing(20)
        main_layout.addWidget(self.sell_button, alignment=Qt.AlignmentFlag.AlignCenter)

        self.setLayout(main_layout)

    # ======= Responsive background adjustment on resize ======= #
    def on_resize(self, event):
        palette = QPalette()
        background = QPixmap("C:/Users/elyas/Pictures/bg_finance.png")
        palette.setBrush(QPalette.Window, QBrush(background.scaled(
            event.size(), Qt.KeepAspectRatioByExpanding, Qt.SmoothTransformation)))
        self.setPalette(palette)

    # ======= Action when sale button is clicked ======= #
    def on_execute_sale(self):
        stock = self.stock_input.text()
        amount = self.amount_input.text()

        if not stock or not amount:
            QMessageBox.warning(self, "Missing Fields", "Please fill in both stock name and amount ❗")
            return

        response = APIService.sell_stock(stock, amount)

        if response.get("success"):
            QMessageBox.information(self, "Sale Complete ✅", response["message"])
        else:
            QMessageBox.critical(self, "Error ❌", response["message"])
