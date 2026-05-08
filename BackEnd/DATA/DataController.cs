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
    /// <summary>
    /// Static class that handles the json data and the serialization
    /// </summary>
    internal static class DataController
    {
        private static readonly string AppName = "LemonLabManagment";
        public static Dictionary<string, Account> Accounts = [];
        private static readonly string AccountFile = "Accounts.lemon";
        public static Dictionary<string, byte[]> Salts = [];
        private static readonly string SaltsFile = "Salts.lemon";
        public static Dictionary<string, Organisation> Organisations = [];
        private static readonly string OrganisationsFile = "Organisations.lemon";
        public static Dictionary<string, Machine> MachineDB = [];
        private static readonly string MachinesFile = "Machines.lemon";
        private static JsonSerializerSettings SerializerSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };


        private static string GetPathForFile(string filename)
        {
            string pathdata = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            string folderpath = Path.Combine(pathdata, AppName);
            string filepath = Path.Combine(folderpath, filename);
            if(!Directory.Exists(folderpath))
                Directory.CreateDirectory(folderpath);
            return filepath;

        }

        /// <summary>
        /// Method that add a machine in the database if the machine is not currently in it
        /// </summary>
        /// <param name="pMachine">the machine to add</param>
        public static void AddMachine(Machine pMachine)
        {
            if(!MachineDB.ContainsKey(pMachine.Code))
                MachineDB.Add(pMachine.Code, pMachine);
        } 

        /// <summary>
        /// Method that adds a user to the database and stores he's salt 
        /// </summary>
        /// <param name="pAccount"></param>
        /// <param name="pSalt"></param>
        public static void AddAccount(Account pAccount, byte[] pSalt)
        {
            if (!Accounts.ContainsKey(pAccount.Mail))
            {
                Accounts.Add(pAccount.Mail, pAccount);
                Salts.Add(pAccount.Mail, pSalt);
            }
                
        }
        /// <summary>
        /// Load all the data from the json files in the user's appdata directory
        /// </summary>
        public static void load() 
        {
            if (File.Exists(GetPathForFile(AccountFile)))
            {
                using (StreamReader rd = new StreamReader(GetPathForFile(AccountFile)))
                {
                    string json = rd.ReadToEnd();
                    if (!string.IsNullOrEmpty(json))
                        Accounts = JsonConvert.DeserializeObject<Dictionary<string, Account>>(json, SerializerSettings);
                    else
                        Accounts = [];
                }
            }


            if (File.Exists(GetPathForFile(SaltsFile)))
            {
                using (StreamReader slt = new(GetPathForFile(SaltsFile)))
                {
                    string text = slt.ReadToEnd();
                    if (!string.IsNullOrEmpty(text))
                        Salts = JsonConvert.DeserializeObject<Dictionary<string, byte[]>>(text, SerializerSettings);
                    else
                        Salts = [];
                }
            }


            if (File.Exists(GetPathForFile(OrganisationsFile)))
            {
                using (StreamReader orgs = new(GetPathForFile(OrganisationsFile)))
                {
                    string text = orgs.ReadToEnd();
                    if (!string.IsNullOrEmpty(text))
                        Organisations = JsonConvert.DeserializeObject<Dictionary<string, Organisation>>(text, SerializerSettings);
                    else
                        Organisations = [];
                }
            }


            if (File.Exists(GetPathForFile(MachinesFile)))
            {
                using (StreamReader orgs = new(GetPathForFile(MachinesFile)))
                {
                    string text = orgs.ReadToEnd();
                    if (!string.IsNullOrEmpty(text))
                        MachineDB = JsonConvert.DeserializeObject<Dictionary<string, Machine>>(text, SerializerSettings);
                    else
                        MachineDB = [];
                }
            }

        }

        /// <summary>
        /// Save all the data in the user's appdata directory
        /// </summary>
        public static void Save()
        {
            using (StreamWriter wr = new StreamWriter(GetPathForFile(AccountFile)))
            {
                string json = JsonConvert.SerializeObject(Accounts, SerializerSettings);
                wr.Write(json);
            }

            using (StreamWriter wr2 = new(GetPathForFile(SaltsFile)))
            {
                string text = JsonConvert.SerializeObject(Salts, SerializerSettings);
                wr2.Write(text);
            }

            using (StreamWriter wr3 = new StreamWriter(GetPathForFile(OrganisationsFile)))
            {
                string text = JsonConvert.SerializeObject(Organisations, SerializerSettings);
                wr3.Write(text);
            }

            using (StreamWriter wr4 = new StreamWriter(GetPathForFile(MachinesFile)))
            {
                string text = JsonConvert.SerializeObject(MachineDB, SerializerSettings);
                wr4.Write(text);
            }
        }
    }
}
