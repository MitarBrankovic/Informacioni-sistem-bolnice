using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Controller
{
    public class TerminiZaAnketiranjeController
    {
        public bool DaLiPostojiBarJedanIzvrsenTermin(Pacijent pacijent)
        {
            return TerminiZaAnketiranjeService.getInstance().DaLiPostojiBarJedanIzvrsenTermin(pacijent);
        }

        public List<Termin> SviTerminiKojiSuIzvrseni(Pacijent pacijent)
        {
            return TerminiZaAnketiranjeService.getInstance().SviTerminiKojiSuIzvrseni(pacijent);
        }
    }
}
