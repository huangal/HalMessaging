using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace HalMessaging.Security
{
    public static class AesCypher
    {
        // Change these keys
        private static byte[] key =
        {
            123, 217, 19, 11, 24, 26, 85, 45, 114, 184, 27, 162, 37, 112, 222, 209, 241, 24, 175, 144, 173, 53, 196, 29, 24, 26, 17, 218, 131, 236, 53, 255 //999999999
        };
        // 
        private static byte[] vector = { 146, 64, 191, 111, 23, 3, 113, 119, 231, 121, 1, 112, 79, 32, 114, 22 };
        
        private static readonly ICryptoTransform encryptor;
        private static readonly ICryptoTransform decryptor;
        private static readonly UTF8Encoding encoder;

        static AesCypher()
        {
            using (var rm = new RijndaelManaged())
            {
                encryptor = rm.CreateEncryptor(key, vector);
                decryptor = rm.CreateDecryptor(key, vector);
            }

            encoder = new UTF8Encoding();
        }

        

        public static string Encrypt(this string unencrypted)
        {
            return Convert.ToBase64String(Encrypt(encoder.GetBytes(unencrypted)));
        }

        public static string Decrypt(this string encrypted)
        {
            return encoder.GetString(Decrypt(Convert.FromBase64String(encrypted)));
        }

        public static byte[] Encrypt(byte[] buffer)
        {
            return Transform(buffer, encryptor);
        }

        public static byte[] Decrypt(byte[] buffer)
        {
            return Transform(buffer, decryptor);
        }

        private static byte[] Transform(byte[] buffer, ICryptoTransform transform)
        {
            using (var stream = new MemoryStream())
            {
                using (var cs = new CryptoStream(stream, transform, CryptoStreamMode.Write))
                {
                    cs.Write(buffer, 0, buffer.Length);
                }

                return stream.ToArray();
            }
        }
    }
}
