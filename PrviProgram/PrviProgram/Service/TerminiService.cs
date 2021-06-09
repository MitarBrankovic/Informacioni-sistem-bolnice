using Model;
using PrviProgram.Repository;
using Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Service
{
    public class TerminiService
    {
        private TerminiRenoviranjaRepository terminiRenoviranjaRepository = new TerminiRenoviranjaRepository();
        private TerminiRepository terminiRepository = new TerminiRepository();
        private TerminiLekarService terminiLekarService;
        private LekarRepository lekarRepository = new LekarRepository();
        public List<Termin> terminiSlobodni = new List<Termin>();
        private UtilityService utilityService = new UtilityService();
       
        private List<Termin> termini = new List<Termin>();

        private static TerminiService instance = null;
        public Lekar lekar = new Lekar();
        public static TerminiService getInstance()
        {
            if (instance == null)
            {
                instance = new TerminiService();
            }
            return instance;
        }
        public void DodavanjeTermina(Termin termin)
        {
            List<Termin> termini = terminiRepository.CitanjeIzFajla();
            termini.Add(termin);
            terminiRepository.UpisivanjeUFajl(termini);
        }

        public void BrisanjeTermina(Termin selektovaniTermin)
        {
            List<Termin> termini = terminiRepository.CitanjeIzFajla();
            foreach (Termin termin in termini)
            {
                if (termin.SifraTermina.Equals(selektovaniTermin.SifraTermina) && !termin.Izvrsen)
                {
                    termini.Remove(termin);
                    terminiRepository.UpisivanjeUFajl(termini);
                    break;
                }
            }
        }
        public bool IzmenaTermina(Termin termin)
        {
            List<Termin> termini = terminiRepository.CitanjeIzFajla();
            foreach (Termin t in termini)
            {
                if (t.SifraTermina.Equals(termin.SifraTermina))
                {
                    termini.Remove(t);
                    termini.Add(termin);
                    terminiRepository.UpisivanjeUFajl(termini);
                    return true;
                }
            }
            return false;
        }

        public List<Termin> SviSlobodniTermini(DateTime pocetakDatum, DateTime krajDatum, Lekar selektovaniLekar,TipTermina tipTermina)
        { 
            for(int i = 0;i <= (int)(krajDatum - pocetakDatum).TotalDays; i++)
            {
                for(int j=0;j< utilityService.nizVremena.Count; j++)
                {

                   if (SlobodnoVreme(pocetakDatum) != null)
                   {
                      Termin termin = new Termin(tipTermina, SlobodnoVreme(pocetakDatum), pocetakDatum, selektovaniLekar);
                      terminiSlobodni.Add(termin);
                   }
                }
                
                pocetakDatum = pocetakDatum.AddDays(+1);
            }
            return terminiSlobodni;
        }
        public string SlobodnoVreme(DateTime pocetakDatum)
        {
            foreach (string vreme in utilityService.nizVremena)
            {
                if (ProveraTermina(pocetakDatum, vreme))
                {
                    return vreme;
                }
            }
            return null;
        }

        public bool ProveraTermina(DateTime datum, string vreme)
        {
            List<Termin> termini = terminiRepository.PregledSvihTermina();
            foreach (Termin termin in termini)
            {
                if (termin.Datum.Equals(datum) && termin.Vreme.Equals(vreme))
                {
                    return false;
                }
            }
            return true;

        }

        //REFAKTORISATI
        public List<Termin> ProveraVremenaKodLekara(DateTime pocetakDatum, DateTime krajDatum, Lekar selektovaniLekar, TipTermina tipTermina)
        {
            List<Termin> slobodniTermini = new List<Termin>();
            TimeSpan razlikaDanasnjegDanaiMin = pocetakDatum - DateTime.Today;

            if (razlikaDanasnjegDanaiMin.TotalDays == 0)
            {
                DateTime maxOd = krajDatum.AddDays(1);
                DateTime maxDo = krajDatum.AddDays(2);
                slobodniTermini = SviSlobodniTermini(maxOd, maxDo, selektovaniLekar, tipTermina);
            }
            else if (razlikaDanasnjegDanaiMin.TotalDays == 1)
            {
                DateTime minOd = pocetakDatum.AddDays(-1);
                DateTime minDo = pocetakDatum.AddDays(-1);
                DateTime maxOd = krajDatum.AddDays(1);
                DateTime maxDo = krajDatum.AddDays(2);
                slobodniTermini = SviSlobodniTermini(minOd, minDo, selektovaniLekar, tipTermina);
                slobodniTermini = SviSlobodniTermini(maxOd, maxDo, selektovaniLekar, tipTermina);
            }
            else
            {
                DateTime minOd = pocetakDatum.AddDays(-2);
                DateTime minDo = pocetakDatum.AddDays(-1);
                DateTime maxOd = krajDatum.AddDays(1);
                DateTime maxDo = krajDatum.AddDays(2);
                slobodniTermini = SviSlobodniTermini(minOd, minDo, selektovaniLekar, tipTermina);
                slobodniTermini = SviSlobodniTermini(maxOd, maxDo, selektovaniLekar, tipTermina);

            }

            return this.terminiSlobodni;
        }
        public List<Termin> ProveraLekaraKodVremena(DateTime pocetakDatum, DateTime krajDatum, Lekar selektovaniLekar, TipTermina tipTermina)
        {
            List<Lekar> lekari = lekarRepository.PregledSvihLekara();
            foreach (Lekar lekar in lekari)
            {
                if (!lekar.Jmbg.Equals(selektovaniLekar.Jmbg) && terminiLekarService.ProveraZauzetostiKodLekara(pocetakDatum, krajDatum, lekar))
                {
                    termini = SviSlobodniTermini(pocetakDatum, krajDatum, lekar, tipTermina);
                }
            }
            return this.terminiSlobodni;
        }

        public bool ProveraZauzetostiTermina(Termin selektovaniTermin)
        {
            List<Termin> termini = terminiRepository.CitanjeIzFajla();
            foreach (Termin termin in termini)
            {
                if (termin.lekar.Jmbg.Equals(selektovaniTermin.lekar.Jmbg) && termin.Datum.Date.Equals(selektovaniTermin.Datum.Date) && termin.Vreme.Equals(selektovaniTermin.Vreme))
                {
                    return true;
                }
            }
            return false;
        }

    }
 }
