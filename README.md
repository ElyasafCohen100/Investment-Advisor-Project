# Investment Advisor Project 💼📈🧠

This is a **Stock Portfolio Management System** developed as part of the "Windows Systems Engineering" course 💻🏗️  
It combines a modern **Python GUI (PySide6)** frontend with a powerful **ASP.NET Core** backend – and smart integrations with cloud & AI services 🤖☁️

---

## 🎮 How to Use: 🎮

- 🔐 Log in securely as a user  
- 📈 Buy or Sell stocks through the GUI  
- 📊 View portfolio in graph or table mode  
- 🧠 Ask the built-in AI advisor (powered by Ollama LLM) for financial recommendations  
- 🖼️ Upload supporting files (e.g., charts) via Cloudinary  
- 🌍 Get real-time market data from Polygon.io  

---

## 📁 Project Structure: 📁

### 🔙 Backend (`backend/`) 🔙
- `Controllers/` – API endpoints  
- `Services/` – business logic  
- `Repositories/` – data access  
- `DTOs/`, `Requests/`, `Models/` – structured data  
- `appsettings.json` – environment configs  
- `StockAdvisorBackend.sln` – Visual Studio solution file  

### 🖥️ Frontend (`frontend/`) 🖥️
- `Windows/` – all GUI windows  
- `Services/` – API and helper logic  
- `Constants/` – static values and config  
- `Pictures/` – UI icons, logos, and visuals  
- `mainWindow.py` – main application entry point  

---

## 🧠 Technologies Used: 🧠

### Frontend
- Python 3.9+  
- PySide6 / Qt  
- QtCharts  

### Backend
- ASP.NET Core 6.0 / 8.0  
- MVC + CQRS + Event Sourcing  
- Hosted on `somee.com`  

### ☁️ External Integrations
- 🌐 [Polygon.io](https://polygon.io) – real-time stock market data  
- 🖼️ [Cloudinary](https://cloudinary.com) – image & file upload  
- 🧠 [Ollama](https://ollama.ai) – LLM agent running via Docker, supports RAG-style queries  

---

## 🛠️ How to Run: 🛠️

### Backend (API)

```bash
# Open in Visual Studio
StockAdvisorBackend.sln

# Configure `appsettings.json`
# Then run with F5 or use IIS Express
```

### Frontend (GUI App)

```bash
cd frontend
pip install PySide6
python Windows/mainWindow.py
```

---

## 🧑‍💻 Created By: 🧑‍💻

- Elyasaf Cohen  
- Eldad Cohen  
- Israel Shlomo

GitHub: [@ElyasafCohen100](https://github.com/ElyasafCohen100)

---

> ✨ If you like this project, don’t forget to give it a star on GitHub! ✨   
