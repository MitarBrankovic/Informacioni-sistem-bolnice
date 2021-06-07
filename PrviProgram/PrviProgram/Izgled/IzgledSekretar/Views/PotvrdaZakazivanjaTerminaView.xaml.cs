﻿using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Model;
using Service;

namespace PrviProgram.Izgled.IzgledSekretar.Views
{
    public partial class PotvrdaZakazivanjaTerminaView : Page
    {
        private TerminiService terminiService = new TerminiService();
        private ObservableCollection<Termin> termini;
        private Termin termin;

        public PotvrdaZakazivanjaTerminaView(ObservableCollection<Termin> termini, Termin termin)
        {
            InitializeComponent();
            this.termini = termini;
            this.termin = termin;
            TextBoxDate.Text = termin.Datum.ToString("d");
            if (termin.pacijent != null)
            {
                TextBoxPacijent.Text = termin.pacijent.ToString();
            }
            else
            {
                TextBoxPacijent.Text = termin.guestPacijent.ToString();
            }
            TextBoxLekar.Text = termin.lekar.ToString();
            TextBoxTipTermina.Text = termin.TipTermina.ToString();
            TextBoxVreme.Text = termin.Vreme;
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            if (terminiService.ProveraZauzetostiTermina(termin))
            {
                MessageBox.Show("Termin je zauzet.");
            }
            else
            {
                terminiService.DodavanjeTermina(termin);
                termini.Add(termin);
            }
            Page pregledTermina = new TerminiView();
            NavigationService.Navigate(pregledTermina);
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

    }
}
