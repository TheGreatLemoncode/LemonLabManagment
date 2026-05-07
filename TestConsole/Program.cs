using System;
using System.Security.Cryptography;
using BackEnd.Security;
namespace TestConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var passwords = new Dictionary<string, string>
            {
                { "admin@lemonlab.com",       "Admin1234" },
                { "jdupont@lemonlab.com",     "Dupont1234" },
                { "mtremblay@lemonlab.com",   "Tremblay1234" }
            };

            foreach (var kvp in passwords)
            {
                byte[] salt = Kitchen.CreateSalt();
                string hash = Kitchen.HashPassword(kvp.Value, salt);

                // Plug these into your .lemon files manually
                Console.WriteLine($"\n{kvp.Key}");
                Console.WriteLine($"  Salt (Base64): {Convert.ToBase64String(salt)}");
                Console.WriteLine($"  Hash: {hash}");
            }
            Console.ReadKey();
        }

        
    }
}
