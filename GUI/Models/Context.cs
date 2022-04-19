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
        public string Path { get; private set; }

        internal Context(string path)
        {
            this.Path = path;
            CreateDirs();
        }

        private void CreateDirs()
        {
            DirectoryInfo dirHistory = new DirectoryInfo($@"{Path}\History");
            if (!dirHistory.Exists)
                dirHistory.Create();

            FileInfo fileHistorySummary = new FileInfo($@"{Path}\History\summary.ngshs");
            if (!fileHistorySummary.Exists)
            {
                HistorySummary historySummary = new HistorySummary { Date = "00000000.000.000.000" };
                SaveHistorySummary(historySummary);
            }

            DirectoryInfo dirRenders = new DirectoryInfo($@"{Path}\Renders");
            if (!dirRenders.Exists)
                dirRenders.Create();
        }

        internal void SaveUniverse(Universe universe)
        {
            using (StreamWriter writer = new StreamWriter($@"{Path}\universe.ngsu"))
            {
                string json = JsonConvert.SerializeObject(universe);
                writer.Write(json);
            }
        }

        internal Universe LoadUniverse()
        {
            using (StreamReader reader = new StreamReader($@"{Path}\universe.ngsu"))
            {
                string json = reader.ReadToEnd();
                Universe restored = JsonConvert.DeserializeObject<Universe>(json);
                return restored;
            }
        }

        internal void SaveHistorySummary(HistorySummary summary)
        {
            using (StreamWriter writer = new StreamWriter($@"{Path}\History\summary.ngshs"))
            {
                string json = JsonConvert.SerializeObject(summary);
                writer.Write(json);
            }
        }

        internal HistorySummary LoadHistorySummary()
        {
            using (StreamReader reader = new StreamReader($@"{Path}\History\summary.ngshs"))
            {
                string json = reader.ReadToEnd();
                HistorySummary restored = JsonConvert.DeserializeObject<HistorySummary>(json);
                return restored;
            }
        }

        internal void SaveUniverseState(UniverseState state, Date date)
        {
            string statePath = GetCurrentFileName(date);

            using (StreamWriter writer = new StreamWriter(statePath))
            {
                string json = JsonConvert.SerializeObject(state);
                writer.Write(json);
            }
        }

        internal UniverseState LoadUniverseState(HistorySummary summary)
        {
            using (StreamReader reader = new StreamReader(GetCurrentFileName(Date.Parse(summary.Date))))
            {
                string json = reader.ReadToEnd();
                UniverseState restored = JsonConvert.DeserializeObject<UniverseState>(json);
                return restored;
            }
        }

        private string GetCurrentFileName(Date date)
        {
            DirectoryInfo leodrDir = new DirectoryInfo($@"{Path}\History\{date.Leodr}");
            if (!leodrDir.Exists)
                leodrDir.Create();

            DirectoryInfo milleniumDir = new DirectoryInfo(@$"{Path}\History\{date.Leodr}\{date.Millenium}");
            if (!milleniumDir.Exists)
                milleniumDir.Create();

            DirectoryInfo yearDir = new DirectoryInfo(@$"{Path}\History\{date.Leodr}\{date.Millenium}\{date.Year}");
            if (!yearDir.Exists)
                yearDir.Create();

            return $@"{Path}\History\{date.Leodr}\{date.Millenium}\{date.Year}\{date.Day}.ngsus";
        }
    }
}