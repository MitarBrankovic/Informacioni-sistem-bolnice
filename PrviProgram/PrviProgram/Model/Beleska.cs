using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Beleska
    {
        public string OpisBeleske { get; set; }
        public DateTime PocetakStizanjaNotifikacije { get; set; } 
        public DateTime KrajStizanjaNotifikacije { get; set; }

        public string Jmbg { get; set; }
        public string SifraIzvrsenogPregleda { get; set; }
        public string VremeObavestenja { get; set; }

        public Beleska(string OpisBeleske, DateTime PocetakStizanjaNotifikacije, DateTime KrajStizanjaNotifikacije, string Jmbg, string SifraIzvrsenogPregleda, string VremeObavestenja)
        {
            this.OpisBeleske = OpisBeleske;
            this.PocetakStizanjaNotifikacije = PocetakStizanjaNotifikacije;
            this.KrajStizanjaNotifikacije = KrajStizanjaNotifikacije;
            this.Jmbg = Jmbg;
            this.SifraIzvrsenogPregleda = SifraIzvrsenogPregleda;
            this.VremeObavestenja = VremeObavestenja;
        }


    }
}
