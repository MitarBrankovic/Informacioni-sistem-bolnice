using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrviProgram.Service
{
    public class UpravnikService
    {
        private UpravnikRepository upravnikRepository = new UpravnikRepository();

        public bool IzmenaUpravnika(Upravnik stariUpravnik, Upravnik noviUpravnik)
        {
            List<Upravnik> upravnici = upravnikRepository.PregledSvihUpravnika();
            foreach (Upravnik upravnikBrojac in upravnici)
            {
                if (upravnikBrojac.Jmbg.Equals(stariUpravnik.Jmbg))
                {
                    upravnici.Remove(upravnikBrojac);
                    upravnici.Add(noviUpravnik);
                    upravnikRepository.UpisivanjeUFajl(upravnici);
                    return true;
                }
            }
            return false;
        }

    }
}
