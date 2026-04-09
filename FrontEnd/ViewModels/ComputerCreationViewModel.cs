using FrontEnd.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.ViewModels
{
    public class ComputerCreationViewModel : MachineCreationViewModel, IMachineCreation
    {
        private string _sytemOS;
        public string SystemOS
        {
            get { return _sytemOS; }
            set { _sytemOS = value; OnPropertyChanged(nameof(SystemOS)); }
        }
        public override Dictionary<string,string> GetProperties()
        {
            Dictionary<string, string> properties = base.GetProperties();
            properties.Add(nameof(SystemOS), SystemOS);
            return properties;
        }
    }
}
