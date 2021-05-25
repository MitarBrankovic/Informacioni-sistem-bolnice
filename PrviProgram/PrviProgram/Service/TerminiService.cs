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
        string[] nizVremena = { "08:00:00", "08:30:00", "09:00:00", 
            "09:30:00", "10:00:00", "10:30:00", "11:00:00", "11:30:00", 
            "12:00:00", "12:30:00", "13:00:00", "13:30:00", "14:00:00", 
            "14:30:00", "15:00:00", "15:30:00", "16:00:00", "16:30:00", "17:00:00", 
            "17:30:00", "18:00:00", "18:30:00", "19:00:00", "19:30:00" };
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
        public void DodavanjeTermina(Termin t)
        {
            List<Termin> termini = terminiRepository.CitanjeIzFajla();
            termini.Add(t);
            terminiRepository.UpisivanjeUFajl(termini);
        }

        public void BrisanjeTermina(Termin termin)
        {
            List<Termin> termini = terminiRepository.CitanjeIzFajla();
            foreach (Termin t in termini)
            {
                if (t.SifraTermina.Equals(termin.SifraTermina) && t.Izvrsen != true)
                {
                    termini.Remove(t);
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

        public List<Termin> PregledTermina(Pacijent pacijent)
        {
            List<Termin> termini = terminiRepository.CitanjeIzFajla();
            List<Termin> list = new List<Termin>();
            foreach (Termin pp in termini)
            {
                if (pp.pacijent != null)
                {
                    if (pp.pacijent.Jmbg.Equals(pacijent.Jmbg))
                    {
                        list.Add(pp);

                    }
                }
            }
            return list;
        }


        public bool IzmenaSale(Sala staraSala, Sala novaSala)
        {
            List<Termin> termini = terminiRepository.PregledSvihTermina();
            List<Termin> novaListaTermina = new List<Termin>();
            foreach (Termin terminIterator in termini)
            {
                novaListaTermina.Add(terminIterator);
                if (terminIterator.sala.Sifra.Equals(staraSala.Sifra))
                {
                    novaListaTermina.Remove(terminIterator);
                    terminIterator.sala = novaSala;
                    novaListaTermina.Add(terminIterator);
                    terminiRepository.UpisivanjeUFajl(novaListaTermina);
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

        public int[] ProveraZauzetostiLekara(string jmbg, DateTime selektovaniDatum, string[] niz)
        {
            int[] popunjeniNiz = new int[24] { 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            TerminiRepository datoteka1 = new TerminiRepository();
            List<Termin> termini = datoteka1.CitanjeIzFajla();

            foreach (Termin t in termini)
            {
                if (jmbg.Equals(t.lekar.Jmbg) && selektovaniDatum.Equals(t.Datum))
                {
                    for (int i = 0; i < niz.Length; i++)
                    {
                        if (t.Vreme.Equals(niz[i]))
                        {
                            popunjeniNiz[i] = 1;
                        }
                    }
                }
            }
            return popunjeniNiz;
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
                if (!sala.Tip.Equals(TipSale.Magacin))
                {
                    if (ProveraSale(sala, termini, noviTermin))
                    {
                        novaSala = sala;
                        return novaSala;
                    }
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
            if (!ProveraZauzetostTerminUSali(sala, termini, noviTermin))
            {
                return false;
            }
            return true;
        }

        public bool ProveraZauzetostTerminUSali(Sala sala, List<Termin> termini, Termin noviTermin)
        {
            foreach (Termin termin in termini)
            {
                if (termin.Datum.Equals(noviTermin.Datum) && termin.Vreme.Equals(noviTermin.Vreme) && termin.sala.Sifra.Equals(sala.Sifra))
                {
                    return false;
                }
                if (sala.Tip == TipSale.Magacin || sala.Tip == TipSale.Kancelarija || sala.Tip == TipSale.SalaZaOdmor)
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
                if (terminRenoviranja.Sala.Naziv.Equals(sala.Naziv))
                {
                    if (IzracunajIntervalRenoviranja(terminRenoviranja).Contains(noviTermin.Datum))
                    {
                        return false;
                    }
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

        public List<Termin> SviSlobodniTermini(DateTime min, DateTime max, Lekar selektovaniLekar,string tipTermina)
        {
            
           
            TimeSpan razlika = max - min;
            int razlikaDatuma = (int)razlika.TotalDays;

            for(int i=0;i<=razlikaDatuma;i++)
            {
                foreach (string vreme in nizVremena)
                {
                    if(ProveraTermina(min,vreme))
                    {
                        Termin termin = new Termin();
                        termin.Vreme = vreme;
                        termin.Datum = min;
                        termin.lekar = selektovaniLekar;
                        if (tipTermina.Equals("Pregled"))
                        {
                            termin.TipTermina = TipTermina.Pregled;
                        }
                        if (tipTermina.Equals("Kontrola"))
                        {
                            termin.TipTermina = TipTermina.Kontrola;
                        }
                        
                        terminiSlobodni.Add(termin);
                    }
                }

                min = min.AddDays(+1);
            }
            return terminiSlobodni;
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

        public bool ProveraZauzetostiKodLekara(DateTime min, DateTime max, Lekar selektovaniLekar)
        {
            List<Termin> termini = CitanjeTermina();
            foreach(Termin termin in termini)
            {
                if(termin.Datum>=min && termin.Datum<=max && termin.lekar.Jmbg.Equals(selektovaniLekar.Jmbg))
                 {
                    return false;
                }
            }
            return true;
        }

        public List<Termin> ProveraVremenaKodLekara(DateTime min, DateTime max, Lekar selektovaniLekar, string tipTermina)
        {
            List<Termin> terminiSlobodni1 = new List<Termin>();
            TimeSpan razlikaDanasnjegDanaiMin = min - DateTime.Today;

            if (razlikaDanasnjegDanaiMin.TotalDays == 0)
            {
                DateTime maxOd = max.AddDays(1);
                DateTime maxDo = max.AddDays(2);
                terminiSlobodni1 = SviSlobodniTermini(maxOd, maxDo, selektovaniLekar, tipTermina);
            }
            else if (razlikaDanasnjegDanaiMin.TotalDays == 1)
            {
                DateTime minOd = min.AddDays(-1);
                DateTime minDo = min.AddDays(-1);
                DateTime maxOd = max.AddDays(1);
                DateTime maxDo = max.AddDays(2);
                terminiSlobodni1 = SviSlobodniTermini(minOd, minDo, selektovaniLekar, tipTermina);
                terminiSlobodni1 = SviSlobodniTermini(maxOd, maxDo, selektovaniLekar, tipTermina);
            }
            else
            {
                DateTime minOd = min.AddDays(-2);
                DateTime minDo = min.AddDays(-1);
                DateTime maxOd = max.AddDays(1);
                DateTime maxDo = max.AddDays(2);
                terminiSlobodni1 = SviSlobodniTermini(minOd, minDo, selektovaniLekar, tipTermina);
                terminiSlobodni1 = SviSlobodniTermini(maxOd, maxDo, selektovaniLekar, tipTermina);

            }


            return this.terminiSlobodni;
        }

        public bool ProvaraZauzatostiTermina(Termin termin)
        {
            List<Termin> termini = terminiRepository.CitanjeIzFajla();
            foreach (Termin t in termini)
            {
                if (t.lekar.Jmbg.Equals(termin.lekar.Jmbg) && t.Datum.Date.Equals(termin.Datum.Date) && t.Vreme.Equals(termin.Vreme))
                {
                    return true;
                }
            }
            return false;
        }
        
        public List<Termin> ProveraLekaraKodVremena(DateTime min, DateTime max, Lekar selektovaniLekar, string tipTermina)
        {
            List<Lekar> lekari = CitanjeLekara();
            foreach(Lekar lekar in lekari)
            {
                if (!lekar.Jmbg.Equals(selektovaniLekar.Jmbg))
                {
                    if (ProveraZauzetostiKodLekara(min, max, lekar))
                    {
                        termini = SviSlobodniTermini(min, max, lekar, tipTermina);
                    }
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
