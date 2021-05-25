using System;

namespace Model
{
    public class Termin
    {
        public DateTime Datum { get; set; }
        public TipTermina TipTermina { get; set; }
        public string SifraTermina { get; set; }
        public Sala sala { get; set; }
        public string Vreme { get; set; }
        public GuestPacijent guestPacijent { get; set; }
        public bool Izvrsen { get; set; }

        public Termin() { }

        public Termin(DateTime datum, TipTermina tipTermina, string sifraTermina, string vreme, Lekar lekar, Pacijent pacijent)
        {
            this.Datum = datum;
            this.TipTermina = tipTermina;
            this.SifraTermina = sifraTermina;
            sala = null;
            this.Vreme = vreme;
            this.lekar = lekar;
            this.pacijent = pacijent;
            guestPacijent = null;
            this.Izvrsen = false;
        }
        public Termin(DateTime datum, TipTermina tipTermina, string sifraTermina, Sala sala,string vreme, Lekar lekar, Pacijent pacijent)
        {
            this.Datum = datum;
            this.TipTermina = tipTermina;
            this.SifraTermina = sifraTermina;
            this.sala = sala;
            this.Vreme = vreme;
            this.lekar = lekar;
            this.pacijent = pacijent;
            guestPacijent = null;
            this.Izvrsen = false;
        }

        public Termin(DateTime datum, TipTermina tipTermina, string sifraTermina, string vreme, Lekar lekar)
        {
            Datum = datum;
            TipTermina = tipTermina;
            SifraTermina = sifraTermina;
            sala = null;
            Vreme = vreme;
            this.lekar = lekar;
            pacijent = null;
            guestPacijent = null;
            Izvrsen = false;
        }
        public Termin(TipTermina tipTermina, string sifraTermina, string vreme)
        {
            TipTermina = tipTermina;
            SifraTermina = sifraTermina;
            sala = null;
            Vreme = vreme;
            lekar = null;
            pacijent = null;
            guestPacijent = null;
            Izvrsen = false;
        }
        public Termin(TipTermina tipTermina, string vreme, DateTime datum,Lekar lekar)
        {
            this.TipTermina = tipTermina;
            this.SifraTermina = null;
            this.sala = null;
            this.Vreme = vreme;
            this.lekar = lekar;
            this.pacijent = null;
            this.guestPacijent = null;
            this.Datum = datum;
            this.Izvrsen = false;
        }


        public override string ToString()
        {
            return Datum.Day + "/" + Datum.Month + "/" + Datum.Year + " " + Vreme;
        }

        public Lekar lekar
        {
            get; set;
        }

        public Lekar GetLekar()
        {
            return lekar;
        }

        public void SetLekar(Lekar newLekar)
        {
            if (this.lekar != newLekar)
            {
                if (this.lekar != null)
                {
                    Lekar oldLekar = this.lekar;
                    this.lekar = null;
                    oldLekar.RemoveTermin(this);
                }
                if (newLekar != null)
                {
                    this.lekar = newLekar;
                    this.lekar.AddTermin(this);
                }
            }
        }

        public Pacijent pacijent { get; set; }

        public Pacijent GetPacijent()
        {
            return pacijent;
        }

        public void SetPacijent(Pacijent newPacijent)
        {
            if (this.pacijent != newPacijent)
            {
                if (this.pacijent != null)
                {
                    Pacijent oldPacijent = this.pacijent;
                    this.pacijent = null;
                    oldPacijent.RemoveTermin(this);
                }
                if (newPacijent != null)
                {
                    this.pacijent = newPacijent;
                    this.pacijent.AddTermin(this);
                }
            }
        }

    }
}