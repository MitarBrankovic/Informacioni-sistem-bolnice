using Model;
using PrviProgram.RadSaDatotekama;
using RadSaDatotekama;
using System.Collections.Generic;

namespace Logika.LogikaPacijent
{
    public class UpravljanjeTerminima
    {
        private static UpravljanjeTerminima instance = null;
        public static UpravljanjeTerminima getInstance()
        {
            if (instance == null)
            {
                instance = new UpravljanjeTerminima();
            }
            return instance;
        }
        public void DodavanjeTermina(Termin t, Pacijent p)
        {
            DatotekaPacijent datoteka = new DatotekaPacijent();
            DatotekaTermini datoteka1 = new DatotekaTermini();

            List<Termin> termini = datoteka1.CitanjeIzFajla();


            foreach(Termin tt in termini)
            {
                if(tt.pacijent.Jmbg.Equals(p.Jmbg) && t.SifraTermina.Equals(tt.SifraTermina))
                {

                }
            }
            t.pacijent = p;
            termini.Add(t);

            /*foreach (Pacijent pp in pacijenti)
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
            datoteka1.UpisivanjeUFajl(termini);


        }

        public void BrisanjeTermina(Termin t, Pacijent p)
        {


            DatotekaTermini datoteka1 = new DatotekaTermini();

            List<Termin> termini = datoteka1.CitanjeIzFajla();

            foreach (Termin tt in termini)
            {
                if (tt.pacijent.Jmbg.Equals(p.Jmbg) && t.SifraTermina.Equals(tt.SifraTermina))
                {
                    termini.Remove(t);
                }
            }
            datoteka1.UpisivanjeUFajl(termini);
        }

    

            /*foreach (Pacijent pp in pacijenti)
            {
                if (pp.Jmbg.Equals(p.Jmbg))
                {
                    foreach (Termin tt in pp.GetTermin())
                    {
                        termini.Add(tt);
                        if (tt.SifraTermina.Equals(t.SifraTermina))
                        {
                            termini.Remove(tt);
                        }

                    }*/

            //        pp.termin = termini;
              //      break;

    



        public bool IzmenaTermina(Termin t, Pacijent p)
        {
            DatotekaPacijent datoteka = new DatotekaPacijent();
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
            return false;

        }

        public List<Termin> PregledTermina(Pacijent p)
        {

            DatotekaPacijent datoteka = new DatotekaPacijent();
            List<Pacijent> pacijenti = datoteka.CitanjeIzFajla();
            List<Termin> list = new List<Termin>();
            DatotekaTermini datotek1 = new DatotekaTermini();
            List<Termin> termini = datotek1.CitanjeIzFajla();

            foreach (Termin pp in termini)
            {
                if (pp.pacijent.Jmbg.Equals(p.Jmbg))
                {
                        list.Add(pp);

                }

            }
            return list;
        }


        public bool IzmenaSale(Sala staraSala, Sala novaSala)
        {
            DatotekaPacijent datoteka = new DatotekaPacijent();
            List<Pacijent> pacijenti = datoteka.CitanjeIzFajla();
            List<Termin> list = new List<Termin>();
            Sala s = new Sala();
            foreach (Pacijent pp in pacijenti)
            {    
                foreach (Termin tt in pp.GetTermin())
                {
                    list.Add(tt);
                    if (tt.sala.Sifra.Equals(staraSala.Sifra))
                    {
                        list.Remove(tt);
                        tt.sala = novaSala;
                        list.Add(tt);
                    }
                }
                pp.termin = list;
                datoteka.UpisivanjeUFajl(pacijenti);
                return true;

                
            }
            return false;

        }

    }
}