using System.Collections.Generic;
using System.IO;
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
            List<Pacijent> pacijenti = this.CitanjeIzFajla();
            foreach (Pacijent p in pacijenti)
            {
                if (p.Jmbg.Equals(jmbg))
                {
                    return p;
                }
            }
            return null;
        }

        public List<Pacijent> PregledSvihPacijenata()
        {
            List<Pacijent> pacijenti = this.CitanjeIzFajla();
            return pacijenti;
        }

    }
}