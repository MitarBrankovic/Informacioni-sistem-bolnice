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

    }
}