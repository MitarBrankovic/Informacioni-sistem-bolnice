using Controller;
using Model;
using PrviProgram.Service;
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
using System.Windows.Shapes;

namespace PrviProgram.Izgled.IzgledUpravnik
{
    public partial class PodesavanjaNalogaUpravnika : Window
    {
        private Upravnik upravnik;
        private UpravnikService upravnikService = new UpravnikService();
        private SekretarController sekretarController = new SekretarController();
        private DrzaveRepository drzaveRepository = new DrzaveRepository();
        private GradoviRepository gradoviRepository = new GradoviRepository();
        private UtilityService utilityService = new UtilityService();
        public PodesavanjaNalogaUpravnika(Upravnik upravnik)
        {
            InitializeComponent();
            this.upravnik = upravnik;
            InicijalizacijaCombo();
            InicijalizacijaForme(upravnik);
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            Korisnik korisnik = new Korisnik(textBoxKorisnickoIme.Text, textBoxLozinka.Password, TipKorisnika.Upravnik);

            Adresa adresaStanovanja = new Adresa(textBoxUlica.Text, utilityService.IntParser(textBoxBroj.Text),
                                     utilityService.IntParser(textBoxSprat.Text),
                                     utilityService.IntParser(textBoxStan.Text),
                                     GradIzForme(textBoxDrzava.Text, textBoxGrad.Text));

            Osoba osoba = new Osoba(GradIzForme(textBoxMestoRodjenjaDrzava.Text, textBoxMestoRodjenjaGrad.Text),
                        korisnik, adresaStanovanja, textBoxIme.Text, textBoxPrezime.Text, textBoxEmail.Text,
                        textBoxJMBG.Text, datePickerDatumRodjenja.SelectedDate.GetValueOrDefault(),
                        (bool)radioButtonPolM.IsChecked ? Pol.Muski : Pol.Zenski, textBoxKontaktTelefon.Text);
            Upravnik noviUpravnik = new Upravnik(osoba);
            if (upravnikService.IzmenaUpravnika(upravnik, noviUpravnik))
            {
                Close();
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

        private void InicijalizacijaForme(Upravnik upravnik)
        {
            textBoxIme.Text = upravnik.Ime;
            textBoxPrezime.Text = upravnik.Prezime;
            textBoxJMBG.Text = upravnik.Jmbg;
            datePickerDatumRodjenja.SelectedDate = upravnik.DatumRodjenja;
            textBoxMestoRodjenjaGrad.Text = upravnik.MestoRodjenja.Ime;
            textBoxMestoRodjenjaDrzava.Text = upravnik.MestoRodjenja.drzava.Ime;
            textBoxUlica.Text = upravnik.AdresaStanovanja.Ulica;
            textBoxBroj.Text = upravnik.AdresaStanovanja.Broj.ToString();
            textBoxSprat.Text = upravnik.AdresaStanovanja.Sprat.ToString();
            textBoxStan.Text = upravnik.AdresaStanovanja.Stan.ToString();
            textBoxGrad.Text = upravnik.AdresaStanovanja.grad.Ime;
            textBoxDrzava.Text = upravnik.AdresaStanovanja.grad.drzava.Ime;
            radioButtonPolM.IsChecked = upravnik.Pol.Equals(Pol.Muski);
            radioButtonPolZ.IsChecked = upravnik.Pol.Equals(Pol.Zenski);
            textBoxEmail.Text = upravnik.Email;
            textBoxKontaktTelefon.Text = upravnik.KontaktTelefon;
            textBoxKorisnickoIme.Text = upravnik.Korisnik.KorisnickoIme;
            textBoxLozinka.Password = upravnik.Korisnik.Lozinka;
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
