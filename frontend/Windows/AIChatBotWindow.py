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

import os
from PySide6.QtWidgets import (
    QWidget, QLabel, QVBoxLayout, QLineEdit, QPushButton,
    QHBoxLayout, QScrollArea, QSizePolicy, QFrame
)
from PySide6.QtGui import QPalette, QBrush, QPixmap
from PySide6.QtCore import Qt, QTimer
from Frontend.Services.Ollama_api import ask_ollama_contextual


# ======================================== AI CHATBOT WINDOW ======================================== #
class AIChatBotWindow(QWidget):
    def __init__(self):
        super().__init__()
        self.setWindowTitle("🤖 AI Chat Assistant")
        self.resize(850, 650)

        # ========== 🌍 Set background image using relative path ========== #
        current_dir = os.path.dirname(os.path.abspath(__file__))
        self.bg_path = os.path.normpath(os.path.join(current_dir, "..", "..", "Pictures", "background_pic.jpeg"))

        palette = QPalette()
        background = QPixmap(self.bg_path)
        palette.setBrush(QPalette.Window, QBrush(background))
        self.setPalette(palette)

        # ========== 🌈 Global Styling ========== #
        self.setStyleSheet("""
            QWidget {
                background-repeat: no-repeat;
                background-position: center;
                font-family: 'Segoe UI';
                font-size: 14px;
                color: white;
            }

            QLabel {
                font-weight: bold;
            }

            QLineEdit {
                background-color: rgba(255, 255, 255, 0.9);
                color: black;
                border-radius: 10px;
                padding: 10px;
            }

            QPushButton {
                background-color: rgba(255, 255, 255, 0.9);
                color: #1a237e;
                font-weight: bold;
                padding: 10px 20px;
                border-radius: 10px;
            }

            QPushButton:hover {
                background-color: #c5e1f9;
                color: #0d47a1;
            }
        """)

        # ========== 🧠 Chat Area ========== #
        self.chat_area = QVBoxLayout()
        self.chat_area.setAlignment(Qt.AlignmentFlag.AlignTop)

        chat_container = QWidget()
        chat_container.setLayout(self.chat_area)

        self.scroll = QScrollArea()
        self.scroll.setWidgetResizable(True)
        self.scroll.setWidget(chat_container)
        self.scroll.setStyleSheet("border: none;")

        # ========== 🧾 Input Area ========== #
        self.input_field = QLineEdit()
        self.input_field.setPlaceholderText("Ask me something... 🤖")
        self.input_field.returnPressed.connect(self.handle_send)

        self.send_button = QPushButton("Send 🚀")
        self.send_button.clicked.connect(self.handle_send)

        input_layout = QHBoxLayout()
        input_layout.addWidget(self.input_field)
        input_layout.addWidget(self.send_button)

        # ========== 📋 Main Layout ========== #
        main_layout = QVBoxLayout()
        main_layout.setAlignment(Qt.AlignmentFlag.AlignTop)
        main_layout.addWidget(QLabel("🤖 AI ChatBot"))
        main_layout.addWidget(self.scroll)
        main_layout.addLayout(input_layout)

        self.setLayout(main_layout)

    # ========== 🖼️ Responsive background resize ========== #
    def resizeEvent(self, event):
        pixmap = QPixmap(self.bg_path)
        scaled = pixmap.scaled(self.size(), Qt.KeepAspectRatioByExpanding, Qt.SmoothTransformation)

        palette = QPalette()
        palette.setBrush(QPalette.Window, QBrush(scaled))
        self.setPalette(palette)

        super().resizeEvent(event)

    # ========== 🗨️ Chat Bubble Builder ========== #
    def create_bubble(self, text, is_user=True):
        bubble = QLabel(text)
        bubble.setWordWrap(True)
        bubble.setContentsMargins(14, 10, 14, 10)
        bubble.setMaximumWidth(500)
        bubble.setSizePolicy(QSizePolicy.Expanding, QSizePolicy.Minimum)

        bubble.setStyleSheet(f"""
            background-color: {'#c5e1f9' if is_user else '#37474f'};
            color: {'black' if is_user else 'white'};
            border-top-left-radius: 16px;
            border-top-right-radius: 16px;
            border-bottom-left-radius: {14 if is_user else 20}px;
            border-bottom-right-radius: {20 if is_user else 14}px;
            padding: 10px 14px;
        """)

        avatar = QLabel("🧑‍💻" if is_user else "🤖")
        avatar.setFixedSize(30, 30)
        avatar.setAlignment(Qt.AlignmentFlag.AlignBottom | Qt.AlignmentFlag.AlignCenter)

        layout = QHBoxLayout()
        if is_user:
            layout.addStretch()
            layout.addWidget(bubble)
            layout.addWidget(avatar)
        else:
            layout.addWidget(avatar)
            layout.addWidget(bubble)
            layout.addStretch()

        wrapper = QFrame()
        wrapper.setLayout(layout)
        wrapper.setFrameStyle(QFrame.NoFrame)
        return wrapper

    # ========== 🎯 Send Action Handler ========== #
    def handle_send(self):
        prompt = self.input_field.text().strip()
        if not prompt:
            return

        self.chat_area.addWidget(self.create_bubble(prompt, is_user=True))

        self.loading_label = QLabel(" Thinking... 🤖")
        self.loading_label.setStyleSheet("""
            color: #00acc1;
            font-style: italic;
            padding: 10px;
        """)
        self.chat_area.addWidget(self.loading_label)

        self.input_field.clear()
        self.get_bot_reply(prompt)

    # ========== 📡 Get Response from LLM (contextual) ========== #
    def get_bot_reply(self, prompt):
        try:
            response = ask_ollama_contextual(prompt)
        except Exception as e:
            response = f"Error: {e}"

        def update_ui():
            self.chat_area.removeWidget(self.loading_label)
            self.loading_label.deleteLater()
            self.chat_area.addWidget(self.create_bubble(response, is_user=False))

        QTimer.singleShot(0, update_ui)
