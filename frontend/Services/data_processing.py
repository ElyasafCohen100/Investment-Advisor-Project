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
# Step-by-step preprocessing pipeline for preparing PDF data  #
# to be used with a RAG-based LLM assistant like Ollama 🤖    #
# =========================================================== #

# ------------------------------------------------ #
# Step 1: Load the PDF and extract its raw text    #
# Step 2: Clean the text (remove weird characters) #
# Step 3: Split the text into small chunks         #
# Step 4: (Later) Create embeddings for each chunk #
# ------------------------------------------------ #

import re
import os
from PyPDF2 import PdfReader


# ==== Loads a PDF file and extracts all its text content ==== #
def load_pdf_text(file_path: str) -> str:
    reader = PdfReader(file_path)
    full_text = ""
    for page in reader.pages:
        full_text += page.extract_text() + "\n"
    return full_text


# ==== Cleans the text by removing strange symbols and excessive newlines ==== #
def clean_text(text: str) -> str:
    text = re.sub(r'[\x00-\x1F\x7F]+', ' ', text)  # Remove control characters
    text = re.sub(r'\n+', '\n', text)  # Collapse multiple newlines
    text = re.sub(r'\s+', ' ', text)  # Collapse extra spaces
    return text.strip()


# ==== Splits the text into smaller chunks to feed into LLM context windows ==== #
def split_to_chunks(text: str, max_length: int = 500) -> list[str]:
    sentences = re.split(r'(?<=[.!?]) +', text)
    chunks = []
    current_chunk = ""

    for sentence in sentences:
        if len(current_chunk) + len(sentence) <= max_length:
            current_chunk += " " + sentence
        else:
            chunks.append(current_chunk.strip())
            current_chunk = sentence
    if current_chunk:
        chunks.append(current_chunk.strip())

    return chunks


if __name__ == "__main__":
    # Path to the PDF relative to this script
    current_dir = os.path.dirname(__file__)
    pdf_path = os.path.join(current_dir, "Dataset", "merged_data.pdf")

    # Step 1: Load text from the PDF
    text = load_pdf_text(pdf_path)

    # Step 2: Clean it
    cleaned = clean_text(text)

    # Step 3: Chunk it
    chunks = split_to_chunks(cleaned)

    # Step 4: Print a preview
    print(f"🔍 First chunk:\n{chunks[0]}")
    print(f"🧩 Total chunks: {len(chunks)}")
