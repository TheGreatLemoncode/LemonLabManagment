using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BackEnd.API;

namespace FrontEnd.Vues
{
    /// <summary>
    /// Logique d'interaction pour Authentification.xaml
    /// </summary>
    public partial class CreationCompte : UserControl
    {
        public static event EventHandler CreationCompteComplete;
        public CreationCompte()
        {
            InitializeComponent();
        }

        #region evenement input fields
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

        private void txt_displayname_changed(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_displayname.Text))
                txtblock_code.Visibility = Visibility.Hidden;
            else
                txtblock_code.Visibility = Visibility.Visible;
        }
        #endregion
        #region evenement de boutton
        private void reset_btn_clk(object sender, RoutedEventArgs e)
        {
            txt_mail.Text = string.Empty;
            pwd_password.Password = string.Empty;
            txt_displayname.Text = string.Empty;
        }

        private void creation_btn_clk(object sender, RoutedEventArgs arg)
        {
            string mail = txt_mail.Text;
            string name = txt_displayname.Text;
            string password = pwd_password.Password;

            API.UserCreation(name, mail, password);
        }

        //private bool InputFieldCheck()
        //{
        //    bool bMail = false;
        //    bool bPassword = false;
        //    bool bName = false;

        //    bMail = txt_mail.Text.Contains("")
        //    return true;
        //}
        #endregion
    }
}
