/***********************************************************************
 * Module:  TerminRenoviranjaSale.cs
 * Author:  Brankovic
 * Purpose: Definition of the Class Model.TerminRenoviranjaSale
 ***********************************************************************/

using System;

namespace Model
{
   public class TerminRenoviranjaSale
   {
      public DateTime PocetakRenoviranja { get; set; }
      public DateTime KrajRenoviranja { get; set; }

      public Sala sala { get; set; }

    }
}