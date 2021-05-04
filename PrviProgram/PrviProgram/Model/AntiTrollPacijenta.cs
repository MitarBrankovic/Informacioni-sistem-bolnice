using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
   public  class AntiTrollPacijenta
    {
        public int brojacOtkazanihTermina { get; set; }
        public int brojacIzmenenjenihTermina { get; set; }
        public int brojacDodavanihTermina { get; set; }
        public DateTime vremeBanovanja { get; set; }
        public bool daLiJeBanovan { get; set; }
        public Pacijent pacijent { get; set; }

        public AntiTrollPacijenta(int brojacDodavanihTermina, int brojacIzmenenjenihTermina,int brojacOtkazanihTermina, Pacijent pacijent)
        {
            this.brojacDodavanihTermina = brojacDodavanihTermina;
            this.brojacIzmenenjenihTermina = brojacIzmenenjenihTermina;
            this.brojacOtkazanihTermina = brojacOtkazanihTermina;
            this.pacijent = pacijent;
        }
        public AntiTrollPacijenta(int brojacDodavanihTermina, int brojacIzmenenjenihTermina, int brojacOtkazanihTermina, Pacijent pacijent,DateTime vremeBanovanja,bool daLiJeBanovan)
        {
            this.brojacDodavanihTermina = brojacDodavanihTermina;
            this.brojacIzmenenjenihTermina = brojacIzmenenjenihTermina;
            this.brojacOtkazanihTermina = brojacOtkazanihTermina;
            this.pacijent = pacijent;
            this.vremeBanovanja = vremeBanovanja;
            this.daLiJeBanovan = daLiJeBanovan;
        }
        public AntiTrollPacijenta()
        {
        }
    }
}
