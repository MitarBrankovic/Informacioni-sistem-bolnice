using System;
using System.Collections.Generic;
using System.IO;
using Model;
using Newtonsoft.Json;


namespace PrviProgram.Repository
{
    public class AntiTrollRepository
    {
        private string path;

        public AntiTrollRepository()
        {
            path = @"..\..\..\data\antiTrollPacijenta.json";
        }

        public void UpisivanjeUFajl(List<AntiTrollPacijenta> antiTrollPacijenata )

        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
            StreamWriter writer = new StreamWriter(path);
            JsonWriter jWriter = new JsonTextWriter(writer);
            serializer.Serialize(jWriter, antiTrollPacijenata);
            jWriter.Close();
            writer.Close();
        }

        public List<AntiTrollPacijenta> CitanjeIzFajla()
        {
            List<AntiTrollPacijenta> antiTrollPacijenata = new List<AntiTrollPacijenta>();
            if (File.Exists(path))
            {
                string jsonText = File.ReadAllText(path);
                if (!string.IsNullOrEmpty(jsonText))
                {
                    antiTrollPacijenata = JsonConvert.DeserializeObject<List<AntiTrollPacijenta>>(jsonText);
                }
            }
            return antiTrollPacijenata;
        }
    }
}

