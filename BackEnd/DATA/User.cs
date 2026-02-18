using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DATA
{
    internal class User(string pName, string pPwd ,string? pData)
    {
        public string Name { get; set; } = pName;
        public string Password { get; set; } = pPwd;
        public string Data { get; set; } = pData;

        public byte[] GetHashPwd()
        {
            return Encoding.UTF8.GetBytes(Password);
        }
    }
}
