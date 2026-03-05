using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FrontEnd.ViewModels
{
    class InputViewModel:BaseViewModel
    {
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
            set { _isCorrect = value; OnPropertyChanged(nameof(isCorrect));}
        }

        internal void CheckContent()
        {
            isCorrect = !string.IsNullOrEmpty(Content);
        }


    }
}
