using ApiSandbox.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSandbox.DataAccess
{
    public class SquaresDataAccess
    {
        public SquareName[] sNames;
        public SquaresDataAccess()
        {
            string names = string.Empty;
            if (File.Exists(@".\Data\names.json"))
            {
                names = File.ReadAllText(@".\Data\names.json");
            }
            else
            {
                names = File.ReadAllText(@".\Data\names.original.json");
                string showsMaster = File.ReadAllText(@".\Data\names.original.json");
                using (var sw = new StreamWriter(@".\Data\names.json", false))
                {
                    sw.Write(showsMaster);
                }
            }
            sNames = JsonConvert.DeserializeObject<SquareName[]>(names);
        }

        public void SaveSquareNames(SquareName[] newNames)
        {
            string newNamesString = JsonConvert.SerializeObject(newNames);
            File.WriteAllText(@".\Data\names.json", newNamesString);
        }

    }
}
