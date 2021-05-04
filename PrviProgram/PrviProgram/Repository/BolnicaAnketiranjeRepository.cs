using System;
using System.Collections.Generic;
using System.IO;
using Model;
using Newtonsoft.Json;

namespace PrviProgram.Repository
{
    class BolnicaAnketiranjeRepository
    {
        private string path;

        public BolnicaAnketiranjeRepository()
        {
            path = @"..\..\..\data\anketiranjeBolnice.json";
        }

        public void UpisivanjeUFajl(List<BolnicaAnketiranje> ankete)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
            StreamWriter writer = new StreamWriter(path);
            JsonWriter jWriter = new JsonTextWriter(writer);
            serializer.Serialize(jWriter, ankete);
            jWriter.Close();
            writer.Close();
        }

        public List<BolnicaAnketiranje> CitanjeIzFajla()
        {
            List<BolnicaAnketiranje> ankete = new List<BolnicaAnketiranje>();
            if (File.Exists(path))
            {
                string jsonText = File.ReadAllText(path);
                if (!string.IsNullOrEmpty(jsonText))
                {
                    ankete = JsonConvert.DeserializeObject<List<BolnicaAnketiranje>>(jsonText);
                }
            }
            return ankete;
        }
    }
}
