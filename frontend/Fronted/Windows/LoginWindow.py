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

import sys

from PySide6.QtCore import Qt
from PySide6.QtGui import QBrush, QCursor, QPalette, QPixmap
from PySide6.QtWidgets import (
    QApplication,
    QCheckBox,
    QLabel,
    QLineEdit,
    QMessageBox,
    QPushButton,
    QVBoxLayout,
    QWidget,
)

from Fronted.Services.api_service import APIService
from Fronted.Windows.MainWindow import MainWindow


# ======================================== LOGIN WINDOW ======================================== #
class LoginWindow(QWidget):
    def __init__(self):
        super().__init__()
        self.setWindowTitle("Login – Secure Access 🔐")
        self.resize(800, 500)

        # ===== Set Background Image ===== #
        palette = QPalette()
        background = QPixmap(
            "C:\\Users\\Israel\\PycharmProjects\\InvestmentAdvisor\\Pictures\\background_pic.jpeg"
        )  # ✅ make sure this exists
        palette.setBrush(QPalette.Window, QBrush(background))
        self.setPalette(palette)

        # ====== Style ====== #
        self.setStyleSheet(
            """
            QWidget {
                font-family: 'Segoe UI';
                background-repeat: no-repeat;
                background-position: center;
                font-size: 14px;
                color: white;
            }

            QLabel#titleLabel {
                font-size: 28px;
                font-weight: bold;
                padding-bottom: 20px;
                margin-bottom: 20px;
            }

            QLineEdit {
                background-color: rgba(255, 255, 255, 0.9);
                color: black;
                padding: 10px;
                border-radius: 15px;
            }

            QPushButton {
                background-color: rgba(255, 255, 255, 0.9);
                color: #1a237e;
                font-weight: bold;
                padding: 10px 24px;
                border-radius: 14px;
                max-width: 180px;
            }

            QPushButton:hover {
                background-color: #c5e1f9;
                color: #0d47a1;
            }

            QCheckBox {
                color: white;
                font-size: 13px;
            }
        """
        )

        # ====== Widgets ====== #
        title = QLabel("✌️ Welcome to our application! ✌️", self)
        title.setObjectName("titleLabel")
        title.setAlignment(Qt.AlignCenter)

        self.username_input = QLineEdit()
        self.username_input.setFixedWidth(350)
        self.username_input.setPlaceholderText("Username")
        self.username_input.returnPressed.connect(self.handle_login)

        self.password_input = QLineEdit()
        self.password_input.setFixedWidth(350)
        self.password_input.setEchoMode(QLineEdit.Password)
        self.password_input.setPlaceholderText("Password")
        self.password_input.returnPressed.connect(self.handle_login)

        self.remember_me_checkbox = QCheckBox("Remember me")

        # ====== LOGIN BUTTON ====== #
        self.login_button = QPushButton("🔒 Login 🗝️")
        self.login_button.setFixedWidth(180)
        self.login_button.setCursor(QCursor(Qt.PointingHandCursor))
        self.login_button.clicked.connect(self.handle_login)

        # ====== signup button ====== #
        self.signup_button = QPushButton("📝 Sign Up")
        self.signup_button.setFixedWidth(180)
        self.signup_button.setCursor(QCursor(Qt.PointingHandCursor))
        self.signup_button.clicked.connect(self.handle_signup)

        # ====== Layout ====== #
        layout = QVBoxLayout()
        layout.setAlignment(Qt.AlignCenter)
        layout.setSpacing(12)
        layout.addWidget(title)
        layout.addWidget(self.username_input, alignment=Qt.AlignCenter)
        layout.addWidget(self.password_input, alignment=Qt.AlignCenter)
        layout.addWidget(self.remember_me_checkbox, alignment=Qt.AlignCenter)
        layout.addWidget(self.login_button, alignment=Qt.AlignCenter)
        layout.addWidget(self.signup_button, alignment=Qt.AlignCenter)

        self.setLayout(layout)

    # ====== Logic for login success ====== #
    def handle_login(self):
        username = self.username_input.text().strip()
        password = self.password_input.text().strip()
        # remember = self.remember_me_checkbox.isChecked()

        if not username or not password:
            QMessageBox.warning(
                self, " Missing Info❗", "Please enter both username and password."
            )
            return

        response = APIService.login(username, password)

        if "userId" in response:
            APIService.current_user_id = response["userId"]

        if response.get("success"):
            QMessageBox.information(self, "Welcome 👋", f"Welcome {username}!")
            self.open_main_window()
        else:
            error_message = response.get("message", "Login failed ❌")
            QMessageBox.critical(self, "Login Failed ❌", error_message)

    def open_main_window(self):
        self.main_window = MainWindow()
        self.main_window.show()
        self.close()

    def handle_signup(self):
        from Fronted.Windows.SignUpWindow import SignUpWindow
        self.signup_window = SignUpWindow(login_window=self)
        self.signup_window.show()
        self.hide()


# ======================================== MAIN EXECUTION ======================================== #
if __name__ == "__main__":
    app = QApplication(sys.argv)
    login = LoginWindow()
    login.show()
    sys.exit(app.exec())
