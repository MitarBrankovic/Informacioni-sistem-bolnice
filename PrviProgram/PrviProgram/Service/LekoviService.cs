using Model;
using Repository;
using System;
using System.Collections.Generic;

namespace Service
{
   public class LekoviService
   {
        private LekoviRepository lekoviRepository = new LekoviRepository();
        private static LekoviService instance = null;
        public static LekoviService GetInstance()
        {
            if (instance == null)
            {
                instance = new LekoviService();
            }
            return instance;
        }

        public bool DodavanjeLeka(Lek lek)
      {
            List<Lek> lekovi = lekoviRepository.CitanjeIzFajla();
            foreach (Lek lekBrojac in lekovi)
            {
                if (lekBrojac.Sifra.Equals(lek.Sifra))
                {
                    return false;
                }
            }
            lekovi.Add(lek);
            lekoviRepository.UpisivanjeUFajl(lekovi);
            return true;
        }
      
      public bool BrisanjeLeka(Lek lek)
      {
            List<Lek> lekovi = lekoviRepository.CitanjeIzFajla();
            foreach (Lek lekBrojac in lekovi)
            {
                if (lekBrojac.Sifra.Equals(lek.Sifra))
                {
                    lekovi.Remove(lekBrojac);
                    lekoviRepository.UpisivanjeUFajl(lekovi);
                    return true;
                }
            }
            return false;
      }

        public bool BrisanjeAlternativnihLekova(Lek lek)        
        {
            List<Lek> lekovi = lekoviRepository.CitanjeIzFajla();
            foreach (Lek lekBrojac in lekovi)
            {
                BrisanjeAlternativnog(lek, lekBrojac);
            }
            lekoviRepository.UpisivanjeUFajl(lekovi);
            return true;
        }

        public bool IzmenaLeka(Lek stariLek, Lek noviLek)
        {
            List<Lek> lekovi = lekoviRepository.CitanjeIzFajla();
            foreach (Lek lekBrojac in lekovi)
            {
                if (lekBrojac.Sifra.Equals(stariLek.Sifra))
                {
                    lekovi.Remove(lekBrojac);
                    lekovi.Add(noviLek);
                    lekoviRepository.UpisivanjeUFajl(lekovi);
                    return true;
                }
            }
            return false;
        }
        public void BrisanjeAlternativnog(Lek lek, Lek lekBrojac)
        {
            foreach (Lek alternativniLek in lekBrojac.ZamenaZaLek)
            {
                if (alternativniLek.Sifra.Equals(lek.Sifra))
                {
                    lekBrojac.ZamenaZaLek.Remove(alternativniLek);
                    break;
                }
            }
        }
    }
}