using System.Collections.Generic;
using System.IO;
using Model;
using Newtonsoft.Json;

namespace Repository
{
    public class GradoviRepository
    {
        private string path;

        public GradoviRepository()
        {
            path = @"..\..\..\data\gradovi.json";
        }

        public void UpisivanjeUFajl(List<Grad> gradovi)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
            StreamWriter writer = new StreamWriter(path);
            JsonWriter jWriter = new JsonTextWriter(writer);
            serializer.Serialize(jWriter, gradovi);
            jWriter.Close();
            writer.Close();
        }

        public List<Grad> CitanjeIzFajla()
        {
            List<Grad> gradovi = new List<Grad>();
            if (File.Exists(path))
            {
                string jsonText = File.ReadAllText(path);
                if (!string.IsNullOrEmpty(jsonText))
                {
                    gradovi = JsonConvert.DeserializeObject<List<Grad>>(jsonText);
                }
            }

            return gradovi;
        }
        public List<Grad> PregledSvihGradova()
        {
            List<Grad> gradovi = CitanjeIzFajla();
            return gradovi;
        }

    }
}