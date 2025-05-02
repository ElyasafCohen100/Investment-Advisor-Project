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

# =========================================================== #
# 🧠 What does this file do?
# 1. Load the PDF file 📄
# 2. Clean the text 🧼
# 3. Split it into chunks ✂️
# 4. Create an embedding vector for each chunk 🔢
# 5. Save everything to a JSON file 💾
# 6. Build a FAISS index for later search 🧠
# =========================================================== #

# =========== Load libraries ============ #
import os
from vector_store import create_faiss_index
from embedder import create_embeddings, save_embeddings
from data_processing import load_pdf_text, clean_text, split_to_chunks

# =========== Set the path to the PDF file (relative path) ============ #
current_dir = os.path.dirname(__file__)
pdf_path = os.path.join(current_dir, "Dataset", "merged_data.pdf")

# =========== Step 1: Load and clean the text ============ #
text = load_pdf_text(pdf_path)
cleaned_text = clean_text(text)

# =========== Step 2: Split text into chunks ============ #
chunks = split_to_chunks(cleaned_text)

# =========== Step 3: Create embeddings from chunks ============ #
embeddings = create_embeddings(chunks)

# =========== Step 4: Save the embeddings and chunks ============ #
output_json = os.path.join(current_dir, "Dataset", "embeddings.json")
os.makedirs(os.path.dirname(output_json), exist_ok=True)
save_embeddings(chunks, embeddings, output_json)

# =========== Step 5: Create FAISS index (optional test) ============ #
index = create_faiss_index(embeddings)

# =========== Print confirmation ============ #
print("✅ All steps completed successfully!")
print(f"🔢 Total chunks: {len(chunks)}")
