using Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace RadSaDatotekama
{
    public class DatotekaUpravnik
    {
        private string path;

        public DatotekaUpravnik(string nazivFajla)
        {
            this.path = @"..\..\..\data\" + nazivFajla;
        }

        public void UpisivanjeUFajl(Sala sala)
        {
            JsonSerializer serializer = new JsonSerializer();
            StreamWriter writer = new StreamWriter(path);
            JsonWriter jWriter = new JsonTextWriter(writer);
            serializer.Serialize(jWriter, sala);
            writer.Close();
        }
        public List<Sala> CitanjeIzFajla()
        {
            List<Sala> sale = new List<Sala>();
            if (File.Exists(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                StreamReader reader = new StreamReader(path);
                JsonReader jReader = new JsonTextReader(reader);
                sale = (List<Sala>)serializer.Deserialize(jReader);
            }
            return sale;
        }

    }
}