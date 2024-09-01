using MainProgram;
using System;
using System.IO;

namespace FileDecryptor
{
    public class Decryptor
    {
        public static void DecryptFile(string filePath, string privateKey)
        {
            byte[] encryptedData = File.ReadAllBytes(filePath);
            byte[] decryptedData = DecryptionModule.Decryption.Decrypt(encryptedData, privateKey);
            File.WriteAllBytes(filePath, decryptedData);
        }
    }
}