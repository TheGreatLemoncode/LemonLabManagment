using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class Router(string pName, string pMarque) : Machine(pName, pMarque)
    {
        private readonly List<string> DevicesKeys = [];
        public bool ConnectDevice(Machine pDevice)
        {
            if (DevicesKeys.Contains(pDevice.Code))
                return true;
            DevicesKeys.Add(pDevice.Code);
            return DevicesKeys.Contains(pDevice.Code);
        }

        public bool DisconnectDevice(Machine pDevice)
        {
            DevicesKeys.Remove(pDevice.Code);
            return DevicesKeys.Contains(pDevice.Code);
        }

        public List<string> GetDevices() { return DevicesKeys; }
    }
}
