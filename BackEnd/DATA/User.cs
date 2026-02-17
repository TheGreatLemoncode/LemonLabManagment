using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DATA
{
    internal class User(string pName, string? pData)
    {
        public string Name { get; set; } = pName;
        public string Data { get; set; } = pData;
    }
}
