using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class Account(string pName, string pPwd ,string? pData) : INotifyPropertyChanged
    {
        public string _name = pName;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }
        public string Mail { get; set; }
        public string Password { get; set; } = pPwd;
        public string Data { get; set; } = pData;
        private Organisation _organisation;
        public Organisation Organisation
        {
            get { return _organisation; }
            set { _organisation = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }

        public byte[] GetHashPwd()
        {
            return Encoding.UTF8.GetBytes(Password);
        }
    }
}
