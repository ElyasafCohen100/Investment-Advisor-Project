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

# ╔══════════════════════════════════════════════════════════════════════════╗
# ║ 🧠 vector_store.py – FAISS Search Utility for Chunk Embedding Retrieval
# ║
# ║ 📌 Purpose:
# ║ This module loads text chunks and their embeddings (from JSON),
# ║ builds a FAISS index from them, and allows querying for similar chunks
# ║ using cosine similarity (L2 distance).
# ╚══════════════════════════════════════════════════════════════════════════╝


# ========== 📦 Import Libraries ========== #
import faiss
import numpy as np
import json


# ========== 📂 Load Embeddings + Chunks from JSON ========== #
def load_data(json_path: str):
    """
    📄 Loads saved chunks and their embeddings from a JSON file.
    Returns a tuple of (chunks list, embeddings as numpy array).
    """
    with open(json_path, "r", encoding="utf-8") as f:
        data = json.load(f)

    # ===== Extract chunks and embeddings separately ===== #
    chunks = [item["chunk"] for item in data]
    embeddings = np.array([item["embedding"] for item in data], dtype="float32")
    return chunks, embeddings


# ========== 🧠 Create FAISS Index from Embeddings ========== #
def create_faiss_index(embeddings: np.ndarray) -> faiss.IndexFlatL2:
    """
    🧠 Builds a FAISS flat index (L2) from given embeddings array.
    """
    dim = embeddings.shape[1]  # ← number of features per embedding
    index = faiss.IndexFlatL2(dim)
    index.add(embeddings)
    return index


# ========== 🔍 Search for Most Similar Chunks ========== #
def search_similar_chunks(
        question_embedding: list[float],
        index: faiss.IndexFlatL2,
        chunks: list[str],
        top_k: int = 5
) -> list[str]:
    """
    🔍 Search for top_k similar chunks based on a given embedding.
    Returns a list of text chunks closest to the query.
    """
    query = np.array([question_embedding], dtype="float32")
    distances, indices = index.search(query, top_k)
    return [chunks[i] for i in indices[0]]
