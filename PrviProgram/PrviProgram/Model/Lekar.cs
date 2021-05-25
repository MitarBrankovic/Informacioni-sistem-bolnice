using System.Collections.Generic;

namespace Model
{
    public class Lekar : Osoba
    {

        public List<Termin> termin;
        public List<Specijalizacija> specijalizacija;

        public Lekar() { }
        public Lekar(string ime, string prezime)
        {
            Ime = ime;
            Prezime = prezime;
        }

        public Lekar(Osoba osoba, List<Termin> termini, List<Specijalizacija> specijalizacije) : base(osoba)
        {
            specijalizacija = specijalizacije;
            termin = termini;
        }

        public List<Termin> GetTermin()
        {
            if (termin == null)
                termin = new List<Termin>();
            return termin;
        }

        public void SetTermin(List<Termin>  newTermin)
        {
            RemoveAllTermin();
            foreach (Termin oTermin in newTermin)
                AddTermin(oTermin);
        }

        public void AddTermin(Termin newTermin)
        {
            if (newTermin == null)
                return;
            if (this.termin == null)
                this.termin = new List<Termin>();
            if (!this.termin.Contains(newTermin))
            {
                this.termin.Add(newTermin);
                newTermin.SetLekar(this);
            }
        }

        public void RemoveTermin(Termin oldTermin)
        {
            if (oldTermin == null)
                return;
            if (this.termin != null)
                if (this.termin.Contains(oldTermin))
                {
                    this.termin.Remove(oldTermin);
                    oldTermin.SetLekar((Lekar)null);
                }
        }

        public void RemoveAllTermin()
        {
            if (termin != null)
            {
                List<Termin> tmpTermin = new List<Termin>();
                foreach (Termin oldTermin in termin)
                    tmpTermin.Add(oldTermin);
                termin.Clear();
                foreach (Termin oldTermin in tmpTermin)
                    oldTermin.SetLekar((Lekar)null);
                tmpTermin.Clear();
            }
        }

        public List<Specijalizacija> GetSpecijalizacija()
        {
            if (specijalizacija == null)
                specijalizacija = new List<Specijalizacija>();
            return specijalizacija;
        }

        public void SetSpecijalizacija(List<Specijalizacija> newSpecijalizacija)
        {
            RemoveAllSpecijalizacija();
            foreach (Specijalizacija oSpecijalizacija in newSpecijalizacija)
                AddSpecijalizacija(oSpecijalizacija);
        }

        public void AddSpecijalizacija(Specijalizacija newSpecijalizacija)
        {
            if (newSpecijalizacija == null)
                return;
            if (this.specijalizacija == null)
                this.specijalizacija = new List<Specijalizacija>();
            if (!this.specijalizacija.Contains(newSpecijalizacija))
                this.specijalizacija.Add(newSpecijalizacija);
        }

        public void RemoveSpecijalizacija(Specijalizacija oldSpecijalizacija)
        {
            if (oldSpecijalizacija == null)
                return;
            if (this.specijalizacija != null)
                if (this.specijalizacija.Contains(oldSpecijalizacija))
                    this.specijalizacija.Remove(oldSpecijalizacija);
        }

        public void RemoveAllSpecijalizacija()
        {
            if (specijalizacija != null)
                specijalizacija.Clear();
        }

        public override string ToString()
        {
            return "dr. " + Ime + " " + Prezime + " ";
        }

    }
}