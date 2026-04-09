using BackEnd.API;
using FrontEnd.Dialogs;
using FrontEnd.Interfaces;
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
            MachineCreationType box = new();
            box.ShowDialog();
            if ((bool)box.DialogResult)
            {
                switch (box.Value)
                {
                    case 0:
                        //nMachine = new Machine();
                        API.CreateMachine((byte)box.Value, DefaultMachineCreation());
                        break;
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                }
            }
        }

        private void All_display_btn_clk(object sender, RoutedEventArgs e)
        {
            Load_default_machine_list();

            if (((DefaultMachineLIst)MainMenuDisplay.Content).conter < 1)
            {
                ((DefaultMachineLIst)MainMenuDisplay.Content).CurrentLayout = LayoutMode.All;
                ((DefaultMachineLIst)MainMenuDisplay.Content).conter++;
            }
            
        }

        private static Dictionary<string,string> DefaultMachineCreation()
        {
            MachineCreation InfoDialog = new();
            if((bool)InfoDialog.ShowDialog())
            {
                return InfoDialog.viewModel.GetProperties();
            }
            else
                return new Dictionary<string, string>{ };
        } 
    }
}
