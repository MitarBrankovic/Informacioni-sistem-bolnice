/***********************************************************************
 * Module:  TerminiService.cs
 * Author:  Nesa
 * Purpose: Definition of the Class Service.LogikaLekar.TerminiService
 ***********************************************************************/
using Model;
using PrviProgram.Service;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Controller
{
    public class LekarController
   {
        private PrimedbeNaLekService primedbeNaLekService = new PrimedbeNaLekService();
        private PacijentRepository pacijentRepository = new PacijentRepository();
        private LekoviRepository lekoviRepository = new LekoviRepository();
        private List<string> constVreme = new List<string>() {  "08:00:00", "08:30:00", "09:00:00", "09:30:00", "10:00:00", "10:30:00", "11:00:00", "11:30:00", "12:00:00"
                                                                , "12:30:00", "13:00:00", "13:30:00", "14:00:00", "14:30:00", "15:00:00", "15:30:00", "16:00:00", "16:30:00"
                                                                , "17:00:00", "17:30:00", "18:00:00", "18:30:00", "19:00:00", "19:30:00" };
        private TerminiService terminiService = new TerminiService();


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

        public void KreiranjePrimedbe(PrimedbaNaLek primedbaNaLek)
        {
            primedbeNaLekService.KreiranjePrimedbe(primedbaNaLek);
        }
   
        
        
        public KartonPacijentaService kartonPacijentaService;

        public ObservableCollection<string> DobavljanjeSlobodnihTerminaLekara(Lekar lekar, DateTime datumTermina)
        {
            List<string> zauzetiTermini = terminiService.ZauzetiTerminiLekaraDatuma(lekar, datumTermina);
            ObservableCollection<string> slobodniTermini = new ObservableCollection<string>(constVreme);
            foreach (String vremeT in constVreme)
            {
                foreach (String zauzetiT in zauzetiTermini)
                {
                    if (vremeT.Equals(zauzetiT))
                    {
                        slobodniTermini.Remove(vremeT);
                    }
                }
            }
            return slobodniTermini;
        }

        public ObservableCollection<String> DobavljanjeSlobodnihTerminaLekaraZaIzmenuTermina(Lekar lekar, DateTime datumTermina, Termin termin)
        {
            List<string> zauzetiTermini = terminiService.ZauzetiTerminiLekaraDatuma(lekar, datumTermina);
            ObservableCollection<string> slobodniTermini = new ObservableCollection<string>(constVreme);
            foreach (String vremeT in constVreme)
            {
                foreach (String zauzetiT in zauzetiTermini)
                {
                    if (vremeT.Equals(zauzetiT) && termin.Vreme != zauzetiT)
                    {
                        slobodniTermini.Remove(vremeT);
                    }
                }
            }
            return slobodniTermini;
        }
   
        public bool PacijentAlergicanNaLek(Pacijent prosledjeniPacijent, Lek lek)
        {
            Pacijent pacijent = pacijentRepository.PregledPacijenta(prosledjeniPacijent.Jmbg);
            List<String> sastojci = lekoviRepository.DobavljanjeSastojakaLeka(lek);
            if(pacijent.kartonPacijenta.alergen != null)
            {
                foreach (Alergen alergen in pacijent.kartonPacijenta.alergen)
                {
                    if (sastojci.Contains(alergen.Naziv.ToLower()))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
   }
}