using FrontEnd.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.ViewModels
{
    /// <summary>
    /// Viewmodel used for the computercreation window. Inherits from the base machinecreationviewmodel
    /// </summary>
    public class ComputerCreationViewModel : MachineCreationViewModel
    {
        private string _systemOS;
        public string SystemOS
        {
            get { return _systemOS; }
            set { _systemOS = value; OnPropertyChanged(nameof(SystemOS)); }
        }

        /// <summary>
        /// Transform the informations about the machine currently being create into a 
        /// [string, string] dictionary.
        /// </summary>
        /// <returns>A string, string dictionary that contains all the
        /// properties of the machine being created</returns>
        public override Dictionary<string,string> GetProperties()
        {
            Dictionary<string, string> properties = base.GetProperties();
            properties.Add(nameof(SystemOS), SystemOS);
            return properties;
        }
    }
}
