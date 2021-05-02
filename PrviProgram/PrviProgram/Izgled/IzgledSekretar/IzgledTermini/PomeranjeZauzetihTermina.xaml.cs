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
    public partial class PomeranjeZauzetihTermina : Window
    {
        private LekarRepository lekarRepository = new LekarRepository();
        private Repository.SalaRepository salaRepository = new Repository.SalaRepository();
        private TerminiService terminiService = new TerminiService();
        private ObservableCollection<Termin> termini;
        private ObservableCollection<Termin> sviTermini;
        private List<string> constVreme = new List<string>() { "08:00:00", "08:30:00", "09:00:00", "09:30:00", "10:00:00", "10:30:00", "11:00:00", "11:30:00", "12:00:00", "12:30:00", "13:00:00", "13:30:00", "14:00:00", "14:30:00", "15:00:00", "15:30:00", "16:00:00", "16:30:00", "17:00:00", "17:30:00", "18:00:00", "18:30:00", "19:00:00", "19:30:00" };
        private ObservableCollection<string> vreme = new ObservableCollection<string> { "08:00:00", "08:30:00", "09:00:00", "09:30:00", "10:00:00", "10:30:00", "11:00:00", "11:30:00", "12:00:00", "12:30:00", "13:00:00", "13:30:00", "14:00:00", "14:30:00", "15:00:00", "15:30:00", "16:00:00", "16:30:00", "17:00:00", "17:30:00", "18:00:00", "18:30:00", "19:00:00", "19:30:00" };
        private ObservableCollection<TipTermina> tipTermina = new ObservableCollection<TipTermina> { TipTermina.Pregled, TipTermina.Operacija, TipTermina.Kontrola };
        private Termin termin;
        public PomeranjeZauzetihTermina(ObservableCollection<Termin> sviTermini, ObservableCollection<Termin> termini, Termin termin)
        {
            InitializeComponent();
            this.termini = termini;
            this.sviTermini = sviTermini;
            this.termin = termin;
            comboBoxLekari.ItemsSource = lekarRepository.PregledSvihLekara();

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
            comboBoxPacijenti.SelectedIndex = 0;
            comboBoxPacijenti.IsEnabled = false;
            
            vremeText.ItemsSource = vreme;
            vremeText.SelectedIndex = vreme.IndexOf(termin.Vreme);
            
            TipTerminaText.ItemsSource = tipTermina;
            TipTerminaText.SelectedIndex = tipTermina.IndexOf(termin.TipTermina);

            int i = 0;
            foreach (Model.Lekar a in comboBoxLekari.Items.Cast<Model.Lekar>())
            {
                if (a.Jmbg.Equals(termin.lekar.Jmbg))
                {
                    comboBoxLekari.SelectedIndex = i;
                }
                i++;
            }
            datePicker.SelectedDate = termin.Datum;

            comboBoxLekari.IsEnabled = false;
            TipTerminaText.IsEnabled = false;
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            Termin termin = PreuzmiTerminIzForme();
            terminiService.IzmenaTermina(termin);
            this.termini.Remove(this.termin);
            this.termini.Add(termin);
            this.sviTermini.Remove(this.termin);
            this.sviTermini.Add(termin);
            this.Close();
        }

        private Termin PreuzmiTerminIzForme()
        {
            Termin termin = new Termin();
            termin.Datum = (DateTime)(datePicker.SelectedDate);
            termin.Vreme = vremeText.Text;
            termin.lekar = (Model.Lekar)comboBoxLekari.SelectedItem;
            if (this.termin.pacijent != null)
            {
                termin.pacijent = (Pacijent)comboBoxPacijenti.SelectedItem;
            }
            else
            {
                termin.guestPacijent = (GuestPacijent)comboBoxPacijenti.SelectedItem;
            }
            termin.sala = TerminiService.getInstance().dobavljanjeSale(termin);
            termin.SifraTermina = this.termin.SifraTermina;
            termin.TipTermina = (TipTermina)TipTerminaText.SelectedItem;
            return termin;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void datePicker_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            AzurirajVreme();
        }


        private void AzurirajVreme()
        {
            if ((Lekar)comboBoxLekari.SelectedItem != null && datePicker.SelectedDate != null)
            {
                vreme = new ObservableCollection<string>(constVreme);
                vremeText.ItemsSource = vreme;
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
            foreach (String vremeT in constVreme)
            {
                foreach (String zauzetiT in zauzetiTermini)
                {
                    if (vremeT.Equals(zauzetiT))
                    {
                        vreme.Remove(vremeT);
                    }
                }
            }
        }

    }
}
