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
    /// <summary>
    /// Static class that handle salt creation, hash creation, randomizer and regex for the backend
    /// </summary>
    internal static class Kitchen
    {
        private static RandomNumberGenerator rng = RandomNumberGenerator.Create();
        private static string IpPatern = @"^(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)$";

        /// <summary>
        /// Compare a hash sequence and a clear password using a salt
        /// </summary>
        /// <param name="clear">the clear password as entered by the user</param>
        /// <param name="hash">the hash sequence stored in the database</param>
        /// <param name="salt">salt sequence stored in the database</param>
        /// <returns>true if the sequence is the same as the clear password. returns false otherwise</returns>
        internal static bool CompareHashClear(string clear, string hash ,byte[] salt)
        {
            string nHash = HashPassword(clear, salt);
            return Encoding.UTF8.GetBytes(nHash).SequenceEqual(Encoding.UTF8.GetBytes(hash));
        }

        /// <summary>
        /// Create a hash sequence using a clear password and a salt sequence
        /// </summary>
        /// <param name="pwd">clear password as entered by the user</param>
        /// <param name="salt">salt sequence stored in the database or generated</param>
        /// <returns>A string version of the hash sequence</returns>
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

        /// <summary>
        /// Method that create a salt using a 32 size byte array
        /// </summary>
        /// <returns>A salt sequence as a byte array</returns>
        internal static byte[] CreateSalt()
        {
            byte[] buffer = new byte[32];
            rng.GetBytes(buffer);
            return buffer;
        }

        /// <summary>
        /// Method that return a random string of size <pSize> 
        /// </summary>
        /// <param name="pSize"></param>
        /// <returns>A random string of <pSize> </returns>
        internal static string GetRandomString(byte pSize)
        {
            string chararray = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            string returnstring = "";
            for(byte i = 0; i < pSize; i++)
            {
                returnstring += chararray[RandomNumberGenerator.GetInt32(chararray.Length)];
            }
            return returnstring;
        }

        /// <summary>
        /// Check if the string passes as parameter matches the
        /// regex patern of an ip address
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
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
