using System;

namespace Model
{
    public class Vest
    {
        public DateTime Datum { get; set; }
        public string Tekst { get; set; }
        public string SifraVesti { get; set; }
        public Sekretar Autor { get; set; }
        public string Naslov { get; set; }

        public Vest() { }

        public Vest(DateTime datum, string tekst, string sifraVesti, Sekretar autor, string naslov)
        {
            Datum = datum;
            Tekst = tekst;
            SifraVesti = sifraVesti;
            Autor = autor;
            Naslov = naslov;
        }

    }
}