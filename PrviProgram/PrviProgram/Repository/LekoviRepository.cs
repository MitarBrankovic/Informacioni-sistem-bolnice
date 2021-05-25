using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Repository
{
    public class LekoviRepository
   {
        private string path;
        public LekoviRepository() 
        {
            path = @"..\..\..\data\lekovi.json";
        }

      public void UpisivanjeUFajl(List<Lek> lekovi)
      {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
            StreamWriter writer = new StreamWriter(path);
            JsonWriter jWriter = new JsonTextWriter(writer);
            serializer.Serialize(jWriter, lekovi);
            jWriter.Close();
            writer.Close();
        }
      
      public List<Lek> CitanjeIzFajla()
      {
            List<Lek> lekovi = new List<Lek>();
            if (File.Exists(path))
            {
                string jsonText = File.ReadAllText(path);
                if (!string.IsNullOrEmpty(jsonText))
                {
                    lekovi = JsonConvert.DeserializeObject<List<Lek>>(jsonText);
                }
            }
            return lekovi;
        }
      
      public Lek PregledLeka(string sifraLeka)
      {
            List<Lek> lekovi = CitanjeIzFajla();
            foreach (Lek lekBrojac in lekovi)
            {
                if (lekBrojac.Sifra.Equals(sifraLeka))
                {
                    return lekBrojac;
                }
            }
            return null;
        }
      
      public List<Lek> PregledSvihLekova()
      {
            List<Lek> lekovi = CitanjeIzFajla();
            return lekovi;
        }

        public List<string> DobavljanjeSastojakaLeka(Lek lekArg)
        {
            Lek lek = PregledLeka(lekArg.Sifra);
            try
            {
                string[] sastojciLeka = lek.Sastojci.Split(new Char[] {',', '\n' });
                List<string> sastojciLekaList = new List<string>();
                foreach(var sastojak in sastojciLeka)
                {
                    sastojciLekaList.Add(sastojak.ToLower());
                }
                return sastojciLekaList;
            }
            catch(Exception e)
            {
                return null;
            }
        }
   }
}