using Controller;
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
    public partial class NalogPrikaz : UserControl
    {
        private Lekar lekar;
        private DrzaveRepository drzaveRepository = new DrzaveRepository();
        private GradoviRepository gradoviRepository = new GradoviRepository();
        private UtilityService utilityService = new UtilityService();
        private SekretarController sekretarController = new SekretarController();
        private PocetniPrikaz pocetniPrikaz;
        private bool BoolIzmeni = false;
        public NalogPrikaz(PocetniPrikaz pocetniPrikaz, Lekar lekar)
        {
            this.pocetniPrikaz = pocetniPrikaz;
            this.lekar = lekar;
            InitializeComponent();
            InicijalizacijaCombo();
            PopuniInformacijeLekara();
            DisableTextbox();
        }

        private void InicijalizacijaCombo()
        {
            textBoxMestoRodjenjaGrad.ItemsSource = gradoviRepository.PregledSvihGradova();
            textBoxGrad.ItemsSource = gradoviRepository.PregledSvihGradova();
            textBoxMestoRodjenjaDrzava.ItemsSource = drzaveRepository.PregledSvihDrzava();
            textBoxDrzava.ItemsSource = drzaveRepository.PregledSvihDrzava();
        }

        private void PopuniInformacijeLekara()
        {
            textBoxIme.Text = lekar.Ime;
            textBoxPrezime.Text = lekar.Prezime;
            textBoxJMBG.Text = lekar.Jmbg;
            datePickerDatumRodjenja.SelectedDate = lekar.DatumRodjenja;
            textBoxMestoRodjenjaGrad.Text = lekar.MestoRodjenja.Ime;
            textBoxMestoRodjenjaDrzava.Text = lekar.MestoRodjenja.drzava.Ime;
            textBoxUlica.Text = lekar.AdresaStanovanja.Ulica;
            textBoxBroj.Text = lekar.AdresaStanovanja.Broj.ToString();
            textBoxSprat.Text = lekar.AdresaStanovanja.Sprat.ToString();
            textBoxStan.Text = lekar.AdresaStanovanja.Stan.ToString();
            textBoxGrad.Text = lekar.AdresaStanovanja.grad.Ime;
            textBoxDrzava.Text = lekar.AdresaStanovanja.grad.drzava.Ime;
            radioButtonPolM.IsChecked = lekar.Pol.Equals(Model.Pol.Muski);
            radioButtonPolZ.IsChecked = lekar.Pol.Equals(Model.Pol.Zenski);
            textBoxEmail.Text = lekar.Email;
            textBoxKontaktTelefon.Text = lekar.KontaktTelefon;
            textBoxKorisnickoIme.Text = lekar.Korisnik.KorisnickoIme;
            textBoxLozinka.Password = lekar.Korisnik.Lozinka;
        }

        private void DisableTextbox()
        {
            textBoxIme.IsEnabled = false;
            textBoxPrezime.IsEnabled = false;
            textBoxJMBG.IsEnabled = false;
            datePickerDatumRodjenja.IsEnabled = false;
            textBoxMestoRodjenjaGrad.IsEnabled = false;
            textBoxMestoRodjenjaDrzava.IsEnabled = false;
            textBoxUlica.IsEnabled = false;
            textBoxBroj.IsEnabled = false;
            textBoxSprat.IsEnabled = false;
            textBoxStan.IsEnabled = false;
            textBoxGrad.IsEnabled = false;
            textBoxDrzava.IsEnabled = false;
            radioButtonPolM.IsEnabled = false;
            radioButtonPolZ.IsEnabled = false;
            textBoxEmail.IsEnabled = false;
            textBoxKontaktTelefon.IsEnabled = false;
            textBoxKorisnickoIme.IsEnabled = false;
            textBoxLozinka.IsEnabled = false;
        }

        private void EnableTextbox()
        {
            textBoxIme.IsEnabled = true;
            textBoxPrezime.IsEnabled = true;
            datePickerDatumRodjenja.IsEnabled = true;
            textBoxMestoRodjenjaGrad.IsEnabled = true;
            textBoxMestoRodjenjaDrzava.IsEnabled = true;
            textBoxUlica.IsEnabled = true;
            textBoxBroj.IsEnabled = true;
            textBoxSprat.IsEnabled = true;
            textBoxStan.IsEnabled = true;
            textBoxGrad.IsEnabled = true;
            textBoxDrzava.IsEnabled = true;
            radioButtonPolM.IsEnabled = true;
            radioButtonPolZ.IsEnabled = true;
            textBoxEmail.IsEnabled = true;
            textBoxKontaktTelefon.IsEnabled = true;
            textBoxKorisnickoIme.IsEnabled = true;
            textBoxLozinka.IsEnabled = true;
        }

        private void IzmeniPritisnut()
        {
            if (BoolIzmeni)
            {
                DisableTextbox();
                BoolIzmeni = false;
                SacuvajIzmeneLekara();
                Izmeni.Content = "Izmeni informacije";
            }
            else
            {
                EnableTextbox();
                BoolIzmeni = true;
                Izmeni.Content = "Sačuvaj";
            }
        }

        private void SacuvajIzmeneLekara()
        {
            Korisnik korisnik = new Korisnik(textBoxKorisnickoIme.Text, textBoxLozinka.Password, TipKorisnika.Lekar);

            Adresa adresaStanovanja = new Adresa(textBoxUlica.Text, utilityService.IntParser(textBoxBroj.Text),
                                                 utilityService.IntParser(textBoxSprat.Text),
                                                 utilityService.IntParser(textBoxStan.Text),
                                                 GradIzForme(textBoxDrzava.Text, textBoxGrad.Text));

            Osoba osoba = new Osoba(GradIzForme(textBoxMestoRodjenjaDrzava.Text, textBoxMestoRodjenjaGrad.Text),
                                    korisnik, adresaStanovanja, textBoxIme.Text, textBoxPrezime.Text, textBoxEmail.Text,
                                    textBoxJMBG.Text, datePickerDatumRodjenja.SelectedDate.GetValueOrDefault(),
                                    (bool)radioButtonPolM.IsChecked ? Pol.Muski : Pol.Zenski, textBoxKontaktTelefon.Text);

            Lekar noviLekar = new Lekar(osoba, new List<Termin>(), lekar.GetSpecijalizacija());

            if (sekretarController.IzmenaLekara(lekar, noviLekar))
            {
                MessageBox.Show("Izmene će se primeniti kada se sledeći put ulogujete.");
            }
        }

        private Grad GradIzForme(string drzava, string grad)
        {
            Drzava drzavaDTO = new Drzava(drzava);
            Grad gradDTO = new Grad(grad, drzavaDTO);
            sekretarController.DodavanjeDrzave(drzavaDTO);
            sekretarController.DodavanjeGrada(gradDTO);
            return gradDTO;
        }

        private void IzlogujSe_Click(object sender, RoutedEventArgs e)
        {
            pocetniPrikaz.Close();
        }

        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {
            IzmeniPritisnut();
        }

        private void textBoxGrad_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Grad grad = (Grad)textBoxGrad.SelectedItem;
            if (grad != null)
            {
                textBoxDrzava.Text = grad.drzava.Ime;
            }
        }

        private void textBoxMestoRodjenjaGrad_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Grad grad = (Grad)textBoxMestoRodjenjaGrad.SelectedItem;
            if (grad != null)
            {
                textBoxMestoRodjenjaDrzava.Text = grad.drzava.Ime;
            }
        }
    }
}
