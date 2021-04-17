using Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace PrviProgram.Repository
{
    public class LekarRepository
    {
        private string path;

        public LekarRepository()
        {
            path = @"..\..\..\data\lekar.json";
        }

        public void UpisivanjeUFajl(List<Lekar> lekari)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
            StreamWriter writer = new StreamWriter(path);
            JsonWriter jWriter = new JsonTextWriter(writer);
            serializer.Serialize(jWriter, lekari);
            jWriter.Close();
            writer.Close();
        }

        public List<Lekar> CitanjeIzFajla()
        {
            List<Lekar> lekari = new List<Lekar>();
            if (File.Exists(path))
            {
                string jsonText = File.ReadAllText(path);
                if (!string.IsNullOrEmpty(jsonText))
                {
                    lekari = JsonConvert.DeserializeObject<List<Lekar>>(jsonText);
                }
            }

            return lekari;
        }

    }
}