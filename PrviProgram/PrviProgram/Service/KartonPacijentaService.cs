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

namespace Service
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

        public bool IzmenaIzvrsenogPregleda(IzvrseniPregled izvrseniPregled,Pacijent selektovaniPacijent)
        {
            List<Pacijent> pacijenti = pacijentRepository.CitanjeIzFajla();
            foreach(Pacijent pacijent in pacijenti)
            {
                if(pacijent.Jmbg.Equals(selektovaniPacijent.Jmbg))
                {
                    pacijenti.Remove(pacijent);
                    pacijenti.Add(DodavanjeBeleskeUKarton(selektovaniPacijent, izvrseniPregled));
                    pacijentRepository.UpisivanjeUFajl(pacijenti);
                    return true;
                }
            }
          return false;            
        }
        public Pacijent DodavanjeBeleskeUKarton(Pacijent pacijent, IzvrseniPregled izvrsen)
        {
            foreach (IzvrseniPregled izvrseni in pacijent.KartonPacijenta.izvrseniPregled)
            {
                if (izvrseni.Sifra.Equals(izvrsen.Sifra))
                {
                    pacijent.KartonPacijenta.izvrseniPregled.Remove(izvrseni);
                    pacijent.KartonPacijenta.izvrseniPregled.Add(izvrsen);
                    return pacijent;
                }
            }
            return null;
        }
        public void IzvrsenaAnamneza(IzvrseniPregled izvrseniPregled, Pacijent pacijent)
        {
            Pacijent pacijentStari = pacijent;
            Pacijent pacijentNovi = pacijent;
            foreach(IzvrseniPregled izvrseniPregledIterator in pacijentRepository.PregledPacijenta(pacijent.Jmbg).KartonPacijenta.izvrseniPregled)
            {
                if(izvrseniPregled.Sifra == izvrseniPregledIterator.Sifra)
                {
                    AzurirajPostojeciIzvrseniPregled(pacijent, pacijentStari, pacijentNovi, izvrseniPregled);
                    return;
                }
            }
            KreirajNoviIzvrseniPregled(pacijent, pacijentStari, pacijentNovi, izvrseniPregled);
        }

        private void AzurirajPostojeciIzvrseniPregled(Pacijent pacijent, Pacijent pacijentStari, Pacijent pacijentNovi,IzvrseniPregled izvrseniPregled)
        {
            pacijent.KartonPacijenta.AzurirajIzvrseniPregled(izvrseniPregled);
            pacijentiService.IzmenaPacijenta(pacijentStari, pacijentNovi);
        }

        private void KreirajNoviIzvrseniPregled(Pacijent pacijent, Pacijent pacijentStari, Pacijent pacijentNovi, IzvrseniPregled izvrseniPregled)
        {
            pacijent.KartonPacijenta.izvrseniPregled.Add(izvrseniPregled);
            pacijentiService.IzmenaPacijenta(pacijentStari, pacijentNovi);
        }

        public KartonPacijentaRepository kartonPacijentaRepository;

    }
}