using System;
using System.Collections.Generic;

namespace Model
{
    public class KartonPacijenta
    {
        public string Sifra { get; set; }

        public List<Alergen> alergen;
       
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

        public List<IzvrseniPregled> izvrseniPregled = new List<IzvrseniPregled>();

        public void AzurirajIzvrseniPregled(IzvrseniPregled izvrseni)
        {
            foreach(IzvrseniPregled ip in izvrseniPregled)
            {
                if(ip.Sifra == izvrseni.Sifra)
                {
                    ip.recept = izvrseni.recept;
                   
                    ip.anamneza = izvrseni.anamneza;
                }
            }
        }

        /// <pdGenerated>default getter</pdGenerated>
        public List<IzvrseniPregled> GetIzvrseniPregled()
        {
            if (izvrseniPregled == null)
                izvrseniPregled = new List<IzvrseniPregled>();
            return izvrseniPregled;
        }

        /// <pdGenerated>default setter</pdGenerated>
        public void SetIzvrseniPregled(List<IzvrseniPregled> newIzvrseniPregled)
        {
            RemoveAllIzvrseniPregled();
            foreach (IzvrseniPregled oIzvrseniPregled in newIzvrseniPregled)
                AddIzvrseniPregled(oIzvrseniPregled);
        }

        /// <pdGenerated>default Add</pdGenerated>
        public void AddIzvrseniPregled(IzvrseniPregled newIzvrseniPregled)
        {
            if (newIzvrseniPregled == null)
                return;
            if (this.izvrseniPregled == null)
                this.izvrseniPregled = new List<IzvrseniPregled>();
            if (!this.izvrseniPregled.Contains(newIzvrseniPregled))
                this.izvrseniPregled.Add(newIzvrseniPregled);
        }

        /// <pdGenerated>default Remove</pdGenerated>
        public void RemoveIzvrseniPregled(IzvrseniPregled oldIzvrseniPregled)
        {
            if (oldIzvrseniPregled == null)
                return;
            if (this.izvrseniPregled != null)
                if (this.izvrseniPregled.Contains(oldIzvrseniPregled))
                    this.izvrseniPregled.Remove(oldIzvrseniPregled);
        }

        /// <pdGenerated>default removeAll</pdGenerated>
        public void RemoveAllIzvrseniPregled()
        {
            if (izvrseniPregled != null)
                izvrseniPregled.Clear();
        }

    }
}