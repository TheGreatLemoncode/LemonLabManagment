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
                Account nUser = new Account(pName, Kitchen.HashPassword(pPassword, nSalt), null);
                nUser.Mail = pMail;
                DataController.Salts.Add(pMail, nSalt);
                DataController.Accounts.Add(pMail, nUser);
                ConnectedUser = nUser;
                return true;
            }
            return false;
        }

        public static bool Connection( string pPword, string pMail = "")
        {
            if (DataController.Salts.ContainsKey(pMail))
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
        public static void SaveUserInformation()
        {
            DataController.Save();
        }
    }

}
