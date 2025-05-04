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

from Frontend.Windows.MainWindow import MainWindow
from PySide6.QtWidgets import (
    QWidget, QLabel, QVBoxLayout, QLineEdit, QPushButton, QMessageBox, QHBoxLayout
)
from PySide6.QtGui import QPalette, QBrush, QPixmap, QCursor
from PySide6.QtCore import Qt
from Frontend.Services.api_service import APIService
import os

# ╔══════════════════════════════════════════════════════════════╗
# ║ 📝 SignUpWindow – UI for Creating a New User Account
# ║ This window allows a new user to sign up by providing
# ║ a username and password. If the registration is successful,
# ║ the user is redirected to the MainWindow.
# ╚══════════════════════════════════════════════════════════════╝


# ====== SignUpWindow Class ====== #
# A window that allows a user to create an account.
# If signup is successful, it opens MainWindow.

class SignUpWindow(QWidget):
    def __init__(self, login_window=None):
        super().__init__()
        self.setWindowTitle("📝 Sign Up – Create Your Account")
        self.login_window = login_window
        self.resize(800, 500)

        # ====== Set background image ====== #
        palette = QPalette()
        current_dir = os.path.dirname(__file__)
        background_path = os.path.normpath(os.path.join(current_dir, "..", "..", "Pictures", "background_pic.jpeg"))
        background = QPixmap(background_path)
        palette.setBrush(QPalette.Window, QBrush(background))
        self.setPalette(palette)

        # ====== Global style (labels, inputs, buttons) ====== #
        self.setStyleSheet("""
            QLabel#titleLabel {
                color: white;
                font-size: 26px;
                font-weight: bold;
            }

            QLineEdit {
                background-color: white;
                color: black;
                border: none;
                border-radius: 15px;
                padding: 1px 10px;
                font-size: 16px;
                min-width: 340px;
                max-width: 340px;
                height: 42px;
            }

            QPushButton {
                background-color: white;
                color: #1a237e;
                font-weight: bold;
                font-size: 16px;
                border-radius: 14px;
                padding: 10px 30px;
                min-width: 200px;
                max-width: 180px;
            }

            QPushButton:hover {
                background-color: #eeeeee;
            }
        """)

        # ====== Title Label ====== #
        self.title_label = QLabel("📝 Sign Up")
        self.title_label.setObjectName("titleLabel")
        self.title_label.setAlignment(Qt.AlignCenter)

        # ====== Input Fields ====== #
        self.username_input = QLineEdit()
        self.username_input.setPlaceholderText("Username")

        self.password_input = QLineEdit()
        self.password_input.setPlaceholderText("Password")
        self.password_input.setEchoMode(QLineEdit.Password)

        # ====== Signup Button ====== #
        self.signup_button = QPushButton("✅ Create Account")
        self.signup_button.setCursor(QCursor(Qt.PointingHandCursor))
        self.signup_button.clicked.connect(self.handle_signup)

        # ====== Center the button ====== #
        button_wrapper = QHBoxLayout()
        button_wrapper.addStretch()
        button_wrapper.addWidget(self.signup_button)
        button_wrapper.addStretch()

        # ====== Main Layout ====== #
        layout = QVBoxLayout()
        layout.setAlignment(Qt.AlignCenter)
        layout.setSpacing(18)
        layout.addWidget(self.title_label)
        layout.addWidget(self.username_input, alignment=Qt.AlignCenter)
        layout.addWidget(self.password_input, alignment=Qt.AlignCenter)
        layout.addLayout(button_wrapper)

        self.setLayout(layout)

    # ====== handle_signup() ====== #
    # This function handles the signup logic.
    # If successful, it opens MainWindow. Else, it shows an error.

    def handle_signup(self):
        username = self.username_input.text().strip()
        password = self.password_input.text().strip()

        if not username or not password:
            QMessageBox.warning(self, "Missing Fields", "Please fill in all fields.")
            return

        response = APIService.create_user(username, password)
        print("Login response:", response)

        if response.get("success") and "userId" in response:
            APIService.current_user_id = response["userId"]
            QMessageBox.information(self, "Success", response["message"])

            # ====== Open MainWindow after successful signup ====== #
            self.MainWindow1 = MainWindow(user_id=response["userId"])
            self.MainWindow1.show()
            self.close()
        else:
            QMessageBox.critical(self, "Error", response.get("message", "Unknown error."))
