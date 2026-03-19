using BackEnd.Models;
using FrontEnd.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FrontEnd.ViewModels
{
    public class MachineViewModel : BaseViewModel
    {
        public Machine machine { get; set; } = new Machine();
        public ICommand Command {get;}
        
        public MachineViewModel(Machine pMachine)
        {
            machine = pMachine;
            Command = new RelayCommand<string>(ActionToTake);
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
            set { machine.Locataire = value; }
        }

        public string Marque
        {
            get { return machine.Marque; }
            set { machine.Marque = value; }
        }

        public Status Status
        {
            get { return machine.Status; }
            set { machine.Status = value; }
        }

        public string Code
        {
            get { return machine.Code; }
            set { machine.Code = value; }
        }

        public string NameFormat
        {
            get { return Name + "\n" + Marque; }
        }

        private void ActionToTake(string message)
        {
            MachineDetails details = new MachineDetails(this);
            details.Show();
        }
    }
}
