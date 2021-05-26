using Model;
using Repository;
using System;
using System.Collections.Generic;

namespace Service
{
    public class NotifikacijeService
    {
        private NotifikacijeObavestenjaRepository notifikacijeObavestenjaRepository = new NotifikacijeObavestenjaRepository();
        private PacijentRepository pacijentRepository = new PacijentRepository();

        public void DodavanjeNotifikacije(NotifikacijePacijenta notifikacija)
        {
            List<NotifikacijePacijenta> notifikacije = notifikacijeObavestenjaRepository.CitanjeIzFajla();
            notifikacije.Add(notifikacija);
            notifikacijeObavestenjaRepository.UpisivanjeUFajl(notifikacije);
        }

        public List<Recept> PregledRecepata(Pacijent pacijent)
        {
            List<Pacijent> pacijenti = pacijentRepository.CitanjeIzFajla();
            List<Recept> recepti = new List<Recept>();

            foreach (Pacijent pacijentBrojac in pacijenti)
            {
                if (pacijentBrojac.Jmbg.Equals(pacijent.Jmbg))
                {
                    DodavanjeIzvrsenogPregleda(pacijentBrojac, recepti);
                }
            }
            return recepti;
        }

        public void DodavanjeIzvrsenogPregleda(Pacijent pacijentBrojac, List<Recept> recepti) 
        {
            foreach (IzvrseniPregled izvrseniPregledBrojac in pacijentBrojac.KartonPacijenta.izvrseniPregled)
            {
                recepti.Add(izvrseniPregledBrojac.recept);
            }
        }

        public string PronadjiOpis(Recept recept, Pacijent pacijent)
        {
            List<Pacijent> pacijenti = pacijentRepository.CitanjeIzFajla();
            foreach (Pacijent pacijentBrojac in pacijenti)
            {
                if (pacijent.Jmbg.Equals(pacijentBrojac.Jmbg))
                {
                    VracanjeOpisa(pacijentBrojac, recept);
                }
            }
            return "";
        }

        public string VracanjeOpisa(Pacijent pacijentBrojac, Recept recept)
        {
            foreach (IzvrseniPregled izvrseniPregledBrojac in pacijentBrojac.KartonPacijenta.izvrseniPregled)
            {
                if (izvrseniPregledBrojac.recept.Lekovi.Equals(recept.Lekovi))
                {
                    return izvrseniPregledBrojac.recept.OpisLeka.ToString();
                }
            }
            return null;
        }
    }
}