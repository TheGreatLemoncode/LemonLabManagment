using BackEnd.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class Computer: Machine
    {
        public Computer(string pName, string pMarque) : base(pName, pMarque) { }
        
    }
}
