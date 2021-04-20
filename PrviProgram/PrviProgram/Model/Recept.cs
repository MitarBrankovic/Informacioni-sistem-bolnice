/***********************************************************************
 * Module:  Recept.cs
 * Author:  Brankovic
 * Purpose: Definition of the Class Model.Recept
 ***********************************************************************/

using System;

namespace Model
{
   public class Recept
   {
      public String Lekovi { get; set; }
      public DateTime DatumIzdavanja { get; set; }
      public int VremenskiPeriodUzimanjaLeka { get; set; }
      public String OpisLeka { get; set; }
      


    }
}