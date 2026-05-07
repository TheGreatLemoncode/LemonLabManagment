using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class Account(string pName, string pPwd ) 
    {
        public string _name = pName;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
            }
        }
        public string Mail { get; set; }
        public string Password { get; set; } = pPwd;
        private Organisation _organisation;
        public Organisation Organisation
        {
            get { return _organisation; }
            set { _organisation = value; }
        }

    }
}
