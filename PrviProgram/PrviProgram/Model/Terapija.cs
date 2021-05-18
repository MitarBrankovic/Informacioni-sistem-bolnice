/***********************************************************************
 * Module:  Terapija.cs
 * Author:  Brankovic
 * Purpose: Definition of the Class Model.Terapija
 ***********************************************************************/

using System;

namespace Model
{
   public class Terapija
   {
      public string Opis { get; set; }
      public DateTime Datum { get; set; }

        public Terapija(String opis, DateTime date)
        {
            Opis = opis;
            Datum = date;
        }
    }
}