import cloudinary
import cloudinary.uploader
import cloudinary.api

cloudinary.config(
  cloud_name='dkyqt9duq',
  api_key='277851643969462',
  api_secret='zwY3d0KdWQyvseILjzYxWHi47t4',
  secure=True
)

def upload_image(local_path, public_id=None):
    return cloudinary.uploader.upload(local_path, public_id=public_id)
