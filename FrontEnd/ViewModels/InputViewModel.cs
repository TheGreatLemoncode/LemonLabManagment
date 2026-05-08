using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FrontEnd.ViewModels
{
    /// <summary>
    /// Viewmodel used for textbox field.
    /// </summary>
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

        /// <summary>
        /// Method that verify the content properties and update the 
        /// isCorrect properties
        /// </summary>
        public void CheckContent()
        {
            isCorrect = !string.IsNullOrEmpty(Content);
        }

    
    }
}
