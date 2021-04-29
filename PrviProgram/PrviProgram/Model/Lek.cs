using System.Collections.Generic;

namespace Model
{
   public class Lek
   {
      public string Sifra { get; set; }
      public string Naziv { get; set; }
        //public int Kolicina;
        public List<Lek> ZamenaZaLek;
      public string Sastojci { get; set; }
      public string Opis { get; set; }


        public override string ToString()
        {
            return Naziv;
        }
    }
}