using BackEnd.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using FrontEnd.Interfaces;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToastNotifications.Messages;
using FrontEnd.ViewModels;

namespace FrontEnd.Controls
{
    /// <summary>
    /// Logique d'interaction pour Authentification.xaml
    /// </summary>
    public partial class Connection : UserControl, IUserControlEvent
    {
        public static event EventHandler ControlUsed;
        public Connection()
        {
            InitializeComponent();
            StackField.DataContext = new MailViewModel();
        }

        // Vérifie le format du mail entré
        private void TextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            ((MailViewModel)txt_mail.DataContext).CheckContent();
        }

        private void Btn_Connection_clk(object sender, RoutedEventArgs e)
        {
            if (!((MailViewModel)StackField.DataContext).IsCorrect)
                ((MainWindow)Application.Current.MainWindow)._notifier.ShowError("Le courriel n'est pas valide");
            else
            {
                if( ((MailViewModel)txt_mail.DataContext).IsCorrect && API.Connection(pwd_user.Password, ((MailViewModel)txt_mail.DataContext).Content))
                {
                    ControlUsed?.Invoke(this, EventArgs.Empty);
                    ((MainWindow)Application.Current.MainWindow)._notifier.ShowSuccess($"Bienvenu {API.ConnectedUser?.Name}");
                }
                else { ((MainWindow)Application.Current.MainWindow)._notifier.ShowError("Connexion refusée !"); }
            }
        }

        private void Btn_Inscription_clk(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).LoadAccountCreation(this, EventArgs.Empty);
        }
    }

}
