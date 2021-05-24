using System.Collections.Generic;
using Model;
using Repository;

namespace Service
{
    public class VestiService
    {
        private VestiRepository vestiRepository = new VestiRepository();

        public bool DodavanjeVesti(Vest vest)
        {
            if (vestiRepository.PregledVesti(vest.SifraVesti) != null)
            {
                return false;
            }
            List<Vest> vesti = vestiRepository.PregledSvihVesti();
            vesti.Add(vest);
            vestiRepository.UpisivanjeUFajl(vesti);
            return true;
        }

        public bool BrisanjeVesti(Vest vest)
        {
            List<Vest> vesti = vestiRepository.PregledSvihVesti();
            foreach (Vest vestToRemove in vesti)
            {
                if (vestToRemove.SifraVesti.Equals(vest.SifraVesti))
                {
                    vesti.Remove(vestToRemove);
                    vestiRepository.UpisivanjeUFajl(vesti);
                    return true;
                }
            }
            return false;
        }

        public bool IzmenaVesti(Vest vest)
        {
            List<Vest> vesti = vestiRepository.PregledSvihVesti();
            foreach (Vest vestToChange in vesti)
            {
                if (vestToChange.SifraVesti.Equals(vest.SifraVesti))
                {
                    vesti.Remove(vestToChange);
                    vesti.Add(vest);
                    vestiRepository.UpisivanjeUFajl(vesti);
                    return true;
                }
            }
            return false;
        }

    }
}