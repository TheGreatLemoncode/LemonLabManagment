using System;
using System.Security.Cryptography;

namespace TestConsole
{
    internal class Program
    {
        static int Main(string[] args)
        {
            Console.WriteLine(GetRandomString(6));
            DirectoryInfo di = Directory.CreateDirectory("DATA");
            Console.ReadKey();
            return 9;
        }

        private static string GetRandomString(byte size)
        {
            string chararray = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            string returnstring = "";
            for (byte i = 0; i < size; i++)
            {
                returnstring += chararray[RandomNumberGenerator.GetInt32(chararray.Length)];
            }
            return returnstring;
        }
    }
}
