using System.Collections.Generic;
using Model;
using Repository;

namespace Service
{
    public class LekariService
    {
        public ILekarRepository lekarRepository = new LekarRepository();

        public bool DodavanjeLekara(Lekar lekar)
        {
            if (lekarRepository.PregledLekara(lekar.Jmbg) != null)
            {
                return false;
            }
            List<Lekar> lekari = lekarRepository.PregledSvihLekara();
            lekari.Add(lekar);
            lekarRepository.UpisivanjeUFajl(lekari);
            return true;
        }

        public bool BrisanjeLekara(Lekar lekar)
        {
            List<Lekar> lekari = lekarRepository.PregledSvihLekara();
            foreach (Lekar l in lekari)
            {
                if (l.Jmbg.Equals(lekar.Jmbg))
                {
                    lekari.Remove(l);
                    lekarRepository.UpisivanjeUFajl(lekari);
                    return true;
                }
            }
            return false;
        }

        public bool IzmenaLekara(Lekar stariLekar, Lekar noviLekar)
        {
            List<Lekar> lekari = lekarRepository.PregledSvihLekara();
            foreach (Lekar l in lekari)
            {
                if (l.Jmbg.Equals(stariLekar.Jmbg))
                {
                    lekari.Remove(l);
                    lekari.Add(noviLekar);
                    lekarRepository.UpisivanjeUFajl(lekari);
                    return true;
                }
            }
            return false;
        }

    }
}