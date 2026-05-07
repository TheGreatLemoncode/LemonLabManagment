using BackEnd.DATA;
using BackEnd.Models;
using BackEnd.Security;
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
                Account nUser = new Account(pName, Kitchen.HashPassword(pPassword, nSalt));
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
                if (Kitchen.CompareHashClear(pPword, nUser.Password, nsalt)){
                    ConnectedUser = nUser;
                    return true;
                }
                else 
                    return false;
            }
            return false;
        }

        public static void Deconnection()
        {
            SaveUserInformation();
            ConnectedUser = null;
        }

        public static bool OrganisationExist(string pCode)
        {
            return DataController.Organisations.ContainsKey(pCode);
        }

        public static bool OrganisationExistByName(string pName)
        {
            foreach (Organisation p in DataController.Organisations.Values)
            {
                if (p.Name == pName)
                    return true;
            }
            return false;
        }

        public static Organisation GetOrganisation(string pCode)
        {
            return DataController.Organisations[pCode];
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
            if(ConnectedUser.Organisation == null)
            {
                return OrderMachineByStatusName(DataController.MachineDB.Values.ToList());
            }
            return OrderMachineByStatusName(GetMachinesByCodes(ConnectedUser.Organisation.Machines));
        }

        public static List<Machine> RequestMachineByStatus(Status pStatus)
        {
            List<Machine> toreturn = [];
            toreturn = DataController.MachineDB.Values.ToList().FindAll(x => x.Status == pStatus);
            return OrderMachineByStatusName(toreturn);
        }

        public static Machine? RequestMachineByName(string pName)
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

        public static Machine? RequestMachineByCode(string code)
        {
            return DataController.MachineDB.ContainsKey(code) ? DataController.MachineDB[code] : null;
        }

        public static List<string> RequestUserCreatedMachineCode()
        {
            List<string> codes = [];
            foreach (Machine m in DataController.MachineDB.Values)
            {
                if (m.Createur == ConnectedUser.Mail)
                    codes.Add(m.Code);
            }
            return codes;
        }

        public static void CreateMachine(byte TypeIndex, Dictionary<string, string> MachineInfo)
        {
            Machine nMachine = new();

            if (!(MachineInfo.Keys.Count > 0)) { return; }

            switch (TypeIndex)
            {
                case 1:
                    nMachine = new Computer(MachineInfo["MachineName"], MachineInfo["SystemOS"]);
                    nMachine.SetUp();
                    nMachine.IP = MachineInfo["MachineIpAddress"];
                    nMachine.DescriptionSetUp();
                    break;
                case 2:
                    nMachine = new Server(MachineInfo["MachineName"], MachineInfo["Services"].Split(',').ToList());
                    nMachine.SetUp();
                    nMachine.IP = MachineInfo["MachineIpAddress"];
                    nMachine.DescriptionSetUp();
                    break;
                case 3:
                    nMachine = new Router(MachineInfo["MachineName"], MachineInfo["Marque"]);
                    nMachine.SetUp();
                    nMachine.IP = MachineInfo["MachineIpAddress"];
                    nMachine.DescriptionSetUp();
                    break;
                default:
                    nMachine = new Machine();
                    DefaultMachineSetUp(nMachine, MachineInfo);
                    break;
            }
            nMachine.Createur = ConnectedUser.Mail;
            DataController.AddMachine(nMachine);
        }

        private static List<Machine> OrderMachineByStatusName(List<Machine> pList)
        {
            List<Machine> nList = pList.OrderBy(m => m.Status).ThenBy(m => m.Name).ToList();
            return nList;
        }

        

        public static void SaveUserInformation()
        {
            DataController.Save();
        }

        

        private static void DefaultMachineSetUp(Machine pMachine, Dictionary<string,string> MachineInfo)
        {
            pMachine.SetUp();
            pMachine.Name = MachineInfo["MachineName"];
            pMachine.IP = MachineInfo["MachineIpAddress"];
            pMachine.DescriptionSetUp(MachineInfo["MachineDescription"]);
        }

        private static List<Machine> GetMachinesByCodes(List<string> codes)
        {
            List<Machine> machines = [];
            foreach(string s in codes)
            {
                machines.Add(RequestMachineByCode(s));
            }
            return machines;
        }   
    }
}
