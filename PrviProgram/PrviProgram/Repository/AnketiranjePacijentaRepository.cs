using System;
using System.Collections.Generic;
using System.IO;
using Model;
using Newtonsoft.Json;


namespace PrviProgram.Repository
{
   public class AnketiranjePacijentaRepository
    {
        private string path;

        public AnketiranjePacijentaRepository()
        {
            path = @"..\..\..\data\anketerinjaPacijenta.json";
        }

        public void UpisivanjeUFajl(List<AnketiranjePacijenta> ankete)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
            StreamWriter writer = new StreamWriter(path);
            JsonWriter jWriter = new JsonTextWriter(writer);
            serializer.Serialize(jWriter, ankete);
            jWriter.Close();
            writer.Close();
        }

        public List<AnketiranjePacijenta> CitanjeIzFajla()
        {
            List<AnketiranjePacijenta> ankete= new List<AnketiranjePacijenta>();
            if (File.Exists(path))
            {
                string jsonText = File.ReadAllText(path);
                if (!string.IsNullOrEmpty(jsonText))
                {
                    ankete = JsonConvert.DeserializeObject<List<AnketiranjePacijenta>>(jsonText);
                }
            }
            return ankete;
        }
    }
}
