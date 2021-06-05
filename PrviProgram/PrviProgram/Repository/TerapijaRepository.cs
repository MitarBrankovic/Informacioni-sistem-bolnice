using System.Collections.Generic;
using System.IO;
using Model;
using Newtonsoft.Json;

namespace Repository
{
    public class TerapijaRepository
    {
        private string path;

        public TerapijaRepository()
        {
            path = @"..\..\..\data\terapija.json";
        }

        public void UpisivanjeUFajl(List<Terapija> terapije)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
            StreamWriter writer = new StreamWriter(path);
            JsonWriter jWriter = new JsonTextWriter(writer);
            serializer.Serialize(jWriter, terapije);
            jWriter.Close();
            writer.Close();
        }
        public List<Terapija> CitanjeIzFajla()
        {
            List<Terapija> terapije = new List<Terapija>();
            if (File.Exists(path))
            {
                string jsonText = File.ReadAllText(path);
                if (!string.IsNullOrEmpty(jsonText))
                {
                    terapije = JsonConvert.DeserializeObject<List<Terapija>>(jsonText);
                }
            }
            return terapije;
        }
    }
}