using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class BolnicaAnketiranje
    {
        public DateTime datumANketiranja { get; set; }
        public string ocenaBolnice { get; set; }
        public string zadovoljanHigijenom { get; set; }
        public string zadovoljanOsobljem { get; set; }
        public string zadovoljanUslugamaBolnice { get; set; }


        public BolnicaAnketiranje(DateTime datum, string ocena, string higijena, string osoblje, string usluge)
        {
            this.datumANketiranja = datum;
            this.ocenaBolnice = ocena;
            this.zadovoljanHigijenom = higijena;
            this.zadovoljanOsobljem = osoblje;
            this.zadovoljanUslugamaBolnice = usluge;
        }
    }
    
}
