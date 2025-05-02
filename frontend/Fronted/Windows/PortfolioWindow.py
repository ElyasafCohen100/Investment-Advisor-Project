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
    QWidget, QLabel, QVBoxLayout, QHBoxLayout, QListWidget, QListWidgetItem,
    QPushButton, QTableWidget, QTableWidgetItem, QMessageBox
)
from PySide6.QtGui import QPixmap, QPalette, QBrush, QCursor
from PySide6.QtCore import Qt
from Fronted.Services.api_service import APIService


# ======================================== PORTFOLIO WINDOW ======================================== #
class PortfolioWindow(QWidget):
    def __init__(self):
        super().__init__()
        self.setWindowTitle("🌐 Portfolio – Investment Overview 🌐")
        self.resize(950, 650)

        # ===== Set Background Image ===== #
        palette = QPalette()
        background = QPixmap(
            "C:/Users/elyas/PycharmProjects/InvestmentAdvisor/Pictures/background_pic.jpeg")  # ✅ make sure this exists
        palette.setBrush(QPalette.Window, QBrush(background))
        self.setPalette(palette)

        # ===== Title ===== #
        title = QLabel("📊 Your Investment Portfolio 📊")
        title.setStyleSheet("""
            color: white;
            font-size: 22px;
            font-weight: bold;
            padding-bottom: 12px;
        """)

        # ===== Buttons ===== #
        self.refresh_button = QPushButton("🔄 Refresh")
        self.refresh_button.setCursor(QCursor(Qt.PointingHandCursor))
        self.refresh_button.clicked.connect(self.refresh_portfolio)

        self.save_button = QPushButton("💾 Save to File")
        self.save_button.setCursor(QCursor(Qt.PointingHandCursor))
        self.save_button.clicked.connect(self.save_portfolio_to_file)

        self.switch_view_button = QPushButton("📈 Table View")
        self.switch_view_button.setCursor(QCursor(Qt.PointingHandCursor))
        self.switch_view_button.clicked.connect(self.toggle_table_view)

        for btn in [self.refresh_button, self.save_button, self.switch_view_button]:
            btn.setStyleSheet("""
               QPushButton {
                  background-color: transparent;
                  color: #ffffff;
                  font-weight: bold;
                  border: 2px solid #64ffda;
                  border-radius: 10px;
                  padding: 10px 20px;
                }
                QPushButton:hover {
                       background-color: rgba(100, 255, 218, 0.1);
                }

            """)
            btn.setCursor(Qt.PointingHandCursor)

        button_layout = QHBoxLayout()
        button_layout.addWidget(self.refresh_button)
        button_layout.addWidget(self.save_button)
        button_layout.addWidget(self.switch_view_button)
        button_layout.addStretch()

        # ===== Stock List ===== #
        self.stock_list = QListWidget()
        self.stock_list.setStyleSheet("""
            background-color: rgba(255, 255, 255, 0.9);
            padding: 12px;
            border-radius: 12px;
            color: #0d47a1;  /* Deep blue text color */
        """)

        # ===== Stats Table ===== #
        self.stats_table = QTableWidget()
        self.stats_table.setColumnCount(3)
        self.stats_table.setHorizontalHeaderLabels(["Stock", "Shares", "Value ($)"])
        self.stats_table.setVisible(False)
        self.stats_table.setStyleSheet("""
            QTableWidget {
                background-color: 1e1e1e
                color:  #ffffff;
                font-size: 16px;
                border-radius: 10px;
                gridline-color: #90caf9;
                alternate-background-color:  #2b2b2b;
            }
            QHeaderView::section {
                background-color: rgba(240, 248, 255, 0.95);
                color: #0d47a1;
                font-weight: bold;
                padding: 6px;
                border: none;
            }
            QTableWidget::item {
                padding: 6px;
                border: none;
            }
        """)
        self.stats_table.setAlternatingRowColors(True)
        self.stats_table.horizontalHeader().setStretchLastSection(True)

        # ===== Layout ===== #
        layout = QVBoxLayout()
        layout.setAlignment(Qt.AlignTop)
        layout.addWidget(title)
        layout.addLayout(button_layout)
        layout.addWidget(self.stock_list)
        layout.addWidget(self.stats_table)
        self.setLayout(layout)

        self.refresh_portfolio()

    def refresh_portfolio(self):
        self.stock_list.clear()
        self.stats_table.setRowCount(0)

        if APIService.current_user_id is None:
            QMessageBox.warning(self, "User not logged in", "Please log in first.")
            return

        try:
            print("Sending GET to:", f"{APIService.BASE_URL}/Portfolio/user/{APIService.current_user_id}")
            response = APIService.get_portfolio()

            if not response["success"]:
                raise Exception(response["message"])

            if "portfolio" not in response["data"]:
                raise Exception("Response JSON missing 'portfolio' key")

            portfolio_data = response["data"]["portfolio"]
            print("✅ Portfolio response received:", portfolio_data)

            self.stats_table.setRowCount(len(portfolio_data))

            for i, item in enumerate(portfolio_data):
                try:
                    symbol = item.get("stockSymbol", "N/A")
                    amount = item.get("amount", 0)
                    price = item.get("purchasePrice", 0)
                    value = item.get("value", amount * price)

                    QListWidgetItem(f"{symbol} – {amount} shares", self.stock_list)

                    self.stats_table.setItem(i, 0, QTableWidgetItem(symbol))
                    self.stats_table.setItem(i, 1, QTableWidgetItem(str(amount)))
                    self.stats_table.setItem(i, 2, QTableWidgetItem(f"${value:,.2f}"))

                except Exception as item_err:
                    print(f"❌ Error processing item at index {i}: {item_err}")
                    continue

            self.stats_table.setVisible(True)
            self.stock_list.setVisible(False)

        except Exception as e:
            print(f"❌ Exception while loading portfolio: {e}")
            QMessageBox.critical(self, "Error ❌", f"Failed to load portfolio:\n{e}")

    def save_portfolio_to_file(self):
        # כאן תכתוב את הלוגיקה לשמירת התיק לקובץ
        print("Saving portfolio to file...")  # לבינתיים הודעה לבדיקה

    def toggle_table_view(self):
        if self.stats_table.isVisible():
            self.stats_table.setVisible(False)
            self.stock_list.setVisible(True)
        else:
            self.stats_table.setVisible(True)
            self.stock_list.setVisible(False)
