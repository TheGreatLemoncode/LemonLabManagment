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
        private string _operatingSystem = string.Empty;
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

        public override void DescriptionSetUp(string? ajout)
        {
            base.DescriptionSetUp(ajout);
            Description += ". " + "Système d'exploitation: " + OperatingSystem + "\n";
            Description += ajout;
        }
    }
}
