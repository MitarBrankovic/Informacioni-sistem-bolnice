using System;
using Model;
using Service;

namespace Controller
{
    public class UpravnikController
   {
      public bool DodavanjeOpreme(Oprema oprema, Sala sala)
      {
            if (OpremaService.GetInstance().DodavanjeOpreme(oprema, sala) == true)
            {
                return true;
            }
            else { return false; }
        }
      
      public bool BrisanjeOpreme(Oprema oprema, Sala sala)
      {
            if (OpremaService.GetInstance().BrisanjeOpreme(oprema, sala) == true)
            {
                return true;
            }
            else { return false; }
        }
      
      public bool IzmenaOpreme(Oprema staraOprema, Oprema novaOprema, Sala sala)
      {
            if (OpremaService.GetInstance().IzmenaOpreme(staraOprema, novaOprema, sala) == true)
            {
                return true;
            }
            else { return false; }
        }
      
      public bool PremestanjeOpreme(Oprema oprema, Sala staraSala, Sala novaSala)
      {
            if (OpremaService.GetInstance().PremestanjeOpreme(oprema, staraSala, novaSala) == true)
            {
                return true;
            }
            else { return false; }
        }
      
      public bool DodavanjeSale(Sala sala)
      {
            if (SaleService.GetInstance().DodavanjeSale(sala) == true)
            {
                return true;
            }
            else { return false; }
        }
      
      public bool BrisanjeSale(Sala sala)
      {
            if (SaleService.GetInstance().BrisanjeSale(sala) == true)
            {
                return true;
            }
            else { return false; }
        }
      
      public bool IzmenaSale(Sala staraSala, Sala novaSala)
      {
            if (SaleService.GetInstance().IzmenaSale(staraSala, novaSala) == true)
            {
                return true;
            }
            else { return false; }
        }

        public bool RenoviranjeSale(Sala sala, DateTime pocetakRenoviranja, DateTime krajRenoviranja)
        {
            if (SaleService.GetInstance().RenoviranjeSale(sala, pocetakRenoviranja, krajRenoviranja) == true)
            {
                return true;
            }
            else { return false; }
        }

        public bool DodavanjeLeka(Lek lek)
        {
            if (LekoviService.GetInstance().DodavanjeLeka(lek) == true)
            {
                return true;
            }
            else { return false; }
        }

        public bool BrisanjeLeka(Lek lek)
        {
            if (LekoviService.GetInstance().BrisanjeLeka(lek) == true)
            {
                LekoviService.GetInstance().BrisanjeAlternativnihLekova(lek);
                return true;
            }
            else { return false; }
        }

        public bool IzmenaLeka(Lek stariLek, Lek noviLek)
        {
            if (LekoviService.GetInstance().IzmenaLeka(stariLek, noviLek) == true)
            {
                return true;
            }
            else { return false; }
        }

        public SaleService saleService;
        public OpremaService opremaService;
        public LekoviService lekoviService;
   
   }
}