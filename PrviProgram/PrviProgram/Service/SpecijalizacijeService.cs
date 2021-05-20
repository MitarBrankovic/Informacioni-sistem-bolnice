using System.Collections.Generic;
using Model;
using Repository;

namespace Service
{
    class SpecijalizacijeService
    {
        private SpecijalizacijeRepository specijalizacijeRepository = new SpecijalizacijeRepository();

        public bool DodavanjeSpecijalizacije(Specijalizacija specijalizacija)
        {
            if (specijalizacijeRepository.PregledSpecijalizacije(specijalizacija.Naziv) != null)
            {
                return false;
            }
            List<Specijalizacija> specijalizacije = specijalizacijeRepository.PregledSvihSpecijalizacija();
            specijalizacije.Add(specijalizacija);
            specijalizacijeRepository.UpisivanjeUFajl(specijalizacije);
            return true;
        }
        
    }
}
