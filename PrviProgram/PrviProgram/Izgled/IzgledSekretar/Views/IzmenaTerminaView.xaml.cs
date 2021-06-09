using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Controller;
using Model;
using PrviProgram.Controller;
using Repository;
using Service;

namespace PrviProgram.Izgled.IzgledSekretar.Views
{
    public partial class IzmenaTerminaView : Page
    {
        public TerminiLekarController terminiLekarController = new TerminiLekarController();
        public TerminiSaleController terminiSaleController = new TerminiSaleController();
        private LekarRepository lekarRepository = new LekarRepository();
        private TerminiService terminiService = new TerminiService();
        private UtilityService utilityService = new UtilityService();
        private ObservableCollection<Termin> termini;
        private ObservableCollection<string> vremeTermina;
        private ObservableCollection<TipTermina> tipTermina = new ObservableCollection<TipTermina>(Enum.GetValues(typeof(TipTermina)).Cast<TipTermina>());
        private Termin termin;

        public IzmenaTerminaView(ObservableCollection<Termin> termini, Termin termin)
        {
            InitializeComponent();
            vremeTermina = new ObservableCollection<string>(utilityService.nizVremena);
            this.termini = termini;
            this.termin = termin;
            InicijalizacijaCombo();
        }

        private void InicijalizacijaCombo()
        {
            comboBoxLekari.ItemsSource = lekarRepository.PregledSvihLekara();
            InicijalizacijaPacijenta();
            comboBoxPacijenti.SelectedIndex = 0;
            comboBoxPacijenti.IsEnabled = false;
            vremeText.ItemsSource = vremeTermina;
            vremeText.SelectedIndex = vremeTermina.IndexOf(termin.Vreme);
            TipTerminaText.ItemsSource = tipTermina;
            TipTerminaText.SelectedIndex = tipTermina.IndexOf(termin.TipTermina);
            InicijalizacijaLekara();
            datePicker.SelectedDate = termin.Datum;
        }

        private void InicijalizacijaLekara()
        {
            int i = 0;
            foreach (Lekar lekar in comboBoxLekari.Items.Cast<Lekar>())
            {
                if (lekar.Jmbg.Equals(termin.lekar.Jmbg))
                {
                    comboBoxLekari.SelectedIndex = i;
                }
                i++;
            }
        }

        private void InicijalizacijaPacijenta()
        {
            if (termin.pacijent != null)
            {
                ObservableCollection<Pacijent> pacijents = new ObservableCollection<Pacijent>();
                pacijents.Add(termin.pacijent);
                comboBoxPacijenti.ItemsSource = pacijents;
            }
            else
            {
                ObservableCollection<GuestPacijent> guestPacijents = new ObservableCollection<GuestPacijent>();
                guestPacijents.Add(termin.guestPacijent);
                comboBoxPacijenti.ItemsSource = guestPacijents;
            }
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            Termin termin = PreuzmiTerminIzForme();
            if (terminiService.ProveraZauzetostiTermina(termin))
            {
                MessageBox.Show("Termin je zauzet.");
            }
            else
            {
                terminiService.IzmenaTermina(termin);
                termini.Remove(this.termin);
                termini.Add(termin);
                NavigationService.GoBack();
            }
        }

        private Termin PreuzmiTerminIzForme()
        {
            Termin termin = new Termin((DateTime)datePicker.SelectedDate,
                                       (TipTermina)TipTerminaText.SelectedItem, this.termin.SifraTermina,
                                       vremeText.Text, (Lekar)comboBoxLekari.SelectedItem);
            if (this.termin.pacijent != null)
            {
                termin.pacijent = (Pacijent)comboBoxPacijenti.SelectedItem;
            }
            else
            {
                termin.guestPacijent = (GuestPacijent)comboBoxPacijenti.SelectedItem;
            }
            termin.sala = terminiSaleController.DobavljanjeSale(termin);
            return termin;
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void AzurirajVreme()
        {
            if ((Lekar)comboBoxLekari.SelectedItem != null && datePicker.SelectedDate != null)
            {
                vremeTermina = new ObservableCollection<string>(utilityService.nizVremena);
                vremeText.ItemsSource = vremeTermina;
                vremeText.IsEnabled = true;
                ObrisiZauzeteTermineLekara();
                vremeText.SelectedItem = vremeText.Items[0];
            }
            else
            {
                vremeText.IsEnabled = false;
            }
        }

        private void ObrisiZauzeteTermineLekara()
        {
            List<string> zauzetiTermini = terminiLekarController.ZauzetiTerminiLekaraDatuma((Lekar)comboBoxLekari.SelectedItem, (DateTime)datePicker.SelectedDate);
            foreach (String vremeTerminaZaBrisanje in utilityService.nizVremena)
            {
                foreach (String zauzetiT in zauzetiTermini)
                {
                    if (vremeTerminaZaBrisanje.Equals(zauzetiT))
                    {
                        vremeTermina.Remove(vremeTerminaZaBrisanje);
                    }
                }
            }
        }

        private void ComboBoxLekari_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            AzurirajVreme();
        }

        private void DatePicker_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            AzurirajVreme();
        }

    }
}
