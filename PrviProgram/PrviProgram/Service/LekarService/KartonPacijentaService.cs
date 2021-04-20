/***********************************************************************
 * Module:  KartonPacijentaService.cs
 * Author:  Brankovic
 * Purpose: Definition of the Class Service.LekarService.KartonPacijentaService
 ***********************************************************************/

using System;
using System.Collections.Generic;
using Controller;
using Model;
using Repository;
using Service.SekretarService;

namespace Service.LekarService
{
   public class KartonPacijentaService
   {


        private PacijentiService pacijentiService = new PacijentiService();
        private PacijentRepository pacijentRepository = new PacijentRepository();

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
      }
      
      public void IzmenaKartona(KartonPacijenta karton)
      {
         // TODO: implement
      }
      
      public void DodavanjeKartona(KartonPacijenta karton)
      {
         // TODO: implement
      }
   

        public void IzvrsenaAnamneza(IzvrseniPregled izvrseniPregled, Pacijent pacijent)
        {
            
            Pacijent pacijentStari = pacijent;
            Pacijent pacijentNovi = pacijent;
            //pacijentNovi.kartonPacijenta=pacijentRepository.PregledKartona(pacijentNovi.Jmbg);
            pacijentNovi.kartonPacijenta.izvrseniPregled.Add(izvrseniPregled);
            //termin.pacijent.kartonPacijenta.izvrseniPregled.Add(izvrseniPregled);

            pacijentiService.IzmenaPacijenta(pacijentStari, pacijentNovi);
            //PreglediService.getInstance().IzmenaPregleda(termin);

        }

      public Repository.KartonPacijentaRepository kartonPacijentaRepository;
   
   }
}