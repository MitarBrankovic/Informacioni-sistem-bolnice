using Model;
using PrviProgram.Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrviProgram.Izgled.IzgledLekar
{
    public partial class TerminiPrikaz : UserControl
    {
        public ObservableCollection<Termin> termini;
        private TerminiService terminiService = new TerminiService();
        private TerminiRepository terminiRepository = new TerminiRepository();
        private Lekar lekar;
        private PocetniPrikaz pocetniPrikaz;
        public TerminiPrikaz(PocetniPrikaz pocetniPrikaz, Lekar lekar)
        {
            InitializeComponent();
            this.pocetniPrikaz = pocetniPrikaz;
            this.lekar = lekar;
            termini = new ObservableCollection<Termin>(terminiRepository.PregledTerminaLekara(lekar));
            dataGridLekar.ItemsSource = termini;
            pocetniPrikaz.DodajUserControl(this);
            DisableButtons();
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            DodavanjeTermina dodavanje = new DodavanjeTermina(termini, lekar);
            dodavanje.ShowDialog();
        }

        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {
            Termin termin = (Termin)dataGridLekar.SelectedItem;
            if (dataGridLekar.SelectedIndex != -1)
            {
                KreirajProzorIzmenaTermina(termin);
            }
            else
            {
                MessageBox.Show("Morate izabrati termin!");
            }

        }

        private void KreirajProzorIzmenaTermina(Termin termin)
        {
            if (termin.Izvrsen != true)
            {
                IzmenaTermina izmena = new IzmenaTermina(termini, (Termin)dataGridLekar.SelectedItem);
                izmena.ShowDialog();
            }
            else
            {
                MessageBox.Show("Ovaj termin ste vec izvrsili!");
            }
        }

        private void Izbrisi_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridLekar.SelectedItem != null)
            {
                Termin termin = (Termin)dataGridLekar.SelectedItem;
                terminiService.BrisanjeTermina(termin);
                termini.Remove(termin);
                MessageBox.Show("Uspesno ste obrisali termin!");
                DisableButtons();
            }
            else
            {
                MessageBox.Show("Morate izabrati termin!");
            }
        }
        private void Informacije_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridLekar.SelectedIndex != -1)
            {
                PacijentPrikaz pacijentPrikaz = new PacijentPrikaz(pocetniPrikaz, ((Termin)dataGridLekar.SelectedItem).pacijent);
                pocetniPrikaz.ContentArea.Children.Remove(this);
                pocetniPrikaz.ContentArea.Children.Add(pacijentPrikaz);
            }
            else
            {
                MessageBox.Show("Morate izabrati termin!");

            }
        }

        private void Anamneza_Click(object sender, RoutedEventArgs e)
        {
            Termin termin = (Termin)dataGridLekar.SelectedItem;
            if (termin.Izvrsen != true)
            {
                KreirajProzorAnamnezaPrikaz();
            }
            else
            {
                MessageBox.Show("Ovaj termin ste vec izvrsili!");
            }
        }

        private void KreirajProzorAnamnezaPrikaz()
        {
            if (dataGridLekar.SelectedIndex != -1)
            {
                AnamnezaPrikaz anamnezaPrikaz = new AnamnezaPrikaz(pocetniPrikaz, ((Termin)dataGridLekar.SelectedItem));
                pocetniPrikaz.ContentArea.Children.Remove(this);
                pocetniPrikaz.ContentArea.Children.Add(anamnezaPrikaz);
            }
            else
            {
                MessageBox.Show("Morate izabrati termin!");
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EnableButtons();
        }

        private void EnableButtons()
        {
            Izmeni.IsEnabled = true;
            Anamneza.IsEnabled = true;
            Informacije.IsEnabled = true;
            Izbrisi.IsEnabled = true;
        }
        private void DisableButtons()
        {
            Izmeni.IsEnabled = false;
            Anamneza.IsEnabled = false;
            Informacije.IsEnabled = false;
            Izbrisi.IsEnabled = false;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            termini.Clear();
            ObservableCollection<Termin> pomocni;
            pomocni = new ObservableCollection<Termin>(terminiRepository.PretragaTermina(TextboxSearch.Text));
            dataGridLekar.ItemsSource = pomocni;
        }
    }
}
