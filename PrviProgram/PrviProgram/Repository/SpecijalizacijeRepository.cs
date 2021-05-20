using System.Collections.Generic;
using System.IO;
using Model;
using Newtonsoft.Json;

namespace Repository
{
    class SpecijalizacijeRepository
    {
        private string path;

        public SpecijalizacijeRepository()
        {
            path = @"..\..\..\data\specijalizacije.json";
        }

        public void UpisivanjeUFajl(List<Specijalizacija> specijalizacije)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
            StreamWriter writer = new StreamWriter(path);
            JsonWriter jWriter = new JsonTextWriter(writer);
            serializer.Serialize(jWriter, specijalizacije);
            jWriter.Close();
            writer.Close();
        }

        public List<Specijalizacija> CitanjeIzFajla()
        {
            List<Specijalizacija> specijalizacije = new List<Specijalizacija>();
            if (File.Exists(path))
            {
                string jsonText = File.ReadAllText(path);
                if (!string.IsNullOrEmpty(jsonText))
                {
                    specijalizacije = JsonConvert.DeserializeObject<List<Specijalizacija>>(jsonText);
                }
            }
            return specijalizacije;
        }

        public Specijalizacija PregledSpecijalizacije(string naziv)
        {
            foreach (Specijalizacija specijalizacija in CitanjeIzFajla())
            {
                if (specijalizacija.Naziv.Equals(naziv))
                {
                    return specijalizacija;
                }
            }
            return null;
        }

        public List<Specijalizacija> PregledSvihSpecijalizacija()
        {
            List<Specijalizacija> specijalizacije = this.CitanjeIzFajla();
            return specijalizacije;
        }

    }
}
