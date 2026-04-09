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
    /// Logique d'interaction pour ComputerCreation.xaml
    /// </summary>
    public partial class ComputerCreation : Window
    {
        private ComputerCreationViewModel viewModel;
        public ComputerCreation()
        {
            InitializeComponent();
            viewModel = new ComputerCreationViewModel();
            DataContext = viewModel;
        }

        private void submit_btn_clk(object sender, RoutedEventArgs e)
        {
            viewModel.SystemOS = ((TextBlock)cbox_sytemOS.SelectedItem).Text;
            DialogResult = true;
        }

        private void cancel_btn_clk(Object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        public Dictionary<string, string> GetProperties()
        {
            return viewModel.GetProperties();
        }
    }
}
