
using Model;
using PrviProgram.Repository;
using PrviProgram.Service;
using Service;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Controller
{
    public class PacijentControler
    {
        private TerminiRepository terminiRepository = new TerminiRepository();
        public void DodavanjeTermina(Termin termin)
        {
            TerminiService.getInstance().DodavanjeTermina(termin);
        }
        public void BrisanjeTermina(Termin termin)
        {
            TerminiService.getInstance().BrisanjeTermina(termin);
        }
        public bool IzmenaTermina(Termin termin)
        {
            return TerminiService.getInstance().IzmenaTermina(termin);
        }
        public List<Termin> PregledTermina(Pacijent pacijent)
        { 
            return terminiRepository.PregledTerminaPacijenta(pacijent);
        }
        public List<Termin> SviSlobodniTermini(DateTime pocetakDatum, DateTime krajDatum, Lekar selektovaniLekar, TipTermina tipTermina)
        {
            return TerminiService.getInstance().SviSlobodniTermini(pocetakDatum, krajDatum, selektovaniLekar, tipTermina);
        }
        public List<Termin> ProveraVremenaKodLekara(DateTime pocetakDatum, DateTime krajDatum, Lekar selektovaniLekar, TipTermina tipTermina)
        {
            return TerminiService.getInstance().ProveraVremenaKodLekara(pocetakDatum,krajDatum,selektovaniLekar,tipTermina);
        }
        public List<Termin> ProveraLekaraKodVremena(DateTime pocetakDatum, DateTime krajDatum, Lekar selektovaniLekar, TipTermina tipTermina)
        {
            return TerminiService.getInstance().ProveraLekaraKodVremena(pocetakDatum, krajDatum, selektovaniLekar, tipTermina);
        }
        public bool ProveraZauzetostiTermina(Termin termin)
        {
            return TerminiService.getInstance().ProveraZauzetostiTermina(termin);
        }
        public bool DaLiJePregledVecAnketiran(Termin termin)
        {
            return anketaService.DaLiJePregledVecAnketiran(termin);
        }
        public void DodavanjeTerapije(Terapija terapija)
        {
            terapijaService.DodavanjeTerapije(terapija);
        }


        public TerapijaService terapijaService=new TerapijaService();
        public Service.TerminiService terminiService;
        public Service.NotifikacijeService notifikacijeService;
        public Service.AnketaService anketaService;
        public Service.AntiTrollService antiTrollService;

    }
}