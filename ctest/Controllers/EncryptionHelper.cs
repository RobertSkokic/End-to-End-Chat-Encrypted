using ctest.Models;
using System.Security.Cryptography;
using System.Text;

namespace ctest.Controllers
{
    public class EncryptionHelper
    {
        // Beispiel 16-Byte Schlüssel (128 Bit)
        private static readonly byte[] Key = new byte[]
        {
            0x80, 0xE1, 0x6E, 0x0C, 0xFB, 0x4C, 0xD3, 0x8D,
            0x63, 0x42, 0xE9, 0x8D, 0xF8, 0x0A, 0xB4, 0x0F
        };

        // Beispiel 16-Byte Initialisierungsvektor (IV)
        private static readonly byte[] IV = new byte[]
        {
            0xA9, 0x67, 0x93, 0x3D, 0x0A, 0x18, 0xD2, 0x4A,
            0x63, 0x25, 0x13, 0xC0, 0xA4, 0xFE, 0xCF, 0xA8
        };

        public static string Encrypt(string plainText)
        {

            using (Aes aes = Aes.Create())
            {

                aes.Key = Key;
                aes.IV = IV;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(plainText);
                        }
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
        }

        public static string Decrypt(string cipherText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
