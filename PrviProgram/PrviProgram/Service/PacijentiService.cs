using System.Collections.Generic;
using Model;
using Repository;

namespace Service
{
    public class PacijentiService
    {
        public PacijentRepository pacijentRepository = new PacijentRepository();

        public bool DodavanjePacijenta(Pacijent pacijent)
        {
            List<Pacijent> pacijenti = pacijentRepository.CitanjeIzFajla();
            foreach (Pacijent p in pacijenti)
            {
                if (p.Jmbg.Equals(pacijent.Jmbg))
                {
                    return false;
                }
            }
            pacijenti.Add(pacijent);
            pacijentRepository.UpisivanjeUFajl(pacijenti);
            return true;
        }

        public bool BrisanjePacijenta(Pacijent pacijent)
        {
            List<Pacijent> pacijenti = pacijentRepository.CitanjeIzFajla();
            foreach (Pacijent p in pacijenti)
            {
                if (p.Jmbg.Equals(pacijent.Jmbg))
                {
                    pacijenti.Remove(p);
                    pacijentRepository.UpisivanjeUFajl(pacijenti);
                    return true;
                }
            }
            return false;
        }

        public bool IzmenaPacijenta(Pacijent stariPacijent, Pacijent noviPacijent)
        {
            List<Pacijent> pacijenti = pacijentRepository.CitanjeIzFajla();
            foreach (Pacijent p in pacijenti)
            {
                if (p.Jmbg.Equals(stariPacijent.Jmbg))
                {
                    pacijenti.Remove(p);
                    pacijenti.Add(noviPacijent);
                    pacijentRepository.UpisivanjeUFajl(pacijenti);
                    return true;
                }
            }
            return false;
        }
        public Pacijent PregledPacijenta(string jmbg)
        {
            List<Pacijent> pacijenti = pacijentRepository.CitanjeIzFajla();
            foreach (Pacijent p in pacijenti)
            {
                if (p.Jmbg.Equals(jmbg))
                {
                    return p;
                }
            }
            return null;
        }

        public List<Pacijent> PregledSvihPacijenata()
        {
            List<Pacijent> pacijenti = pacijentRepository.CitanjeIzFajla();
            return pacijenti;
        }

    }
}