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

        public static bool CompareHashClear(string clear, byte[] hash ,byte[] salt)
        {
            return HashPassword(clear, salt).SequenceEqual(hash);
        }

        private static byte[] HashPassword(string pwd, byte[] salt)
        {
            Argon2id crypto = new Argon2id(Encoding.UTF8.GetBytes(pwd));
            crypto.Salt = salt;
            crypto.MemorySize = 1024 * 1024;
            crypto.Iterations = 4;
            crypto.DegreeOfParallelism = 8;
            return crypto.GetBytes(32);
        }
        public static byte[] CreateSalt()
        {
            byte[] buffer = new byte[32];
            rng.GetBytes(buffer);
            return buffer;
        }
    }
}
