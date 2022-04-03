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

            FileInfo fileHistorySummary = new FileInfo($@"{path}\History\summary.ngshs");
            if (!fileHistorySummary.Exists)
            {
                HistorySummary historySummary = new HistorySummary { Date = "00000000.000.000.000" };
                SaveHistorySummary(historySummary);
            }

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

        internal void SaveHistorySummary(HistorySummary summary)
        {
            using (StreamWriter writer = new StreamWriter($@"{path}\History\summary.ngshs"))
            {
                string json = JsonConvert.SerializeObject(summary);
                writer.Write(json);
            }
        }

        internal HistorySummary LoadHistorySummary()
        {
            using (StreamReader reader = new StreamReader($@"{path}\History\summary.ngshs"))
            {
                string json = reader.ReadToEnd();
                HistorySummary restored = JsonConvert.DeserializeObject<HistorySummary>(json);
                return restored;
            }
        }

        internal void SaveUniverseState(UniverseState state, Date date)
        {
            string statePath = GetCurrentFileName(date.ToString());

            using (StreamWriter writer = new StreamWriter(statePath))
            {
                string json = JsonConvert.SerializeObject(state);
                writer.Write(json);
            }
        }

        internal UniverseState LoadUniverseState(HistorySummary summary)
        {
            using (StreamReader reader = new StreamReader(GetCurrentFileName(summary.Date)))
            {
                string json = reader.ReadToEnd();
                UniverseState restored = JsonConvert.DeserializeObject<UniverseState>(json);
                return restored;
            }
        }

        private string GetCurrentFileName(string date)
        {
            string[] sdate = date.Split('.');

            DirectoryInfo leodrDir = new DirectoryInfo($@"{path}\History\{sdate[0]}");
            if (!leodrDir.Exists)
                leodrDir.Create();

            DirectoryInfo milleniumDir = new DirectoryInfo(@$"{path}\History\{sdate[0]}\{sdate[1]}");
            if (!milleniumDir.Exists)
                milleniumDir.Create();

            DirectoryInfo yearDir = new DirectoryInfo(@$"{path}\History\{sdate[0]}\{sdate[1]}\{sdate[2]}");
            if (!yearDir.Exists)
                yearDir.Create();

            return $@"{path}\History\{sdate[0]}\{sdate[1]}\{sdate[2]}\{sdate[3]}.ngsus";
        }
    }
}