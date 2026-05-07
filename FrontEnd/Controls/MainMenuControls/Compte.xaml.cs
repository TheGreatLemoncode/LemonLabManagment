using BackEnd.API;
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
using Microsoft.VisualBasic;
using BackEnd.Models;
using ToastNotifications.Messages;
using Wpf.Ui;
using FrontEnd.Dialogs;
using FrontEnd.ViewModels;
using FrontEnd.Interfaces;

namespace FrontEnd.Controls.MainMenuControls
{
    /// <summary>
    /// Logique d'interaction pour Compte.xaml
    /// </summary>
    public partial class Compte : UserControl, IUserControlEvent
    {
        public AccountViewModel viewmodel { get; set; } = new AccountViewModel();
        public static event EventHandler ControlUsed;
        public Compte()
        {
            InitializeComponent();
            DataContext = viewmodel;
            Lst_buttons.ItemsSource = viewmodel.AccountButtons;
            viewmodel.AccountButtons[0].Click += join_btn_clk;
            viewmodel.AccountButtons[0].Style = this.FindResource("LLM.Button.Primary") as Style;
            viewmodel.AccountButtons[1].Click += create_btn_clk;
            viewmodel.AccountButtons[1].Style = this.FindResource("LLM.Button.Primary") as Style;
            viewmodel.AccountButtons[2].Click += disconnect_btn_clk;
            viewmodel.AccountButtons[2].Style = this.FindResource("LLM.Button.Primary") as Style;
        }

        public void join_btn_clk(object sender, RoutedEventArgs args)
        {
            string code = string.Empty;
            IntakeBox Box = new("Rejoindre une organisation", "Entrez le code de l'organisation");
            Box.Owner = Application.Current.MainWindow;
            if ((bool)Box.ShowDialog())
            {
                code = Box.Result;
            }

            if (!string.IsNullOrEmpty(code))
            {
                if (API.OrganisationExist(code))
                {
                    Organisation org = API.GetOrganisation(code);
                    MessageBoxResult result = MessageBox.Show($"Rejoindre {org.Name} ?", "Rejoindre une organisation", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        org.AddMember(API.ConnectedUser);
                        ((MainWindow)Application.Current.MainWindow)._notifier.ShowSuccess($"Bienvenue dans le groupe {org.Name}");
                        ((MainWindow)Application.Current.MainWindow).LoadMainMenu(this, new EventArgs());
                    }

                }
                else { ((MainWindow)Application.Current.MainWindow)._notifier.ShowError("Ce code n'existe pas \nVeuillez recommencer"); }
            }  
        }

        public void create_btn_clk(object sender, RoutedEventArgs args)
        {

            IntakeBox Box = new("création d'une organisation", "Entrez le nom de l'organisation");
            Box.Owner = Application.Current.MainWindow;
            string pName = string.Empty;
            if ((bool)Box.ShowDialog())
            {
                pName = Box.Result;
            }

            if (!string.IsNullOrEmpty(pName))
            {
                Organisation org = new(pName);
                org.CreateCode();
                org.Owner = API.ConnectedUser.Mail;
                if (API.NewOrganisation(org))
                {
                    ((MainWindow)Application.Current.MainWindow)._notifier.ShowSuccess("Organisation ajouté");
                    ((MainWindow)Application.Current.MainWindow).LoadMainMenu(this, new EventArgs());
                }
                else
                    ((MainWindow)Application.Current.MainWindow)._notifier.ShowError("Organisation déjà existante");
            }
        }

        private void copy_btn_clk(object sender,  RoutedEventArgs args)
        {
            Clipboard.SetText(API.ConnectedUser.Organisation.Code);
            ((MainWindow)Application.Current.MainWindow)._notifier.ShowInformation("Code copié");
        }

        private void disconnect_btn_clk(object sender, RoutedEventArgs e)
        {
            API.Deconnection();
            ((MainWindow)Application.Current.MainWindow).LoadAccountConnexion();
        }
    }
}
