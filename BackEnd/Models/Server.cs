using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BackEnd.Models
{
    public class Server(string pName, List<string> pServices) : Computer(pName)
    {
        public List<string> Services { get; } = pServices;

        public void AddService(string pService)
        {
            if (!Services.Contains(pService))
            {
                Services.Add(pService);
            }
        }

        public override void DescriptionSetUp(string? ajout = null)
        {
            base.DescriptionSetUp(ajout);
            Description += "SERVICES : \n";
            foreach(string s in Services)
            {
                Description +=  ". " + s;
            }
        }
    }
}
