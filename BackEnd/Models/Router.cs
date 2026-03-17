using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class Router(string pName, string pMarque) : Machine(pName, pMarque)
    {
        private readonly List<Machine> Devices = [];
        public bool ConnectDevice(Machine pDevice)
        {
            if (Devices.Contains(pDevice))
                return true;
            Devices.Add(pDevice);
            return Devices.Contains(pDevice);
        }

        public bool DisconnectDevice(Machine pDevice)
        {
            Devices.Remove(pDevice);
            return Devices.Contains(pDevice);
        }

        public List<Machine> GetDevices() { return  Devices; }
    }
}
