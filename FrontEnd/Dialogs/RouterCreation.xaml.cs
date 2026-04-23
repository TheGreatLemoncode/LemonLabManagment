using FrontEnd.ViewModels;
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

namespace FrontEnd.Dialogs
{
    /// <summary>
    /// Logique d'interaction pour RouterCreation.xaml
    /// </summary>
    public partial class RouterCreation : Window
    {
        private RouterCreationViewModel viewmodel { get; set; } = new();
        public RouterCreation()
        {
            InitializeComponent();
            DataContext = viewmodel;
        }

        public Dictionary<string,string> GetProperties()
        {
            return viewmodel.GetProperties();
        }

        private void cancel_btn_clk(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void submit_btn_clk(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
