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
        private string _createur = string.Empty;
        public string Createur { get { return _createur; } set { _createur = value; } }

        public Machine(string pName)
        {
            _name = pName;
            Code = Kitchen.GetRandomString(7);
        }
        public Machine() { }

        /// <summary>
        /// Method that setup the machine code attribute with
        /// a random string
        /// </summary>
        public virtual void SetUp()
        {
            Code = Kitchen.GetRandomString(7);
        }

        /// <summary>
        /// Method that set up the machine description with it's properties 
        /// and add an additional information
        /// </summary>
        /// <param name="ajout"></param>
        public virtual void DescriptionSetUp(string? ajout = null)
        {
            Description += ". " + this.GetType().Name + "\n";
            Description += ". Adresse IP: " + IP + "\n";
            Description += $". Date de création : {DateTime.Today}\n";
        }
    } 


    /// <summary>
    /// Enumeration that represent the actual status of
    /// a machine
    /// </summary>
    public enum Status
    {
        Utilisé,
        Disponible,
        Indisponible
    }
}
