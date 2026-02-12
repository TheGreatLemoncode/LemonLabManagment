using BackEnd.DATA;
namespace BackEnd.API
{
    public static class API
    {
        private static DataController dataController; 
        public static void Initialisation()
        {
            dataController = new DataController();
        }
    }

}
