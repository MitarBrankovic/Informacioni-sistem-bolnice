using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Repository
{

    public class RadnoVremeLekaraRepository
    {
        private string path;

        public RadnoVremeLekaraRepository()
        {
            path = @"..\..\..\data\radnoVremeLekara.json";
        }

        public void UpisivanjeUFajl(List<RadnoVremeLekara> radnaVremena)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Formatting = Formatting.Indented;
            StreamWriter writer = new StreamWriter(path);
            JsonWriter jWriter = new JsonTextWriter(writer);
            serializer.Serialize(jWriter, radnaVremena);
            jWriter.Close();
            writer.Close();
        }

        public List<RadnoVremeLekara> CitanjeIzFajla()
        {
            List<RadnoVremeLekara> radnaVremena = new List<RadnoVremeLekara>();
            if (File.Exists(path))
            {
                string jsonText = File.ReadAllText(path);
                if (!string.IsNullOrEmpty(jsonText))
                {
                    radnaVremena = JsonConvert.DeserializeObject<List<RadnoVremeLekara>>(jsonText);
                }
            }
            return radnaVremena;
        }

        public RadnoVremeLekara PregledRadnogVremenaLekara(string nazivLekara)
        {
            List<RadnoVremeLekara> radnaVremena = CitanjeIzFajla();
            foreach (RadnoVremeLekara radnoVremeBrojac in radnaVremena)
            {
                if (radnoVremeBrojac.Lekar.Ime.Equals(nazivLekara))
                {
                    return radnoVremeBrojac;
                }
            }
            return null;
        }

        public List<RadnoVremeLekara> PregledSvihRadnihVremena()
        {
            List<RadnoVremeLekara> radnaVremena = CitanjeIzFajla();
            return radnaVremena;
        }
    }
}
