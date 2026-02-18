using Konscious.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Security
{
    internal static class Cuisine
    {
        private static RandomNumberGenerator rng = RandomNumberGenerator.Create();

        internal static bool CompareHashClear(string clear, byte[] hash ,byte[] salt)
        {
            string nHash = HashPassword(clear, salt);
            return Encoding.UTF8.GetBytes(nHash).SequenceEqual(hash);
        }

        internal static string HashPassword(string pwd, byte[] salt)
        {
            Argon2id crypto = new Argon2id(Encoding.UTF8.GetBytes(pwd))
            {
                Salt = salt,
                MemorySize = 1024 * 1024,
                Iterations = 4,
                DegreeOfParallelism = 8
            };
            return Encoding.UTF8.GetString(crypto.GetBytes(16));
        }
        internal static byte[] CreateSalt()
        {
            byte[] buffer = new byte[32];
            rng.GetBytes(buffer);
            return buffer;
        }
    }
}
