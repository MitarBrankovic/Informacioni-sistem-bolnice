using Controller;
using Model;
using PrviProgram.Service;
using Repository;
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
using System.Windows.Shapes;

namespace PrviProgram.Izgled.IzgledUpravnik
{
    /// <summary>
    /// Interaction logic for ZaposleniIzmeni.xaml
    /// </summary>
    public partial class ZaposleniIzmeni : Window
    {
        private ObservableCollection<Osoba> zaposleni;
        private Osoba osoba;
        private SekretarController sekretarController = new SekretarController();
        private DrzaveRepository drzaveRepository = new DrzaveRepository();
        private GradoviRepository gradoviRepository = new GradoviRepository();
        private LekarRepository lekarRepository = new LekarRepository();
        private SekretarRepository sekretarRepository = new SekretarRepository();
        private UtilityService utilityService = new UtilityService();
        private SekretarService sekretarService = new SekretarService();

        public ZaposleniIzmeni(ObservableCollection<Osoba> zaposleni, Osoba osoba)
        {
            InitializeComponent();
            this.zaposleni = zaposleni;
            this.osoba = osoba;
            InicijalizacijaCombo();
            InicijalizacijaForme(osoba);
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            Adresa adresaStanovanja = new Adresa(textBoxUlica.Text, utilityService.IntParser(textBoxBroj.Text),
                                     utilityService.IntParser(textBoxSprat.Text),
                                     utilityService.IntParser(textBoxStan.Text),
                                     GradIzForme(textBoxDrzava.Text, textBoxGrad.Text));
            if (osoba.Korisnik.TipKorisnika == TipKorisnika.Lekar)
            {
                Korisnik korisnik = new Korisnik(textBoxKorisnickoIme.Text, textBoxLozinka.Password, TipKorisnika.Lekar);
                Osoba osoba = new Osoba(GradIzForme(textBoxMestoRodjenjaDrzava.Text, textBoxMestoRodjenjaGrad.Text),
                        korisnik, adresaStanovanja, textBoxIme.Text, textBoxPrezime.Text, textBoxEmail.Text,
                        textBoxJMBG.Text, datePickerDatumRodjenja.SelectedDate.GetValueOrDefault(),
                        (bool)radioButtonPolM.IsChecked ? Pol.Muski : Pol.Zenski, textBoxKontaktTelefon.Text);
                Lekar noviLekar = new Lekar(osoba, new List<Termin>(), new List<Specijalizacija>());
                Lekar stariLekar = lekarRepository.PregledLekara(this.osoba.Jmbg);
                if (sekretarController.IzmenaLekara(stariLekar, noviLekar))
                {
                    zaposleni.Remove(this.osoba);
                    zaposleni.Add(noviLekar);
                    Close();
                }
            }
            else if (osoba.Korisnik.TipKorisnika == TipKorisnika.Sekretar)
            {
                Korisnik korisnik = new Korisnik(textBoxKorisnickoIme.Text, textBoxLozinka.Password, TipKorisnika.Sekretar);
                Osoba osoba = new Osoba(GradIzForme(textBoxMestoRodjenjaDrzava.Text, textBoxMestoRodjenjaGrad.Text),
                        korisnik, adresaStanovanja, textBoxIme.Text, textBoxPrezime.Text, textBoxEmail.Text,
                        textBoxJMBG.Text, datePickerDatumRodjenja.SelectedDate.GetValueOrDefault(),
                        (bool)radioButtonPolM.IsChecked ? Pol.Muski : Pol.Zenski, textBoxKontaktTelefon.Text);
                Sekretar noviSekretar = new Sekretar(osoba);
                Sekretar stariSekretar = sekretarRepository.PregledSekretara(this.osoba.Jmbg);
                if (sekretarService.IzmenaSekretara(stariSekretar, noviSekretar))
                {
                    zaposleni.Remove(this.osoba);
                    zaposleni.Add(noviSekretar);
                    Close();
                }
            }
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void InicijalizacijaCombo()
        {
            textBoxMestoRodjenjaGrad.ItemsSource = gradoviRepository.PregledSvihGradova();
            textBoxGrad.ItemsSource = gradoviRepository.PregledSvihGradova();
            textBoxMestoRodjenjaDrzava.ItemsSource = drzaveRepository.PregledSvihDrzava();
            textBoxDrzava.ItemsSource = drzaveRepository.PregledSvihDrzava();
        }

        private void InicijalizacijaForme(Osoba osoba)
        {
            textBoxIme.Text = osoba.Ime;
            textBoxPrezime.Text = osoba.Prezime;
            textBoxJMBG.Text = osoba.Jmbg;
            datePickerDatumRodjenja.SelectedDate = osoba.DatumRodjenja;
            textBoxMestoRodjenjaGrad.Text = osoba.MestoRodjenja.Ime;
            textBoxMestoRodjenjaDrzava.Text = osoba.MestoRodjenja.drzava.Ime;
            textBoxUlica.Text = osoba.AdresaStanovanja.Ulica;
            textBoxBroj.Text = osoba.AdresaStanovanja.Broj.ToString();
            textBoxSprat.Text = osoba.AdresaStanovanja.Sprat.ToString();
            textBoxStan.Text = osoba.AdresaStanovanja.Stan.ToString();
            textBoxGrad.Text = osoba.AdresaStanovanja.grad.Ime;
            textBoxDrzava.Text = osoba.AdresaStanovanja.grad.drzava.Ime;
            radioButtonPolM.IsChecked = osoba.Pol.Equals(Pol.Muski);
            radioButtonPolZ.IsChecked = osoba.Pol.Equals(Pol.Zenski);
            textBoxEmail.Text = osoba.Email;
            textBoxKontaktTelefon.Text = osoba.KontaktTelefon;
            textBoxKorisnickoIme.Text = osoba.Korisnik.KorisnickoIme;
            textBoxLozinka.Password = osoba.Korisnik.Lozinka;
        }

        private Grad GradIzForme(string drzava, string grad)
        {
            Drzava drzavaDTO = new Drzava(drzava);
            Grad gradDTO = new Grad(grad, drzavaDTO);
            sekretarController.DodavanjeDrzave(drzavaDTO);
            sekretarController.DodavanjeGrada(gradDTO);
            return gradDTO;
        }

        private void textBoxGrad_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Grad grad = (Grad)textBoxGrad.SelectedItem;
            if (grad != null)
            {
                textBoxDrzava.Text = grad.drzava.Ime;
            }
        }

        private void textBoxMestoRodjenjaGrad_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Grad grad = (Grad)textBoxMestoRodjenjaGrad.SelectedItem;
            if (grad != null)
            {
                textBoxMestoRodjenjaDrzava.Text = grad.drzava.Ime;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }
    }
}
