using System.Collections.Generic;
using System.IO;
using System.Linq;
using Model;
using Newtonsoft.Json;

namespace Repository
{
    public class PacijentRepository
    {
        private string path;

        public PacijentRepository()
        {
            path = @"..\..\..\data\pacijent.json";
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

        public Pacijent PregledPacijenta(string jmbg)
        {
            List<Pacijent> pacijenti = CitanjeIzFajla();
            foreach (Pacijent pacijent in pacijenti)
            {
                if (pacijent.Jmbg.Equals(jmbg))
                {
                    return pacijent;
                }
            }
            return null;
        }

        public List<Pacijent> PregledSvihPacijenata()
        {
            List<Pacijent> pacijenti = CitanjeIzFajla();
            return pacijenti;
        }

        public List<Korisnik> PregledSvihKorisnika()
        {
            List<Pacijent> pacijenti = CitanjeIzFajla();
            List<Korisnik> korisnici = pacijenti.Select(o => o.Korisnik).ToList();
            return korisnici;
        }

        public List<Pacijent> PregledPacijenta(Pacijent selektovaniPacijent)
        {

            List<Pacijent> pacijenti = CitanjeIzFajla();
            List<Pacijent> potrebniPacijenti = new List<Pacijent>();
            foreach (Pacijent pacijent in pacijenti)
            {
                if (pacijent != null && pacijent.Jmbg.Equals(selektovaniPacijent.Jmbg))
                {
                        potrebniPacijenti.Add(pacijent);
                }
            }
            return potrebniPacijenti;
        }

        public List<Pacijent> PretragaPacijent(string pretraga)
        {
            string tekst = pretraga.ToLower();
            List<Pacijent> pacijenti = CitanjeIzFajla();
            List<Pacijent> pronadjeniPacijenti = new List<Pacijent>();
            foreach (Pacijent pacijentIterator in pacijenti)
            {
                if (pacijentIterator.Ime.ToLower().Contains(tekst) || pacijentIterator.Prezime.ToLower().Contains(tekst) || pacijentIterator.Jmbg.Contains(tekst))
                {
                    pronadjeniPacijenti.Add(pacijentIterator);
                }
            }
            return pronadjeniPacijenti;
        }

        public IzvrseniPregled PregledIzvrsenogPregleda(string sifra)
        {
            List<Pacijent> pacijenti = CitanjeIzFajla();
            foreach (Pacijent p in pacijenti) 
            {
                foreach (IzvrseniPregled i in p.KartonPacijenta.izvrseniPregled)
                {
                    if (i.Sifra.Equals(sifra))
                        return i;
                }
            }
            return null;
        }
    }
}