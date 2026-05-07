using Konscious.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BackEnd.Security
{
    internal static class Kitchen
    {
        private static RandomNumberGenerator rng = RandomNumberGenerator.Create();
        private static string IpPatern = @"^(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)$";

        internal static bool CompareHashClear(string clear, string hash ,byte[] salt)
        {
            string nHash = HashPassword(clear, salt);
            return Encoding.UTF8.GetBytes(nHash).SequenceEqual(Encoding.UTF8.GetBytes(hash));
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

        internal static string GetRandomString(byte size)
        {
            string chararray = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            string returnstring = "";
            for(byte i = 0; i < size; i++)
            {
                returnstring += chararray[RandomNumberGenerator.GetInt32(chararray.Length)];
            }
            return returnstring;
        }

        internal static bool CheckIpFormat(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return false;
            }
            return Regex.IsMatch(content, IpPatern);
        }
    }
}
