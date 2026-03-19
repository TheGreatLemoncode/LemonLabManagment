using BackEnd.API;
using BackEnd.Interface;
using BackEnd.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackEnd.API;

namespace BackEnd.Models
{
    public class Machine : ILocation
    {

        public Machine(string pName, string pMarque)
        {
            _name = pName;
            _marque = pMarque;
            Code = Kitchen.GetRandomString(7);
        }
        public Machine() { }

        private string _code = Kitchen.GetRandomString(7);
        public string Code
        {
            get { return _code; }
            set
            {
                if(!string.IsNullOrEmpty(value) && _code != value && value.Length == 12)
                {
                    DataController.MachineDB.Remove(Code);
                    DataController.MachineDB.Add(value, this);
                    _code = value;
                }   
            }
        }

        public void Reservation()
        {
            if(API.API.ConnectedUser != null)
            {
                Locataire = API.API.ConnectedUser.Name;
            }
            else
            {
                Locataire = "Test location";
            }
            Status = Status.Utilisé;
        }

        public string? Locataire { get; set; } = string.Empty;

        private Status _status = Status.Disponible;
        public Status Status { get { return _status; } set{ _status = value; }  }

        private string _name = string.Empty;
        public string Name { get { return _name; } set { _name = value; } }

        private string _description = string.Empty;
        public string Description { get { return _description; } set { _description = value; } }

        private string _marque;
        public string Marque { get { return _marque; } set{ _marque = value; } }

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
