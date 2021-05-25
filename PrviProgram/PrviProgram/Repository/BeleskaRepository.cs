using System;
using System.Collections.Generic;
using System.IO;
using Model;
using Newtonsoft.Json;


namespace PrviProgram.Repository
{
    public class BeleskaRepository
    {
        private string path;

        public BeleskaRepository()
        {
            path = @"..\..\..\data\beleska.json";
        }

        public void UpisivanjeUFajl(List<Beleska> beleske)

        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
            StreamWriter writer = new StreamWriter(path);
            JsonWriter jWriter = new JsonTextWriter(writer);
            serializer.Serialize(jWriter, beleske);
            jWriter.Close();
            writer.Close();
        }

        public List<Beleska> CitanjeIzFajla()
        {
            List<Beleska> beleske = new List<Beleska>();
            if (File.Exists(path))
            {
                string jsonText = File.ReadAllText(path);
                if (!string.IsNullOrEmpty(jsonText))
                {
                    beleske = JsonConvert.DeserializeObject<List<Beleska>>(jsonText);
                }
            }
            return beleske;
        }
    }
       
}
