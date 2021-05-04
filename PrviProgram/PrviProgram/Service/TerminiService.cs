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
        string[] nizVremena = { "08:00:00", "08:30:00", "09:00:00", "09:30:00", "10:00:00", "10:30:00", "11:00:00", "11:30:00", "12:00:00", "12:30:00", "13:00:00", "13:30:00", "14:00:00", "14:30:00", "15:00:00", "15:30:00", "16:00:00", "16:30:00", "17:00:00", "17:30:00", "18:00:00", "18:30:00", "19:00:00", "19:30:00" };
        private List<Termin> termini = new List<Termin>();
        public List<Termin> izvrseniTermini = new List<Termin>();

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
            TerminiRepository terminiRepository = new TerminiRepository();
            List<Termin> termini = terminiRepository.CitanjeIzFajla();
            foreach (Termin t in termini)
            {
                if (t.SifraTermina.Equals(termin.SifraTermina) && t.izvrsen != true)
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
            foreach (Termin t in termini)
            {
                if (t.lekar.Jmbg.Equals(lekar.Jmbg) && t.Datum.Date.Equals(datumTermina.Date))
                {
                    zauzetiTermini.Add(t.Vreme);
                }
            }
            return zauzetiTermini;
        }

        public bool IzmenaTermina(Termin termin)
        {
            TerminiRepository terminiRepository = new TerminiRepository();
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

        public List<Termin> PregledTermina(Pacijent p)
        {
            TerminiRepository datoteka = new TerminiRepository();
            List<Termin> termini = datoteka.CitanjeIzFajla();
            List<Termin> list = new List<Termin>();
            foreach (Termin pp in termini)
            {
                if (pp.pacijent != null)
                {
                    if (pp.pacijent.Jmbg.Equals(p.Jmbg))
                    {
                        list.Add(pp);

                    }
                }
            }
            return list;
        }


        public bool IzmenaSale(Sala staraSala, Sala novaSala)
        {
            TerminiRepository datoteka = new TerminiRepository();
            List<Termin> termini = datoteka.CitanjeIzFajla();
            List<Termin> list = new List<Termin>();
            Sala s = new Sala();

            foreach (Termin tt in termini)
            {
                list.Add(tt);
                if (tt.sala.Sifra.Equals(staraSala.Sifra))
                {
                    list.Remove(tt);
                    tt.sala = novaSala;
                    list.Add(tt);
                    datoteka.UpisivanjeUFajl(list);
                    return true;

                }
            }
            return false;

        }
        public int[] proveraZauzetostiLekara(string jmbg, DateTime selektovaniDatum, string[] niz)
        {
            int[] popunjeniNiz = new int[24] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
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

        public List<Termin> citanjeTermina()
        {
            TerminiRepository datoteka = new TerminiRepository();
            List<Termin> termini = datoteka.CitanjeIzFajla();
            return termini;
        }
        public List<Sala> citanjeSala()
        {
            SalaRepository datoteka = new SalaRepository();
            List<Sala> sale = datoteka.CitanjeIzFajla();
            return sale;
        }
        public List<AnketiranjePacijenta> citanjeAnketa()
        {
            AnketiranjePacijentaRepository datoteka = new AnketiranjePacijentaRepository();
            List<AnketiranjePacijenta> ankete = datoteka.CitanjeIzFajla();
            return ankete;
        }
        public List<Lekar> citanjeLekara()
        {
            LekarRepository datoteka = new LekarRepository();
            List<Lekar> lekari = datoteka.CitanjeIzFajla();
            return lekari;
        }

        public Sala dobavljanjeSale(Termin noviTermin)
        {

            List<Termin> termini = citanjeTermina();
            List<Sala> sale = citanjeSala();
            Sala novaSala = new Sala();

            foreach (Sala sala in sale)
            {
                if (!sala.Tip.Equals(TipSale.Magacin))
                {
                    if (proveraSale(sala, termini, noviTermin))
                    {
                        novaSala = sala;
                        return novaSala;
                    }
                }
            }
            return null;

        }
        public bool proveraSale(Sala sala, List<Termin> termini, Termin noviTermin)
        {
            List<TerminRenoviranjaSale> terminiRenoviranja = terminiRenoviranjaRepository.CitanjeIzFajla();
            foreach (TerminRenoviranjaSale terminRenoviranja in terminiRenoviranja) 
            {
                if (terminRenoviranja.sala.Naziv.Equals(sala.Naziv))
                {
                    var intervalRenoviranja = new List<DateTime>();
                    for (var dt = terminRenoviranja.PocetakRenoviranja; dt <= terminRenoviranja.KrajRenoviranja; dt = dt.AddDays(1))
                    {
                        intervalRenoviranja.Add(dt);
                    }
                    if (intervalRenoviranja.Contains(noviTermin.Datum))
                    {
                        return false;
                    }
                    intervalRenoviranja.Clear();
                }
            }
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

        public List<Termin> sviSlobodniTermini(DateTime min, DateTime max, Lekar selektovaniLekar,string tipTermina)
        {
            
           
            TimeSpan razlika = max - min;
            int razlikaDatuma = (int)razlika.TotalDays;

            for(int i=0;i<=razlikaDatuma;i++)
            {
                foreach (string vreme in nizVremena)
                {
                    if(proveraTermina(min,vreme))
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
        public bool proveraTermina(DateTime datum, string vreme)
        {
            List<Termin> termini = citanjeTermina();
            foreach (Termin termin in termini)
            {
                if (termin.Datum.Equals(datum) && termin.Vreme.Equals(vreme))
                {
                    return false;
                }
            }
            return true;

        }
        public bool proveraZauzetostiKodLekara(DateTime min, DateTime max, Lekar selektovaniLekar)
        {
            List<Termin> termini = citanjeTermina();
            foreach(Termin termin in termini)
            {
                if(termin.Datum>=min && termin.Datum<=max && termin.lekar.Jmbg.Equals(selektovaniLekar.Jmbg))
                 {
                    return false;
                }
            }
            return true;
        }

        public List<Termin> proveraVremenaKodLekara(DateTime min, DateTime max, Lekar selektovaniLekar, string tipTermina)
        {
            List<Termin> terminiSlobodni1 = new List<Termin>();
            TimeSpan razlikaDanasnjegDanaiMin = min - DateTime.Today;

            if (razlikaDanasnjegDanaiMin.TotalDays == 0)
            {
                DateTime maxOd = max.AddDays(1);
                DateTime maxDo = max.AddDays(2);
                terminiSlobodni1 = sviSlobodniTermini(maxOd, maxDo, selektovaniLekar, tipTermina);
            }
            else if (razlikaDanasnjegDanaiMin.TotalDays == 1)
            {
                DateTime minOd = min.AddDays(-1);
                DateTime minDo = min.AddDays(-1);
                DateTime maxOd = max.AddDays(1);
                DateTime maxDo = max.AddDays(2);
                terminiSlobodni1 = sviSlobodniTermini(minOd, minDo, selektovaniLekar, tipTermina);
                terminiSlobodni1 = sviSlobodniTermini(maxOd, maxDo, selektovaniLekar, tipTermina);
            }
            else
            {
                DateTime minOd = min.AddDays(-2);
                DateTime minDo = min.AddDays(-1);
                DateTime maxOd = max.AddDays(1);
                DateTime maxDo = max.AddDays(2);
                terminiSlobodni1 = sviSlobodniTermini(minOd, minDo, selektovaniLekar, tipTermina);
                terminiSlobodni1 = sviSlobodniTermini(maxOd, maxDo, selektovaniLekar, tipTermina);

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
        
        public List<Termin> proveraLekaraKodVremena(DateTime min, DateTime max, Lekar selektovaniLekar, string tipTermina)
        {
            List<Lekar> lekari = citanjeLekara();
            foreach(Lekar lekar in lekari)
            {
                if (!lekar.Jmbg.Equals(selektovaniLekar.Jmbg))
                {
                    if (proveraZauzetostiKodLekara(min, max, lekar))
                    {
                        termini = sviSlobodniTermini(min, max, lekar, tipTermina);
                    }
                }
            }
            return this.terminiSlobodni;
        }


        public bool daLiPostojiBarJedanIzvrsenTermin(Pacijent pacijent)
        {
            List<Termin> termini = citanjeTermina();
            foreach(Termin termin in termini)
            {
                if(termin.pacijent.Jmbg.Equals(pacijent.Jmbg) && termin.izvrsen==true && daLiJePregledVecAnketiran(termin))
                {
                    return true;
                }
            }
            return false;
        }

        public List<Termin> sviTerminiKojiSuIzvrseni(Pacijent pacijent)
        {
            List<Termin> termini = citanjeTermina();
            List<Termin> izvrseniTermini = new List<Termin>();
            
            foreach (Termin termin in termini)
            {
                if (termin.pacijent.Jmbg.Equals(pacijent.Jmbg) && termin.izvrsen == true && daLiJePregledVecAnketiran(termin))
                {
                   izvrseniTermini.Add(termin);
                }
            }
            return izvrseniTermini;
        }

        public bool daLiJePregledVecAnketiran(Termin termin)
        {
            List<AnketiranjePacijenta> ankete = citanjeAnketa();
            foreach(AnketiranjePacijenta anketa in ankete)
            {
                if(termin.SifraTermina.Equals(anketa.termin.SifraTermina))
                {
                    return false;
                }
            }
            return true;

        }

        public Lekar lekarKojiJeZaduzenZaTermin(Termin selektovanitermin)
        {
            Lekar lekar = new Lekar();
            List<Termin> termini = citanjeTermina();
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
