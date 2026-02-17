using BackEnd.DATA;
using BackEnd.Security;
using System.Security.Cryptography.X509Certificates;
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
                User nUser = new User(pName, null);
                byte[] nSalt = Cuisine.CreateSalt();
                dataController.Salts.Add(pMail, nSalt);
                dataController.Users.Add(Cuisine.HashPassword(pPassword, nSalt), nUser);
                ConnectedUser = nUser;
                return true;
            }
            return false;
        }

        public static void SaveUserInformation()
        {
            dataController.Save();
        }
    }

}
