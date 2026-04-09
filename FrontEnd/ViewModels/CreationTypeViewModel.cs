using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace FrontEnd.ViewModels
{
    internal class CreationTypeViewModel:BaseViewModel
    {
        public byte Value;
        public ObservableCollection<object> TypeListe = [];
        public ICommand CreationButtonCommand { get; set; }
        public ICommand CancelButtonCommand { get; set; }

        //public CreationTypeViewModel()
        //{
        //    CreationButtonCommand = new RelayCommand(confirmation_btn_clk, () => { return !Value == null; });
        //}

        //private void confirmation_btn_clk()
        //{
        //    Value = Lst_options.SelectedIndex;
        //    DialogResult = true;
        //}
    }
}
