using System.Collections.Generic;
using Model;
using Repository;

namespace Service
{
    public class PacijentiService
    {
        private PacijentRepository pacijentRepository = new PacijentRepository();

        public bool DodavanjePacijenta(Pacijent pacijent)
        {
            if (pacijentRepository.PregledPacijenta(pacijent.Jmbg) != null)
            {
                return false;
            }
            List<Pacijent> pacijenti = pacijentRepository.PregledSvihPacijenata();
            pacijenti.Add(pacijent);
            pacijentRepository.UpisivanjeUFajl(pacijenti);
            return true;
        }

        public bool BrisanjePacijenta(Pacijent pacijent)
        {
            List<Pacijent> pacijenti = pacijentRepository.PregledSvihPacijenata();
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
            List<Pacijent> pacijenti = pacijentRepository.PregledSvihPacijenata();
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

    }
}