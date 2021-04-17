/***********************************************************************
 * Module:  KartonPacijenta.cs
 * Author:  Brankovic
 * Purpose: Definition of the Class Model.KartonPacijenta
 ***********************************************************************/

using System;

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
      public Anamneza anamneza;
      public Recept recept;
      public Terapija terapija;
      public Pacijent pacijent;
   
   }
}