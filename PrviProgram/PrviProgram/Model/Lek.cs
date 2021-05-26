using System.Collections.Generic;

namespace Model
{
   public class Lek
   {
        public Lek() { }

        public Lek(string sifra, string naziv, string sastojci, string opis)
        {
            this.Sifra = sifra;
            this.Naziv = naziv;
            this.Sastojci = sastojci;
            this.Opis = opis;
        }


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