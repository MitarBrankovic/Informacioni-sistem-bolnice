﻿using Model;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
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
    /// <summary>
    /// Interaction logic for AnamnezaPrikaz.xaml
    /// </summary>
    public partial class AnamnezaPrikaz : UserControl
    {
        private PocetniPrikaz pocetniPrikaz;
        private Pacijent pacijent;
        private Termin termin;
        private PacijentRepository pacijentRepository = new PacijentRepository();
        private KartonPacijentaService kartonPacijentaService = new KartonPacijentaService();
        private IzvrseniPregled izvrseniPregled = new IzvrseniPregled();
        private TerminiService terminiService = new TerminiService();
        private LekoviRepository lekoviRepository = new LekoviRepository();
        private string ReceptTextHolder = "";
        public AnamnezaPrikaz(PocetniPrikaz pocetniPrikaz, Termin termin)
        {
            InitializeComponent();
            this.termin = termin;
            this.pacijent = pacijentRepository.PregledPacijenta(termin.pacijent.Jmbg);
            this.pocetniPrikaz = pocetniPrikaz;
            pocetniPrikaz.DodajUserControl(this);
            pocetniPrikaz.GoBackButtonVisibilityTrue();
            PopuniInformacijePacijenta();
            ComboboxLek.ItemsSource = lekoviRepository.PregledSvihLekova();
        }
        private void PopuniInformacijePacijenta()
        {
            textBoxIme.Text = pacijent.Ime;
            textBoxPrezime.Text = pacijent.Prezime;
            textBoxJMBG.Text = pacijent.Jmbg;
            datePickerDatumRodjenja.SelectedDate = pacijent.DatumRodjenja;
            radioButtonPolM.IsChecked = pacijent.Pol.Equals(Model.Pol.Muski);
            radioButtonPolZ.IsChecked = pacijent.Pol.Equals(Model.Pol.Zenski);
        }
        private void Zavrsi_Click(object sender, RoutedEventArgs e)
        {
            //TODO ne sacuva se posledji text holder
            InicijalizacijaIzvrsenogTermina();
            KreiranjeAnamneze();
            termin.izvrsen = true;
            terminiService.IzmenaTermina(termin);
            kartonPacijentaService.IzvrsenaAnamneza(izvrseniPregled, pacijentRepository.PregledPacijenta(pacijent.Jmbg));

        }


        private void InicijalizacijaIzvrsenogTermina()
        {
            izvrseniPregled = new IzvrseniPregled();
            izvrseniPregled.Lekar = termin.lekar;
            izvrseniPregled.Datum = termin.Datum;
            izvrseniPregled.TipTermina = termin.TipTermina;
            izvrseniPregled.Sifra = termin.SifraTermina;
        }

        private void KreiranjeAnamneze()
        {
            Anamneza anamneza = new Anamneza(TextboxAnamneza.Text);
            Recept recept = new Recept();
            recept.Lekovi = (Lek)ComboboxLek.SelectedItem;
            recept.OpisLeka = TextboxOpisLeka.Text;
            recept.VremenskiPeriodUzimanjaLeka = int.Parse(BrojDana.Text);
            izvrseniPregled.anamneza = anamneza;
            izvrseniPregled.recept = recept;
        }

        private void Karton_Click(object sender, RoutedEventArgs e)
        {
            PacijentPrikaz pacijentPrikaz = new PacijentPrikaz(pocetniPrikaz, pacijent);
            pocetniPrikaz.ContentArea.Children.Remove(this);
            pocetniPrikaz.ContentArea.Children.Add(pacijentPrikaz);
        }

        private void Uput_Click(object sender, RoutedEventArgs e)
        {
            UputWindow uputWindow = new UputWindow(pacijent, termin.lekar);
            uputWindow.Show();
        }

        private void UputLecenje_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BrojDana_TextChanged(object sender, TextChangedEventArgs e)
        {
            int result = 0;
            if (int.TryParse(BrojDana.Text, out result))
            {
                // Your conditions
                if (result > 0 && result < 1000)
                {
                    // Your number
                }
            }
            else
            {
                BrojDana.Text = "";
            }
        }
    }
}
