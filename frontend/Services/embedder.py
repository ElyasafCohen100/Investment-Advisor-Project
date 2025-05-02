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
from sentence_transformers import SentenceTransformer
import json

# =========== Create embeddings for all chunks ============ #
def create_embeddings(chunks: list[str], model_name: str = 'all-MiniLM-L6-v2') -> list[list[float]]:
    model = SentenceTransformer(model_name)
    embeddings = model.encode(chunks, show_progress_bar=True)
    return embeddings

# =========== Save chunks and embeddings to JSON ============ #
def save_embeddings(chunks: list[str], embeddings: list[list[float]], output_path: str):
    data = []
    for chunk, emb in zip(chunks, embeddings):
        data.append({
            "chunk": chunk,
            "embedding": emb.tolist()
        })
    with open(output_path, "w", encoding="utf-8") as f:
        json.dump(data, f, indent=2, ensure_ascii=False)
