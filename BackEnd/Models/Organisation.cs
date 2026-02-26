using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackEnd.Security;

namespace BackEnd.Models
{
    public class Organisation
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public List<string> Members { get; set; } = [];
        public string Owner { get; set; }
        public bool AddMember(Account p)
        {
            if(Members.Contains(p.Mail)) 
                return true;
            Members.Add(p.Mail);
            p.Organisation = this;
            return Members.Contains(p.Mail);
        }

        public Organisation(string pName)
        {
            Name = pName;
        }

        public void CreateCode()
        {
            Code = Kitchen.GetRandomString(6);
        }
    }
}
