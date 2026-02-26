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
using System.ComponentModel;
using System.Security.Authentication;
using ToastNotifications.Messages;

namespace FrontEnd.Controls
{
    /// <summary>
    /// Logique d'interaction pour Authentification.xaml
    /// </summary>
    public partial class CreationCompte : UserControl, Interfaces.IUserControlEvent
    {
        public static event EventHandler ControlUsed;
        public TextBox MailBox { get; set; } = new();
        public TextBox NameBox { get; set; } = new();
        public CreationCompte()
        {
            InitializeComponent();
            DataContext = this;
        }

        #region evenement input fields
        private void txt_mail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(MailBox.Content))
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
        private void connexion_btn_clk(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).LoadAccountConnexion();
        }

        private void creation_btn_clk(object sender, RoutedEventArgs arg)
        {
            string password = pwd_password.Password;

            if (MailFieldCheck(MailBox.Content))
                MailBox.IsCorrect = true;
            else
            {
                MailBox.IsCorrect = false;
                ((MainWindow)Application.Current.MainWindow)._notifier.ShowError("Le mail n'est pas valide");
            }

            if (string.IsNullOrEmpty(NameBox.Content) || NameBox.Content.Length < 3)
            {
                NameBox.IsCorrect = false;
                ((MainWindow)Application.Current.MainWindow)._notifier.ShowError("Le nom n'est pas valide");
            }

            if(MailBox.IsCorrect && NameBox.IsCorrect)
            {
                if(API.UserCreation(NameBox.Content, MailBox.Content, password))
                {
                    ((MainWindow)Application.Current.MainWindow)._notifier.ShowSuccess($"Bienvenu {NameBox.Content}");
                    ControlUsed?.Invoke(this, new EventArgs());
                }
                else
                    ((MainWindow)Application.Current.MainWindow)._notifier.ShowError($"Le compte {MailBox.Content} existe déjà");
            }
                

            
        }

        private bool MailFieldCheck(string pText)
        {
            if (!pText.Contains('@'))
                return false;

            string[] MailFormat = pText.Split('@');
            return (!string.IsNullOrEmpty(MailFormat[0]) && MailFormat[1].Contains('.') && !string.IsNullOrEmpty(MailFormat[1].Split('.')[1]));
        }
        #endregion
    }

    public class TextBox : INotifyPropertyChanged
    {
        private string? Text;
        private bool Valid = true;
        public string Content
        {
            get
            {
                if (string.IsNullOrEmpty(Text))
                    return string.Empty;
                return Text;
            }
            set
            {
                Text = value;
                NotifyPropertyChanged(nameof(Content));
            }
        }
        public bool IsCorrect
        {
            get { return Valid; }
            set { Valid = value; NotifyPropertyChanged(nameof(IsCorrect)); } 
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string PropertyName)
        {
            if(PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}
