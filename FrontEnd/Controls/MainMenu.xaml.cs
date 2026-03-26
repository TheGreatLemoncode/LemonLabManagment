using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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

        private void add_machine_clk(object sender, RoutedEventArgs args)
        {
            Dialogs.MachineCreationType box = new();
            box.ShowDialog();
        }

        private void All_display_btn_clk(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(((DefaultMachineLIst)MainMenuDisplay.Content).CurrentLayout.ToString());
            if (((DefaultMachineLIst)MainMenuDisplay.Content).conter < 1)
            {
                ((DefaultMachineLIst)MainMenuDisplay.Content).CurrentLayout = LayoutMode.All;
                ((DefaultMachineLIst)MainMenuDisplay.Content).conter++;
            }
            else
            {
                ((DefaultMachineLIst)MainMenuDisplay.Content).CurrentLayout = LayoutMode.Default;
                ((DefaultMachineLIst)MainMenuDisplay.Content).conter--;
            }
        }
    }
}
