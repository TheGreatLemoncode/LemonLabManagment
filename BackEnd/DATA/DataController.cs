using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Konscious.Security.Cryptography;
using System.IO;
using BackEnd.Models;

namespace BackEnd.DATA
{
    internal static class DataController
    {
        public static Dictionary<string, Account> Accounts = [];
        public static Dictionary<string, byte[]> Salts = [];
        public static Dictionary<string, Organisation> Organisations = [];
        public static Dictionary<string, Machine> MachineDB = [];

        internal static void AddMachine(Machine pMachine)
        {
            if(!MachineDB.ContainsKey(pMachine.Code))
                MachineDB.Add(pMachine.Code, pMachine);
        } 

        internal static void AddAccount(Account pAccount, byte[] pSalt)
        {
            if (!Accounts.ContainsKey(pAccount.Mail))
            {
                Accounts.Add(pAccount.Mail, pAccount);
                Salts.Add(pAccount.Mail, pSalt);
            }
                
        }

        internal static void load() 
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


            if (File.Exists("DATA/Machines.lemon"))
            {
                using (StreamReader orgs = new("DATA/Machines.lemon"))
                {
                    string text = orgs.ReadToEnd();
                    if (!string.IsNullOrEmpty(text))
                        MachineDB = JsonConvert.DeserializeObject<Dictionary<string, Machine>>(text);
                    else
                        MachineDB = [];
                }
            }

        }

        internal static void Save()
        {
            DirectoryInfo di = Directory.CreateDirectory("DATA");
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

            using (StreamWriter wr4 = new StreamWriter("DATA/Machines.lemon"))
            {
                string text = JsonConvert.SerializeObject(MachineDB);
                wr4.Write(text);
            }
        }
    }
}
