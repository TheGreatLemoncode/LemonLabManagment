using BackEnd.API;
using FrontEnd.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Logique d'interaction pour Authentification.xaml
    /// </summary>
    public partial class CreationCompte : UserControl, Interfaces.IUserControlEvent
    {
        public static event EventHandler ControlUsed;
        public CreationCompte()
        {
            InitializeComponent();
            StackField.DataContext = new MailViewModel();
            txt_name.DataContext = new InputViewModel();
        }

        #region evenement input fields
        private void TextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            ((MailViewModel)txt_mail.DataContext).CheckContent();
        }
        private void Name_TextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            ((InputViewModel)txt_name.DataContext).CheckContent();
        }
        #endregion
        #region evenement de boutton
        private void connexion_btn_clk(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).LoadAccountConnexion();
        }

        private void creation_btn_clk(object sender, RoutedEventArgs arg)
        {
               
            if (!((MailViewModel)StackField.DataContext).isCorrect)
                ((MainWindow)Application.Current.MainWindow)._notifier.ShowError("Le format du courriel n'est pas valide");

            if (string.IsNullOrEmpty(txt_name.Text) || string.IsNullOrEmpty(pwd_user.Password))
            {
                ((MainWindow)Application.Current.MainWindow)._notifier.ShowError("Inscription refusée");
            }
            else
            {
                if (API.UserCreation(txt_name.Text, txt_mail.Text, pwd_user.Password))
                {
                    ControlUsed?.Invoke(this, new EventArgs());
                    ((MainWindow)Application.Current.MainWindow)._notifier.ShowSuccess($"Bienvenu {API.ConnectedUser.Name}");
                }
            }
        }
        #endregion
    }

    
}
