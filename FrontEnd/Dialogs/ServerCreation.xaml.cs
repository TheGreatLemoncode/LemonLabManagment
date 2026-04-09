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
    /// Logique d'interaction pour ServerCreation.xaml
    /// </summary>
    public partial class ServerCreation : Window
    {
        public Dictionary<string, string> MachineInfoContainer { get; set; } = [];
        private ServerCreationViewModel viewModel;
        public ServerCreation()
        {
            InitializeComponent();
            viewModel = new();
            DataContext = viewModel;
        }

        public Dictionary<string,string> GetProperties()
        {
            return viewModel.GetProperties();
        }

        private void submit_btn_clk(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Service_button_clk(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            viewModel.RemoveService((string)btn.DataContext);
        }

        private void Add_Service_clk(object sender, RoutedEventArgs e)
        {
            viewModel.AddService();
        }

        private void cancel_btn_clk(Object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
