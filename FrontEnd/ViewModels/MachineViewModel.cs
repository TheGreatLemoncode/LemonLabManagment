using BackEnd.API;
using BackEnd.Models;
using FrontEnd.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ToastNotifications.Messages;
using FrontEnd;

namespace FrontEnd.ViewModels
{
    public class MachineViewModel : BaseViewModel
    {
        public Machine machine { get; set; }
        public Array StatusValues { get; } = Enum.GetValues(typeof(Status));
        public ICommand DefaultButtonCommand { get; }
        public ICommand Command { get; }
        
        public MachineViewModel(Machine pMachine)
        {
            machine = pMachine;
            DefaultButtonCommand = new RelayCommand(DefaultButtonCommandEvent);
            Command = new RelayCommand(ShowDetails);
        }

        public string Name
        {
            get { return machine.Name; }
            set {  machine.Name = value; }
        }
        public string Description
        {
            get { return machine.Description; }
            set { machine.Description = value; }
        }

        public string Locataire
        {
            get { return machine.Locataire; }
            set { machine.Locataire = value; OnPropertyChanged(nameof(Locataire)); }
        }

        public Status Status
        {
            get { return machine.Status; }
            set { machine.Status = value; OnPropertyChanged(nameof(Status)); }
        }

        public string Code
        {
            get { return machine.Code; }
            set { machine.Code = value; }
        }

        public string TypeFormat
        {
            get { return machine.GetType().Name; }
        }


        private void DefaultButtonCommandEvent()
        {
            if(Status == Status.Disponible)
            {
                machine.Reservation();
                ((MainWindow)Application.Current.MainWindow)._notifier.ShowInformation("Machine réservée");
            }
            else
            {
                machine.Remise();
                ((MainWindow)Application.Current.MainWindow)._notifier.ShowInformation("Machine remise");
            }
        }

        private void ShowDetails()
        {
            MachineDetails detailbox = new(this);
            detailbox.Owner = Application.Current.MainWindow;
            detailbox.Show();
        }
    }
}
