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

namespace FrontEnd.Controls
{
    /// <summary>
    /// Logique d'interaction pour Authentification.xaml
    /// </summary>
    public partial class Connexion : UserControl, IUserControlEvent
    {
        internal static event EventHandler CreationEvent;
        internal static event EventHandler ControlUsed;
        public Connexion()
        {
            InitializeComponent();
        }

        #region event input fields
        private void txt_mail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_mail.Text))
                txtblock_mail.Visibility = Visibility.Hidden;
            else
                txtblock_mail.Visibility = Visibility.Visible;
        }

        private void pwd_password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(pwd_password.Password))
                txtblock_pwd.Visibility = Visibility.Hidden;
            else
                txtblock_pwd.Visibility = Visibility.Visible;
        }
        #endregion

        #region evenement de boutton
        private void reset_btn_clk(object sender, RoutedEventArgs e)
        {
            txt_mail.Text = string.Empty;
            pwd_password.Password = string.Empty;
        }

        private void connexion_btn_clk(object sender, RoutedEventArgs arg)
        {
            string mail = txt_mail.Text;
            string pword = pwd_password.Password;
            if (API.Connexion(mail, pword))
            {
                ControlUsed?.Invoke(this, new EventArgs());
                ((MainWindow)Application.Current.MainWindow)._notifier.ShowSuccess($"Bienvenu {API.ConnectedUser.Name}");
            }
            else
                ((MainWindow)Application.Current.MainWindow)._notifier.ShowError("Connection refusée");
        }

        private void btn_creation_clk(object sender, RoutedEventArgs arg)
        {
            CreationEvent?.Invoke(this, EventArgs.Empty);
        }
        #endregion
    }
}
