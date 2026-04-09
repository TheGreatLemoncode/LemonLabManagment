using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BackEnd.Models
{
    public class Server(string pName, string pMarque, string pService) : Computer(pName, pMarque)
    {
        public List<string> Services { get; } = [pService];

        public void AddService(string pService)
        {
            if (!Services.Contains(pService))
            {
                Services.Add(pService);
            }
        }
    }
}
