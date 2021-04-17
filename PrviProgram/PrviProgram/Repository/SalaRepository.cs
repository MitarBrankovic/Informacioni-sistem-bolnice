
using Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace PrviProgram.Repository
{
    public class SalaRepository
    {
        private string path;

        public SalaRepository()
        {
            path = @"..\..\..\data\sale.json";
        }

        public void UpisivanjeUFajl(List<Sala> sale)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
            StreamWriter writer = new StreamWriter(path);
            JsonWriter jWriter = new JsonTextWriter(writer);
            serializer.Serialize(jWriter, sale);
            jWriter.Close();
            writer.Close();
        }
        public List<Sala> CitanjeIzFajla()
        {
            List<Sala> pacijenti = new List<Sala>();
            if (File.Exists(path))
            {
                string jsonText = File.ReadAllText(path);
                if (!string.IsNullOrEmpty(jsonText))
                {
                    pacijenti = JsonConvert.DeserializeObject<List<Sala>>(jsonText);
                }
            }
            return pacijenti;
        }

    }
}