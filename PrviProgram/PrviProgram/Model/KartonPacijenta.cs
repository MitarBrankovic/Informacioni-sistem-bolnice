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
      public string Sifra;
      
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


      //public Anamneza anamneza;
      //public Recept recept;
      //public Terapija terapija;
      public Pacijent pacijent;

        public List<Anamneza> anamneza;
        public List<Recept> recept;
        public List<Terapija> terapija;

        /*public System.Collections.ArrayList anamneza;

        /// <pdGenerated>default getter</pdGenerated>
        public System.Collections.ArrayList GetAnamneza()
        {
            if (anamneza == null)
                anamneza = new System.Collections.ArrayList();
            return anamneza;
        }

        /// <pdGenerated>default setter</pdGenerated>
        public void SetAnamneza(System.Collections.ArrayList newAnamneza)
        {
            RemoveAllAnamneza();
            foreach (Anamneza oAnamneza in newAnamneza)
                AddAnamneza(oAnamneza);
        }

        /// <pdGenerated>default Add</pdGenerated>
        public void AddAnamneza(Anamneza newAnamneza)
        {
            if (newAnamneza == null)
                return;
            if (this.anamneza == null)
                this.anamneza = new System.Collections.ArrayList();
            if (!this.anamneza.Contains(newAnamneza))
                this.anamneza.Add(newAnamneza);
        }

        /// <pdGenerated>default Remove</pdGenerated>
        public void RemoveAnamneza(Anamneza oldAnamneza)
        {
            if (oldAnamneza == null)
                return;
            if (this.anamneza != null)
                if (this.anamneza.Contains(oldAnamneza))
                    this.anamneza.Remove(oldAnamneza);
        }

        /// <pdGenerated>default removeAll</pdGenerated>
        public void RemoveAllAnamneza()
        {
            if (anamneza != null)
                anamneza.Clear();
        }
        




        public System.Collections.ArrayList terapija;

        /// <pdGenerated>default getter</pdGenerated>
        public System.Collections.ArrayList GetTerapija()
        {
            if (terapija == null)
                terapija = new System.Collections.ArrayList();
            return terapija;
        }

        /// <pdGenerated>default setter</pdGenerated>
        public void SetTerapija(System.Collections.ArrayList newTerapija)
        {
            RemoveAllTerapija();
            foreach (Terapija oTerapija in newTerapija)
                AddTerapija(oTerapija);
        }

        /// <pdGenerated>default Add</pdGenerated>
        public void AddTerapija(Terapija newTerapija)
        {
            if (newTerapija == null)
                return;
            if (this.terapija == null)
                this.terapija = new System.Collections.ArrayList();
            if (!this.terapija.Contains(newTerapija))
                this.terapija.Add(newTerapija);
        }

        /// <pdGenerated>default Remove</pdGenerated>
        public void RemoveTerapija(Terapija oldTerapija)
        {
            if (oldTerapija == null)
                return;
            if (this.terapija != null)
                if (this.terapija.Contains(oldTerapija))
                    this.terapija.Remove(oldTerapija);
        }

        /// <pdGenerated>default removeAll</pdGenerated>
        public void RemoveAllTerapija()
        {
            if (terapija != null)
                terapija.Clear();
        }


        public System.Collections.ArrayList recept;

        /// <pdGenerated>default getter</pdGenerated>
        public System.Collections.ArrayList GetRecept()
        {
            if (recept == null)
                recept = new System.Collections.ArrayList();
            return recept;
        }

        /// <pdGenerated>default setter</pdGenerated>
        public void SetRecpet(System.Collections.ArrayList newRecept)
        {
            RemoveAllRecept();
            foreach (Recept oRecept in newRecept)
                AddRecept(oRecept);
        }

        /// <pdGenerated>default Add</pdGenerated>
        public void AddRecept(Recept newRecept)
        {
            if (newRecept == null)
                return;
            if (this.recept == null)
                this.recept = new System.Collections.ArrayList();
            if (!this.recept.Contains(newRecept))
                this.recept.Add(newRecept);
        }

        /// <pdGenerated>default Remove</pdGenerated>
        public void RemoveRecept(Recept oldRecept)
        {
            if (oldRecept == null)
                return;
            if (this.recept != null)
                if (this.recept.Contains(oldRecept))
                    this.recept.Remove(oldRecept);
        }

        /// <pdGenerated>default removeAll</pdGenerated>
        public void RemoveAllRecept()
        {
            if (recept != null)
                recept.Clear();
        }*/
    }
}