using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace FrontEnd.ViewModels
{
    internal class MailViewModel : BaseViewModel
    {
        private readonly string Patern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        internal string _Content = string.Empty;

        public string Content
        {
            get { return _Content; }
            set { _Content = value; OnPropertyChanged(nameof(Content)); } 
        }

        public bool IsCorrect
        {
            get { return CheckContent(); }
        }

        public bool CheckContent()
        {
            if (string.IsNullOrEmpty(Content))
            {
                return false;
            }
            return Regex.IsMatch(Content, Patern);
        }


    }
}
