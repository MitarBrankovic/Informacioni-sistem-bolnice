/***********************************************************************
 * Module:  UpravljanjeTerminima.cs
 * Author:  Saska
 * Purpose: Definition of the Class Service.LogikaPacijent.UpravljanjeTerminima
 ***********************************************************************/
using Model;
using Service;
using System;
using System.Collections.Generic;

namespace Controller
{
    public class PacijentController
   {
      public void DodavanjeTermina(Termin termin, Pacijent p)
      {
         // TODO: implement
      }
      
      public void BrisanjeTermina(Termin t, Pacijent p)
      {
         // TODO: implement
      }
      
      public bool IzmenaTermina(Termin t, Pacijent p)
      {
         // TODO: implement
         return false;
      }
      
      public List<Termin> PregledTermina(Pacijent p)
      {
         // TODO: implement
         return null;
      }
      
      public bool IzmenaSale(Sala staraSala, Sala novaSala)
      {
         // TODO: implement
         return false;
      }
   
      public TerminiService terminiService;
   
   }
}