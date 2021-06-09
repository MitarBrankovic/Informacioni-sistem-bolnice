using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Controller
{
    public class SaleController
    {
        private SaleService saleService = new SaleService();
        private TerminiService terminiService = new TerminiService();
        public TerminiSaleService terminiSaleService= new TerminiSaleService();

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
            {
                terminiSaleService.BrisanjeSaleIzTermina(sala);
                return true;
            }
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

        public bool RenoviranjeSale(TerminRenoviranjaSale terminRenoviranjaSale)
        {
            if (saleService.RenoviranjeSale(terminRenoviranjaSale))
                return true;
            else
                return false;
        }
    }
}
