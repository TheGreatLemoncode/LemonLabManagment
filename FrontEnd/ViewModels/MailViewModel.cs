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
        internal string? _Content;
        internal bool _isCorrect { get; set; } = true;

        public string Content
        {
            get { return _Content; }
            set { _Content = value; OnPropertyChanged(nameof(Content)); } 
        }

        public bool isCorrect
        {
            get { return _isCorrect; }
            set { _isCorrect = value; OnPropertyChanged(nameof(isCorrect)); }
        }

        internal bool CheckContent()
        {
            if (string.IsNullOrEmpty(Content))
            {
                isCorrect = false;
                return false;
            }
            isCorrect = Regex.IsMatch(Content, Patern);
            return isCorrect;
        }


    }
}
