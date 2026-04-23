using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Wpf.Ui.Controls;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace FrontEnd.ViewModels
{
    public class AccountViewModel
    {
        public ObservableCollection<Button> AccountButtons { get; set; } = new ObservableCollection<Button>();

        public AccountViewModel()
        {
            AccountButtons.Add(new Button
            {
                Content = "REJOINDRE ORGANISATION",
                Margin = new Thickness(0, 10, 0, 0),
                Height = 56,
                Width = 210
            });

            AccountButtons.Add(new Button
            {
                Content = "CRÉER ORGANISATION",
                Margin = new Thickness(0, 10, 0, 0),
                Height = 56,
                Width = 210
            });

            AccountButtons.Add(new Button
            {
                Content = "DÉCONNEXION",
                Margin = new Thickness(0, 10, 0, 0),
                Height = 56,
                Width = 210
            });
        }
    }
}
