/***********************************************************************
 * Module:  NotifikacijeObavestenjaRepository.cs
 * Author:  Saska WorkPC
 * Purpose: Definition of the Class Repository.NotifikacijeObavestenjaRepository
 ***********************************************************************/

using System.Collections.Generic;
using Model;
using System.IO;
using Newtonsoft.Json;
using PrviProgram.Repository;

namespace Repository
{
   public class NotifikacijeObavestenjaRepository
   {
        private string path;
        public NotifikacijeObavestenjaRepository()
        {
            path = @"..\..\..\data\notifikacija.json";
        }
        public void UpisivanjeUFajl(List<Model.NotifikacijePacijenta> notifikacije)
        { 
            JsonSerializer serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
            StreamWriter writer = new StreamWriter(path);
            JsonWriter jWriter = new JsonTextWriter(writer);
            serializer.Serialize(jWriter, notifikacije);
            jWriter.Close();
            writer.Close();
        }
      
      public List<NotifikacijePacijenta> CitanjeIzFajla()
      {
            List<NotifikacijePacijenta> notifikacijes= new List<NotifikacijePacijenta>();
            if (File.Exists(path))
            {
                string jsonText = File.ReadAllText(path);
                if (!string.IsNullOrEmpty(jsonText))
                {
                    notifikacijes = JsonConvert.DeserializeObject<List<NotifikacijePacijenta>>(jsonText);
                }
            }
            return notifikacijes;
        }
    }
   
 }
