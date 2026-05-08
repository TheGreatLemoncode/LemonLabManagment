using BackEnd.DATA;
using BackEnd.Models;
using BackEnd.Security;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
namespace BackEnd.API
{
    /// <summary>
    /// Static collection of method to deal with the datacontroller and the
    /// models
    /// </summary>
    public static class API
    {
        public static Account? ConnectedUser;
 
        /// <summary>
        /// Initialize the datas
        /// </summary>
        public static void Initialisation()
        {
            DataController.load();
        }

        /// <summary>
        /// Create a user with the given information. returns true if the process is successfull
        /// </summary>
        /// <param name="pName">The user's name</param>
        /// <param name="pMail">the user's mail</param>
        /// <param name="pPassword">the user's clear password</param>
        /// <returns> returns true if the process is successfull. Otherwise, return false</returns>
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


        /// <summary>
        /// Look for a user with the given informations and set him as the connected user.
        /// return true if the process is successfull
        /// </summary>
        /// <param name="pPword">the user's clear password</param>
        /// <param name="pMail">the user's mail</param>
        /// <returns>return true if the process is successfull. otherwise, return false</returns>
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

        /// <summary>
        /// Method that set the connected to null after saving it's informations 
        /// with the datacontroller
        /// </summary>
        public static void Deconnection()
        {
            SaveUserInformation();
            ConnectedUser = null;
        }

        /// <summary>
        /// Method that verify that an organisation with the given code does 
        /// exist
        /// </summary>
        /// <param name="pCode">the organisation's code</param>
        /// <returns>Return true if the organisation exist. otherwise returns false</returns>
        public static bool OrganisationExist(string pCode)
        {
            return DataController.Organisations.ContainsKey(pCode);
        }

        /// <summary>
        /// Method that verify that an organisation with the given name does 
        /// exist
        /// </summary>
        /// <param name="pCode">the organisation's name</param>
        /// <returns>Return true if the organisation the exist. otherwise returns false</returns>
        public static bool OrganisationExistByName(string pName)
        {
            foreach (Organisation p in DataController.Organisations.Values)
            {
                if (p.Name == pName)
                    return true;
            }
            return false;
        }


        /// <summary>
        /// Method that return the organisation with the given code
        /// from the database
        /// </summary>
        /// <param name="pCode">the organisation's code</param>
        /// <returns>the organiation with the code</returns>
        public static Organisation GetOrganisation(string pCode)
        {
            return DataController.Organisations[pCode];
        }

        /// <summary>
        /// Add a new organisation to the database if the given organisation
        /// does not exists. return true if the process is successfull
        /// </summary>
        /// <param name="pOrg">the organisation to add in the database</param>
        /// <returns>true if the organisation was added. otherwise false</returns>
        public static bool NewOrganisation(Organisation pOrg)
        {
            if (OrganisationExist(pOrg.Code) || OrganisationExistByName(pOrg.Name))
                return false;
            pOrg.AddMember(ConnectedUser);
            DataController.Organisations.Add(pOrg.Code, pOrg);
            return true;
        }

        /// <summary>
        /// Method that returns all the machines created by the connected user if he does not 
        /// have an organisation. otherwise, return all the machines in he's organisation
        /// </summary>
        /// <returns>a list of all the machines accessible to the connected user ordered by status then name</returns>
        public static List<Machine> RequestAllMachines()
        {
            List<Machine> machines = [];
            if(ConnectedUser.Organisation != null)
            {
                foreach(Machine m in DataController.MachineDB.Values)
                {
                    if(ConnectedUser.Organisation.Machines.Contains(m.Code))
                        machines.Add(m);
                }
            }
            else
            {
                foreach (Machine m in DataController.MachineDB.Values)
                {
                    if (m.Createur == ConnectedUser.Mail)
                        machines.Add(m);
                }
            }
            return OrderMachineByStatusName(machines);
        }

        /// <summary>
        /// Method that return all the machines accessible to the user with a given status.
        /// </summary>
        /// <param name="pStatus">the status used for the search</param>
        /// <returns>A list of all the machien accessible to the user with the status</returns>
        public static List<Machine> RequestMachinesByStatus(Status pStatus)
        {
            List<Machine> toreturn = [];
            toreturn = RequestAllMachines().FindAll(x => x.Status == pStatus);
            return OrderMachineByStatusName(toreturn);
        }

        /// <summary>
        /// Returns a machine accessible to the user with the given name if it exists.
        /// if not, returns a null object
        /// </summary>
        /// <param name="pName">name of the machine to seach</param>
        /// <returns>a machine </returns>
        public static Machine? RequestMachineByName(string pName)
        {
            return RequestAllMachines().FirstOrDefault(x => x.Name == pName);
        }

        /// <summary>
        /// Returns a machine with the given code if it exists. otherwise returns a null object
        /// </summary>
        /// <param name="code">the code of the machine to search</param>
        /// <returns>a machine with the given code or a null object</returns>
        public static Machine? RequestMachineByCode(string code)
        {
            return DataController.MachineDB.ContainsKey(code) ? DataController.MachineDB[code] : null;
        }

        /// <summary>
        /// Return a list of codes of all the machines created by the user
        /// </summary>
        /// <returns>a list of codes of all the machines created by the user</returns>
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

        /// <summary>
        /// Method that create a machine object with the given information and then add it to the database
        /// </summary>
        /// <param name="TypeIndex">an index that represent the type of the machine object</param>
        /// <param name="MachineInfo">the informations of the machine being created</param>
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

            ConnectedUser.Organisation?.Machines.Add(nMachine.Code);
            nMachine.Createur = ConnectedUser.Mail;
            DataController.AddMachine(nMachine);
        }

        /// <summary>
        /// Save the user's informations with the datacontroller
        /// </summary>
        public static void SaveUserInformation()
        {
            DataController.Save();
        }

        private static List<Machine> OrderMachineByStatusName(List<Machine> pList)
        {
            List<Machine> nList = pList.OrderBy(m => m.Status).ThenBy(m => m.Name).ToList();
            return nList;
        }


        private static void DefaultMachineSetUp(Machine pMachine, Dictionary<string,string> MachineInfo)
        {
            pMachine.SetUp();
            pMachine.Name = MachineInfo["MachineName"];
            pMachine.IP = MachineInfo["MachineIpAddress"];
            pMachine.DescriptionSetUp(MachineInfo["MachineDescription"]);
        } 
    }
}
