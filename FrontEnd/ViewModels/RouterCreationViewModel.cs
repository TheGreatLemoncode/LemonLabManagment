using FrontEnd.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.ViewModels
{
    public class RouterCreationViewModel : MachineCreationViewModel
    {
        private string _marque;
        public string Marque
        {
            get { return _marque; }
            set { _marque = value; OnPropertyChanged(nameof(Marque)); }
        }
        public override Dictionary<string, string> GetProperties()
        {
            Dictionary<string, string> properties = base.GetProperties();
            properties.Add(nameof(Marque), Marque);
            return properties;
        }
    }
}
