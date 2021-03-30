using Model;
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
            List<Pacijent> pacijenti = datoteka.CitanjeIzFajla();
            List<Termin> termini = new List<Termin>();
            termini.Add(t);

            foreach (Pacijent pp in pacijenti)
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
            }
            datoteka.UpisivanjeUFajl(pacijenti);


        }

        public void BrisanjeTermina(Termin t, Pacijent p)
        {
            DatotekaPacijent datoteka = new DatotekaPacijent();
            List<Pacijent> pacijenti = datoteka.CitanjeIzFajla();
            List<Termin> termini = new List<Termin>();

            foreach (Pacijent pp in pacijenti)
            {
                if (pp.Jmbg.Equals(p.Jmbg))
                {
                    foreach (Termin tt in pp.GetTermin())
                    {
                        termini.Add(tt);
                        if (tt.Sifra.Equals(t.Sifra))
                        {
                            termini.Remove(tt);
                        }

                    }

                    pp.termin = termini;
                    break;

                }

            }
            datoteka.UpisivanjeUFajl(pacijenti);


        }



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
                        if (tt.Sifra.Equals(t.Sifra))
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

            foreach (Pacijent pp in pacijenti)
            {
                if (pp.Jmbg.Equals(p.Jmbg))
                {
                    foreach (Termin t in pp.GetTermin())
                    {
                        list.Add(t);

                    }

                }

            }
            return list;


        }

    }
}