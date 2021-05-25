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
        public List<Termin> terminiSlobodni = new List<Termin>();
        private UtilityService utilityService = new UtilityService();
        public int[] nizSlobodnihIZauzetihTermina = new int[24] { 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        private List<Termin> termini = new List<Termin>();
        private List<Termin> izvrseniTermini = new List<Termin>();
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
                if (termin.SifraTermina.Equals(termin.SifraTermina) && termin.Izvrsen != true)
                {
                    termini.Remove(termin);
                    terminiRepository.UpisivanjeUFajl(termini);
                    break;
                }
            }
        }

        public List<string> ZauzetiTerminiLekaraDatuma(Lekar lekar, DateTime datumTermina)
        {
            List<string> zauzetiTermini = new List<string>();
            List<Termin> termini = terminiRepository.CitanjeIzFajla();
            foreach (Termin termin in termini)
            {
                if (termin.lekar.Jmbg.Equals(lekar.Jmbg) && termin.Datum.Date.Equals(datumTermina.Date))
                {
                    zauzetiTermini.Add(termin.Vreme);
                }
            }
            return zauzetiTermini;
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
        //MORA U REPOSITORY
        public List<Termin> PregledTermina(Pacijent pacijent)
        {
            List<Termin> termini = terminiRepository.CitanjeIzFajla();
            List<Termin> noviTermini = new List<Termin>();
            foreach (Termin termin in termini)
            {
                if (termin.pacijent != null && termin.pacijent.Jmbg.Equals(pacijent.Jmbg))
                {
                    noviTermini.Add(termin);
                }
            }
            return noviTermini;
        }


        public bool IzmenaSale(Sala staraSala, Sala novaSala)
        {

            List<Termin> termini = terminiRepository.CitanjeIzFajla();
            List<Termin> noviTermini = new List<Termin>();
            foreach (Termin termin in termini)
            {
                noviTermini.Add(termin);
                if (termin.sala.Sifra.Equals(staraSala.Sifra))
                {
                    noviTermini.Remove(termin);
                    termin.sala = novaSala;
                    noviTermini.Add(termin);
                    terminiRepository.UpisivanjeUFajl(noviTermini);

                    return true;
                }
            }
            return false;
        }

        public void BrisanjeSaleIzTermina(Sala sala)
        {
            List<Termin> termini = terminiRepository.PregledSvihTermina();
            foreach (Termin terminIterator in termini)
            {
                if (terminIterator != null)
                {
                    if (terminIterator.sala.Sifra == sala.Sifra)
                    {
                        terminIterator.sala = null;
                        IzmenaTermina(terminIterator);
                    }
                }
            }
        }

        public int[] ProveraZauzetostiLekara(string jmbg, DateTime selektovaniDatum, string[] nizSlobodnihTermina)

        {
            List<Termin> termini = CitanjeTermina();
            foreach (Termin termin in termini)
            {
                if (jmbg.Equals(termin.lekar.Jmbg) && selektovaniDatum.Equals(termin.Datum))
                {
                    PopunjavanjeNizaZauzetihTermina(nizSlobodnihTermina, termin);
                }
            }
            return this.nizSlobodnihIZauzetihTermina;
        }
        public void PopunjavanjeNizaZauzetihTermina(string[] nizSlobodnihTermina, Termin termin)
        {
            for (int i = 0; i < nizSlobodnihTermina.Length; i++)
            {
                if (termin.Vreme.Equals(nizSlobodnihTermina[i]))
                {
                    this.nizSlobodnihIZauzetihTermina[i] = 1;
                }
            }
        }

        public List<Termin> CitanjeTermina()
        {
            TerminiRepository datoteka = new TerminiRepository();
            List<Termin> termini = datoteka.CitanjeIzFajla();
            return termini;
        }
        public List<Sala> CitanjeSala()
        {
            SalaRepository datoteka = new SalaRepository();
            List<Sala> sale = datoteka.CitanjeIzFajla();
            return sale;
        }
        
        public List<Lekar> CitanjeLekara()
        {
            LekarRepository datoteka = new LekarRepository();
            List<Lekar> lekari = datoteka.CitanjeIzFajla();
            return lekari;
        }

        public Sala DobavljanjeSale(Termin noviTermin)
        {
            List<Termin> termini = CitanjeTermina();
            List<Sala> sale = CitanjeSala();
            Sala novaSala = new Sala();
            foreach (Sala sala in sale)
            {
                if (!sala.Tip.Equals(TipSale.Magacin) && ProveraSale(sala, termini, noviTermin))
                {
                        novaSala = sala;
                        return novaSala;
                }
            }
            return null;
        }
        public bool ProveraSale(Sala sala, List<Termin> termini, Termin noviTermin)
        {
            if (!ProveraRenoviranjaSale(sala, noviTermin))
            {
                return false;
            }
            if (!ProveraZauzetostTerminaUSali(sala, termini, noviTermin))
            {
                return false;
            }
            return true;
        }

        public bool ProveraZauzetostTerminaUSali(Sala sala, List<Termin> termini, Termin noviTermin)
        {
            foreach (Termin termin in termini)
            {
                if ((termin.Datum.Equals(noviTermin.Datum) && termin.Vreme.Equals(noviTermin.Vreme) && termin.sala.Sifra.Equals(sala.Sifra))
                    || (sala.Tip == TipSale.Magacin || sala.Tip == TipSale.Kancelarija || sala.Tip == TipSale.SalaZaOdmor))
                {
                    return false;
                }
            }
            return true;
        }

        public bool ProveraRenoviranjaSale(Sala sala, Termin noviTermin)
        {
            foreach (TerminRenoviranjaSale terminRenoviranja in terminiRenoviranjaRepository.CitanjeIzFajla())
            {
                if (terminRenoviranja.Sala.Naziv.Equals(sala.Naziv) && IzracunajIntervalRenoviranja(terminRenoviranja).Contains(noviTermin.Datum))
                {
                        return false;
                }
            }
            return true;
        }

        public List<DateTime> IzracunajIntervalRenoviranja(TerminRenoviranjaSale terminRenoviranja)
        {
            List<DateTime> intervalRenoviranja = new List<DateTime>();
            for (DateTime datum = terminRenoviranja.PocetakRenoviranja; datum <= terminRenoviranja.KrajRenoviranja; datum = datum.AddDays(1))
            {
                intervalRenoviranja.Add(datum);
            }

            return intervalRenoviranja;
        }

        public List<Termin> SviSlobodniTermini(DateTime pocetakDatum, DateTime krajDatum, Lekar selektovaniLekar,TipTermina tipTermina)
        {
            for(int i=0;i<= (int)(krajDatum - pocetakDatum).TotalDays; i++)
            {
                    if (SlobodnoVreme(pocetakDatum)!=null)
                    {
                        Termin termin = new Termin(tipTermina, SlobodnoVreme(pocetakDatum), pocetakDatum, selektovaniLekar);
                        terminiSlobodni.Add(termin);
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
            List<Termin> termini = CitanjeTermina();
            foreach (Termin termin in termini)
            {
                if (termin.Datum.Equals(datum) && termin.Vreme.Equals(vreme))
                {
                    return false;
                }
            }
            return true;

        }

        public bool ProveraZauzetostiKodLekara(DateTime pocetakDatum, DateTime krajDatum, Lekar selektovaniLekar)
        {
            List<Termin> termini = CitanjeTermina();
            foreach(Termin termin in termini)
            {
                if(termin.Datum>=pocetakDatum && termin.Datum<=krajDatum && termin.lekar.Jmbg.Equals(selektovaniLekar.Jmbg))
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
        
        public List<Termin> ProveraLekaraKodVremena(DateTime pocetakDatum, DateTime krajDatum, Lekar selektovaniLekar, TipTermina tipTermina)
        {
            List<Lekar> lekari = CitanjeLekara();
            foreach(Lekar lekar in lekari)
            {
                if (!lekar.Jmbg.Equals(selektovaniLekar.Jmbg) && ProveraZauzetostiKodLekara(pocetakDatum, krajDatum, lekar))
                {
                       termini = SviSlobodniTermini(pocetakDatum, krajDatum, lekar, tipTermina);
                }
            }
            return this.terminiSlobodni;
        }

        public Lekar LekarKojiJeZaduzenZaTermin(Termin selektovanitermin)
        {
            Lekar lekar = new Lekar();
            List<Termin> termini = CitanjeTermina();
            foreach(Termin termin in termini)
            {
                if(termin.SifraTermina.Equals(selektovanitermin.SifraTermina))
                {
                    lekar = termin.lekar;
                    return lekar;
                }
            }
            return null;
        }

    }
 }
