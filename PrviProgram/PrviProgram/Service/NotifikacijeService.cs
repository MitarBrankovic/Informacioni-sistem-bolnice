/***********************************************************************
 * Module:  NotifikacijeService.cs
 * Author:  Saska WorkPC
 * Purpose: Definition of the Class Service.PacijentService.NotifikacijeService
 ***********************************************************************/

using Model;
using Repository;
using System;
using System.Collections.Generic;

namespace Service
{
    public class NotifikacijeService
    {
        public void DodavanjeNotifikacije(NotifikacijePacijenta notifikacija)
        {

            NotifikacijeObavestenjaRepository datoteka = new NotifikacijeObavestenjaRepository();
            List<NotifikacijePacijenta> notifikacije = datoteka.CitanjeIzFajla();
            notifikacije.Add(notifikacija);

            datoteka.UpisivanjeUFajl(notifikacije);

        }

        public void IzmeniNotifikaciju(KartonPacijenta karton)
        {

        }



        public List<Recept> pregledRecepata(Pacijent p)
        {
            PacijentRepository datotetka = new PacijentRepository();
            List<Pacijent> pacijenti = datotetka.CitanjeIzFajla();
            List<Recept> recepti = new List<Recept>();

            foreach (Pacijent pp in pacijenti)
            {
                if (pp.Jmbg.Equals(p.Jmbg))
                {
                    foreach (IzvrseniPregled i in pp.kartonPacijenta.izvrseniPregled)
                    {
                        recepti.Add(i.recept);
                    }
                }
            }
            return recepti;
        }
        public string PronadjiOpis(Recept r, Pacijent p)
        {
            PacijentRepository datotetka = new PacijentRepository();
            List<Pacijent> pacijenti = datotetka.CitanjeIzFajla();
            foreach (Pacijent pp in pacijenti)
            {
                if (p.Jmbg.Equals(pp.Jmbg))
                {
                    foreach (IzvrseniPregled i in pp.kartonPacijenta.izvrseniPregled)
                    {
                        if (i.recept.Lekovi.Equals(r.Lekovi))
                        {
                            return i.recept.OpisLeka.ToString();
                        }
                    }
                }
            }
            return "";
        }

        /// <pdGenerated>default getter</pdGenerated>

    }
}