using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Controller
{
    public class NotifikacijeController
    {
        NotifikacijeService notifikacijeService = new NotifikacijeService();
        public void DodavanjeNotifikacije(NotifikacijePacijenta notifikacija)
        {
            notifikacijeService.DodavanjeNotifikacije(notifikacija);
        }

        public List<Recept> PregledRecepata(Pacijent pacijent)
        {
            return notifikacijeService.PregledRecepata(pacijent);
        }

        public string PronadjiOpis(Pacijent pacijent, Recept recept)
        {

            return notifikacijeService.PronadjiOpis(recept, pacijent);
        }
    }
}
