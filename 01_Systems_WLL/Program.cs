using _01_Systems_WLL.BusinessLayer;
using System;

namespace _01_Systems_WLL
{
    class Program
    {
        static void Main(string[] args)
        {
            bool EncryptionFlag = true;
            try
            {
                while(EncryptionFlag)
                {
                    Console.WriteLine("Enter the plain text:");
                    string plainText = Console.ReadLine();

                    Console.WriteLine("Enter the key (16, 24, or 32 characters long):");
                    string key = Console.ReadLine();

                    string encrypted = BLStringEncryption.Encrypt(plainText, key);
                    Console.WriteLine($"Encrypted: {encrypted}");

                    string decrypted = BLStringEncryption.Decrypt(encrypted, key);
                    Console.WriteLine($"Decrypted: {decrypted}");

                    Console.WriteLine("Do you want to encrypt another string? (yes/no):");
                    string response = Console.ReadLine()?.Trim().ToLower();

                    // Continue if the user inputs 'yes'; otherwise, stop.
                    if (response != "yes")
                    {
                        EncryptionFlag = false;
                        Console.WriteLine("Program terminated.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
