/***********************************************************************
 * Module:  KartonPacijentaService.cs
 * Author:  Brankovic
 * Purpose: Definition of the Class Service.LekarService.KartonPacijentaService
 ***********************************************************************/

using System;
using System.Collections.Generic;
using Model;
using PrviProgram.Izgled.Sekretar;
using Service.SekretarService;

namespace Service.LekarService
{
   public class KartonPacijentaService
   {


        private PacijentiService pacijentiService;

        private static KartonPacijentaService instance = null;
        public static KartonPacijentaService getInstance()
        {
            if (instance == null)
            {
                instance = new KartonPacijentaService();
            }
            return instance;
        }

        public void BrisanjeKartona(string sifraKartona)
      {
            // TODO: implement
            pacijentiService = new PacijentiService();
      }
      
      public void IzmenaKartona(KartonPacijenta karton)
      {
         // TODO: implement
      }
      
      public void DodavanjeKartona(KartonPacijenta karton)
      {
         // TODO: implement
      }
   

        public void IzvrsenaAnamneza(IzvrseniPregled izvrseniPregled, Termin termin)
        {
            
            Pacijent pacijentStari = termin.pacijent;
            Pacijent pacijentNovi = termin.pacijent;
            pacijentNovi.kartonPacijenta.izvrseniPregled.Add(izvrseniPregled);
            termin.pacijent.kartonPacijenta.izvrseniPregled.Add(izvrseniPregled);

            pacijentiService.IzmenaPacijenta(pacijentStari, pacijentNovi);
            PreglediService.getInstance().IzmenaPregleda(termin);

        }

      public Repository.KartonPacijentaRepository kartonPacijentaRepository;
   
   }
}