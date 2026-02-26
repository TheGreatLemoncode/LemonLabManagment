using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Konscious.Security.Cryptography;
using System.IO;

namespace BackEnd.Models
{
    internal class DataController
    {
        public Dictionary<string, Account> Accounts = [];
        public Dictionary<string, byte[]> Salts = [];
        public Dictionary<string, Organisation> Organisations = [];

        public DataController()
        {
            load();
        }
        private void load() 
        {
            if (File.Exists("DATA/Accounts.lemon"))
            {
                using (StreamReader rd = new StreamReader("DATA/Accounts.lemon"))
                {
                    string json = rd.ReadToEnd();
                    if (!string.IsNullOrEmpty(json))
                        Accounts = JsonConvert.DeserializeObject<Dictionary<string, Account>>(json);
                    else
                        Accounts = [];
                }
            }
            else
                Accounts = [];

            if (File.Exists("DATA/Salts.lemon"))
            {
                using (StreamReader slt = new("DATA/Salts.lemon"))
                {
                    string text = slt.ReadToEnd();
                    if (!string.IsNullOrEmpty(text))
                        Salts = JsonConvert.DeserializeObject<Dictionary<string, byte[]>>(text);
                    else
                        Salts = [];
                }
            }
            else
                Salts = [];

            if (File.Exists("DATA/Organisations.lemon"))
            {
                using (StreamReader orgs = new("DATA/Organisations.lemon"))
                {
                    string text = orgs.ReadToEnd();
                    if (!string.IsNullOrEmpty(text))
                        Organisations = JsonConvert.DeserializeObject<Dictionary<string, Organisation>>(text);
                    else
                        Organisations = [];
                }
            }
            
        }

        public void Save()
        {
            using (StreamWriter wr = new StreamWriter("DATA/Accounts.lemon"))
            {
                string json = JsonConvert.SerializeObject(Accounts);
                wr.Write(json);
            }

            using (StreamWriter wr2 = new("DATA/Salts.lemon"))
            {
                string text = JsonConvert.SerializeObject(Salts);
                wr2.Write(text);
            }

            using (StreamWriter wr3 = new StreamWriter("DATA/Organisations.lemon"))
            {
                string text = JsonConvert.SerializeObject(Organisations);
                wr3.Write(text);
            }
        }
    }
}
