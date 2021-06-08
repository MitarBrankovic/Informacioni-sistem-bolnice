using Model;
using PrviProgram.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class PrimedbeNaLekService
    {
        private IPrimedbeNaLekRepository primedbeNaLekRepository = Factory.KreirajPrimedeRep();
        public void KreiranjePrimedbe(PrimedbaNaLek primedbaNaLek)
        {
            List<PrimedbaNaLek> primedbe = primedbeNaLekRepository.CitanjeIzFajla();
            primedbe.Add(primedbaNaLek);
            primedbeNaLekRepository.UpisivanjeUFajl(primedbe);
        }

        public bool BrisanjePrimedbe(PrimedbaNaLek primedba)
        {
            List<PrimedbaNaLek> primedbe = primedbeNaLekRepository.CitanjeIzFajla();
            foreach (PrimedbaNaLek primedbaBrojac in primedbe)
            {
                if (primedbaBrojac.Sifra.Equals(primedba.Sifra))
                {
                    primedbe.Remove(primedbaBrojac);
                    primedbeNaLekRepository.UpisivanjeUFajl(primedbe);
                    return true;
                }
            }
            return false;
        }
    }
}
