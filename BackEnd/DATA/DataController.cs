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
        public Dictionary<byte[], User> Users = [];
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
                if(!string.IsNullOrEmpty(json))
                    Users = JsonConvert.DeserializeObject<Dictionary<byte[], User>>(json);
                else
                    Users = [];
            }

            using(StreamReader slt = new("salts.lemon"))
            {
                string text = slt.ReadToEnd();
                if (!string.IsNullOrEmpty(text))
                    Salts = JsonConvert.DeserializeObject<Dictionary<string, byte[]>>(text);
                else
                    Salts = [];
            }
        }

        public void Save()
        {
            using (StreamWriter wr = new StreamWriter("users.lemon"))
            {
                string json = JsonConvert.SerializeObject(Users);
                wr.Write(json);
            }

            using (StreamWriter wr2 = new("salts.lemon"))
            {
                string text = JsonConvert.SerializeObject(Salts);
                wr2.Write(text);
            }
        }
    }
}
