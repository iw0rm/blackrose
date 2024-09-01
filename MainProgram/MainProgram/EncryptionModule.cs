using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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