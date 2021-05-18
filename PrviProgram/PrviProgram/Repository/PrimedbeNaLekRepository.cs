using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PrviProgram.Repository
{
    class PrimedbeNaLekRepository
    {
        private string path;
        public PrimedbeNaLekRepository()
        {
            path = @"..\..\..\data\primedbeNaLek.json";
        }

        public void UpisivanjeUFajl(List<PrimedbaNaLek> primedbe)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
            StreamWriter writer = new StreamWriter(path);
            JsonWriter jWriter = new JsonTextWriter(writer);
            serializer.Serialize(jWriter, primedbe);
            jWriter.Close();
            writer.Close();
        }

        public List<PrimedbaNaLek> CitanjeIzFajla()
        {
            List<PrimedbaNaLek> primedbe = new List<PrimedbaNaLek>();
            if (File.Exists(path))
            {
                string jsonText = File.ReadAllText(path);
                if (!string.IsNullOrEmpty(jsonText))
                {
                    primedbe = JsonConvert.DeserializeObject<List<PrimedbaNaLek>>(jsonText);
                }
            }
            return primedbe;
        }

        public PrimedbaNaLek PregledPrimedbe(string sifra)
        {
            List<PrimedbaNaLek> primedbe = CitanjeIzFajla();
            foreach (PrimedbaNaLek i in primedbe)
            {
                if (i.Sifra.Equals(sifra))
                {
                    return i;
                }
            }
            return null;
        }

        public List<PrimedbaNaLek> PregledSvihPrimedbi()
        {
            List<PrimedbaNaLek> primedbe = CitanjeIzFajla();
            return primedbe;
        }
    }
}
