using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class BolnicaAnketiranje
    {
        public DateTime DatumANketiranja { get; set; }
        public string OcenaBolnice { get; set; }
        public string ZadovoljanHigijenom { get; set; }
        public string ZadovoljanOsobljem { get; set; }
        public string ZadovoljanUslugamaBolnice { get; set; }


        public BolnicaAnketiranje(DateTime datum, string ocena, string higijena, string osoblje, string usluge)
        {
            this.DatumANketiranja = datum;
            this.OcenaBolnice = ocena;
            this.ZadovoljanHigijenom = higijena;
            this.ZadovoljanOsobljem = osoblje;
            this.ZadovoljanUslugamaBolnice = usluge;
        }
    }
    
}
