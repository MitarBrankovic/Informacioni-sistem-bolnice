using System.Collections.Generic;
using System.IO;
using System.Linq;
using Model;
using Newtonsoft.Json;

namespace Repository
{
    public class LekarRepository
    {
        private string path;

        public LekarRepository()
        {
            path = @"..\..\..\data\lekar.json";
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

        public Lekar PregledLekara(string jmbg)
        {
            List<Lekar> lekari = this.CitanjeIzFajla();
            foreach (Lekar l in lekari)
            {
                if (l.Jmbg.Equals(jmbg))
                {
                    return l;
                }
            }
            return null;
        }

        public List<Lekar> PregledSvihLekara()
        {
            List<Lekar> lekari = this.CitanjeIzFajla();
            return lekari;
        }

        public List<Korisnik> PregledSvihKorisnika()
        {
            List<Lekar> lekari = this.CitanjeIzFajla();
            List<Korisnik> korisnici = lekari.Select(o => o.Korisnik).ToList();
            return korisnici;
        }

    }
}