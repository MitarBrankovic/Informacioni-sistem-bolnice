/***********************************************************************
 * Module:  NotifikacijeService.cs
 * Author:  Saska WorkPC
 * Purpose: Definition of the Class Service.PacijentService.NotifikacijeService
 ***********************************************************************/

using Model;
using Repository;
using System;
using System.Collections.Generic;

namespace Service.PacijentService
{
   public class NotifikacijeService
   {
      public void DodavanjeNotifikacije(List<NotifikacijePacijenta> notifikacija)
      {

            NotifikacijeObavestenjaRepository datoteka = new NotifikacijeObavestenjaRepository();
            datoteka.UpisivanjeUFajl(notifikacija);

      }
      
      public void IzmeniNotifikaciju(KartonPacijenta karton)
      {
         
      }
   
      public List<NotifikacijePacijenta> notifikacijeObavestenjaRepository;
      
      /// <pdGenerated>default getter</pdGenerated>
      
   }
}