/***********************************************************************
 * Module:  OpremaService.cs
 * Author:  Nesa
 * Purpose: Definition of the Class Service.LogikaUpravnik.OpremaService
 ***********************************************************************/

using System;
using Model;
using Service;

namespace Controller
{
    public class UpravnikController
   {
      public void DodavanjeOpreme(Oprema oprema, Sala sala)
      {
            OpremaService.getInstance().DodavanjeOpreme(oprema, sala);
      }
      
      public void BrisanjeOpreme(Oprema oprema, Sala sala)
      {
            OpremaService.getInstance().BrisanjeOpreme(oprema, sala);
      }
      
      public void IzmenaOpreme(Oprema staraOprema, Oprema novaOprema, Sala sala)
      {
            OpremaService.getInstance().IzmenaOpreme(staraOprema, novaOprema, sala);
      }
      
      public void PremestanjeOpreme(Oprema oprema, Sala staraSala, Sala novaSala)
      {
            OpremaService.getInstance().PremestanjeOpreme(oprema, staraSala, novaSala);
      }
      
      public void DodavanjeSale(Sala sala)
      {
            SaleService.getInstance().DodavanjeSale(sala);
      }
      
      public void BrisanjeSale(Sala sala)
      {
            SaleService.getInstance().BrisanjeSale(sala);
      }
      
      public void IzmenaSale(Sala stara, Sala nova)
      {
            SaleService.getInstance().IzmenaSale(stara, nova);
      }

        public bool RenoviranjeSale(Sala sala)
        {
            // TODO: implement
            return false;
        }

        public SaleService saleService;
      public OpremaService opremaService;
   
   }
}