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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FrontEnd.Controls
{
    /// <summary>
    /// Logique d'interaction pour MainMenu.xaml
    /// </summary>
    public partial class MainMenu : UserControl
    {
        public MainMenu()
        {
            InitializeComponent();
            Load_default_machine_list();
        }

        public void account_btn_clk(object sender, RoutedEventArgs args)
        {
            MainMenuDisplay.Content = new MainMenuControls.Compte();
        }

        public void Load_default_machine_list()
        {
            MainMenuDisplay.Content = new DefaultMachineLIst();
        }

        private void acceuil_btn_clk(object sender, RoutedEventArgs e)
        {
            Load_default_machine_list();
        }
    }
}
