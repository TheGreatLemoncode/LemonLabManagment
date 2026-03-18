using BackEnd.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class Machine : ILocation
    {

        public Machine(string pName, string pMarque)
        {
            _name = pName;
            _marque = pMarque;
        }
        public Machine() { }
        public string Locataire { get; set; } = string.Empty;

        private Status _status = Status.Disponible;
        public Status Status { get { return _status; } set{ _status = value; }  }

        private string _name = string.Empty;
        public string Name { get { return _name; } set { _name = value; } }

        private string _description = string.Empty;
        public string Description { get { return _description; } set { _description = value; } }

        private readonly string _marque = string.Empty;
        public string Marque { get { return _marque; } }

        private string _ipAddress = string.Empty;
        public string IP { get { if (_ipAddress != null) return _ipAddress; else { return string.Empty; } } }
    }

    public enum Status
    {
        Disponible,
        Utilisé,
        Indisponible
    }
}
