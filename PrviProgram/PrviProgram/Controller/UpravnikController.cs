using System;
using Model;
using Service;

namespace Controller
{
    public class UpravnikController
   {
        private SaleService saleService = new SaleService();
        private OpremaService opremaService = new OpremaService();
        private LekoviService lekoviService = new LekoviService();

        public bool DodavanjeOpreme(Oprema oprema, Sala sala)
      {
            if (opremaService.DodavanjeOpreme(oprema, sala))
                return true;
            else
                return false;
        }
      
      public bool BrisanjeOpreme(Oprema oprema, Sala sala)
      {
            if (opremaService.BrisanjeOpreme(oprema, sala))
                return true;
            else
                return false;
        }
      
      public bool IzmenaOpreme(Oprema staraOprema, Oprema novaOprema, Sala sala)
      {
            if (opremaService.IzmenaOpreme(staraOprema, novaOprema, sala))
                return true;
            else
                return false;
        }
      
      public bool PremestanjeOpreme(Oprema oprema, Sala staraSala, Sala novaSala)
      {
            if (opremaService.PremestanjeOpreme(oprema, staraSala, novaSala))
                return true;
            else
                return false;
        }
      
      public bool DodavanjeSale(Sala sala)
      {
            if (saleService.DodavanjeSale(sala))
                return true;
            else
                return false;
        }
      
      public bool BrisanjeSale(Sala sala)
      {
            if (saleService.BrisanjeSale(sala))
                return true;
            else
                return false;
        }
      
      public bool IzmenaSale(Sala staraSala, Sala novaSala)
      {
            if (saleService.IzmenaSale(staraSala, novaSala))
                return true;
            else
                return false;
        }

        public bool RenoviranjeSale(Sala sala, DateTime pocetakRenoviranja, DateTime krajRenoviranja)
        {
            if (saleService.RenoviranjeSale(sala, pocetakRenoviranja, krajRenoviranja))
                return true;
            else
                return false;
        }

        public bool DodavanjeLeka(Lek lek)
        {
            if (lekoviService.DodavanjeLeka(lek))
                return true;
            else
                return false;
        }

        public bool BrisanjeLeka(Lek lek)
        {
            if (lekoviService.BrisanjeLeka(lek))
            {
                lekoviService.BrisanjeAlternativnihLekova(lek);
                return true;
            }
            else 
                return false;
        }

        public bool IzmenaLeka(Lek stariLek, Lek noviLek)
        {
            if (lekoviService.IzmenaLeka(stariLek, noviLek))
                return true;
            else
                return false;
        }
   }
}