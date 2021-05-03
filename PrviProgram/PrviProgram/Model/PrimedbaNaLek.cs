using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class PrimedbaNaLek
    {
        public string sifra { get; set; }
        public Lekar lekar { get; set; }
        public Lek lek { get; set; }
        public string primedba { get; set; }
        public DateTime datum { get; set; }

        public PrimedbaNaLek(string sifra, Lekar lekar, Lek lek, string primedba, DateTime datum)
        {
            this.sifra = sifra;
            this.lekar = lekar;
            this.lek = lek;
            this.primedba = primedba;
            this.datum = datum;
        }
    }
}
