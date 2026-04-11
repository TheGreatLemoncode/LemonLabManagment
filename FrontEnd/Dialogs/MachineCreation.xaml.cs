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
    /// Logique d'interaction pour MachineCreation.xaml
    /// </summary>
    public partial class MachineCreation : Window
    {
        private MachineCreationViewModel viewModel;
        public MachineCreation()
        {
            InitializeComponent();
            viewModel = new();
            this.DataContext = viewModel;
        }

        private void Commit_btn_clk(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            //MessageBox.Show(viewModel.CanCommit.ToString());
        }

        public Dictionary<string,string> GetProperties()
        {
            return viewModel.GetProperties();
        }

        private void Btn_cancel_clk(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}
