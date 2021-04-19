/***********************************************************************
 * Module:  KartonPacijentaService.cs
 * Author:  Brankovic
 * Purpose: Definition of the Class Service.LekarService.KartonPacijentaService
 ***********************************************************************/

using System;
using Model;

namespace Service.LekarService
{
   public class KartonPacijentaService
   {
      public void BrisanjeKartona(string sifraKartona)
      {
         // TODO: implement
      }
      
      public void IzmenaKartona(KartonPacijenta karton)
      {
         // TODO: implement
      }
      
      public void DodavanjeKartona(KartonPacijenta karton)
      {
         // TODO: implement
      }
   

        public void IzvrsenaAnamneza(IzvrseniPregled izvrseniPregled, KartonPacijenta kartonPacijenta)
        {
            kartonPacijenta.izvrseniPregled.Add(izvrseniPregled);
        }

      public Repository.KartonPacijentaRepository kartonPacijentaRepository;
   
   }
}