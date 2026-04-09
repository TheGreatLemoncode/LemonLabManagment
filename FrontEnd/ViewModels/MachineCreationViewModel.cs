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
            MachineName = string.Empty;
            MachineDescription = string.Empty;
            MachineIpAddress = string.Empty;
        }

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
