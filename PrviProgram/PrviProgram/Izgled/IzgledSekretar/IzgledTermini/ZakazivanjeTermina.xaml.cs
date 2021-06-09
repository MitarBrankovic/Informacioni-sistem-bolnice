using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using Controller;
using Model;
using PrviProgram.Controller;
using Repository;
using Service;

namespace PrviProgram.Izgled.IzgledSekretar.IzgledTermini
{
    public partial class ZakazivanjeTermina : Window
    {
        public TerminiLekarController terminiLekarController;
        public TerminiSaleController terminiSaleController;
        private PacijentRepository pacijentRepository = new PacijentRepository();
        private LekarRepository lekarRepository = new LekarRepository();
        private TerminiService terminiService = new TerminiService();
        private UtilityService utilityService = new UtilityService();
        private ObservableCollection<Termin> termini;
        private ObservableCollection<string> vremeTermina;
        public static GuestPacijent guestPacijent;

        public ZakazivanjeTermina(ObservableCollection<Termin> termini)
        {
            InitializeComponent();
            this.termini = termini;
            vremeTermina = new ObservableCollection<string>(utilityService.nizVremena);
            InicijalizacijaCombo();
        }

        private void InicijalizacijaCombo()
        {
            comboBoxLekari.ItemsSource = lekarRepository.PregledSvihLekara();
            comboBoxPacijenti.ItemsSource = pacijentRepository.PregledSvihPacijenata();
            vremeText.ItemsSource = vremeTermina;
            TipTerminaText.ItemsSource = Enum.GetValues(typeof(TipTermina));
            vremeText.IsEnabled = false;
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            Termin termin = PreuzmiTerminIzForme();
            if (terminiService.ProveraZauzetostiTermina(termin))
            {
                PregledZauzetihTermina pregledZauzetihTermina = new PregledZauzetihTermina(termini, termin);
                pregledZauzetihTermina.Show();
            }
            else
            {
                terminiService.DodavanjeTermina(termin);
                this.termini.Add(termin);
                this.Close();
            }
        }

        private Termin PreuzmiTerminIzForme()
        {
            Termin termin = new Termin(datePicker.SelectedDate.GetValueOrDefault(),
                                       (TipTermina)TipTerminaText.SelectedItem, utilityService.GenerisanjeSifre(),
                                       vremeText.Text, (Lekar)comboBoxLekari.SelectedItem);
            if (guestPacijent == null)
            {
                termin.pacijent = (Pacijent)comboBoxPacijenti.SelectedItem;
            }
            else
            {
                termin.guestPacijent = guestPacijent;
            }
            termin.sala = terminiSaleController.DobavljanjeSale(termin);
            return termin;
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

        private void ComboBoxLekari_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (checkBoxHitanSlucaj.IsChecked.Value)
            {
                PostaviVremenaNaSlobodna();
            }
            else
            {
                AzurirajVreme();
            }
        }

        private void DatePicker_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (checkBoxHitanSlucaj.IsChecked.Value)
            {
                PostaviVremenaNaSlobodna();
            }
            else
            {
                AzurirajVreme();
            }
        }

        private void AzurirajVreme()
        {
            if ((Lekar)comboBoxLekari.SelectedItem != null && datePicker.SelectedDate != null)
            {
                PostaviVremenaNaSlobodna();
                ObrisiZauzeteTermineLekara();
                vremeText.SelectedItem = vremeText.Items[0];
            }
            else
            {
                vremeText.IsEnabled = false;
            }
        }

        private void PostaviVremenaNaSlobodna()
        {
            vremeTermina = new ObservableCollection<string>(utilityService.nizVremena);
            vremeText.ItemsSource = vremeTermina;
            vremeText.IsEnabled = true;
        }

        private void ObrisiZauzeteTermineLekara()
        {
            List<string> zauzetiTermini = terminiLekarController.ZauzetiTerminiLekaraDatuma((Lekar)comboBoxLekari.SelectedItem, (DateTime)datePicker.SelectedDate);
            foreach (String vremeTerminaZaBrisanje in utilityService.nizVremena)
            {
                foreach (String zauzetiTermin in zauzetiTermini)
                {
                    if (vremeTerminaZaBrisanje.Equals(zauzetiTermin))
                    {
                        vremeTermina.Remove(vremeTerminaZaBrisanje);
                    }
                }
            }
        }

        private void CheckBoxHitanSlucaj_Checked(object sender, RoutedEventArgs e)
        {
            PostaviVremenaNaSlobodna();
        }

        private void CheckBoxHitanSlucaj_Unchecked(object sender, RoutedEventArgs e)
        {
            AzurirajVreme();
        }

    }
}
