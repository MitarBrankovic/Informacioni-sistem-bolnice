using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PrviProgram.Repository
{
    class BolnickoLecenjeRepository
    {
        private string path;
        public BolnickoLecenjeRepository()
        {
            path = @"..\..\..\data\bolnickaLecenja.json";
        }

        public void UpisivanjeUFajl(List<BolnickoLecenje> bolnickoLecenje)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
            StreamWriter writer = new StreamWriter(path);
            JsonWriter jWriter = new JsonTextWriter(writer);
            serializer.Serialize(jWriter, bolnickoLecenje);
            jWriter.Close();
            writer.Close();
        }

        public List<BolnickoLecenje> CitanjeIzFajla()
        {
            List<BolnickoLecenje> bolnickoLecenje = new List<BolnickoLecenje>();
            if (File.Exists(path))
            {
                string jsonText = File.ReadAllText(path);
                if (!string.IsNullOrEmpty(jsonText))
                {
                    bolnickoLecenje = JsonConvert.DeserializeObject<List<BolnickoLecenje>>(jsonText);
                }
            }
            return bolnickoLecenje;
        }

        public List<BolnickoLecenje> PregledSvihBolnickihLecenja()
        {
            List<BolnickoLecenje> bolnickoLecenjeLista = CitanjeIzFajla();
            return bolnickoLecenjeLista;
        }

        public BolnickoLecenje PregledBolnickogLecenja(String sifra)
        {
            List<BolnickoLecenje> bolnickoLecenjeLista = CitanjeIzFajla();
            foreach (BolnickoLecenje bolnickoLecenje in bolnickoLecenjeLista)
            {
                if (bolnickoLecenje.Sifra.Equals(sifra))
                    return bolnickoLecenje;
            }
            return null;
        }

        public List<BolnickoLecenje> PregledSvihBolnickihLecenjaPacijenta(string jmbg)
        {
            List<BolnickoLecenje> bolnickoLecenjeLista = CitanjeIzFajla();
            List<BolnickoLecenje> bolnickoLecenjePacijentaLista = new List<BolnickoLecenje>();
            foreach (BolnickoLecenje bolnickoLecenje in bolnickoLecenjeLista)
            {
                if (bolnickoLecenje.Pacijent.Jmbg.Equals(jmbg))
                {
                    bolnickoLecenjePacijentaLista.Add(bolnickoLecenje);
                }
            }
            return bolnickoLecenjePacijentaLista;
        }
    }
}
