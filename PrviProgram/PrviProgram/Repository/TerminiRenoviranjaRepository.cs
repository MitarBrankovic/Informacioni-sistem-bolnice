using Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Repository
{
   class TerminiRenoviranjaRepository
   {
        private string path;

        public TerminiRenoviranjaRepository()
        {
            path = @"..\..\..\data\terminiRenoviranja.json";
        }

        public void UpisivanjeUFajl(List<TerminRenoviranjaSale> termini)
      {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
            StreamWriter writer = new StreamWriter(path);
            JsonWriter jWriter = new JsonTextWriter(writer);
            serializer.Serialize(jWriter, termini);
            jWriter.Close();
            writer.Close();
        }
      
      public List<TerminRenoviranjaSale> CitanjeIzFajla()
      {
            List<TerminRenoviranjaSale> termini = new List<TerminRenoviranjaSale>();
            if (File.Exists(path))
            {
                string jsonText = File.ReadAllText(path);
                if (!string.IsNullOrEmpty(jsonText))
                {
                    termini = JsonConvert.DeserializeObject<List<TerminRenoviranjaSale>>(jsonText);
                }
            }
            return termini;
        }
   }
}