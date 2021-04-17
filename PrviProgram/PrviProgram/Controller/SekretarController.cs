/***********************************************************************
 * Module:  UpravljanjeSalama.cs
 * Author:  Saska
 * Purpose: Definition of the Class Logika.LogikaLekar.UpravljanjeSalama
 ***********************************************************************/

using System;
using Model;

namespace Controller
{
   public class SekretarController
   {
      public bool DodavanjePacijenta(Pacijent pacijent)
      {
         // TODO: implement
         return false;
      }
      
      public bool BrisanjePacijenta(Pacijent p)
      {
         // TODO: implement
         return false;
      }
      
      public bool IzmenaPacijenta(Pacijent stari, Pacijent novi)
      {
         // TODO: implement
         return false;
      }
      
      public bool DodavanjeAlergena(Alergen alergen)
      {
         // TODO: implement
         return false;
      }
      
      public bool BrisanjeAlergena(Alergen alergen)
      {
         // TODO: implement
         return false;
      }
      
      public bool IzmenaAlergena(Alergen alergen)
      {
         // TODO: implement
         return false;
      }
      
      public bool ZakazivanjeTermina(Termin termin)
      {
         // TODO: implement
         return false;
      }
      
      public bool DodavanjeGuestPacijenta(GuestPacijent guestPacijent)
      {
         // TODO: implement
         return false;
      }
      
      public bool BrisanjeGuestPacijenta(GuestPacijent guestPacijent)
      {
         // TODO: implement
         return false;
      }
      
      public bool IzmenaGuestPacijenta(GuestPacijent stari, GuestPacijent novi)
      {
         // TODO: implement
         return false;
      }
   
      public Service.LekarService.PreglediService terminiService;
      public Service.SekretarService.GuestPacijentiService guestPacijentiService;
      public Service.SekretarService.PacijentiService pacijentiService;
      public Service.SekretarService.AlergeniService alergeniService;
   
   }
}