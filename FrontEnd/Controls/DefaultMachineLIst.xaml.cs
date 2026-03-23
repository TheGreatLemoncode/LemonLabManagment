using BackEnd.API;
using BackEnd.Models;
using FrontEnd.Dialogs;
using FrontEnd.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Logique d'interaction pour DefaultMachineLIst.xaml
    /// </summary>
    public partial class DefaultMachineLIst : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private LayoutMode _layout = LayoutMode.Default;

        public int conter = 0;

        public ObservableCollection<MachineViewModel> AllItems { get; } = [];
        public ObservableCollection<MachineViewModel> UsedItems { get; } = [];

        public LayoutMode CurrentLayout
        {
            get {  return _layout; }
            set
            {
                _layout = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentLayout)));
            }
        }

        public DefaultMachineLIst()
        {
            InitializeComponent();
            MachineControls.DataContext = this;

            foreach(Machine m in API.RequestAllMachines())
            {
                AllItems.Add(new MachineViewModel(m));
            }

            foreach(Machine n in API.RequestMachineByStatus(Status.Utilisé))
            {
                UsedItems.Add(new MachineViewModel(n));
            }
        }

        public void Search_btn_clk(object sender, RoutedEventArgs e)
        {
            MachineDetails DetailsWindow;
            if( string.IsNullOrEmpty(SearchBarContent.Text))
            {
                ((MainWindow)Application.Current.MainWindow)._notifier.ShowError("Object introuvable, réessayez");
                return;
            }

            if(API.RequestByName(SearchBarContent.Text) != null)
            {
                DetailsWindow = new MachineDetails(new(API.RequestByName(SearchBarContent.Text)));
                DetailsWindow.Show();
            }
            else if(API.ResquestByCode(SearchBarContent.Text) != null)
            {
                DetailsWindow = new MachineDetails(new(API.RequestByName(SearchBarContent.Text)));
                DetailsWindow.Show();
            }
            else
            {
                ((MainWindow)Application.Current.MainWindow)._notifier.ShowError("Object introuvable, réessayez");
            }

        }

        
    }
}
