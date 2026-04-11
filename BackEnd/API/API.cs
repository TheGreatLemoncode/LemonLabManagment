using BackEnd.DATA;
using BackEnd.Models;
using BackEnd.Security;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
namespace BackEnd.API
{
    public static class API
    {
        public static Account? ConnectedUser;
        public static void Initialisation()
        {
            DataController.load();
        }

        public static bool UserCreation(string pName, string pMail, string pPassword)
        {
            if (!DataController.Salts.ContainsKey(pMail))
            {
                byte[] nSalt = Kitchen.CreateSalt();
                Account nUser = new Account(pName, Kitchen.HashPassword(pPassword, nSalt), null);
                nUser.Mail = pMail;
                DataController.AddAccount(nUser, nSalt);
                ConnectedUser = nUser;
                return true;
            }
            return false;
        }

        public static bool Connection( string pPword, string pMail = "")
        {
            if (pMail != null && DataController.Salts.ContainsKey(pMail))
            {
                byte[] nsalt = DataController.Salts[pMail];
                Account nUser = DataController.Accounts[pMail];
                if (Kitchen.CompareHashClear(pPword, nUser.GetHashPwd(), nsalt)){
                    ConnectedUser = nUser;
                    return true;
                }
                else 
                    return false;
            }
            return false;
        }

        public static bool OrganisationExist(string pCode)
        {
            return DataController.Organisations.ContainsKey(pCode);
        }

        public static Organisation GetOrganisation(string pCode)
        {
            return DataController.Organisations[pCode];
        }

        public static bool OrganisationExistByName(string pName)
        {
            foreach(Organisation p in DataController.Organisations.Values)
            {
                if (p.Name == pName)
                    return true;
            }
            return false;
        }

        public static void Deconnection()
        {
            SaveUserInformation();
            ConnectedUser = null;
        }

        public static bool NewOrganisation(Organisation pOrg)
        {
            if (OrganisationExist(pOrg.Code) || OrganisationExistByName(pOrg.Name))
                return false;
            pOrg.AddMember(ConnectedUser);
            DataController.Organisations.Add(pOrg.Code, pOrg);
            return true;
        }

        public static List<Machine> RequestAllMachines()
        {
            return OrderListByStatusName(DataController.MachineDB.Values.ToList());
        }

        public static List<Machine> RequestMachineByStatus(Status pStatus)
        {
            List<Machine> toreturn = [];
            //foreach(Machine m in DataController.MachineDB.Values)
            //{
            //    if(m.Status == pStatus && m.Locataire == ConnectedUser?.Name)
            //    {
            //        toreturn.Add(m);
            //    }
            //}
            toreturn = DataController.MachineDB.Values.ToList().FindAll(x => x.Status == pStatus);
            return OrderListByStatusName(toreturn);
        }

        public static Machine? RequestByName(string pName)
        {
            foreach (Machine m in DataController.MachineDB.Values)
            {
                if (m.Name == pName)
                {
                    return m;
                }
            }
            return null;
        }

        public static Machine? RequestByCode(string code)
        {
            //if (DataController.MachineDB.ContainsKey(code))
            //{
            //    //MessageBox.Show("Hello world");
            //}
            return DataController.MachineDB.ContainsKey(code) ? DataController.MachineDB[code] : null;
        }

        private static List<Machine> OrderListByStatusName(List<Machine> pList)
        {
            List<Machine> nList = pList.OrderBy(m => m.Status).ThenBy(m => m.Name).ToList();
            return nList;
        }

        public static void CreateMachine(byte TypeIndex, Dictionary<string, string> MachineInfo)
        {
            Machine nMachine = new();

            if(!(MachineInfo.Keys.Count > 0)) { return; }

            switch (TypeIndex)
            { 
                case 1:
                    nMachine = new Computer(MachineInfo["MachineName"], MachineInfo["SystemOS"]);
                    nMachine.SetUp();
                    nMachine.IP = MachineInfo["MachineIpAddress"];
                    break;
                case 2:
                    nMachine = new Server(MachineInfo["MachineName"], MachineInfo["Services"].Split(',').ToList());
                    nMachine.SetUp();
                    nMachine.IP = MachineInfo["MachineIpAddress"];
                    string description = $"Server créé le {DateTime.Now}\n";
                    description += $"Adresse IP : {nMachine.IP}\n";
                    description += "Services : \n";
                    foreach(string s in ((Server)nMachine).Services)
                    {
                        description += $". {s}\n";
                    }
                    nMachine.Description = description;
                    break;
                default:
                    nMachine = new Machine();
                    nMachine.SetUp();
                    nMachine.Name = MachineInfo["MachineName"];
                    nMachine.IP = MachineInfo["MachineIpAddress"];
                    nMachine.Description = MachineInfo["MachineDescription"];
                    break;
            }
            DataController.AddMachine(nMachine);
        }

        public static void SaveUserInformation()
        {
            DataController.Save();
        }
    }

}
