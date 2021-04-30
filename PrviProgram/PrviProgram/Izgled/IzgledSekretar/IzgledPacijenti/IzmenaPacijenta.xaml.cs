using Model;
using Service;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using Repository;

namespace PrviProgram.Izgled.IzgledSekretar.IzgledPacijenti
{
    public partial class IzmenaPacijenta : Window
    {
        private DrzaveRepository drzaveRepository = new DrzaveRepository();
        private GradoviRepository gradoviRepository = new GradoviRepository();
        private PacijentiService pacijentiService = new PacijentiService();
        private GradoviService gradoviService = new GradoviService();
        private DrzaveService drzaveService = new DrzaveService();
        private ObservableCollection<Model.Pacijent> pacijenti;
        private Model.Pacijent pacijent;

        public IzmenaPacijenta(ObservableCollection<Model.Pacijent> pacijenti, Model.Pacijent pacijent)
        {
            InitializeComponent();
            this.pacijenti = pacijenti;
            this.pacijent = pacijent;
            textBoxMestoRodjenjaGrad.ItemsSource = gradoviRepository.PregledSvihGradova();
            textBoxGrad.ItemsSource = gradoviRepository.PregledSvihGradova();
            textBoxMestoRodjenjaDrzava.ItemsSource = drzaveRepository.PregledSvihDrzava();
            textBoxDrzava.ItemsSource = drzaveRepository.PregledSvihDrzava();

            textBoxIme.Text = pacijent.Ime;
            textBoxPrezime.Text = pacijent.Prezime;
            textBoxJMBG.Text = pacijent.Jmbg;
            datePickerDatumRodjenja.SelectedDate = pacijent.DatumRodjenja;

            textBoxMestoRodjenjaGrad.Text = pacijent.MestoRodjenja.Ime;
            textBoxMestoRodjenjaDrzava.Text = pacijent.MestoRodjenja.drzava.Ime;

            textBoxUlica.Text = pacijent.AdresaStanovanja.Ulica;
            textBoxBroj.Text = pacijent.AdresaStanovanja.Broj.ToString();
            textBoxSprat.Text = pacijent.AdresaStanovanja.Sprat.ToString();
            textBoxStan.Text = pacijent.AdresaStanovanja.Stan.ToString();
            textBoxGrad.Text = pacijent.AdresaStanovanja.grad.Ime;
            textBoxDrzava.Text = pacijent.AdresaStanovanja.grad.drzava.Ime;

            radioButtonPolM.IsChecked = pacijent.Pol.Equals(Model.Pol.Muski);
            radioButtonPolZ.IsChecked = pacijent.Pol.Equals(Model.Pol.Zenski);
            textBoxEmail.Text = pacijent.Email;
            textBoxKontaktTelefon.Text = pacijent.KontaktTelefon;

            textBoxKorisnickoIme.Text = pacijent.Korisnik.KorisnickoIme;
            textBoxLozinka.Password = pacijent.Korisnik.Lozinka;

        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            Model.Korisnik korisnik = new Model.Korisnik();
            korisnik.KorisnickoIme = textBoxKorisnickoIme.Text;
            korisnik.Lozinka = textBoxLozinka.Password;
            korisnik.TipKorisnika = this.pacijent.Korisnik.TipKorisnika;

            Model.Pacijent noviPacijent = new Model.Pacijent();
            noviPacijent.Ime = textBoxIme.Text;
            noviPacijent.Prezime = textBoxPrezime.Text;
            noviPacijent.Jmbg = textBoxJMBG.Text;
            if (datePickerDatumRodjenja.SelectedDate != null)
            {
                noviPacijent.DatumRodjenja = (DateTime)datePickerDatumRodjenja.SelectedDate;
            }

            Drzava drzavaRodjenja = new Drzava();
            drzavaRodjenja.Ime = textBoxMestoRodjenjaDrzava.Text;
            Grad gradRodjenja = new Grad();
            gradRodjenja.Ime = textBoxMestoRodjenjaGrad.Text;
            gradRodjenja.drzava = drzavaRodjenja;

            noviPacijent.MestoRodjenja = gradRodjenja;
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

            noviPacijent.AdresaStanovanja = adresa;

            noviPacijent.Pol = ((bool)radioButtonPolM.IsChecked ? Model.Pol.Muski : Model.Pol.Zenski);
            noviPacijent.Email = textBoxEmail.Text;
            noviPacijent.KontaktTelefon = textBoxKontaktTelefon.Text;

            noviPacijent.termin = this.pacijent.termin;
            noviPacijent.kartonPacijenta = this.pacijent.kartonPacijenta;
            noviPacijent.Korisnik = korisnik;

            if (pacijentiService.IzmenaPacijenta(this.pacijent, noviPacijent) == true)
            {
                int index = this.pacijenti.IndexOf(this.pacijent);
                this.pacijenti.Remove(this.pacijent);
                this.pacijenti.Insert(index, noviPacijent);
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