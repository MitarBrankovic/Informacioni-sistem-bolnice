using Model;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace PrviProgram.Izgled.IzgledSekretar.IzgledPacijenti
{
    public partial class Registracija : Window
    {
        private DrzaveRepository drzaveRepository = new DrzaveRepository();
        private GradoviRepository gradoviRepository = new GradoviRepository();
        private PacijentiService pacijentiService = new PacijentiService();
        private GradoviService gradoviService = new GradoviService();
        private DrzaveService drzaveService = new DrzaveService();
        private ObservableCollection<Pacijent> pacijenti;

        public Registracija(ObservableCollection<Pacijent> pacijenti)
        {
            InitializeComponent();
            this.pacijenti = pacijenti;
            textBoxMestoRodjenjaGrad.ItemsSource = gradoviRepository.PregledSvihGradova();
            textBoxGrad.ItemsSource = gradoviRepository.PregledSvihGradova();
            textBoxMestoRodjenjaDrzava.ItemsSource = drzaveRepository.PregledSvihDrzava();
            textBoxDrzava.ItemsSource = drzaveRepository.PregledSvihDrzava();
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            Korisnik korisnik = new Korisnik();
            korisnik.KorisnickoIme = textBoxKorisnickoIme.Text;
            korisnik.Lozinka = textBoxLozinka.Password;
            korisnik.TipKorisnika = TipKorisnika.Pacijent;

            Pacijent pacijent = new Pacijent();
            pacijent.Ime = textBoxIme.Text;
            pacijent.Prezime = textBoxPrezime.Text;
            pacijent.Jmbg = textBoxJMBG.Text;
            if (datePickerDatumRodjenja.SelectedDate != null)
            {
                pacijent.DatumRodjenja = (DateTime)datePickerDatumRodjenja.SelectedDate;
            }

            Drzava drzavaRodjenja = new Drzava();
            drzavaRodjenja.Ime = textBoxMestoRodjenjaDrzava.Text;
            Grad gradRodjenja = new Grad();
            gradRodjenja.Ime = textBoxMestoRodjenjaGrad.Text;
            gradRodjenja.drzava = drzavaRodjenja;

            pacijent.MestoRodjenja = gradRodjenja;
            drzaveService.DodavanjeDrzave(drzavaRodjenja);
            gradoviService.DodavanjeGrada(gradRodjenja);

            Drzava drzava = new Drzava();
            drzava.Ime = textBoxDrzava.Text;
            Grad grad = new Grad();
            grad.Ime = textBoxGrad.Text;
            grad.drzava = drzava;

            drzaveService.DodavanjeDrzave(drzava);
            gradoviService.DodavanjeGrada(grad);

            Adresa adresa = new Adresa();
            adresa.Ulica = textBoxUlica.Text;
            int temp;
            adresa.Broj = int.TryParse(textBoxBroj.Text, out temp) ? temp : default;
            adresa.Sprat = int.TryParse(textBoxSprat.Text, out temp) ? temp : default;
            adresa.Stan = int.TryParse(textBoxStan.Text, out temp) ? temp : default;
            adresa.grad = grad;

            pacijent.AdresaStanovanja = adresa;

            pacijent.Pol = ((bool)radioButtonPolM.IsChecked ? Model.Pol.Muski : Model.Pol.Zenski);
            pacijent.Email = textBoxEmail.Text;
            pacijent.KontaktTelefon = textBoxKontaktTelefon.Text;

            pacijent.termin = new List<Termin>();
            pacijent.kartonPacijenta = new KartonPacijenta();
            
            pacijent.Korisnik = korisnik;
            Pacijent tmpPacijent = new Pacijent();
            tmpPacijent.Jmbg = pacijent.Jmbg;
            pacijent.kartonPacijenta.pacijent = tmpPacijent;
            
            
            if (pacijentiService.DodavanjePacijenta(pacijent) == true)
            {
                this.pacijenti.Add(pacijent);
            }
            this.Close();
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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