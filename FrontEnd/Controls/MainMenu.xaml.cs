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
using ToastNotifications.Messages;

namespace FrontEnd.Controls
{
    /// <summary>
    /// Logique d'interaction pour MainMenu.xaml
    /// </summary>
    public partial class MainMenu : UserControl
    {
        private bool WasCreated;
        public MainMenu()
        {
            InitializeComponent();
            All_display_btn_clk(this, new RoutedEventArgs());
        }

        private void account_btn_clk(object sender, RoutedEventArgs args)
        {
            MainMenuDisplay.Content = new MainMenuControls.Compte();
        }

        /// <summary>
        /// Method that change the this window display property to the 
        /// the DefaultMachineList control
        /// </summary>
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
            ((MainWindow)Application.Current.MainWindow).PlayWindowSound();
            if ((bool)box.DialogResult)
            {
                switch (box.Value)
                {
                    case 1:
                        API.CreateMachine((byte)box.Value, ComputerMachineCreation());
                        break;
                    case 2:
                        API.CreateMachine((byte)box.Value, ServerMachineCreation());
                        break;
                    case 3:
                        API.CreateMachine((byte)box.Value, RouterMachineCreation());
                        break;
                    default:
                        API.CreateMachine((byte)box.Value, DefaultMachineCreation());
                        break;
                    
                }
                All_display_btn_clk(this, new RoutedEventArgs());
                if (WasCreated)
                {
                    ((MainWindow)Application.Current.MainWindow)._notifier.ShowSuccess("Machine créé avec succès");
                    WasCreated = false;
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
        private void canvas_mode_clk(object sender, RoutedEventArgs e)
        {
            CanvasWindow window = new(API.RequestMachinesByStatus(BackEnd.Models.Status.Utilisé));
            window.Owner = Application.Current.MainWindow;
            window.Show();
            ((MainWindow)Application.Current.MainWindow).PlayWindowSound();
        }

        private Dictionary<string,string> DefaultMachineCreation()
        {
            MachineCreation InfoDialog = new();
            ((MainWindow)Application.Current.MainWindow).PlayWindowSound();
            if ((bool)InfoDialog.ShowDialog())
            {
                WasCreated = true;
                return InfoDialog.GetProperties();
            }
            else
                return new Dictionary<string, string>{ };
        } 

        private Dictionary<string,string> ServerMachineCreation()
        {
            ServerCreation InfoDialog = new();
            ((MainWindow)Application.Current.MainWindow).PlayWindowSound();
            if ((bool)InfoDialog.ShowDialog())
            {
                WasCreated = true;
                return InfoDialog.GetProperties();
            }
            else
                return new Dictionary<string, string>{ };
        }

        private Dictionary<string, string> ComputerMachineCreation()
        {
            ComputerCreation InfoDialog = new();
            ((MainWindow)Application.Current.MainWindow).PlayWindowSound();
            if ((bool)InfoDialog.ShowDialog())
            {
                WasCreated = true;
                return InfoDialog.GetProperties();
            }
            else
                return new Dictionary<string, string>{ };
        }

        private Dictionary<string, string> RouterMachineCreation()
        {
            RouterCreation InfoDialog = new();
            ((MainWindow)Application.Current.MainWindow).PlayWindowSound();
            if ((bool)InfoDialog.ShowDialog())
            {
                WasCreated = true;
                return InfoDialog.GetProperties();
            }            
            else
                return new Dictionary<string, string>{ };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).PlayInformationVideo();
        }
    }
}
