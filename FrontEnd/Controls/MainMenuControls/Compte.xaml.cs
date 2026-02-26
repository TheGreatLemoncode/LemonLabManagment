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

namespace FrontEnd.Controls.MainMenuControls
{
    /// <summary>
    /// Logique d'interaction pour Compte.xaml
    /// </summary>
    public partial class Compte : UserControl
    {
        public Compte()
        {
            InitializeComponent();
            DataContext = API.ConnectedUser;
        }

        public void join_btn_clk(object sender, RoutedEventArgs args)
        {
            string code = Interaction.InputBox("Entrer le code de l'organisation", Title: "Rejoindre une organisation");
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
            else
                MessageBox.Show("Ce code n'existe pas", "Rejoindre une organisation");
        }

        public void create_btn_clk(object sender, RoutedEventArgs args)
        {
            string pName = Interaction.InputBox("Entrer le nom de l'organisation", Title: "Créer une organisation");
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
}
