using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Controller
{
    public class OpremaController
    {
        private OpremaService opremaService = new OpremaService();

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
    }
}
