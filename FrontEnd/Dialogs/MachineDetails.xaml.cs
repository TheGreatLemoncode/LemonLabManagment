using BackEnd.Models;
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
    /// Logique d'interaction pour MachineDetails.xaml
    /// </summary>
    public partial class MachineDetails : Window
    {
        private MachineViewModel _machine;
        public MachineViewModel Machine
        {
            get { return _machine; }
        }
        public MachineDetails(MachineViewModel pMachine)
        {
            InitializeComponent();
            _machine = pMachine;
            DataContext = Machine;
            cbox_status.ItemsSource = Enum.GetValues(typeof(Status));
        }

        public void cancel_btn_clk(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
