using Controller;
using Model;
using PrviProgram.Service;
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
        private KartonPacijentaService kartonPacijentaService = new KartonPacijentaService();
        private BolnickoLecenjeService bolnickoLecenjeService = new BolnickoLecenjeService();
        private TerminiService terminiService = new TerminiService();
        private PacijentRepository pacijentRepository = new PacijentRepository();
        private LekoviRepository lekoviRepository = new LekoviRepository();
        private LekarController lekarController = new LekarController();
        private PocetniPrikaz pocetniPrikaz;
        private Pacijent pacijent;
        private Termin termin;
        private IzvrseniPregled izvrseniPregled = new IzvrseniPregled();
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
            DaLiPacijentImaZakazanoLecenje();
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
            if(!lekarController.PacijentAlergicanNaLek(pacijent, izvrseniPregled.recept.Lekovi)){
                termin.Izvrsen = true;
                terminiService.IzmenaTermina(termin);
                kartonPacijentaService.IzvrsenaAnamneza(izvrseniPregled, pacijentRepository.PregledPacijenta(pacijent.Jmbg));
                Zavrsi.IsEnabled = false;
            }
            else
            {
                AlternativniLekWindow alternativniLekWindow = new AlternativniLekWindow(this, izvrseniPregled.recept.Lekovi);
                alternativniLekWindow.Show();
            }   
        }

        public void IzaberiZamenuZaLek(Lek alternativniLek)
        {
            termin.Izvrsen = true;
            terminiService.IzmenaTermina(termin);
            izvrseniPregled.recept.Lekovi = alternativniLek;
            kartonPacijentaService.IzvrsenaAnamneza(izvrseniPregled, pacijentRepository.PregledPacijenta(pacijent.Jmbg));
            Zavrsi.IsEnabled = false;
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
            try
            {
                recept.VremenskiPeriodUzimanjaLeka = int.Parse(BrojDana.Text);
            }catch(Exception e)
            {
                
            }
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
            UputNaBolnickoLecenjeWindow uputNaBolnickoLecenjeWindow = new UputNaBolnickoLecenjeWindow(pacijent);
            uputNaBolnickoLecenjeWindow.Show();
        }

        private void DaLiPacijentImaZakazanoLecenje()
        {
            if (bolnickoLecenjeService.ProveraDaLiPacijentImaZakazanoBolnickoLecenje(pacijent))
            {
                UputLecenje.IsEnabled = false;
            }
        }

        private void BrojDana_TextChanged(object sender, TextChangedEventArgs e)
        {
            int result = 0;
            if (int.TryParse(BrojDana.Text, out result))
            {
            }
            else
            {
                BrojDana.Text = "";
            }
        }
    }
}
