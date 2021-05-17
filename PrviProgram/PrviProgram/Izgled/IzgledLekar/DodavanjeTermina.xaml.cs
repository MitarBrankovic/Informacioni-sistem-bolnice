﻿using Controller;
using Model;
using PrviProgram.Repository;
using PrviProgram.Service;
using Repository;
using Service;
using System;
using System.Collections.ObjectModel;
using System.Windows;
//using Logika.LogikaSekretar;

namespace PrviProgram.Izgled.IzgledLekar
{
    /// <summary>
    /// Interaction logic for DodavanjeTermina.xaml
    /// </summary>
    public partial class DodavanjeTermina : Window
    {
        private ObservableCollection<Termin> termini;
        private PacijentRepository pacijentRepository = new PacijentRepository();
        private LekarController lekarController = new LekarController();
        private SalaRepository salaRepository = new SalaRepository();
        private TerminiService terminiService = new TerminiService();
        private UtilityService utilityService = new UtilityService();
        private ObservableCollection<string> vreme;
        private Lekar lekar;

        public DodavanjeTermina(ObservableCollection<Termin> termini, Lekar lekar)
        {
            InitializeComponent();
            this.lekar = lekar;
            this.termini = termini;
            ComboboxPacijent.ItemsSource = pacijentRepository.PregledSvihPacijenata();
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            Termin tempTermin = new Termin((DateTime)(DatumText.SelectedDate), SelektovaniTipTermina(), utilityService.GenerisanjeSifre(),
                vremeText.Text, lekar, (Pacijent)ComboboxPacijent.SelectedItem);
            tempTermin.sala = terminiService.DobavljanjeSale(tempTermin);
            if (!terminiService.ProvaraZauzatostiTermina(tempTermin))
            {
                terminiService.DodavanjeTermina(tempTermin);
                this.termini.Add(tempTermin);
                this.Close();
            }
            else MessageBox.Show("Greska!");
        }

        private TipTermina SelektovaniTipTermina()
        {
            String tip = TipTerm.Text;
            if (tip.Equals("Pregled"))
                return TipTermina.Pregled;
            else if (tip.Equals("Operacija"))
                return TipTermina.Operacija;
            else
                return TipTermina.Kontrola;
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DatumText_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            AzurirajVreme();
        }

        private void AzurirajVreme()
        {
            vreme = new ObservableCollection<string>(lekarController.DobavljanjeSlobodnihTerminaLekara(lekar, (DateTime)DatumText.SelectedDate));
            vremeText.ItemsSource = vreme;
            vremeText.SelectedItem = vremeText.Items[0];
        }
    }
}
