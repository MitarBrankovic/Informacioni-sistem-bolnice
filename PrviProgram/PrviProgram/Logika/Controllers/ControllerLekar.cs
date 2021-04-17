using Model;
using Service.LekarService;
using Service.PacijentService;
using Service.SekretarService;
using Service.UpravnikService;
using System.Collections.ObjectModel;

namespace PrviProgram.Logika.Controllers
{
    public class ControllerLekar
    {
        private static ControllerLekar instance = null;
        public static ControllerLekar getInstance()
        {
            if (instance == null)
            {
                instance = new ControllerLekar();
            }
            return instance;
        }

        public SaleService upravljanjeSalama;
        public PacijentiService upravljanjePacijentima;
        public PreglediService upravljanjePregledima;
        public TerminiService UpravljanjeTerminima;

        public ControllerLekar()
        {
            upravljanjeSalama = new SaleService();
            upravljanjePacijentima = new PacijentiService();
            upravljanjePregledima = new PreglediService();
            UpravljanjeTerminima = new TerminiService();

        }

        public void BrisanjeTermina(Termin termin, ObservableCollection<Termin> termini)
        {
            upravljanjePregledima.BrisanjePregleda(termin.SifraTermina);
            UpravljanjeTerminima.BrisanjeTermina(termin, termin.pacijent);

            termini.Remove(termin);
        }

        public void IzmenaTermina(Termin termin, Pacijent pacijent)
        {
            upravljanjePregledima.IzmenaPregleda(termin);
            UpravljanjeTerminima.IzmenaTermina(termin, pacijent);
        }

        public void DodavanjeTermina(Termin termin, Pacijent pacijent)
        {
            upravljanjePregledima.DodavanjePregleda(termin);
            UpravljanjeTerminima.DodavanjeTermina(termin, pacijent);
        }
    }
}
