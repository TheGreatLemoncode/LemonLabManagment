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
        public string ServcicePrincipal
        {
            get
            {
                if(ServcicePrincipal == null) { return Services[0]; }
                return ServcicePrincipal;
            }
            set
            {
                if (Services.Contains(value)) { ServcicePrincipal = value; }
                else { Services.Add(value); ServcicePrincipal = value; }
            }
        }

        public void AddService(string pService)
        {
            if (!Services.Contains(pService))
            {
                Services.Add(pService);
            }
        }
    }
}
