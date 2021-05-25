using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    class BolnickoLecenje
    {
        public string Sifra { get; set; }
        public Pacijent Pacijent { get; set; }
        public Sala Sala { get; set; }
        public DateTime DatumPocetka { get; set; }
        public DateTime DatumZavrsetka { get; set; }

        public BolnickoLecenje(string sifra, Pacijent pacijent, Sala sala, DateTime datumPocetka, DateTime datumZavrsetka)
        {
            this.Sifra = sifra;
            this.Pacijent = pacijent;
            this.Sala = sala;
            this.DatumPocetka = datumPocetka;
            this.DatumZavrsetka = datumZavrsetka;
        }
    }
}
