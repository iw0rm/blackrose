using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace KeyGenerator
{
    public class KeyPair
    {
        public static string GenerateKeys()
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                // Save the public key
                var publicKey = rsa.ToXmlString(false);
                File.WriteAllText("publicKey.xml", publicKey);
                // Save the private key
                var privateKey = rsa.ToXmlString(true);
                File.WriteAllText("privateKey.xml", privateKey);

                return "Keys saved to publicKey.xml and privateKey.xml";
            }
        }
    }
}