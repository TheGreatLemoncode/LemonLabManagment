using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackEnd.API;
using BackEnd.DATA;
using BackEnd.Security;

namespace BackEnd.Models
{
    public class Organisation
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public List<string> Members { get; set; } = [];
        public List<string> Machines { get; set; } = [];
        public string Owner { get; set; }

        /// <summary>
        /// Method that add a user in an organisation if he is not
        /// and add the user's machines to the organisation then return
        /// true if the process succeed
        /// </summary>
        /// <param name="p">the user to add</param>
        /// <returns>true if the user is added or false if otherwise</returns>
        public bool AddMember(Account p)
        {
            if(Members.Contains(p.Mail)) 
                return true;
            Members.Add(p.Mail);
            p.Organisation = this;
            Machines.AddRange(API.API.RequestUserCreatedMachineCode());
            return Members.Contains(p.Mail);
        }

        public Organisation(string pName)
        {
            Name = pName;
        }

        /// <summary>
        /// Method that set up the organisation code attribute with 
        /// a random string
        /// </summary>
        public void CreateCode()
        {
            Code = Kitchen.GetRandomString(6);
        }
    }
}
