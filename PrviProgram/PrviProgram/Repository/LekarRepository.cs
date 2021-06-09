using System.Collections.Generic;
using System.IO;
using System.Linq;
using Model;
using Newtonsoft.Json;

namespace Repository
{
    public class LekarRepository : ILekarRepository, ILekarSpecijalizacija
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
            List<Lekar> lekari = CitanjeIzFajla();
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
            List<Lekar> lekari = CitanjeIzFajla();
            return lekari;
        }

        public List<Korisnik> PregledSvihKorisnika()
        {
            List<Lekar> lekari = CitanjeIzFajla();
            List<Korisnik> korisnici = lekari.Select(o => o.Korisnik).ToList();
            return korisnici;
        }

        public List<Lekar> PregledLekaraOdredjeneSpecijalizacije(Specijalizacija specijalizacija)
        {
            List<Lekar> lekari = CitanjeIzFajla();
            List<Lekar> lekariSpecijalizacije = new List<Lekar>();
            foreach (Lekar lekar in lekari)
            {
                if (ProveriSpecijalizacijuLekara(lekar, specijalizacija))
                {
                    lekariSpecijalizacije.Add(lekar);
                }
            }
            return lekariSpecijalizacije;
        }

        public bool ProveriSpecijalizacijuLekara(Lekar zaLekara, Specijalizacija specijalizacija)
        {
            Lekar lekar = PregledLekara(zaLekara.Jmbg);
            foreach (Specijalizacija specijalizacijaLekara in lekar.GetSpecijalizacija())
            {
                if (specijalizacijaLekara.Naziv.Equals(specijalizacija.Naziv))
                {
                    return true;
                }
            }
            return false;
        }

    }
}