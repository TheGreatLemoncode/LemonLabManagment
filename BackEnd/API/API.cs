using BackEnd.Models;
using BackEnd.Security;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
namespace BackEnd.API
{
    public static class API
    {
        public static Account? ConnectedUser;
        private static DataController dataController; 
        public static void Initialisation()
        {
            dataController = new DataController();
        }

        public static bool UserCreation(string pName, string pMail, string pPassword)
        {
            if (!dataController.Salts.ContainsKey(pMail))
            {
                byte[] nSalt = Kitchen.CreateSalt();
                Account nUser = new Account(pName, Kitchen.HashPassword(pPassword, nSalt), null);
                nUser.Mail = pMail;
                dataController.Salts.Add(pMail, nSalt);
                dataController.Accounts.Add(pMail, nUser);
                ConnectedUser = nUser;
                return true;
            }
            return false;
        }

        public static bool Connexion(string pMail, string pPword)
        {
            if (dataController.Salts.ContainsKey(pMail))
            {
                byte[] nsalt = dataController.Salts[pMail];
                Account nUser = dataController.Accounts[pMail];
                if (Kitchen.CompareHashClear(pPword, nUser.GetHashPwd(), nsalt)){
                    MessageBox.Show($"Hello fucking world \n{nUser.Name}");
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
            return dataController.Organisations.ContainsKey(pCode);
        }

        public static Organisation GetOrganisation(string pCode)
        {
            return dataController.Organisations[pCode];
        }

        public static bool OrganisationExistByName(string pName)
        {
            foreach(Organisation p in dataController.Organisations.Values)
            {
                if (p.Name == pName)
                    return true;
            }
            return false;
        }

        public static bool NewOrganisation(Organisation pOrg)
        {
            if (OrganisationExist(pOrg.Code) || OrganisationExistByName(pOrg.Name))
                return false;
            pOrg.AddMember(ConnectedUser);
            dataController.Organisations.Add(pOrg.Code, pOrg);
            return true;
        }
        public static void SaveUserInformation()
        {
            dataController.Save();
        }
    }

}
