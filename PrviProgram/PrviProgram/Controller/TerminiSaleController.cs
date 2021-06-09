using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrviProgram.Controller
{
    public class TerminiSaleController
    {
        TerminiSaleService terminiSaleService=new TerminiSaleService();
        public Sala DobavljanjeSale(Termin noviTermin)
        {
            return terminiSaleService.DobavljanjeSale(noviTermin);
        }

        public void BrisanjeSaleIzTermina(Sala sala)
        {
            terminiSaleService.BrisanjeSaleIzTermina(sala);
        }
        public bool IzmenaSale(Sala staraSala, Sala novaSala)
        {
            return terminiSaleService.IzmenaSale(staraSala, novaSala);
        }
    }
}
