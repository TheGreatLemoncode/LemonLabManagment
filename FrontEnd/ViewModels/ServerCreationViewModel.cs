using FrontEnd.Dialogs;
using FrontEnd.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ToastNotifications.Messages;
using ToastNotifications.Messages.Core;

namespace FrontEnd.ViewModels
{
    /// <summary>
    /// Viewmodel used for the servercreationwindow. Inherits from the base machinecreationviewmodel
    /// </summary>
    public class ServerCreationViewModel: MachineCreationViewModel
    {
        public ObservableCollection<string> Services { get; } = [];

        /// <summary>
        /// Method that open a dialog window that ask the name of the service to add and 
        /// then add the service's name into the services observablecollection
        /// </summary>
        public void AddService()
        {
            IntakeBox box = new IntakeBox("Ajout d'un service", "Entrez le nom du service");
            if ((bool)box.ShowDialog())
            {
                string service = box.Result;
                if (!Services.Contains(service))
                    Services.Add(service);
                else
                    ((MainWindow)Application.Current.MainWindow)._notifier.ShowError("le service existe déjà");
            }
        }

        /// <summary>
        /// Remove the service's name from the observablecollection of services
        /// if it is already in it
        /// </summary>
        /// <param name="service">The name of the service to remove</param>
        public void RemoveService(string service)
        {
            Services.Remove(service);
        }

        /// <summary>
        /// Transform the informations about the machine currently being create into a 
        /// [string, string] dictionary.
        /// </summary>
        /// <returns>A string, string dictionary that contains all the
        /// properties of the machine being created</returns>
        public override Dictionary<string,string> GetProperties()
        {
            Dictionary<string, string> MachineInfo = [];
            MachineInfo.Add(nameof(MachineName), MachineName);
            MachineInfo.Add(nameof(MachineIpAddress), MachineIpAddress);
            MachineInfo.Add(nameof(Services), string.Join(",", Services));
            return MachineInfo;
        }
    }
}
