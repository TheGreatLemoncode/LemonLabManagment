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
using BackEnd.Models;

namespace FrontEnd.Controls
{
    /// <summary>
    /// Logique d'interaction pour DefaultMachineLIst.xaml
    /// </summary>
    public partial class DefaultMachineLIst : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private LayoutMode _layout = LayoutMode.Default;
        private int conter = 0;
        public ObservableCollection<MachineViewModel> Items { get; } = 
        [
            new MachineViewModel(new Machine { Description = "instanced by me", Locataire= "Joseph", Name= "Prada", Status = Status.Disponible, Marque = "Hp" }),
            new MachineViewModel(new Machine { Description = "instanced by me 2", Locataire= "pabl;o", Name= "beach", Status = Status.Indisponible, Marque = "Asus" }),
            new MachineViewModel(new Machine { Description = "instanced by me 3", Locataire= "rohane", Name= "homme", Status = Status.Utilisé, Marque = "Rogue" }),
            new MachineViewModel(new Machine { Description = "instanced by me 4", Locataire= "pamola", Name= "globe", Status = Status.Disponible, Marque = "Alien" })
        ];
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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(CurrentLayout.ToString());
            if (conter < 1)
            {
                CurrentLayout = LayoutMode.All;
                conter++;
            }
            else
            {
                CurrentLayout = LayoutMode.Default;
                conter--;
            }
            MessageBox.Show(CurrentLayout.ToString());
        }
    }
}
