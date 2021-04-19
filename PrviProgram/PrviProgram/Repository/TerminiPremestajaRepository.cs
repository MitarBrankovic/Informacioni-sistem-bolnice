using Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace PrviProgram.Repository
{
    class TerminiPremestajaRepository
    {
        private string path;

        public TerminiPremestajaRepository()
        {
            path = @"..\..\..\data\terminiPremestaja.json";
        }

        public void UpisivanjeUFajl(List<TerminPremestanjaOpreme> termini)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
            StreamWriter writer = new StreamWriter(path);
            JsonWriter jWriter = new JsonTextWriter(writer);
            serializer.Serialize(jWriter, termini);
            jWriter.Close();
            writer.Close();
        }

        public List<TerminPremestanjaOpreme> CitanjeIzFajla()
        {
            List<TerminPremestanjaOpreme> termini = new List<TerminPremestanjaOpreme>();
            if (File.Exists(path))
            {
                string jsonText = File.ReadAllText(path);
                if (!string.IsNullOrEmpty(jsonText))
                {
                    termini = JsonConvert.DeserializeObject<List<TerminPremestanjaOpreme>>(jsonText);
                }
            }
            return termini;
        }







    }
}
