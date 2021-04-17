/***********************************************************************
 * Module:  GuestPacijent.cs
 * Author:  Darko
 * Purpose: Definition of the Class Model.GuestPacijent
 ***********************************************************************/

using System;

namespace Model
{
   public class GuestPacijent
   {
      public string Ime;
      public string Prezime;
      public string KontaktTelefon;
      public string Id;
      
      public Termin[] termin;
   
   }
}