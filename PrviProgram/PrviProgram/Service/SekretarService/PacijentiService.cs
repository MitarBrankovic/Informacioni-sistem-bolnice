using Model;
using PrviProgram.Repository;
using System.Collections.Generic;

namespace Service.SekretarService
{
    public class PacijentiService
    {
        public bool DodavanjePacijenta(Pacijent pacijent)
        {
            PacijentRepository datoteka = new PacijentRepository();
            List<Pacijent> pacijenti = datoteka.CitanjeIzFajla();
            foreach (Pacijent p in pacijenti)
            {
                if (p.Jmbg.Equals(pacijent.Jmbg))
                {
                    return false;
                }
            }
            pacijenti.Add(pacijent);
            datoteka.UpisivanjeUFajl(pacijenti);
            return true;
        }

        public Pacijent PregledPacijenta(string jmbg)
        {
            PacijentRepository datoteka = new PacijentRepository();
            List<Pacijent> pacijenti = datoteka.CitanjeIzFajla();
            foreach (Pacijent p in pacijenti)
            {
                if (p.Jmbg.Equals(jmbg))
                {
                    return p;
                }
            }
            return null;
        }

        public bool BrisanjePacijenta(Pacijent pacijent)
        {
            PacijentRepository datoteka = new PacijentRepository();
            List<Pacijent> pacijenti = datoteka.CitanjeIzFajla();
            foreach (Pacijent p in pacijenti)
            {
                if (p.Jmbg.Equals(pacijent.Jmbg))
                {
                    pacijenti.Remove(p);
                    datoteka.UpisivanjeUFajl(pacijenti);
                    return true;
                }
            }
            return false;
        }

        public bool IzmenaPacijenta(Pacijent stariPacijent, Pacijent noviPacijent)
        {
            PacijentRepository datoteka = new PacijentRepository();
            List<Pacijent> pacijenti = datoteka.CitanjeIzFajla();
            foreach (Pacijent p in pacijenti)
            {
                if (p.Jmbg.Equals(stariPacijent.Jmbg))
                {
                    pacijenti.Remove(p);
                    pacijenti.Add(noviPacijent);
                    datoteka.UpisivanjeUFajl(pacijenti);
                    return true;
                }
            }
            return false;
        }

        public List<Pacijent> PregledSvihPacijenata()
        {
            PacijentRepository datoteka = new PacijentRepository();
            List<Pacijent> pacijenti = datoteka.CitanjeIzFajla();
            return pacijenti;
        }

    }
}