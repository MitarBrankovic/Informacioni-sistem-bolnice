using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Repository
{
    public class GenericFileRepository<T> where T :Entity
    {
        private String path;

        public GenericFileRepository() { }

        public GenericFileRepository(String filePath)
        {
            this.path = filePath;
        }


        public void UpisivanjeUFajl(List<T> lista)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
            StreamWriter writer = new StreamWriter(path);
            JsonWriter jWriter = new JsonTextWriter(writer);
            serializer.Serialize(jWriter, lista);
            jWriter.Close();
            writer.Close();
        }

        public List<T> CitanjeIzFajla()
        {
            List<T> lista = new List<T>();
            if (File.Exists(path))
            {
                string jsonText = File.ReadAllText(path);
                if (!string.IsNullOrEmpty(jsonText))
                {
                    lista = JsonConvert.DeserializeObject<List<T>>(jsonText);
                }
            }
            return lista;
        }

        /*public T Pregled(string sifra)
        {
            List<T> lista = CitanjeIzFajla();
            foreach (T brojac in lista)
            {
                if (brojac.Sifra.Equals(sifra))
                {
                    return brojac;
                }
            }
            return null;
        }*/

        public List<T> PregledSvih()
        {
            List<T> lista = CitanjeIzFajla();
            return lista;
        }
    }
}
