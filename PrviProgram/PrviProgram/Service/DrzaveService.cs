using System.Collections.Generic;
using Model;
using Repository;

namespace Service
{
    class DrzaveService
    {
        private DrzaveRepository drzaveRepository = new DrzaveRepository();

        public bool DodavanjeDrzave(Drzava drzava)
        {
            if (drzaveRepository.PregledDrzave(drzava.Ime) != null)
            {
                return false;
            }
            List<Drzava> drzave = drzaveRepository.PregledSvihDrzava();
            drzave.Add(drzava);
            drzaveRepository.UpisivanjeUFajl(drzave);
            return true;
        }
        
    }
}
