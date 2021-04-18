using System.Collections.Generic;
using Model;
using System.IO;
using Newtonsoft.Json;
using PrviProgram.Repository;

namespace Repository
{
   public class OpremaRepository
   {
        private string path;

        public OpremaRepository()
        {
            path = @"..\..\..\data\oprema.json";
        }

        public void UpisivanjeUFajl(List<Oprema> oprema)
      {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
            StreamWriter writer = new StreamWriter(path);
            JsonWriter jWriter = new JsonTextWriter(writer);
            serializer.Serialize(jWriter, oprema);
            jWriter.Close();
            writer.Close();
        }
      
      public List<Oprema> CitanjeIzFajla()
      {
            List<Oprema> oprema = new List<Oprema>();
            if (File.Exists(path))
            {
                string jsonText = File.ReadAllText(path);
                if (!string.IsNullOrEmpty(jsonText))
                {
                    oprema = JsonConvert.DeserializeObject<List<Oprema>>(jsonText);
                }
            }
            return oprema;
        }
      
      public Oprema PregledOpremePoSali(string nazivOpreme, string sifraSale)
      {
            //List<Oprema> opreme = CitanjeIzFajla();
            SalaRepository datoteka = new SalaRepository();
            List<Sala> sale = datoteka.CitanjeIzFajla();
            foreach (Sala s in sale)
            {
                if (s.Sifra.Equals(sifraSale))
                {
                    foreach (Oprema o in s.oprema) {
                        if (o.Naziv.Equals(nazivOpreme)){
                            return o;
                        }
                    }
                }
            }
            return null;
      }
      
      public List<Oprema> PregledSvihOpremaPoSali(string sifraSale)
      {
            //List<Oprema> opreme = new List<Oprema>();
            SalaRepository datoteka = new SalaRepository();
            List<Sala> sale = datoteka.CitanjeIzFajla();
            foreach (Sala s in sale)
            {
                if (s.Sifra.Equals(sifraSale))
                {
                    s.GetOprema();
                }
            }
            return null;
        }

         
        public List<Oprema> PregledCelokupneOpreme()
        {
            List<Oprema> oprema = CitanjeIzFajla();
            return oprema;
        }
    }
}