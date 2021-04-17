using Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace PrviProgram.Repository
{
    public class UpravnikRepository
    {
        private string path;

        public UpravnikRepository(string nazivFajla)
        {
            path = @"..\..\..\data\upravnik.json";
        }

        public void UpisivanjeUFajl(List<Upravnik> upravnici)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
            StreamWriter writer = new StreamWriter(path);
            JsonWriter jWriter = new JsonTextWriter(writer);
            serializer.Serialize(jWriter, upravnici);
            jWriter.Close();
            writer.Close();
        }
        public List<Upravnik> CitanjeIzFajla()
        {
            List<Upravnik> upravnici = new List<Upravnik>();
            if (File.Exists(path))
            {
                string jsonText = File.ReadAllText(path);
                if (!string.IsNullOrEmpty(jsonText))
                {
                    upravnici = JsonConvert.DeserializeObject<List<Upravnik>>(jsonText);
                }
            }
            return upravnici;
        }

    }
}