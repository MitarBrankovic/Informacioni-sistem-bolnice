using System.Collections.Generic;

namespace Model
{
    public class KartonPacijenta
    {
        public string Sifra { get; set; }
        public List<Alergen> alergen;
        public Anamneza anamneza;
        public Recept recept;
        public Terapija terapija;
        public Pacijent pacijent { get; set; }

        public List<Alergen> GetAlergen()
        {
            if (alergen == null)
                alergen = new List<Alergen>();
            return alergen;
        }

        public void SetAlergen(List<Alergen> newAlergen)
        {
            RemoveAllAlergen();
            foreach (Alergen oAlergen in newAlergen)
                AddAlergen(oAlergen);
        }

        public void AddAlergen(Alergen newAlergen)
        {
            if (newAlergen == null)
                return;
            if (this.alergen == null)
                this.alergen = new List<Alergen>();
            if (!this.alergen.Contains(newAlergen))
                this.alergen.Add(newAlergen);
        }

        public void RemoveAlergen(Alergen oldAlergen)
        {
            if (oldAlergen == null)
                return;
            if (this.alergen != null)
                if (this.alergen.Contains(oldAlergen))
                    this.alergen.Remove(oldAlergen);
        }

        public void RemoveAllAlergen()
        {
            if (alergen != null)
                alergen.Clear();
        }

    }
}