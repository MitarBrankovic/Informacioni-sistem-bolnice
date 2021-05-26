using Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace PrviProgram.Repository
{
    class TerminiRepository
    {
        private string path;
        public TerminiRepository()
        {
            path = @"..\..\..\data\termini.json";
        }

        public void UpisivanjeUFajl(List<Termin> termini)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
            StreamWriter writer = new StreamWriter(path);
            JsonWriter jWriter = new JsonTextWriter(writer);
            serializer.Serialize(jWriter, termini);
            jWriter.Close();
            writer.Close();
        }

        public List<Termin> CitanjeIzFajla()
        {
            List<Termin> termini = new List<Termin>();
            if (File.Exists(path))
            {
                string jsonText = File.ReadAllText(path);
                if (!string.IsNullOrEmpty(jsonText))
                {
                    termini = JsonConvert.DeserializeObject<List<Termin>>(jsonText);
                }
            }
            return termini;
        }
        
        public Termin PregledTermina(string sifraTermina)
        {
            List<Termin> termini = CitanjeIzFajla();
            foreach (Termin termin in termini)
            {
                if (termin.SifraTermina.Equals(sifraTermina))
                {
                    return termin;
                }

            }
            return null;
        }

        public List<Termin> PregledTerminaPacijenta(Pacijent pacijent)
        {
            List<Termin> termini = CitanjeIzFajla();
            List<Termin> noviTermini = new List<Termin>();
            foreach (Termin termin in termini)
            {
                if (termin.pacijent != null && termin.pacijent.Jmbg.Equals(pacijent.Jmbg))
                {
                    noviTermini.Add(termin);
                }
            }
            return noviTermini;
        }

        public List<Termin> PregledTerminaLekara(Lekar lekar)
        {
            List<Termin> termini = new List<Termin>();

            foreach (Termin t in CitanjeIzFajla())
            {
                if (t.lekar.Jmbg == lekar.Jmbg)
                {
                    termini.Add(t);
                }
            }

            return termini;
        }

        public List<Termin> PregledSvihTermina()
        {
            List<Termin> termini = CitanjeIzFajla();
            return termini;
        }

    }
}
