/***********************************************************************
 * Module:  TerminiService.cs
 * Author:  Nesa
 * Purpose: Definition of the Class Service.LogikaLekar.TerminiService
 ***********************************************************************/
using Model;
using System;

namespace Controller
{
   public class LekarController
   {
      public void BrisanjePregleda(string sifraTermina)
      {
         // TODO: implement
      }
      
      public void IzmenaPregleda(Termin termin)
      {
         // TODO: implement
      }
      
      public void DodavanjePregleda(Termin termin)
      {
         // TODO: implement
      }
      
      public bool IzmenaSale(Sala staraSala, Sala novaSala)
      {
         // TODO: implement
         return false;
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
   
      public Service.LekarService.KartonPacijentaService kartonPacijentaService;
   
   }
}