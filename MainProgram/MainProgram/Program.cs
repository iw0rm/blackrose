using System;
using System.Security.Cryptography;

namespace MainProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            // Main oprerating point for the most part depending what you intend to use with or for
            // string publicKey = KeyGenerator.KeyPair.GenerateKeys();
            KeyGenerator.KeyPair.GenerateKeys();

            //
         //   string filePath = "\"C:\\Users\\sin4amin\\Desktop\\testpath.txt\"";
            //
            // <//> Adjust to fit needs <//>
            //
            // FileEncryptor.Encryptor.EncryptFile(filePath, publicKey)
            //
            // <//> Adjust to fit needs <//>
            //
 //           Console.WriteLine("Your files have been encrypted!");
  //          Console.WriteLine("Send me the following public key to decrypt your files:");
            //
            //
            // Console.WriteLine(Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(publicKey)));
            //
            //
   //         Console.WriteLine("Press Enter to decrypt your files...");
    //        Console.ReadLine();
            //
            //
     //       string privateKey = KeyGenerator.KeyPair.GenerateKeys();
    //        FileDecryptor.Decryptor.DecryptFile(filePath, privateKey);
            //
      //      Console.WriteLine("Your files have been decrypted!");
       //     Console.ReadLine();
        }
    }
}