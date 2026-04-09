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
    internal class MachineCreationViewModel: BaseViewModel
    {
        public string MachineName { get; set; }
        public string MachineDescription { get; set; }
        public string MachineIpAddress { get; set; }
        public bool CanCommit { get { return (string.IsNullOrEmpty(MachineName) && string.IsNullOrEmpty(MachineDescription)); }} 

        public MachineCreationViewModel()
        {
            MachineName = string.Empty;
            MachineDescription = string.Empty;
            MachineIpAddress = string.Empty;
        }

        public Dictionary<string, string> GetProperties()
        {
            Dictionary<string, string> KeyValueInfo = [];
            KeyValueInfo.Add(nameof(MachineName), MachineName);
            KeyValueInfo.Add(nameof(MachineDescription), MachineDescription);
            KeyValueInfo.Add(nameof(MachineIpAddress), MachineIpAddress);
            return KeyValueInfo;
        }

    }
}
