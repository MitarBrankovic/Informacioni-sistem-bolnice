using Model;
using PrviProgram.Repository;
using Repository;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Service
{
    public class TerminiService
    {
        private static TerminiService instance = null;
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
            TerminiRepository datoteka = new TerminiRepository();
            List<Termin> termini = datoteka.CitanjeIzFajla();
            // termini.Add(t);

            //foreach (Termin tt in termini)
            //{
            //    if (tt.pacijent.Jmbg.Equals(p.Jmbg) && t.SifraTermina.Equals(tt.SifraTermina))
            //    {
            //
            //    }
            //}
            termini.Add(t);
            datoteka.UpisivanjeUFajl(termini);

            /* foreach (Pacijent pp in pacijenti)
             {
                 if (pp.Jmbg.Equals(p.Jmbg))
                 {
                     foreach (Termin tt in pp.GetTermin())
                     {
                         termini.Add(tt);
                     }

                     pp.termin = termini;
                     break;

                 }
             }*/
            // datoteka.UpisivanjeUFajl(pacijenti);


        }

        public List<Lekar> proveraVremenaKodLekara(string vreme, DateTime datum)
        {
            LekarRepository datoteka = new LekarRepository();
            List<Lekar> lekari = datoteka.CitanjeIzFajla();
            TerminiRepository datoteka1 = new TerminiRepository();
            //string var = vreme.ToString();

            List<Termin> termini = datoteka1.CitanjeIzFajla();
            List<Lekar> lekari1 = new List<Lekar>();
            foreach (Lekar l1 in lekari)
            {
                lekari1.Add(l1);
            }

            foreach (Lekar l in lekari)
            {
                foreach (Termin t in termini)
                {
                    if (t.Vreme.Equals(vreme) && t.lekar.Jmbg.Equals(l.Jmbg) && t.Datum.Equals(datum))
                    {
                        lekari1.Remove(l);
                        break;
                    }
                }
            }


            return lekari1;

        }
        public void BrisanjeTermina(Termin termin)
        {
            TerminiRepository terminiRepository = new TerminiRepository();
            List<Termin> termini = terminiRepository.CitanjeIzFajla();
            foreach (Termin t in termini)
            {
                if (t.SifraTermina.Equals(termin.SifraTermina))
                {
                    termini.Remove(t);
                    terminiRepository.UpisivanjeUFajl(termini);
                    break;
                }
            }
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

        public bool IzmenaTermina(Termin termin)
        {
            TerminiRepository terminiRepository = new TerminiRepository();
            List<Termin> termini = terminiRepository.CitanjeIzFajla();
            foreach (Termin t in termini)
            {
                if (t.SifraTermina.Equals(termin.SifraTermina))
                {
                    termini.Remove(termin);
                    termini.Add(t);
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

        public List<Termin> citanjeTermina()
        {
            TerminiRepository datoteka = new TerminiRepository();
            List<Termin> termini = datoteka.CitanjeIzFajla();
            return termini;
        }
        public List<Sala> citanjeSala()
        {
            SalaRepository datoteka = new SalaRepository();
             List<Sala>sale = datoteka.CitanjeIzFajla();
            return sale;
        }

        public Sala dobavljanjeSale(Termin noviTermin)
        {
            
            List<Termin> termini = citanjeTermina();
            List<Sala> sale = citanjeSala();
            Sala novaSala = new Sala();

            foreach (Sala sala in sale)
            {
                if (!sala.Naziv.Equals("Magacin"))
                {
                    if (proveraSale(sala, termini, noviTermin))
                    {
                        novaSala = sala;
                        novaSala.Dostupnost = false;
                        return novaSala;
                    }
                }
            }
            return null;

        }
        public bool proveraSale(Sala sala, List<Termin> termini,Termin noviTermin)
        {
            foreach (Termin termin in termini)
                {
                    if(termin.Datum.Equals(noviTermin.Datum) && termin.Vreme.Equals(noviTermin.Vreme) && termin.sala.Sifra.Equals(sala.Sifra))
                    {
                            return false;
                    }
                }
            return true;
            }

        }

 }