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


# ======== Import Cloudinary SDK and APIs ========= #
import cloudinary
import cloudinary.api
import cloudinary.uploader

# ======== Configure Cloudinary with credentials ========= #
cloudinary.config(
    cloud_name='dkyqt9duq',                         # Cloudinary cloud name (unique for each account)
    api_key='277851643969462',                      # Public API key
    api_secret='zwY3d0KdWQyvseILjzYxWHi47t4',       # Secret API key (keep secure!)
    secure=True                                     # Ensures HTTPS is used for all requests
)

# ======== Upload function for sending images to Cloudinary ========= #
def upload_image(local_path, public_id=None):
    # Uploads the image file at local_path
    # Optionally sets a custom public_id (Cloudinary image name)
    return cloudinary.uploader.upload(local_path, public_id=public_id)
