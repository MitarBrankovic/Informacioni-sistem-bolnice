using Model;
using System;
using Newtonsoft.Json;
using System.IO;

namespace RadSaDatotekama
{
   public class DatotekaUpravnik
   {
        public void UpisivanjeUFajl(Sala sala, string nazivFajla)
        {

            Osoba a = new Osoba();
            a.Ime = "DArko";
            String JsonResult = JsonConvert.SerializeObject(a, Formatting.Indented);

            string currentDir = Environment.CurrentDirectory;
            DirectoryInfo directory = new DirectoryInfo(
            Path.GetFullPath(Path.Combine(currentDir, @"..\..\" + "a.json")));
            string path =  directory.ToString();
            a.Prezime = directory.ToString();

            using (var writer = new StreamWriter(@"C:\Users\Darko\Desktop\sims\Informacioni-sistem-bolnice\PrviProgram\PrviProgram\a.json"))
            {
                writer.Write(JsonResult);
            }
        }
            public void CitanjeIzFajla(string nazivFajla)
      {
         // TODO: implement
      }
   
   }
}