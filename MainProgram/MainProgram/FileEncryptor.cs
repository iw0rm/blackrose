using MainProgram;
using System;
using System.IO;
using System.Security.Cryptography;

namespace FileEncryptor
{
    public class Encryptor
    {
        public static void EncryptFile(string filePath, string publicKey)
        {
            byte[] data = File.ReadAllBytes(filePath);
            byte[] encryptedData = EncryptionModule.Encryption.Encrypt(data, publicKey);
            File.WriteAllBytes(filePath, encryptedData);
        }
    }
}