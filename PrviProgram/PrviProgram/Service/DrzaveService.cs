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
            List<Drzava> drzave = drzaveRepository.PregledSvihDrzava();
            foreach (Drzava d in drzave)
            {
                if (d.Ime.Equals(drzava.Ime))
                {
                    return false;
                }
            }
            drzave.Add(drzava);
            drzaveRepository.UpisivanjeUFajl(drzave);
            return false;
        }
        
    }
}
