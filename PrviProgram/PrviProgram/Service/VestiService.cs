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
            List<Vest> vesti = vestiRepository.PregledSvihVesti();
            foreach (Vest v in vesti)
            {
                if (v.SifraVesti.Equals(vest.SifraVesti))
                {
                    return false;
                }
            }
            vesti.Add(vest);
            vestiRepository.UpisivanjeUFajl(vesti);
            return true;
        }

        public bool BrisanjeVesti(Vest vest)
        {
            List<Vest> vesti = vestiRepository.PregledSvihVesti();
            foreach (Vest v in vesti)
            {
                if (v.SifraVesti.Equals(vest.SifraVesti))
                {
                    vesti.Remove(v);
                    vestiRepository.UpisivanjeUFajl(vesti);
                    return true;
                }
            }
            return false;
        }

        public bool IzmenaVesti(Vest vest)
        {
            List<Vest> vesti = vestiRepository.PregledSvihVesti();
            foreach (Vest v in vesti)
            {
                if (v.SifraVesti.Equals(vest.SifraVesti))
                {
                    vesti.Remove(v);
                    vesti.Add(vest);
                    vestiRepository.UpisivanjeUFajl(vesti);
                    return true;
                }
            }
            return false;
        }

    }
}