/***********************************************************************
 * Module:  TerminPremestanjaOpreme.cs
 * Author:  Darko
 * Purpose: Definition of the Class Model.TerminPremestanjaOpreme
 ***********************************************************************/

using System;

namespace Model
{
   public class TerminPremestanjaOpreme
   {
      /*public System.Collections.ArrayList sala;
      
      /// <pdGenerated>default getter</pdGenerated>
      public System.Collections.ArrayList GetSala()
      {
         if (sala == null)
            sala = new System.Collections.ArrayList();
         return sala;
      }
      
      /// <pdGenerated>default setter</pdGenerated>
      public void SetSala(System.Collections.ArrayList newSala)
      {
         RemoveAllSala();
         foreach (Sala oSala in newSala)
            AddSala(oSala);
      }
      
      /// <pdGenerated>default Add</pdGenerated>
      public void AddSala(Sala newSala)
      {
         if (newSala == null)
            return;
         if (this.sala == null)
            this.sala = new System.Collections.ArrayList();
         if (!this.sala.Contains(newSala))
            this.sala.Add(newSala);
      }
      
      /// <pdGenerated>default Remove</pdGenerated>
      public void RemoveSala(Sala oldSala)
      {
         if (oldSala == null)
            return;
         if (this.sala != null)
            if (this.sala.Contains(oldSala))
               this.sala.Remove(oldSala);
      }
      
      /// <pdGenerated>default removeAll</pdGenerated>
      public void RemoveAllSala()
      {
         if (sala != null)
            sala.Clear();
      }*/
      public Oprema oprema { get; set; }

      public Sala sala { get; set; }
      public Sala staraSala { get; set; }
   
      public DateTime datumPremestaja { get; set; }
   
   }
}