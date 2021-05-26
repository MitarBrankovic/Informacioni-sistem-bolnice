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

        public Lek(string sifra, string naziv, string sastojci, string opis)
        {
            Sifra = sifra;
            Naziv = naziv;
            Sastojci = sastojci;
            Opis = opis;
            ZamenaZaLek = new List<Lek>();
        }

        public Lek() { }
        public override string ToString()
        {
            return Naziv;
        }
    }
}