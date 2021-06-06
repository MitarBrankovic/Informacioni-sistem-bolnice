using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Repository
{
    public class PovratneInformacijeRepository
    {

        private string path;

        public PovratneInformacijeRepository()
        {
            path = @"..\..\..\data\povratneInformacije.json";
        }

        public void UpisivanjeUFajl(List<PovratneInformacije> povratneInformacije)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
            StreamWriter writer = new StreamWriter(path);
            JsonWriter jWriter = new JsonTextWriter(writer);
            serializer.Serialize(jWriter, povratneInformacije);
            jWriter.Close();
            writer.Close();
        }
        public List<PovratneInformacije> CitanjeIzFajla()
        {
            List<PovratneInformacije> povratneInformacije = new List<PovratneInformacije>();
            if (File.Exists(path))
            {
                string jsonText = File.ReadAllText(path);
                if (!string.IsNullOrEmpty(jsonText))
                {
                    povratneInformacije = JsonConvert.DeserializeObject<List<PovratneInformacije>>(jsonText);
                }
            }
            return povratneInformacije;

        }

        public PovratneInformacije PregledPovratneInformacije(string jmbg)
        {
            List<PovratneInformacije> povratne = CitanjeIzFajla();
            foreach (PovratneInformacije povratna in povratne)
            {
                if (povratna.Osoba.Jmbg.Equals(jmbg))
                {
                    return povratna;
                }
            }
            return null;
        }

        public List<PovratneInformacije> PregledSvihPovratnih() 
        {
            List<PovratneInformacije> povratne = CitanjeIzFajla();
            return povratne;
        }
    }
}
