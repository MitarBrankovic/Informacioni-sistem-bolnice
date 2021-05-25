using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
     public class AnketiranjePacijenta
    {
        public Termin termin { get; set; }
        public Lekar lekar { get; set; }
        public string OcenaLekara { get; set; }
        public string PrimedbaNaLekara { get; set; }
        public string PrimedbaNaPregled { get; set; }
        public string LjubaznostLekara { get; set; }
        public string ZnanjeLekaraOIstorijiBolesti { get; set; }
        public string ObjasnjenjeLekara { get; set; }


        public AnketiranjePacijenta(Termin termin, Lekar lekar,string ocenaLekara,
            string primedbaNaLekara, string primedbaNaPregled, string ljubaznostLekara,string znanjeLekara,
            string objasnjenjeLekara)
        {
            this.termin = termin;
            this.lekar = lekar;
            this.OcenaLekara = ocenaLekara;
            this.PrimedbaNaLekara = primedbaNaLekara;
            this.PrimedbaNaPregled = primedbaNaPregled;
            this.LjubaznostLekara = ljubaznostLekara;
            this.ZnanjeLekaraOIstorijiBolesti = znanjeLekara;
            this.ObjasnjenjeLekara = objasnjenjeLekara;
           
        }

    }
}
