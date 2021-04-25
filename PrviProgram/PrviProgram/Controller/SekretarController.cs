using Model;
using Service;
using Service.LekarService;

namespace Controller
{
    public class SekretarController
    {
        private PreglediService terminiService = new PreglediService();
     
        private PacijentiService pacijentiService = new PacijentiService();
        private AlergeniService alergeniService = new AlergeniService();

        public bool DodavanjePacijenta(Pacijent pacijent)
        {
            // TODO: implement
            return false;
        }

        public bool BrisanjePacijenta(Pacijent p)
        {
            // TODO: implement
            return false;
        }

        public bool IzmenaPacijenta(Pacijent stari, Pacijent novi)
        {
            // TODO: implement
            return false;
        }

        public bool DodavanjeAlergena(Alergen alergen)
        {
            return alergeniService.DodavanjeAlegena(alergen);
        }

        public bool BrisanjeAlergena(Alergen alergen)
        {
            return alergeniService.BrisanjeAlergena(alergen);
        }

        public bool IzmenaAlergena(Alergen stariAlergen, Alergen noviAlergen)
        {
            return alergeniService.IzmenaAlergena(stariAlergen, noviAlergen);
        }

        public bool ZakazivanjeTermina(Termin termin)
        {
            // TODO: implement
            return false;
        }

        public bool DodavanjeGuestPacijenta(GuestPacijent guestPacijent)
        {
            // TODO: implement
            return false;
        }

        public bool BrisanjeGuestPacijenta(GuestPacijent guestPacijent)
        {
            // TODO: implement
            return false;
        }

        public bool IzmenaGuestPacijenta(GuestPacijent stari, GuestPacijent novi)
        {
            // TODO: implement
            return false;
        }

    }
}