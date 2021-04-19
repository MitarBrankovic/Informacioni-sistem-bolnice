using Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace PrviProgram.Repository
{
    public class SalaRepository
    {
        private string path;

        public SalaRepository()
        {
            path = @"..\..\..\data\sale.json";
        }

        public void UpisivanjeUFajl(List<Sala> sale)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
            StreamWriter writer = new StreamWriter(path);
            JsonWriter jWriter = new JsonTextWriter(writer);
            serializer.Serialize(jWriter, sale);
            jWriter.Close();
            writer.Close();
        }
        public List<Sala> CitanjeIzFajla()
        {
            List<Sala> sale = new List<Sala>();
            if (File.Exists(path))
            {
                string jsonText = File.ReadAllText(path);
                if (!string.IsNullOrEmpty(jsonText))
                {
                    sale = JsonConvert.DeserializeObject<List<Sala>>(jsonText);
                }
            }
            return sale;
        }



        public Sala PregledSale(string sifraSale)
        {
            //SalaRepository datoteka = new SalaRepository();
            List<Sala> sale = CitanjeIzFajla();
            foreach (Sala s in sale)
            {
                if (s.Sifra.Equals(sifraSale))
                {
                    return s;
                }
            }
            return null;
        }


        public List<Sala> PregledSvihSala()
        {
            //SalaRepository datoteka = new SalaRepository();
            List<Sala> sale = CitanjeIzFajla();
            return sale;
        }



        public Oprema PregledOpremePoSali(string nazivOpreme, string sifraSale)
        {
            //List<Oprema> opreme = CitanjeIzFajla();
            List<Sala> sale = CitanjeIzFajla();
            foreach (Sala s in sale)
            {
                if (s.Sifra.Equals(sifraSale))
                {
                    foreach (Oprema o in s.oprema)
                    {
                        if (o.Naziv.Equals(nazivOpreme))
                        {
                            return o;
                        }
                    }
                }
            }
            return null;
        }

        public List<Oprema> PregledSvihOpremaPoSali(Sala sala)
        {
            List<Sala> sale = CitanjeIzFajla();
            foreach (Sala s in sale)
            {
                if (s.Sifra.Equals(sala.Sifra))
                {
                    return s.GetOprema();
                }
            }
            return null;
        }
    }
}