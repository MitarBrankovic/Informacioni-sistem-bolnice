/***********************************************************************
 * Module:  KartonPacijenta.cs
 * Author:  Brankovic
 * Purpose: Definition of the Class Model.KartonPacijenta
 ***********************************************************************/

using System;
using System.Collections.Generic;

namespace Model
{
    public class KartonPacijenta
    {
        public string Sifra { get; set; }

        public System.Collections.ArrayList alergen;

        /// <pdGenerated>default getter</pdGenerated>
        public System.Collections.ArrayList GetAlergen()
        {
            if (alergen == null)
                alergen = new System.Collections.ArrayList();
            return alergen;
        }

        /// <pdGenerated>default setter</pdGenerated>
        public void SetAlergen(System.Collections.ArrayList newAlergen)
        {
            RemoveAllAlergen();
            foreach (Alergen oAlergen in newAlergen)
                AddAlergen(oAlergen);
        }

        /// <pdGenerated>default Add</pdGenerated>
        public void AddAlergen(Alergen newAlergen)
        {
            if (newAlergen == null)
                return;
            if (this.alergen == null)
                this.alergen = new System.Collections.ArrayList();
            if (!this.alergen.Contains(newAlergen))
                this.alergen.Add(newAlergen);
        }

        /// <pdGenerated>default Remove</pdGenerated>
        public void RemoveAlergen(Alergen oldAlergen)
        {
            if (oldAlergen == null)
                return;
            if (this.alergen != null)
                if (this.alergen.Contains(oldAlergen))
                    this.alergen.Remove(oldAlergen);
        }

        /// <pdGenerated>default removeAll</pdGenerated>
        public void RemoveAllAlergen()
        {
            if (alergen != null)
                alergen.Clear();
        }
        public List<IzvrseniPregled> izvrseniPregled;

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
        public Pacijent pacijent { get; set; }

    }
}