using System.Collections.Generic;

namespace Model
{
    public class Pacijent : Osoba
    {
        public List<Termin> termin;

        /// <pdGenerated>default getter</pdGenerated>
        public List<Termin> GetTermin()
        {
            if (termin == null)
                termin = new List<Termin>();
            return termin;
        }

        /// <pdGenerated>default setter</pdGenerated>
        public void SetTermin(System.Collections.ArrayList newTermin)
        {
            RemoveAllTermin();
            foreach (Termin oTermin in newTermin)
                AddTermin(oTermin);
        }

        /// <pdGenerated>default Add</pdGenerated>
        public void AddTermin(Termin newTermin)
        {
            if (newTermin == null)
                return;
            if (this.termin == null)
                this.termin = new List<Termin>();
            if (!this.termin.Contains(newTermin))
            {
                this.termin.Add(newTermin);
                newTermin.SetPacijent(this);
            }
        }

        /// <pdGenerated>default Remove</pdGenerated>
        public void RemoveTermin(Termin oldTermin)
        {
            if (oldTermin == null)
                return;
            if (this.termin != null)
                if (this.termin.Contains(oldTermin))
                {
                    this.termin.Remove(oldTermin);
                    oldTermin.SetPacijent((Pacijent)null);
                }
        }

        /// <pdGenerated>default removeAll</pdGenerated>
        public void RemoveAllTermin()
        {
            if (termin != null)
            {
                System.Collections.ArrayList tmpTermin = new System.Collections.ArrayList();
                foreach (Termin oldTermin in termin)
                    tmpTermin.Add(oldTermin);
                termin.Clear();
                foreach (Termin oldTermin in tmpTermin)
                    oldTermin.SetPacijent((Pacijent)null);
                tmpTermin.Clear();
            }
        }
        public KartonPacijenta kartonPacijenta;

    }
}