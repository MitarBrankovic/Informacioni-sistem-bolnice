/***********************************************************************
 * Module:  Recept.cs
 * Author:  Nesa
 * Purpose: Definition of the Class Model.Recept
 ***********************************************************************/

using System;

namespace Model
{

   public class Recept
   {
      public Lek Lekovi { get; set; }
      public DateTime DatumIzdavanja { get; set; }
      public int VremenskiPeriodUzimanjaLeka { get; set; }
      public String OpisLeka { get; set; }

    }
}