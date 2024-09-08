using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading;

    public internal class AESHelper
    {
        private static readonly string AesKeyFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AESKey.dat");
        private static readonly byte[] AesKey;

        static AESHelper()
        {
            if (File.Exists(AesKeyFile))
            {
                AesKey = File.ReadAllBytes(AesKeyFile);
            }
            else
            {
                AesKey = GenerateRandomKey();
                File.WriteAllBytes(AesKeyFile, AesKey);
            }
        }

        public static void EncryptFile(string filePath)
        {
            // Encrypt the file
            EncryptFile(filePath, AesKey);
        }

        private static void EncryptFile(string filePath, byte[] key)
        {
            // Create a new AES instance with the specified key and IV
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = new byte[16];

                // Create a file stream for the input file
                using (FileStream inputStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    // Create a file stream for the output file
                    using (FileStream outputStream = new FileStream(filePath + ".encrypted", FileMode.Create, FileAccess.Write))
                    {
                        // Create a crypto stream for encryption
                        using (CryptoStream cryptoStream = new CryptoStream(outputStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            // Encrypt the file
                            inputStream.CopyTo(cryptoStream);
                        }
                    }
                }
            }
        }

        private static byte[] GenerateRandomKey()
        {
            byte[] key = new byte[32];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(key);
            }
            return key;
        }
    }
}