
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

        public int[] ProveraZauzetostiLekara(Lekar lekar, DateTime datum, string[] niz)
        {
            // TODO: implement
            return null;
        }

        public Sala DobavljanjeSale(Termin noviTermin)
        {
            // TODO: implement
            return null;
        }

        public bool ProveraSale(Sala sala, List<Termin> termini, Termin noviTermin)
        {
            // TODO: implement
            return false;
        }

        public List<Termin> SviSlobodniTermini(DateTime min, DateTime max, Lekar selektovaniLekar, string tipeTermina)
        {
            // TODO: implement
            return null;
        }

        public bool ProveraTermina(DateTime datum, string vreme)
        {
            // TODO: implement
            return false;
        }

        public bool ProveraZauzetostiKodLekara(DateTime min, DateTime max, Lekar selektovaniLekar)
        {
            // TODO: implement
            return false;
        }

        public List<Termin> ProveraVremenaKodLekara(DateTime min, DateTime max, Lekar selektovaniLekar, string tipTermina)
        {
            // TODO: implement
            return null;
        }

        public bool ProveraZauzetostiTermina(Termin termin)
        {
            // TODO: implement
            return false;
        }

        public List<Termin> ProveraLekaraKodVremena(DateTime min, DateTime max, Lekar selektovaniLekar, string tipTermina)
        {
            // TODO: implement
            return null;
        }

        public Lekar LekarKojiJeZaduzenZaTermin(Termin selektovanitermin)
        {
            // TODO: implement
            return null;
        }

        public void DodavanjeNotifikacije(KartonPacijenta karton)
        {
            // TODO: implement
        }

        public void IzmeniNotifikaciju(KartonPacijenta karton)
        {
            // TODO: implement
        }

        public List<Recept> PregledRecepata(Pacijent pacijent)
        {
            // TODO: implement
            return null;
        }

        public string PronadjiOpis(Pacijent pacijent, Recept recept)
        {
            // TODO: implement
            return "";
        }

        public bool DaLiPostojiBarJedanIzvrsenTermin(Pacijent pacijent)
        {
            // TODO: implement
            return false;
        }

        public List<Termin> SviTerminiKojiSuIzvrseni(Pacijent pacijent, Lek noviLek)
        {
            // TODO: implement
            return null;
        }

        public bool DaLiJePregledVecAnketiran(Termin termin)
        {
            // TODO: implement
            return false;
        }

        public void PovecavanjeBrojacaPriDodavanjuTermina()
        {
            // TODO: implement
        }

        public void PovecavanjeBrojacaPriIzmeniTermina()
        {
            // TODO: implement
        }

        public void PovecavanjeBrojacaPriOtkazivanjuTermina()
        {
            // TODO: implement
        }

        public Service.TerminiService terminiService;
        public Service.NotifikacijeService notifikacijeService;
        public Service.AnketaService anketaService;
        public Service.AntiTrollService antiTrollService;

    }
}