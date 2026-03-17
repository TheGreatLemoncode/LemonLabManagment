using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FrontEnd.ViewModels;

namespace FrontEnd.Dialogs
{
    /// <summary>
    /// Logique d'interaction pour IntakeBox.xaml
    /// </summary>
    public partial class IntakeBox : Window
    {
        public string Message { get; set; }
        public string Result
        {
            get { return ((InputViewModel)txt_content.DataContext).Content; } 
        }

        public IntakeBox(string pTitle, string pMessage)
        {
            InitializeComponent();
            Title = pTitle;
            Message = pMessage;
            txt_content.DataContext = new IntakeViewModel();
            txtBlock_message.DataContext = this;

            this.Left = Application.Current.MainWindow.Left + 250;
            this.Top = Application.Current.MainWindow.Top + 150;
        }

        private void Btn_annuler_clk(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void Btn_confirmer_clk(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            DialogResult = false;
        }
    }
}
