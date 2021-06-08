using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class PrimedbaNaLek : Entity
    {
        public string Sifra { get; set; }
        public Lekar Lekar { get; set; }
        public Lek Lek { get; set; }
        public string Primedba { get; set; }
        public DateTime Datum { get; set; }

        public PrimedbaNaLek(string sifra, Lekar lekar, Lek lek, string primedba, DateTime datum)
        {
            this.Sifra = sifra;
            this.Lekar = lekar;
            this.Lek = lek;
            this.Primedba = primedba;
            this.Datum = datum;
        }
    }
}
