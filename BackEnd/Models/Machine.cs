using BackEnd.API;
using BackEnd.Interface;
using BackEnd.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackEnd.API;
using BackEnd.DATA;

namespace BackEnd.Models
{
    public class Machine : ILocation
    {

        public Machine(string pName)
        {
            _name = pName;
            Code = Kitchen.GetRandomString(7);
        }
        public Machine() { }

        private string _code = string.Empty;
        public string Code
        {
            get { return _code; }
            set
            {
                if(!string.IsNullOrEmpty(value) && _code != value && value.Length == 7)
                {
                    if (DataController.MachineDB != null && DataController.MachineDB.ContainsKey(Code))
                    {
                        DataController.MachineDB.Remove(Code);
                        DataController.MachineDB.Add(value, this);
                    } 
                    _code = value;
                }   
            }
        }

        public string? Locataire { get; set; } = string.Empty;

        private Status _status = Status.Disponible;
        public Status Status { get { return _status; } set{ _status = value; }  }

        private string _name = string.Empty;
        public string Name { get { return _name; } set { _name = value; } }

        private string _description = string.Empty;
        public string Description { get { return _description; } set { _description = value; } }
        private string _ipAddress = string.Empty;
        public string IP { get { if (_ipAddress != null) return _ipAddress; else { return string.Empty; } }  set { if (Kitchen.CheckIpFormat(value)) _ipAddress = value;} }

        public virtual void SetUp()
        {
            Code = Kitchen.GetRandomString(7);
        }

        public virtual void DescriptionSetUp(string? ajout = null)
        {
            Description += ". " + this.GetType().Name + "\n";
            Description += ". Adresse IP: " + IP + "\n";
            Description += $"Date de création : {DateTime.Today}";

        }
    }

    public enum Status
    {
        Utilisé,
        Disponible,
        Indisponible
    }
}
