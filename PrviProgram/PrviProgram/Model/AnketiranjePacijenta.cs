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
        public string ocenaLekara { get; set; }
        public string primedbaNaLekara { get; set; }
        public string primedbaNaPregled { get; set; }
        public string ljubaznostLekara { get; set; }
        public string znanjeLekaraOIstorijiBolesti { get; set; }
        public string objasnjenjeLekara { get; set; }


        public AnketiranjePacijenta(Termin termin, Lekar lekar,string ocenaLekara,
            string primedbaNaLekara, string primedbaNaPregled, string ljubaznostLekara,string znanjeLekara,
            string objasnjenjeLekara)
        {
            this.termin = termin;
            this.lekar = lekar;
            this.ocenaLekara = ocenaLekara;
            this.primedbaNaLekara = primedbaNaLekara;
            this.primedbaNaPregled = primedbaNaPregled;
            this.ljubaznostLekara = ljubaznostLekara;
            this.znanjeLekaraOIstorijiBolesti = znanjeLekara;
            this.objasnjenjeLekara = objasnjenjeLekara;
           
        }

    }
}
