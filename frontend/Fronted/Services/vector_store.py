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

# =========== Load libraries ============ #
import faiss
import numpy as np
import json

# =========== Load chunks and embeddings from JSON ============ #
def load_data(json_path: str):
    with open(json_path, "r", encoding="utf-8") as f:
        data = json.load(f)
    chunks = [item["chunk"] for item in data]
    embeddings = np.array([item["embedding"] for item in data], dtype="float32")
    return chunks, embeddings

# =========== Create FAISS index ============ #
def create_faiss_index(embeddings: np.ndarray) -> faiss.IndexFlatL2:
    dim = embeddings.shape[1]
    index = faiss.IndexFlatL2(dim)
    index.add(embeddings)
    return index

# =========== Search for similar chunks ============ #
def search_similar_chunks(question_embedding: list[float], index: faiss.IndexFlatL2, chunks: list[str], top_k: int = 5) -> list[str]:
    query = np.array([question_embedding], dtype="float32")
    distances, indices = index.search(query, top_k)
    return [chunks[i] for i in indices[0]]
