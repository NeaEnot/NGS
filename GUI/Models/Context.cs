using Newtonsoft.Json;
using Physics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Models
{
    internal class Context
    {
        private string path;

        internal Context(string path)
        {
            this.path = path;
            CreateDirs();
        }

        private void CreateDirs()
        {
            DirectoryInfo dirHistory = new DirectoryInfo($@"{path}\History");
            if (!dirHistory.Exists)
                dirHistory.Create();

            DirectoryInfo dirRenders = new DirectoryInfo($@"{path}\Renders");
            if (!dirRenders.Exists)
                dirRenders.Create();
        }

        internal void SaveUniverse(Universe universe)
        {
            using (StreamWriter writer = new StreamWriter($@"{path}\universe.ngsu"))
            {
                string json = JsonConvert.SerializeObject(universe);
                writer.Write(json);
            }
        }

        internal Universe LoadUniverse()
        {
            using (StreamReader reader = new StreamReader($@"{path}\universe.ngsu"))
            {
                string json = reader.ReadToEnd();
                Universe restored = JsonConvert.DeserializeObject<Universe>(json);
                return restored;
            }
        }
    }
}
