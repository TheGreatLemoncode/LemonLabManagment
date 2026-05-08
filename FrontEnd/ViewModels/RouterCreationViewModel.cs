using FrontEnd.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.ViewModels
{
    /// <summary>
    /// Viewmodel used for the routercreationwindow. Inherits from the base machinecreationviewmodel
    /// </summary>
    internal class RouterCreationViewModel : MachineCreationViewModel
    {
        private string _marque;
        public string Marque
        {
            get { return _marque; }
            set { _marque = value; OnPropertyChanged(nameof(Marque)); }
        }

        /// <summary>
        /// Transform the informations about the machine currently being create into a 
        /// [string, string] dictionary.
        /// </summary>
        /// <returns>A string, string dictionary that contains all the
        /// properties of the machine being created</returns>
        public override Dictionary<string, string> GetProperties()
        {
            Dictionary<string, string> properties = base.GetProperties();
            properties.Add(nameof(Marque), Marque);
            return properties;
        }
    }
}
