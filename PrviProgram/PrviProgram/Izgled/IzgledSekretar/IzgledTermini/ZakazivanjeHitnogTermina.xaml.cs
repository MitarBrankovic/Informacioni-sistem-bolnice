using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Model;
using PrviProgram.Controller;
using Repository;
using Service;

namespace PrviProgram.Izgled.IzgledSekretar.IzgledTermini
{
    public partial class ZakazivanjeHitnogTermina : Window
    {
        private ILekarSpecijalizacija lekarSpecijalizacija = new LekarRepository();
        private PacijentRepository pacijentRepository = new PacijentRepository();
        private LekarRepository lekarRepository = new LekarRepository();
        private SpecijalizacijeRepository specijalizacijeRepository = new SpecijalizacijeRepository();
        private TerminiService terminiService = new TerminiService();
        private UtilityService utilityService = new UtilityService();
        private ObservableCollection<Termin> termini;
        public static GuestPacijent guestPacijent;
        public TerminiSaleController terminiSaleController = new TerminiSaleController();

        public ZakazivanjeHitnogTermina(ObservableCollection<Termin> termini)
        {
            InitializeComponent();
            this.termini = termini;
            InicijalizacijaCombo();
            PostaviVremeTerminaZaCombo();
        }

        private void InicijalizacijaCombo()
        {
            comboBoxSpecijalizacija.ItemsSource = specijalizacijeRepository.PregledSvihSpecijalizacija();
            comboBoxPacijenti.ItemsSource = pacijentRepository.PregledSvihPacijenata();
            TipTerminaText.ItemsSource = Enum.GetValues(typeof(TipTermina));
            vremeText.ItemsSource = utilityService.nizVremena;
        }

        private void PostaviVremeTerminaZaCombo()
        {
            string sledeciTermin = IzracunajPrvoSledeceVremeTermina(DateTime.Now);
            foreach (string vremeTermina in utilityService.nizVremena)
            {
                if (vremeTermina.Equals(sledeciTermin))
                {
                    vremeText.SelectedIndex = utilityService.nizVremena.IndexOf(vremeTermina);
                }
            }
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            Termin termin = PreuzmiTerminIzForme();
            termin.lekar = PronadjiLekaraSaSlobodnimTerminom(termin);
            if (termin.lekar == null)
            {
                PregledTerminaHitnoZakazivanje pregledTerminaHitnoZakazivanje = new PregledTerminaHitnoZakazivanje(termini, termin, (Specijalizacija)comboBoxSpecijalizacija.SelectedItem);
                pregledTerminaHitnoZakazivanje.Show();
            }
            else
            {
                PotvrdaZakazivanjaTermina potvrdaZakazivanjaTermina = new PotvrdaZakazivanjaTermina(termini, termin);
                potvrdaZakazivanjaTermina.Show();
            }
            Close();
        }

        private Lekar PronadjiLekaraSaSlobodnimTerminom(Termin termin)
        {
            List<Lekar> lekari = lekarSpecijalizacija.PregledLekaraOdredjeneSpecijalizacije((Specijalizacija)comboBoxSpecijalizacija.SelectedItem);
            foreach (Termin t in termini)
            {
                if (t.Vreme == termin.Vreme && t.Datum.Date.Equals(termin.Datum.Date))
                {
                    lekari.Remove(lekari.SingleOrDefault(lekar => lekar.Jmbg.Equals(t.lekar.Jmbg)));
                }
            }
            return lekari.Count() == 0 ? null : lekari.First();
        }

        private Termin PreuzmiTerminIzForme()
        {
            Termin termin = new Termin((TipTermina)TipTerminaText.SelectedItem, utilityService.GenerisanjeSifre(),
                                       vremeText.Text);
            if (guestPacijent == null)
            {
                termin.pacijent = (Pacijent)comboBoxPacijenti.SelectedItem;
            }
            else
            {
                termin.guestPacijent = guestPacijent;
            }
            termin.sala = terminiSaleController.DobavljanjeSale(termin);
            termin.Datum = DateTime.Today;
            return termin;
        }

        private string IzracunajPrvoSledeceVremeTermina(DateTime datum)
        {
            DateTime datumTermina = datum;
            if (datum.Minute.CompareTo(30) > 0)
            {
                datumTermina = datum.AddHours(1).AddMinutes(-datum.Minute);
            }
            else if (datum.Minute.CompareTo(30) < 0)
            {
                datumTermina = datum.AddMinutes(30-datum.Minute);
            }
            return datumTermina.AddSeconds(-datum.Second).ToString("HH:mm:ss");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void GuestPacijent_Click(object sender, RoutedEventArgs e)
        {
            KreiranjeGuestPacijenta kreiranjeGuestPacijenta = new KreiranjeGuestPacijenta(comboBoxPacijenti);
            kreiranjeGuestPacijenta.Show();
        }

    }
}
