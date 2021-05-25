
using Model;
using PrviProgram.Service;
using Service;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Controller
{
    public class PacijentControler
    {
 
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
            // TODO: implement
            return TerminiService.getInstance().PregledTermina(pacijent);
        }
        public List<string> ZauzetiTerminiLekaraDatuma(Lekar lekar, DateTime datumTermina)
        {
            return TerminiService.getInstance().ZauzetiTerminiLekaraDatuma(lekar, datumTermina);
        }
        public bool IzmenaSale(Sala staraSala, Sala novaSala)
        {

            return TerminiService.getInstance().IzmenaSale(staraSala, novaSala);
        }

        public int[] ProveraZauzetostiLekara(string jmbg, DateTime datum, string[] nizSlobodnihTermina)
        {
            return TerminiService.getInstance().ProveraZauzetostiLekara(jmbg, datum, nizSlobodnihTermina);
        }

        public Sala DobavljanjeSale(Termin noviTermin)
        {
            return TerminiService.getInstance().DobavljanjeSale(noviTermin);
        }

        public bool ProveraSale(Sala sala, List<Termin> termini, Termin noviTermin)
        {
            return TerminiService.getInstance().ProveraSale(sala,termini,noviTermin);
        }

        public List<Termin> SviSlobodniTermini(DateTime pocetakDatum, DateTime krajDatum, Lekar selektovaniLekar, TipTermina tipTermina)
        {
            return TerminiService.getInstance().SviSlobodniTermini(pocetakDatum, krajDatum, selektovaniLekar, tipTermina);
        }

        public bool ProveraZauzetostiKodLekara(DateTime pocetakDatum, DateTime krajDatum, Lekar selektovaniLekar)
        {
            
            return TerminiService.getInstance().ProveraZauzetostiKodLekara(pocetakDatum,krajDatum,selektovaniLekar);
        }

        public List<Termin> ProveraVremenaKodLekara(DateTime pocetakDatum, DateTime krajDatum, Lekar selektovaniLekar, TipTermina tipTermina)
        {
 
            return TerminiService.getInstance().ProveraVremenaKodLekara(pocetakDatum,krajDatum,selektovaniLekar,tipTermina);
        }

        public bool ProveraZauzetostiTermina(Termin termin)
        {
            return TerminiService.getInstance().ProveraZauzetostiTermina(termin);
        }

        public List<Termin> ProveraLekaraKodVremena(DateTime pocetakDatum, DateTime krajDatum, Lekar selektovaniLekar, TipTermina tipTermina)
        {
            // TODO: implement
            return TerminiService.getInstance().ProveraLekaraKodVremena( pocetakDatum,  krajDatum,  selektovaniLekar,  tipTermina);
        }

        public Lekar LekarKojiJeZaduzenZaTermin(Termin selektovanitermin)
        {
            return TerminiService.getInstance().LekarKojiJeZaduzenZaTermin(selektovanitermin);
        }

        public void DodavanjeNotifikacije(NotifikacijePacijenta notifikacija)
        {
            notifikacijeService.DodavanjeNotifikacije(notifikacija);
        }

        public List<Recept> PregledRecepata(Pacijent pacijent)
        {
            return notifikacijeService.PregledRecepata(pacijent);
        }

        public string PronadjiOpis(Pacijent pacijent, Recept recept)
        {
          
            return notifikacijeService.PronadjiOpis(recept,pacijent);
        }

        public bool DaLiPostojiBarJedanIzvrsenTermin(Pacijent pacijent)
        {
            // TODO: implement
            return AnketaService.getInstance().DaLiPostojiBarJedanIzvrsenTermin(pacijent);
        }

        public List<Termin> SviTerminiKojiSuIzvrseni(Pacijent pacijent)
        {
            // TODO: implement
            return AnketaService.getInstance().SviTerminiKojiSuIzvrseni(pacijent);
        }

        public void PovecavanjeBrojacaPriDodavanjuTermina(Pacijent pacijent)
        {
            AntiTrollService.getInstance().PovecavanjeBrojacaPriDodavanjuTermina(pacijent);
        }

        public void PovecavanjeBrojacaPriIzmeniTermina(Pacijent pacijent)
        {
            AntiTrollService.getInstance().PovecavanjeBrojacaPriIzmeniTermina(pacijent);
        }

        public void PovecavanjeBrojacaPriOtkazivanjuTermina(Pacijent pacijent)
        {
            AntiTrollService.getInstance().PovecavanjeBrojacaPriOtkazivanjuTermina(pacijent);
        }
        public void DodavanjeBeleske(Beleska beleska)
        {
            BeleskaService.getInstance().DodavanjeBeleske(beleska);
        }
        public Pacijent DodavanjeBeleskeUKarton(Pacijent pacijent, IzvrseniPregled izvrsen)
        {
            return KartonPacijentaService.getInstance().DodavanjeBeleskeUKarton(pacijent, izvrsen);
        }

        public Service.TerminiService terminiService;
        public Service.NotifikacijeService notifikacijeService;
        public Service.AnketaService anketaService;
        public Service.AntiTrollService antiTrollService;

    }
}