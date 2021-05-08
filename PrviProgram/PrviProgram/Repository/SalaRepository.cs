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
            List<Sala> sale = CitanjeIzFajla();
            foreach (Sala salaBrojac in sale)
            {
                if (salaBrojac.Sifra.Equals(sifraSale))
                {
                    return salaBrojac;
                }
            }
            return null;
        }

        public Sala PregledSalePoNazivu(string nazivSale)
        {
            List<Sala> sale = CitanjeIzFajla();
            foreach (Sala salaBrojac in sale)
            {
                if (salaBrojac.Naziv.Equals(nazivSale))
                {
                    return salaBrojac;
                }
            }
            return null;
        }

        public List<Sala> PregledSvihSala()
        {
            List<Sala> sale = CitanjeIzFajla();
            return sale;
        }

        public Oprema PregledOpremePoSali(string nazivOpreme, string sifraSale)
        {
            List<Sala> sale = CitanjeIzFajla();
            foreach (Sala salaBrojac in sale)
            {
                if (salaBrojac.Sifra.Equals(sifraSale))
                {
                    VracanjeOpremePoSali(nazivOpreme, salaBrojac);
                }
            }
            return null;
        }

        public List<Oprema> PregledSvihOpremaPoSali(Sala sala)
        {
            List<Sala> sale = CitanjeIzFajla();
            foreach (Sala salaBrojac in sale)
            {
                if (salaBrojac.Sifra.Equals(sala.Sifra))
                {
                    return salaBrojac.GetOprema();
                }
            }
            return null;
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