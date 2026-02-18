using BackEnd.DATA;
using BackEnd.Security;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
namespace BackEnd.API
{
    public static class API
    {
        private static User? ConnectedUser;
        private static DataController dataController; 
        public static void Initialisation()
        {
            dataController = new DataController();
        }

        public static bool UserCreation(string pName, string pMail, string pPassword)
        {
            if (!dataController.Salts.ContainsKey(pMail))
            {
                byte[] nSalt = Cuisine.CreateSalt();
                User nUser = new User(pName, Cuisine.HashPassword(pPassword, nSalt), null);
                dataController.Salts.Add(pMail, nSalt);
                dataController.Users.Add(pMail, nUser);
                ConnectedUser = nUser;
                return true;
            }
            return false;
        }

        public static void Connexion(string pMail, string pPword)
        {
            if (dataController.Salts.ContainsKey(pMail))
            {
                byte[] nsalt = dataController.Salts[pMail];
                User nUser = dataController.Users[pMail];
                if (Cuisine.CompareHashClear(pPword, nUser.GetHashPwd(), nsalt)){
                    MessageBox.Show($"Hello fucking world \n{nUser.Name}");
                }
            }
        }

        public static void SaveUserInformation()
        {
            dataController.Save();
        }
    }

}
