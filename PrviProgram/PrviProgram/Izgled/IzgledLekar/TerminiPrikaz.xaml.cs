﻿using Model;
using Service;
using Service.LekarService;
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
        public PreglediService upravljanje;
        public ObservableCollection<Termin> termini;
        private TerminiService terminiService = new TerminiService();
        private Lekar lekar;
        private IzvrseniPregled izvrseniPregled;
        private StackPanel parent;
        private PocetniPrikaz pocetniPrikaz;
        public TerminiPrikaz(PocetniPrikaz pocetniPrikaz, StackPanel parent, Lekar lekar)
        {
            InitializeComponent();
            this.pocetniPrikaz = pocetniPrikaz;
            this.parent = parent;
            this.lekar = lekar;
            upravljanje = new PreglediService();
            termini = new ObservableCollection<Termin>(upravljanje.PregledSvihPregledaLekara(lekar));
            dataGridLekar.ItemsSource = termini;
            DisableButtons();
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            DodavanjeTermina dodavanje = new DodavanjeTermina(termini, lekar);
            dodavanje.Show();
        }

        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {
            Termin termin = (Termin)dataGridLekar.SelectedItem;

            if (dataGridLekar.SelectedIndex != -1)
            {
                if (termin.izvrsen != true)
                {
                    IzmenaTermina izmena = new IzmenaTermina(termini, (Termin)dataGridLekar.SelectedItem);
                    izmena.Show();
                }
                else
                {
                    MessageBox.Show("Ovaj termin ste vec izvrsili!");
                }
            }
            else
            {
                MessageBox.Show("Morate izabrati termin!");
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
                PacijentPrikaz pacijentPrikaz = new PacijentPrikaz(this, pocetniPrikaz, ((Termin)dataGridLekar.SelectedItem).pacijent);
                parent.Children.Remove(this);
                parent.Children.Add(pacijentPrikaz);
            }
            else
            {
                MessageBox.Show("Morate izabrati termin!");

            }
        }


        private void Anamneza_Click(object sender, RoutedEventArgs e)
        {
            Termin termin = (Termin)dataGridLekar.SelectedItem;

            if (termin.izvrsen != true)
            {
                if (dataGridLekar.SelectedIndex != -1)
                {
                    ObservableCollection<IzvrseniPregled> izvrseniPregledi = new ObservableCollection<IzvrseniPregled>();
                    IzvrsavanjeAnamneze anamneza = new IzvrsavanjeAnamneze(izvrseniPregledi, izvrseniPregled, termin.pacijent, termini, termin);
                    anamneza.Show();
                }
                else
                {
                    MessageBox.Show("Morate izabrati termin!");
                }
            }
            else
            {
                MessageBox.Show("Ovaj termin ste vec izvrsili!");
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
    }
}
