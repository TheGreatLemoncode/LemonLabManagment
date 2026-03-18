using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FrontEnd.ViewModels
{
    internal class MachineViewModel : BaseViewModel
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

        public Status Status
        {
            get { return machine.Status; }
            set { machine.Status = value; }
        }

        private void ActionToTake(string message)
        {
            MessageBox.Show($"Message de {Name}\n message");
        }
    }
}
