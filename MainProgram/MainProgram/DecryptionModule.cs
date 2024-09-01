using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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