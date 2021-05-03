using Model;
using PrviProgram.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrviProgram.Service
{
    class PrimedbeNaLekService
    {
        private PrimedbeNaLekRepository primedbeNaLekRepository = new PrimedbeNaLekRepository();
        public void KreiranjePrimedbe(PrimedbaNaLek primedbaNaLek)
        {
            List<PrimedbaNaLek> primedbe = primedbeNaLekRepository.CitanjeIzFajla();
            primedbe.Add(primedbaNaLek);
            primedbeNaLekRepository.UpisivanjeUFajl(primedbe);
        }
    }
}
