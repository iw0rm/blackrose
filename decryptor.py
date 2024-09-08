import os
import json
import base64
from cryptography.fernet import Fernet

def load_config():
    with open('config.json', 'r') as file:
        config = json.load(file)
    return config

def decrypt_file(file_path, key):
    with open(file_path, 'rb') as file:
        encrypted_data = file.read()
    f = Fernet(key)
    decrypted_data = f.decrypt(encrypted_data)
    with open(file_path, 'wb') as file:
        file.write(decrypted_data)

def decrypt_files(key):
    for root, dirs, files in os.walk('C:\\'):
        for file in files:
            file_path = os.path.join(root, file)
            decrypt_file(file_path, key)

def main():
    config = load_config()
    registry_key = config['registry_key']

    # Check if registry key exists
    try:
        import winreg

        key = winreg.OpenKey(winreg.HKEY_CURRENT_USER, registry_key, 0, winreg.KEY_READ)
        winreg.CloseKey(key)
    except FileNotFoundError:
        print("Decryption key not found. Please provide the decryption key.")
        return

    # Get decryption key from registry
    import winreg

    key = winreg.OpenKey(winreg.HKEY_CURRENT_USER, registry_key, 0, winreg.KEY_READ)
    decryption_key = winreg.QueryValueEx(key, 'DecryptionKey')[0]
    winreg.CloseKey(key)

    decryption_key = base64.b64decode(decryption_key)

    # Decrypt files
    decrypt_files(decryption_key)

    print("Your files have been decrypted.")

if __name__ == '__main__':
    main()