using Model;
using PrviProgram.Repository;
using Service;
using Service.LekarService;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Repository;

namespace PrviProgram.Logika
{
    class ControllerUpravnik
    {
        private static ControllerUpravnik instance = null;
        public static ControllerUpravnik getInstance()
        {
            if (instance == null)
            {
                instance = new ControllerUpravnik();
            }
            return instance;
        }

        private PacijentRepository pacijentRepository = new PacijentRepository();
        public SaleService upravljanjeSalama;
        public PacijentiService upravljanjePacijentima;
        public PreglediService upravljanjePregledima;
        public TerminiService UpravljanjeTerminima;

        public TerminiRepository datotekaTermini;

        public ControllerUpravnik()
        {
            upravljanjeSalama = new SaleService();
            upravljanjePacijentima = new PacijentiService();
            upravljanjePregledima = new PreglediService();
            UpravljanjeTerminima = new TerminiService();

            datotekaTermini = new TerminiRepository();
        }

        public void BrisanjeSale(Sala sala, ObservableCollection<Model.Sala> sale)
        {
            SaleService.GetInstance().BrisanjeSale(sala);
            sale.Remove(sala);

            List<Termin> termini = new List<Termin>();
            termini = upravljanjePregledima.PregledSvihPregleda();
            foreach (Termin t in termini)
            {
                if (t != null)
                {
                    if (t.sala.Sifra == sala.Sifra)
                    {
                        t.sala = null;
                        upravljanjePregledima.IzmenaPregleda(t);
                    }
                }
            }

            List<Pacijent> pacijenti = new List<Pacijent>();
            pacijenti = pacijentRepository.PregledSvihPacijenata();
            foreach (Pacijent p in pacijenti)
            {
                if (p != null)
                {
                    foreach (Termin t in p.termin)
                    {
                        if (t != null)
                        {
                            if (t.sala.Sifra == sala.Sifra)
                            {
                                t.sala = null;
                                UpravljanjeTerminima.IzmenaTermina(t);
                            }
                        }
                    }
                }
            }
        }
    }
}
