using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrviProgram.Controller
{
    public class BeleskaController
    {

        public void DodavanjeBeleske(Beleska beleska)
        {
            BeleskaService.getInstance().DodavanjeBeleske(beleska);
        }
        public Pacijent DodavanjeBeleskeUKarton(Pacijent pacijent, IzvrseniPregled izvrsen)
        {
            return KartonPacijentaService.getInstance().DodavanjeBeleskeUKarton(pacijent, izvrsen);
        }
    }
}
