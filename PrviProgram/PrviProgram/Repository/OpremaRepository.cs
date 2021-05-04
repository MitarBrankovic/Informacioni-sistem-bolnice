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
        private SalaRepository salaRepository = new SalaRepository();

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
            List<Sala> sale = salaRepository.CitanjeIzFajla();
            foreach (Sala salaBrojac in sale)
            {
                if (salaBrojac.Sifra.Equals(sifraSale))
                {
                    VracanjeOpremePoSali(nazivOpreme, salaBrojac);
                }
            }
            return null;
      }
      
      public List<Oprema> PregledSvihOpremaPoSali(string sifraSale)
      {
            List<Sala> sale = salaRepository.CitanjeIzFajla();
            foreach (Sala salaBrojac in sale)
            {
                if (salaBrojac.Sifra.Equals(sifraSale))
                {
                    salaBrojac.GetOprema();
                }
            }
            return null;
        }

         
        public List<Oprema> PregledCelokupneOpreme()
        {
            List<Oprema> oprema = CitanjeIzFajla();
            return oprema;
        }


        public Oprema VracanjeOpremePoSali(string nazivOpreme, Sala salaBrojac)
        {
            foreach (Oprema opremaBrojac in salaBrojac.oprema)
            {
                if (opremaBrojac.Naziv.Equals(nazivOpreme))
                {
                    return opremaBrojac;
                }
            }
            return null;
        }
    }
}