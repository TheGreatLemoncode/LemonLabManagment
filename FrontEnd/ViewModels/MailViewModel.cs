using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace FrontEnd.ViewModels
{
    /// <summary>
    /// Viewmodel used for the accountcreation and accountconnexion windows. Inherits from the baseviewmodel class
    /// </summary>
    internal class MailViewModel : BaseViewModel
    {
        private readonly string Patern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        private string _Content = string.Empty;

        public string Content
        {
            get { return _Content; }
            set { _Content = value; OnPropertyChanged(nameof(Content)); } 
        }

        public bool IsCorrect
        {
            get { return CheckContent(); }
        }

        /// <summary>
        /// A method that check the value of _content and return true if it 
        /// matches the patern 
        /// </summary>
        /// <returns>True if it matches the paterns. else, false</returns>
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
