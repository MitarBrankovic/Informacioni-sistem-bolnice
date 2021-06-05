using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public  class Terapija
    {
        public DateTime PocetakTerapije { get; set; }
        public DateTime KrajTerapije { get; set; }
        public string OpisTerapije { get; set; }
        public Pacijent Pacijent { get; set; }

        public Terapija(DateTime pocetakTerapiije, DateTime krajTerapije, string opisTerapija, Pacijent pacijent)
        {
            this.PocetakTerapije = pocetakTerapiije;
            this.KrajTerapije = krajTerapije;
            this.OpisTerapije = opisTerapija;
            this.Pacijent = pacijent;
        }
        public Terapija()
        {

        }
    }
}
