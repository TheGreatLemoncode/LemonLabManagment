using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Konscious.Security.Cryptography;
using System.IO;

namespace BackEnd.DATA
{
    internal class DataController
    {
        public Dictionary<string, User> Users = [];
        public Dictionary<string, byte[]> Salts = [];

        public DataController()
        {
            load();
        }
        private void load() 
        {
            using(StreamReader rd = new StreamReader("users.lemon"))
            {
                string json = rd.ReadToEnd();
                Users = JsonConvert.DeserializeObject<Dictionary<string, User>>(json);
            }

            using(StreamReader slt = new("salts.lemon"))
            {
                string text = slt.ReadToEnd();
                Salts = JsonConvert.DeserializeObject<Dictionary<string, byte[]>>(text);
            }
        }
    }
}
