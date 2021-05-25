using System.Collections.Generic;

namespace Model
{
    public class Pacijent : Osoba
    {
        public List<Termin> Termin;
        public KartonPacijenta KartonPacijenta { get; set; }

        public Pacijent() { }

        public Pacijent(Osoba osoba, List<Termin> termini, KartonPacijenta kartonPacijenta) : base(osoba)
        {
            Termin = termini;
            this.KartonPacijenta = kartonPacijenta;
        }

        public List<Termin> GetTermin()
        {
            if (Termin == null)
                Termin = new List<Termin>();
            return Termin;
        }

        public void SetTermin(List<Termin> newTermin)
        {
            RemoveAllTermin();
            foreach (Termin oTermin in newTermin)
                AddTermin(oTermin);
        }

        public void AddTermin(Termin newTermin)
        {
            if (newTermin == null)
                return;
            if (this.Termin == null)
                this.Termin = new List<Termin>();
            if (!this.Termin.Contains(newTermin))
            {
                this.Termin.Add(newTermin);
                newTermin.SetPacijent(this);
            }
        }

        public void RemoveTermin(Termin oldTermin)
        {
            if (oldTermin == null)
                return;
            if (this.Termin != null)
                if (this.Termin.Contains(oldTermin))
                {
                    this.Termin.Remove(oldTermin);
                    oldTermin.SetPacijent((Pacijent)null);
                }
        }

        public void RemoveAllTermin()
        {
            if (Termin != null)
            {
                List<Termin> tmpTermin = new List<Termin>();
                foreach (Termin oldTermin in Termin)
                    tmpTermin.Add(oldTermin);
                Termin.Clear();
                foreach (Termin oldTermin in tmpTermin)
                    oldTermin.SetPacijent((Pacijent)null);
                tmpTermin.Clear();
            }
        }

        public override string ToString()
        {
            return Ime + " " + Prezime + " ";
        }

    }
}