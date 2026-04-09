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
    /// Logique d'interaction pour MachineCreationType.xaml
    /// </summary>
    public partial class MachineCreationType : Window
    {
        public byte? Value { get; set; }
        public MachineCreationType()
        {
            InitializeComponent();
        }

        private void confirmation_btn_clk(object sender, RoutedEventArgs e)
        {
            if (Lst_options.SelectedItem != null)
            {
                Value = (byte)Lst_options.SelectedIndex;
                DialogResult = true;
            }
            else
                DialogResult = false;
        }

        private void cancel_btn_clk(object sender, RoutedEventArgs e)
        {
            Value = null;
            DialogResult = false;
        }
    }
}
