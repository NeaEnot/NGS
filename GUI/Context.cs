using Newtonsoft.Json;
using Physics;
using System.Collections.Generic;
using System.IO;

namespace GUI
{
    internal static class Context
    {
        internal static List<Universe> Universes { get; private set; }

        internal static void Save()
        {
            using (StreamWriter writer = new StreamWriter($"storage.json"))
            {
                string json = JsonConvert.SerializeObject(Universes);
                writer.Write(json);
            }
        }

        internal static void Load()
        {
            try
            {
                using (StreamReader reader = new StreamReader($"storage.json"))
                {
                    string json = reader.ReadToEnd();
                    List<Universe> restored = JsonConvert.DeserializeObject<List<Universe>>(json);
                    Universes = restored ?? new List<Universe>();
                }
            }
            catch
            {
                Universes = new List<Universe>();
            }
        }
    }
}
