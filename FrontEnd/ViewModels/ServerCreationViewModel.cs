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
    public class ServerCreationViewModel: MachineCreationViewModel
    {
        public ObservableCollection<string> Services { get; } = [];
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
                    //MessageBox.Show("le service existe deja");
            }
        }

        public void RemoveService(string service)
        {
            Services.Remove(service);
        }

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
