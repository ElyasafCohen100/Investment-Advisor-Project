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
    QVBoxLayout, QFormLayout
)
from PySide6.QtGui import QPixmap, QPalette, QBrush
from PySide6.QtCore import Qt

from Fronted.Services.api_service import APIService

# ======================================== BUY STOCKS WINDOW ======================================== #
class BuyStocksWindow(QWidget):
    def __init__(self):
        super().__init__()
        self.setWindowTitle("🌊 Buy Stocks – Responsive Design 🌊")
        self.resize(800, 500)

        # ===== Set Background Image ===== #
        palette = QPalette()
        background = QPixmap(
            "C:/Users/elyas/PycharmProjects/InvestmentAdvisor/Pictures/background_pic.jpeg")  # ✅ make sure this exists
        palette.setBrush(QPalette.Window, QBrush(background))
        self.setPalette(palette)

        # ========== CSS Styling for Modern Blue Look ========== #
        self.setStyleSheet("""
            QLabel {
                color: white;
                font-size: 16px;
                font-weight: bold;
            }
            QLineEdit {
                background-color: #e3f2fd;
                color: #0d47a1;
                border: 1px solid #90caf9;
                border-radius: 6px;
                padding: 6px;
            }
            QPushButton {
                background-color: rgba(255, 255, 255, 0.8);
                color: #0d47a1;
                font-weight: bold;
                border-radius: 16px;
                padding: 10px 24px;
            }
            QPushButton:hover {
                background-color: rgba(255, 255, 255, 1.0);
            }
        """)

        # ========== Inputs and Labels ========== #
        self.stock_input = QLineEdit()
        self.stock_input.setPlaceholderText("e.g., AAPL")
        self.amount_input = QLineEdit()
        self.amount_input.setPlaceholderText("e.g., 10")

        # ========== Submit Button ========== #
        self.execute_purchase_button = QPushButton("✅ Execute Purchase")
        self.execute_purchase_button.clicked.connect(self.on_click_execute_purchase)

        # ========== Form Layout ========== #
        form_layout = QFormLayout()
        form_layout.addRow("Stock name:", self.stock_input)
        form_layout.addRow("Amount:", self.amount_input)

        # ========== Container Layout ========== #
        form_container = QWidget()
        form_container.setLayout(form_layout)
        form_container.setMaximumWidth(400)

        main_layout = QVBoxLayout()
        main_layout.setAlignment(Qt.AlignCenter)
        main_layout.setSpacing(25)
        main_layout.addWidget(form_container)
        main_layout.addWidget(self.execute_purchase_button, alignment=Qt.AlignCenter)

        self.setLayout(main_layout)

    # ========== Confirmation Logic ========== #
    def on_click_execute_purchase(self):
        stock = self.stock_input.text()
        amount = self.amount_input.text()

        if not stock or not amount:
            QMessageBox.warning(self, "Missing Fields ⚠️", "Please fill all the fields")
            return

        response = APIService.buy_stock(stock, amount)

        if response.get("success"):
            QMessageBox.information(self, "Done ✅", response["message"])
        else:
            QMessageBox.critical(self, "Error ❌", response["message"])



