using System;
using System.Collections.Generic;
using System.IO;
using Model;
using Newtonsoft.Json;

namespace Repository
{
    public class AlergeniRepository
    {
        private string path;

        public AlergeniRepository()
        {
            path = @"..\..\..\data\alergeni.json";
        }

        public void UpisivanjeUFajl(List<Alergen> alergeni)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
            StreamWriter writer = new StreamWriter(path);
            JsonWriter jWriter = new JsonTextWriter(writer);
            serializer.Serialize(jWriter, alergeni);
            jWriter.Close();
            writer.Close();
        }

        public List<Alergen> CitanjeIzFajla()
        {
            List<Alergen> alergeni = new List<Alergen>();
            if (File.Exists(path))
            {
                string jsonText = File.ReadAllText(path);
                if (!string.IsNullOrEmpty(jsonText))
                {
                    alergeni = JsonConvert.DeserializeObject<List<Alergen>>(jsonText);
                }
            }
            return alergeni;
        }

        public Alergen PregledAlergena(String naziv)
        {
            List<Alergen> alergeni = CitanjeIzFajla();
            foreach (Alergen a in alergeni)
            {
                if (a.Naziv.Equals(naziv))
                {
                    return a;
                }
            }
            return null;
        }

        public List<Alergen> PregledSvihAlergena()
        {
            List<Alergen> alergeni = CitanjeIzFajla();
            return alergeni;
        }

    }
}