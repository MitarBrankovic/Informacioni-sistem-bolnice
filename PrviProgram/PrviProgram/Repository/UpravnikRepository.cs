using System.Collections.Generic;
using System.IO;
using System.Linq;
using Model;
using Newtonsoft.Json;

namespace Repository
{
    public class UpravnikRepository
    {
        private string path;

        public UpravnikRepository()
        {
            path = @"..\..\..\data\upravnik.json";
        }

        public void UpisivanjeUFajl(List<Upravnik> upravnici)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
            StreamWriter writer = new StreamWriter(path);
            JsonWriter jWriter = new JsonTextWriter(writer);
            serializer.Serialize(jWriter, upravnici);
            jWriter.Close();
            writer.Close();
        }
        public List<Upravnik> CitanjeIzFajla()
        {
            List<Upravnik> upravnici = new List<Upravnik>();
            if (File.Exists(path))
            {
                string jsonText = File.ReadAllText(path);
                if (!string.IsNullOrEmpty(jsonText))
                {
                    upravnici = JsonConvert.DeserializeObject<List<Upravnik>>(jsonText);
                }
            }
            return upravnici;
        }
        public Upravnik PregledUpravnika(string jmbg)
        {
            List<Upravnik> upravnici = this.CitanjeIzFajla();
            foreach (Upravnik u in upravnici)
            {
                if (u.Jmbg.Equals(jmbg))
                {
                    return u;
                }
            }
            return null;
        }

        public List<Upravnik> PregledSvihUpravnika()
        {
            List<Upravnik> upravnici = this.CitanjeIzFajla();
            return upravnici;
        }

        public List<Korisnik> PregledSvihKorisnika()
        {
            List<Upravnik> upravnici = this.CitanjeIzFajla();
            List<Korisnik> korisnici = upravnici.Select(o => o.Korisnik).ToList();
            return korisnici;
        }

    }
}