using System;
using System.Collections.Generic;
using System.IO;
using Model;
using Newtonsoft.Json;

namespace Repository
{
    public class VestiRepository
    {
        private string path;

        public VestiRepository()
        {
            path = @"..\..\..\data\vesti.json";
        }

        public void UpisivanjeUFajl(List<Vest> vesti)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
            StreamWriter writer = new StreamWriter(path);
            JsonWriter jWriter = new JsonTextWriter(writer);
            serializer.Serialize(jWriter, vesti);
            jWriter.Close();
            writer.Close();
        }

        public List<Vest> CitanjeIzFajla()
        {
            List<Vest> vesti = new List<Vest>();
            if (File.Exists(path))
            {
                string jsonText = File.ReadAllText(path);
                if (!string.IsNullOrEmpty(jsonText))
                {
                    vesti = JsonConvert.DeserializeObject<List<Vest>>(jsonText);
                }
            }
            return vesti;
        }

        public Vest PregledVesti(String sifra)
        {
            List<Vest> vesti = CitanjeIzFajla();
            foreach (Vest vest in vesti)
            {
                if (vest.SifraVesti.Equals(sifra))
                {
                    return vest;
                }
            }
            return null;
        }

        public List<Vest> PregledSvihVesti()
        {
            List<Vest> vesti = CitanjeIzFajla();
            return vesti;
        }

    }
}