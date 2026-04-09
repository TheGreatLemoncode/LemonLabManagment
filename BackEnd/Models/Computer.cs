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
        private string _operatingSystem;
        public string OperatingSystem
        {
            get { return _operatingSystem; }
            set { _operatingSystem = value; }
        }
        public Computer(string pName) : base(pName) { }
        public Computer(string pName, string OS) : base(pName)
        {
            OperatingSystem = OS;
        }
        public Computer() : base() { }
    }
}
