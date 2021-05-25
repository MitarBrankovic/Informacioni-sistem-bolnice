using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
   public  class AntiTrollPacijenta
    {
        public int BrojacOtkazanihTermina { get; set; }
        public int BrojacIzmenenjenihTermina { get; set; }
        public int BrojacDodavanihTermina { get; set; }
        public DateTime VremeBanovanja { get; set; }
        public bool DaLiJeBanovan { get; set; }
        public Pacijent pacijent { get; set; }

        public AntiTrollPacijenta(int brojacDodavanihTermina, int brojacIzmenenjenihTermina,
            int brojacOtkazanihTermina, Pacijent pacijent)
        {
            this.BrojacDodavanihTermina = brojacDodavanihTermina;
            this.BrojacIzmenenjenihTermina = brojacIzmenenjenihTermina;
            this.BrojacOtkazanihTermina = brojacOtkazanihTermina;
            this.pacijent = pacijent;
        }
        public AntiTrollPacijenta(int brojacDodavanihTermina, int brojacIzmenenjenihTermina, 
            int brojacOtkazanihTermina, Pacijent pacijent,DateTime vremeBanovanja,bool daLiJeBanovan)
        {
            this.BrojacDodavanihTermina = brojacDodavanihTermina;
            this.BrojacIzmenenjenihTermina = brojacIzmenenjenihTermina;
            this.BrojacOtkazanihTermina = brojacOtkazanihTermina;
            this.pacijent = pacijent;
            this.VremeBanovanja = vremeBanovanja;
            this.DaLiJeBanovan = daLiJeBanovan;
        }
        public AntiTrollPacijenta()
        {
        }
    }
}
