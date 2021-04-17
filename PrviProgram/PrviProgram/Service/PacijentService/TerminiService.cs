using Model;
using Repository;
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
            PacijentRepository datoteka = new PacijentRepository();
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
            PacijentRepository datoteka = new PacijentRepository();
            List<Pacijent> pacijenti = datoteka.CitanjeIzFajla();
            List<Termin> termini = new List<Termin>();

            foreach (Pacijent pp in pacijenti)
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

                    }

                    pp.termin = termini;
                    break;

                }

            }
            datoteka.UpisivanjeUFajl(pacijenti);


        }



        public bool IzmenaTermina(Termin t, Pacijent p)
        {
            PacijentRepository datoteka = new PacijentRepository();
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

            PacijentRepository datoteka = new PacijentRepository();
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


        public bool IzmenaSale(Sala staraSala, Sala novaSala)
        {
            PacijentRepository datoteka = new PacijentRepository();
            List<Pacijent> pacijenti = datoteka.CitanjeIzFajla();
            List<Termin> list = new List<Termin>();
            Sala s = new Sala();
            foreach (Pacijent pp in pacijenti)
            {
                foreach (Termin tt in pp.termin)
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