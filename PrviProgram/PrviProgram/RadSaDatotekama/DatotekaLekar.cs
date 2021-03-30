using Model;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;

namespace RadSaDatotekama
{
    public class DatotekaLekar
    {
        private string path;

        public DatotekaLekar()
        {
            this.path = @"..\..\..\data\lekar.json";
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