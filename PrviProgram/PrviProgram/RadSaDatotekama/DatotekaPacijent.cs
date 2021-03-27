using Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace RadSaDatotekama
{
    public class DatotekaPacijent
    {
        private string path;

        public DatotekaPacijent()
        {
            this.path = @"..\..\..\data\pacijent.json";
        }

        public void UpisivanjeUFajl(List<Pacijent> pacijenti)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
            StreamWriter writer = new StreamWriter(path);
            JsonWriter jWriter = new JsonTextWriter(writer);
            serializer.Serialize(jWriter, pacijenti);
            jWriter.Close();
            writer.Close();
        }
        public List<Pacijent> CitanjeIzFajla()
        {
            List<Pacijent> pacijenti = new List<Pacijent>();
            if (File.Exists(path))
            {
                string jsonText = File.ReadAllText(path);
                if (!string.IsNullOrEmpty(jsonText))
                {
                    pacijenti = JsonConvert.DeserializeObject<List<Pacijent>>(jsonText);
                }
            }
            return pacijenti;
        }
    }
}