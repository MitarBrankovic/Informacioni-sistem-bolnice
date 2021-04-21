using Model;
using PrviProgram.Repository;
using Repository;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Service.PacijentService
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
        public void DodavanjeTermina(Termin t, Pacijent p)
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
            t.pacijent = p;
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

        public List<Lekar> proveraVremenaKodLekara(string vreme,DateTime datum)
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

            foreach( Lekar l in lekari)
            {
                foreach(Termin t in termini)
                {
                    if(t.Vreme.Equals(vreme) && t.lekar.Jmbg.Equals(l.Jmbg) && t.Datum.Equals(datum))
                    {
                        lekari1.Remove(l);
                        break;
                    }
                }
            }


            return lekari1;

        }
        public void BrisanjeTermina(Termin t, Pacijent p)
        {
            TerminiRepository datoteka1 = new TerminiRepository();

            List<Termin> termini = datoteka1.CitanjeIzFajla();

            foreach (Termin tt in termini)
            {
                if (t.SifraTermina.Equals(tt.SifraTermina))
                {
                    termini.Remove(tt);
                    datoteka1.UpisivanjeUFajl(termini);
                    break;
                }
            }
           
        }
        public int[] proveraZauzetostiLekara(string jmbg, DateTime selektovaniDatum,string [] niz)
        {
            int[] popunjeniNiz = new int[24] {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
            TerminiRepository datoteka1 = new TerminiRepository();
            List<Termin> termini = datoteka1.CitanjeIzFajla();

            foreach(Termin t in termini)
            {
                if(jmbg.Equals(t.lekar.Jmbg) && selektovaniDatum.Equals(t.Datum))
                {
                    for(int i=0;i<niz.Length;i++)
                    {
                        if(t.Vreme.Equals(niz[i]))
                        {
                            popunjeniNiz[i] = 1;
                        }
                    }
                }
            }

           
            return popunjeniNiz;
        }

        public bool IzmenaTermina(Termin t, Pacijent p)
        {

            TerminiRepository datoteka1 = new TerminiRepository();

            List<Termin> termini = datoteka1.CitanjeIzFajla();
            List<Termin> list = new List<Termin>();
            foreach(Termin tt in termini)
            {
                list.Add(tt);
                /*if (tt.pacijent.Jmbg.Equals(p.Jmbg))
                {
                   
                    if (tt.SifraTermina.Equals(t.SifraTermina))
                    {
                        list.Remove(tt);
                        list.Add(t);
                        datoteka1.UpisivanjeUFajl()
                    }*/


                }

            foreach(Termin t1 in termini)
            {
                //if (t1.pacijent.Jmbg.Equals(p.Jmbg))
                //{
                    if (t1.SifraTermina.Equals(t.SifraTermina))
                    {
                        list.Remove(t1);
                        list.Add(t);
                        datoteka1.UpisivanjeUFajl(list);
                        return true;

                    }
                //}
            }
            return false;

            

            /*PacijentRepository datoteka = new PacijentRepository();
            List<Pacijent> pacijenti = datoteka.CitanjeIzFajla();
            List<Termin> list = new List<Termin>();
            foreach (Pacijent pp in pacijenti)
            {
                if (pp.Jmbg.Equals(p.Jmbg))
                {
                    foreach (Termin tt in pp.GetTermin())
                    {
                        list.Add(tt);
                        if (tt.SifraTermina.Equals(t.SifraTermina))
                        {
                            list.Remove(tt);
                        }
                    }

                    list.Add(t);
                    pp.termin = list;
                    datoteka.UpisivanjeUFajl(pacijenti);
                    return true;

                }
            }
            return false;*/

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

    }
}