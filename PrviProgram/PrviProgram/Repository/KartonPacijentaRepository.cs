/***********************************************************************
 * Module:  KartonPacijentaRepository.cs
 * Author:  Brankovic
 * Purpose: Definition of the Class Repository.KartonPacijentaRepository
 ***********************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using Model;
using Newtonsoft.Json;

namespace Repository
{
   public class KartonPacijentaRepository
   {
        private string path;

        public KartonPacijentaRepository()
        {
            path = @"..\..\..\data\karton.json";
        }

      public void UpisivanjeUFajl(List<KartonPacijenta> karton)
      {
            // TODO: implement
            JsonSerializer serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
            StreamWriter writer = new StreamWriter(path);
            JsonWriter jWriter = new JsonTextWriter(writer);
            serializer.Serialize(jWriter, karton);
            jWriter.Close();
            writer.Close();
        }
      
      public List<KartonPacijenta> CitanjeIzFajla()
      {
            // TODO: implement
            List<KartonPacijenta> karton = new List<KartonPacijenta>();
            if (File.Exists(path))
            {
                string jsonText = File.ReadAllText(path);
                if (!string.IsNullOrEmpty(jsonText))
                {
                    karton = JsonConvert.DeserializeObject<List<KartonPacijenta>>(jsonText);
                }
            }
            return karton;
        }
      
      public KartonPacijenta PregledKartona(String sifra)
      {
            KartonPacijentaRepository datoteka = new KartonPacijentaRepository();
            List<KartonPacijenta> karton = datoteka.CitanjeIzFajla();
            foreach (KartonPacijenta s in karton)
            {
                if (s.Sifra.Equals(sifra))
                {
                    return s;
                }
            }
            return null;
        }
      
      public List<KartonPacijenta> PregledSvihKartona()
      {
            KartonPacijentaRepository datoteka = new KartonPacijentaRepository();
            List<KartonPacijenta> karton = datoteka.CitanjeIzFajla();
            return karton;
        }

        public List<IzvrseniPregled> PregledSvihIzvrsenihPregleda(KartonPacijenta karton)
        {
            KartonPacijentaRepository datoteka = new KartonPacijentaRepository();
            List<KartonPacijenta> kartoni = datoteka.CitanjeIzFajla();
            foreach (KartonPacijenta s in kartoni)
            {
                if (s.Sifra.Equals(karton.Sifra))
                {
                    return s.izvrseniPregled;
                }
            }
            return null;
        }
   
   }
}