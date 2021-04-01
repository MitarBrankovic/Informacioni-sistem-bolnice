using System;
using System.Collections.Generic;
using System.Text;
using Logika.LogikaUpravnik;
using Logika.LogikaLekar;
using Logika.LogikaPacijent;
using Logika.LogikaSekretar;
using Model;
using PrviProgram.RadSaDatotekama;
using System.Collections.ObjectModel;

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

        public UpravljanjeSalama upravljanjeSalama;
        public UpravljanjePacijentima upravljanjePacijentima;
        public UpravljanjePregledima upravljanjePregledima;
        public UpravljanjeTerminima UpravljanjeTerminima;

        public DatotekaTermini datotekaTermini;

        public ControllerUpravnik()
        {
            upravljanjeSalama = new UpravljanjeSalama();
            upravljanjePacijentima = new UpravljanjePacijentima();
            upravljanjePregledima = new UpravljanjePregledima();
            UpravljanjeTerminima = new UpravljanjeTerminima();

            datotekaTermini = new DatotekaTermini();
        }

        public void BrisanjeSale(Sala sala, ObservableCollection<Model.Sala> sale)
        {
            UpravljanjeSalama.getInstance().BrisanjeSale(sala);
            sale.Remove(sala);

            List<Termin> termini = new List<Termin>();
            termini = upravljanjePregledima.PregledSvihPregleda();
            foreach(Termin t in termini)
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
            pacijenti = upravljanjePacijentima.PregledSvihPacijenata();
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
                                UpravljanjeTerminima.IzmenaTermina(t, p);
                            }
                        }
                    }
                }
            }
        }
    }
}
