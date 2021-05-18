using Model;
using Repository;
using Service;
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
        private PacijentRepository pacijentRepository = new PacijentRepository();
        private KartonPacijentaService kartonPacijentaService = new KartonPacijentaService();
        private IzvrseniPregled izvrseniPregled = new IzvrseniPregled();
        private TerminiService terminiService = new TerminiService();
        private string ReceptTextHolder = "";
        private string TerapijaTextHolder = "";
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
            //TODO ne sacuva se posledji text holder
            SacuvajPromeneReceptTerapija();
            InicijalizacijaIzvrsenogTermina();
            KreiranjeAnamneze();
            termin.izvrsen = true;
            terminiService.IzmenaTermina(termin);
            kartonPacijentaService.IzvrsenaAnamneza(izvrseniPregled, pacijentRepository.PregledPacijenta(pacijent.Jmbg));

        }
        
        private void SacuvajPromeneReceptTerapija()
        {
            if (radioButtonRecpet.IsChecked == true)
            {
                ReceptTextHolder = TextboxReceptTerapija.Text;
            }
            else if (radioButtonTerapija.IsChecked == true)
            {
                TerapijaTextHolder = TextboxReceptTerapija.Text;
            }
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
            Terapija terapija = new Terapija(TerapijaTextHolder, DateTime.Today);
            Recept recept = new Recept();
            recept.Lekovi = ReceptTextHolder;
            izvrseniPregled.anamneza = anamneza;
            izvrseniPregled.terapija = terapija;
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

        private void radioButtonRecpet_Checked(object sender, RoutedEventArgs e)
        {
            PromenaStanjaRadioButtona();
        }

        private void radioButtonTerapija_Checked(object sender, RoutedEventArgs e)
        {
            PromenaStanjaRadioButtona();
        }

        private void PromenaStanjaRadioButtona()
        {
            if (radioButtonRecpet.IsChecked ==  true)
            {
                TerapijaTextHolder = TextboxReceptTerapija.Text;
                TextboxReceptTerapija.Text = ReceptTextHolder;
            }else if(radioButtonTerapija.IsChecked == true)
            {
                ReceptTextHolder = TextboxReceptTerapija.Text;
                TextboxReceptTerapija.Text = TerapijaTextHolder;
            }
        }
    }
}
