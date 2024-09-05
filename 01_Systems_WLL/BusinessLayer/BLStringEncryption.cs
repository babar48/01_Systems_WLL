using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace _01_Systems_WLL.BusinessLayer
{
    class BLStringEncryption
    {
        // Fixed IV for consistent encryption results. For security, normally the IV should be random and not fixed.
        private static readonly byte[] FixedIV = Encoding.UTF8.GetBytes("1234567890123456"); // 16 bytes for AES

        // Encrypts a string using AES algorithm
        public static string Encrypt(string plainText, string key)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);

            // Ensure the key is of appropriate length for AES (16, 24, or 32 bytes)
            using (Aes aes = Aes.Create())
            {
                aes.Key = keyBytes.Length == 16 || keyBytes.Length == 24 || keyBytes.Length == 32
                          ? keyBytes : throw new ArgumentException("Key must be 16, 24, or 32 bytes long.");

                aes.IV = FixedIV; // Use the fixed IV

                // Create an encryptor
                using (ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                using (MemoryStream ms = new MemoryStream())
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                using (StreamWriter sw = new StreamWriter(cs))
                {
                    sw.Write(plainText);
                    sw.Flush();
                    cs.FlushFinalBlock();
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        // Decrypts a string using AES algorithm
        public static string Decrypt(string cipherText, string key)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] cipherBytes = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = keyBytes.Length == 16 || keyBytes.Length == 24 || keyBytes.Length == 32
                          ? keyBytes : throw new ArgumentException("Key must be 16, 24, or 32 bytes long.");

                aes.IV = FixedIV; // Use the fixed IV

                // Create a decryptor
                using (ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                using (MemoryStream ms = new MemoryStream(cipherBytes))
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (StreamReader sr = new StreamReader(cs))
                {
                    return sr.ReadToEnd();
                }
            }
        }
    }
}
