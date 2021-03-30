using Model;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace PrviProgram.RadSaDatotekama
{
    class DatotekaTermini
    {
        private string path;

        public DatotekaTermini() 
        {
            this.path = @"..\..\..\data\termini.json";
        }

        public void UpisivanjeUFajl(List<Termin> termini)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
            StreamWriter writer = new StreamWriter(path);
            JsonWriter jWriter = new JsonTextWriter(writer);
            serializer.Serialize(jWriter, termini);
            jWriter.Close();
            writer.Close();
        }

        public List<Termin> CitanjeIzFajla()
        {
            List<Termin> termini = new List<Termin>();
            if (File.Exists(path))
            {
                string jsonText = File.ReadAllText(path);
                if (!string.IsNullOrEmpty(jsonText))
                {
                    termini = JsonConvert.DeserializeObject<List<Termin>>(jsonText);
                }
            }
            return termini;
        }
    }
}
