using Logika.LogikaLekar;
using Logika.LogikaPacijent;
using Logika.LogikaSekretar;
using Logika.LogikaUpravnik;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

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

        public UpravljanjeSalama upravljanjeSalama;
        public UpravljanjePacijentima upravljanjePacijentima;
        public UpravljanjePregledima upravljanjePregledima;
        public UpravljanjeTerminima UpravljanjeTerminima;

        public ControllerLekar()
        {
            upravljanjeSalama = new UpravljanjeSalama();
            upravljanjePacijentima = new UpravljanjePacijentima();
            upravljanjePregledima = new UpravljanjePregledima();
            UpravljanjeTerminima = new UpravljanjeTerminima();

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
