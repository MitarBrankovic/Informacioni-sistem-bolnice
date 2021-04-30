using System.Collections.Generic;
using System.IO;
using Model;
using Newtonsoft.Json;

namespace Repository
{
    class DrzaveRepository
    {
        private string path;

        public DrzaveRepository()
        {
            path = @"..\..\..\data\drzave.json";
        }

        public void UpisivanjeUFajl(List<Drzava> drzave)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
            StreamWriter writer = new StreamWriter(path);
            JsonWriter jWriter = new JsonTextWriter(writer);
            serializer.Serialize(jWriter, drzave);
            jWriter.Close();
            writer.Close();
        }
        public List<Drzava> CitanjeIzFajla()
        {
            List<Drzava> drzave = new List<Drzava>();
            if (File.Exists(path))
            {
                string jsonText = File.ReadAllText(path);
                if (!string.IsNullOrEmpty(jsonText))
                {
                    drzave = JsonConvert.DeserializeObject<List<Drzava>>(jsonText);
                }
            }
            return drzave;
        }
        public List<Drzava> PregledSvihDrzava()
        {
            List<Drzava> drzave = CitanjeIzFajla();
            return drzave;
        }

    }
}
