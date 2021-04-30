using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Model;
using Repository;
using Service;

namespace PrviProgram.Izgled.IzgledSekretar.IzgledTermini
{
    public partial class ZakazivanjeHitnogTermina : Window
    {
        private PacijentRepository pacijentRepository = new PacijentRepository();
        private LekarRepository lekarRepository = new LekarRepository();
        private TerminiService terminiService = new TerminiService();
        private ObservableCollection<Termin> termini;
        public static GuestPacijent guestPacijent;
        private List<string> constVreme = new List<string>() { "08:00:00", "08:30:00", "09:00:00", "09:30:00", "10:00:00", "10:30:00", "11:00:00", "11:30:00", "12:00:00", "12:30:00", "13:00:00", "13:30:00", "14:00:00", "14:30:00", "15:00:00", "15:30:00", "16:00:00", "16:30:00", "17:00:00", "17:30:00", "18:00:00", "18:30:00", "19:00:00", "19:30:00" };
        private ObservableCollection<TipTermina> tipTermina = new ObservableCollection<TipTermina> { TipTermina.Pregled, TipTermina.Operacija, TipTermina.Kontrola };
        public ZakazivanjeHitnogTermina(ObservableCollection<Termin> termini)
        {
            InitializeComponent();
            this.termini = termini;
            // todo specijalizacija lekara
            comboBoxPacijenti.ItemsSource = pacijentRepository.PregledSvihPacijenata();
            TipTerminaText.ItemsSource = tipTermina;
            vremeText.ItemsSource = constVreme;
            PostaviVremeTerminaZaCombo();
        }

        private void PostaviVremeTerminaZaCombo()
        {
            string sledeciTermin = IzracunajPrvoSledeceVremeTermina(DateTime.Now);
            foreach (string v in constVreme)
            {
                if (v.Equals(sledeciTermin))
                {
                    vremeText.SelectedIndex = constVreme.IndexOf(v);
                }
            }
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            Termin termin = PreuzmiTerminIzForme();
            termin.lekar = PronadjiLekaraSaSlobodnimTerminom(termin);
            if (termin.lekar == null)
            {
                PregledTerminaHitnoZakazivanje pregledTerminaHitnoZakazivanje = new PregledTerminaHitnoZakazivanje(termini, termin);
                pregledTerminaHitnoZakazivanje.Show();
            }
            else
            {

            }
            this.Close();
        }

        private Lekar PronadjiLekaraSaSlobodnimTerminom(Termin termin)
        {
            List<Lekar> lekari = lekarRepository.PregledSvihLekara();
            foreach (Termin t in termini)
            {
                if (// Uslov za specijalizaciju lekara termin.lekar.spec != spec ||
                    t.Vreme == termin.Vreme && t.Datum.ToString("d").Equals(termin.Datum.ToString("d")))
                {
                    lekari.Remove(lekari.Single(lekar => lekar.Jmbg.Equals(t.lekar.Jmbg)));
                }
            }
            return lekari.Count() == 0 ? null : lekari.First();
        }

        private Termin PreuzmiTerminIzForme()
        {
            Termin termin = new Termin();
            termin.Datum = DateTime.Now.AddDays(-1);
            termin.Vreme = vremeText.Text;
            if (guestPacijent == null)
            {
                termin.pacijent = (Pacijent)comboBoxPacijenti.SelectedItem;
            }
            else
            {
                termin.guestPacijent = guestPacijent;
            }
            termin.sala = terminiService.dobavljanjeSale(termin);
            termin.SifraTermina = IzracunajSifruTermina();
            termin.TipTermina = (TipTermina)TipTerminaText.SelectedItem;
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

        private static string IzracunajSifruTermina()
        {
            Random rnd = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var Random = new Random();
            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[Random.Next(chars.Length)];
            }
            var finalString = new String(stringChars);
            return finalString;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void GuestPacijent_Click(object sender, RoutedEventArgs e)
        {
            KreiranjeGuestPacijenta kreiranjeGuestPacijenta = new KreiranjeGuestPacijenta(comboBoxPacijenti);
            kreiranjeGuestPacijenta.Show();
        }

    }
}
