using Model;
using Repository;
using System;
using System.Collections.Generic;
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
    /// <summary>
    /// Interaction logic for AnamnezaPrikaz.xaml
    /// </summary>
    public partial class AnamnezaPrikaz : UserControl
    {
        private PocetniPrikaz pocetniPrikaz;
        private Pacijent pacijent;
        private Termin termin;
        private IzvrseniPregled izvrseniPregled;
        private PacijentRepository pacijentRepository = new PacijentRepository();
        public AnamnezaPrikaz(PocetniPrikaz pocetniPrikaz, Termin termin)
        {
            InitializeComponent();
            this.termin = termin;
            this.pacijent = pacijentRepository.PregledPacijenta(termin.pacijent.Jmbg);
            this.pocetniPrikaz = pocetniPrikaz;
            pocetniPrikaz.DodajUserControl(this);
            pocetniPrikaz.GoBackButtonVisibilityTrue();
            PopuniInformacijePacijenta();
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

        private void radioButtonRecpet_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void radioButtonTerapija_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
