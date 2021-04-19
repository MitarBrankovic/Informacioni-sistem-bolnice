using System.Collections.Generic;
using System.IO;
using System.Linq;
using Model;
using Newtonsoft.Json;

namespace Repository
{
    public class SekretarRepository
    {
        private string path;

        public SekretarRepository()
        {
            path = @"..\..\..\data\sekretar.json";
        }

        public void UpisivanjeUFajl(List<Sekretar> sekretari)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
            StreamWriter writer = new StreamWriter(path);
            JsonWriter jWriter = new JsonTextWriter(writer);
            serializer.Serialize(jWriter, sekretari);
            jWriter.Close();
            writer.Close();
        }
        public List<Sekretar> CitanjeIzFajla()
        {
            List<Sekretar> sekretari = new List<Sekretar>();
            if (File.Exists(path))
            {
                string jsonText = File.ReadAllText(path);
                if (!string.IsNullOrEmpty(jsonText))
                {
                    sekretari = JsonConvert.DeserializeObject<List<Sekretar>>(jsonText);
                }
            }
            return sekretari;
        }

        public Sekretar PregledSekretara(string jmbg)
        {
            List<Sekretar> sekretari = this.CitanjeIzFajla();
            foreach (Sekretar s in sekretari)
            {
                if (s.Jmbg.Equals(jmbg))
                {
                    return s;
                }
            }
            return null;
        }

        public List<Sekretar> PregledSvihSekretara()
        {
            List<Sekretar> sekretari = this.CitanjeIzFajla();
            return sekretari;
        }

        public List<Korisnik> PregledSvihKorisnika()
        {
            List<Sekretar> sekretari = this.CitanjeIzFajla();
            List<Korisnik> korisnici = sekretari.Select(o => o.Korisnik).ToList();
            return korisnici;
        }

    }
}