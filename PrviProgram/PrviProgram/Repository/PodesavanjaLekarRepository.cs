using Model;
using Newtonsoft.Json;
using PrviProgram.Model;
using Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PrviProgram.Repository
{

    public class PodesavanjaLekarRepository
    {
        private string path;
        private LekarRepository lekarRepository = new LekarRepository();
        public PodesavanjaLekarRepository()
        {
            path = @"..\..\..\data\podsavanjaLekar.json";
            InicijalizujListu();
        }
        public void UpisivanjeUFajl(List<PodesavanjaLekar> podesavanja)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
            StreamWriter writer = new StreamWriter(path);
            JsonWriter jWriter = new JsonTextWriter(writer);
            serializer.Serialize(jWriter, podesavanja);
            jWriter.Close();
            writer.Close();
        }

        public List<PodesavanjaLekar> CitanjeIzFajla()
        {
            List<PodesavanjaLekar> podesavanja = new List<PodesavanjaLekar>();
            if (File.Exists(path))
            {
                string jsonText = File.ReadAllText(path);
                if (!string.IsNullOrEmpty(jsonText))
                {
                    podesavanja = JsonConvert.DeserializeObject<List<PodesavanjaLekar>>(jsonText);
                }
            }
            return podesavanja;
        }

        public PodesavanjaLekar PregledPodesavanja(String jmbg)
        {
            List<PodesavanjaLekar> podesavanja = CitanjeIzFajla();
            foreach (PodesavanjaLekar podesavanjeIterator in podesavanja) 
            {
                if (podesavanjeIterator.Lekar.Jmbg.Equals(jmbg))
                {
                    return podesavanjeIterator;
                }
            }
            return null;
        }

        public bool PogledaoWizard(Lekar lekar)
        {
            PodesavanjaLekar podesavanjaLekar = PregledPodesavanja(lekar.Jmbg);
            return podesavanjaLekar.PogledaoWizard;
        }

        public bool IskljucioTooltip(Lekar lekar)
        {
            PodesavanjaLekar podesavanjaLekar = PregledPodesavanja(lekar.Jmbg);
            return podesavanjaLekar.IskljucioToolTips;
        }

        private void InicijalizujListu()
        {
            List<Lekar> lekari = lekarRepository.PregledSvihLekara();
            List<PodesavanjaLekar> podesavanja = CitanjeIzFajla();
            if (podesavanja != null)
            {
                foreach (Lekar lekarIterator in lekari)
                {
                    if(PregledPodesavanja(lekarIterator.Jmbg) == null)
                    {
                        PodesavanjaLekar podesavanjaLekar = new PodesavanjaLekar(lekarIterator);
                        podesavanja.Add(podesavanjaLekar);
                    }
                }

            }
            else
            {
                foreach (Lekar lekarIterator in lekari)
                {
                    PodesavanjaLekar podesavanjaLekar = new PodesavanjaLekar(lekarIterator);
                    podesavanja.Add(podesavanjaLekar);
                }

            }
            UpisivanjeUFajl(podesavanja);
        }
    }

}
