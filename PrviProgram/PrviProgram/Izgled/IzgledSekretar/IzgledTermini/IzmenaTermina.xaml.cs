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
    public partial class IzmenaTermina : Window
    {
        private LekarRepository lekarRepository = new LekarRepository();
        private TerminiService terminiService = new TerminiService();
        private UtilityService utilityService = new UtilityService();
        private ObservableCollection<Termin> termini;
        private ObservableCollection<string> vremeTermina;
        private ObservableCollection<TipTermina> tipTermina = new ObservableCollection<TipTermina>(Enum.GetValues(typeof(TipTermina)).Cast<TipTermina>());
        private Termin termin;
        public IzmenaTermina(ObservableCollection<Termin> termini, Termin termin)
        {
            InitializeComponent();
            vremeTermina = new ObservableCollection<string>(utilityService.termini);
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
            if (terminiService.ProvaraZauzatostiTermina(termin))
            {
                MessageBox.Show("Termin je zauzet.");
            }
            else
            {
                terminiService.IzmenaTermina(termin);
                termini.Remove(this.termin);
                termini.Add(termin);
                Close();
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
            termin.sala = terminiService.DobavljanjeSale(termin);
            return termin;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AzurirajVreme()
        {
            if ((Lekar)comboBoxLekari.SelectedItem != null && datePicker.SelectedDate != null)
            {
                vremeTermina = new ObservableCollection<string>(utilityService.termini);
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
            List<string> zauzetiTermini = terminiService.ZauzetiTerminiLekaraDatuma((Lekar)comboBoxLekari.SelectedItem, (DateTime)datePicker.SelectedDate);
            foreach (String vremeTerminaZaBrisanje in utilityService.termini)
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

        private void comboBoxLekari_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            AzurirajVreme();
        }

        private void datePicker_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            AzurirajVreme();
        }

    }
}

