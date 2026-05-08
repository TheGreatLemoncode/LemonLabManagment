using FrontEnd.Interfaces;
using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FrontEnd.ViewModels
{
    /// <summary>
    /// Viewmodel used for the MachineCreationViewModel. Inherits from the baseviewmodel class
    /// </summary>
    public class MachineCreationViewModel: BaseViewModel, IMachineCreation
    {
        private string _machineName;
        private string _machineDescription;
        private string _machineIpAdresse;

        public string MachineName
        {
            get { return _machineName; }
            set
            {
                _machineName = value;
                OnPropertyChanged(nameof(MachineName));
            }
        }
        public string MachineDescription
        {
            get { return _machineDescription; }
            set
            {
                _machineDescription = value;
                OnPropertyChanged(nameof(MachineDescription));
            }
        }
        public string MachineIpAddress
        {
            get { return _machineIpAdresse; }
            set
            {
                _machineIpAdresse = value;
                OnPropertyChanged(nameof(MachineIpAddress));
            }
        }
        public bool CanCommit { get { return !(string.IsNullOrEmpty(MachineName) && !string.IsNullOrEmpty(MachineDescription)); }} 

        public MachineCreationViewModel()
        {
            MachineName = "Machine";
            MachineDescription = "Description";
            MachineIpAddress = "0.0.0.0";
        }


        /// <summary>
        /// Transform the informations about the machine currently being create into a 
        /// [string, string] dictionary.
        /// </summary>
        /// <returns>A string, string dictionary that contains all the
        /// properties of the machine being created</returns>
        public virtual Dictionary<string, string> GetProperties()
        {
            Dictionary<string, string> KeyValueInfo = [];
            KeyValueInfo.Add(nameof(MachineName), MachineName);
            KeyValueInfo.Add(nameof(MachineDescription), MachineDescription);
            KeyValueInfo.Add(nameof(MachineIpAddress), MachineIpAddress);
            return KeyValueInfo;
        }

    }
}
