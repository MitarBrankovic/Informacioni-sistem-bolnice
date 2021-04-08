using Logika.LogikaSekretar;
using Model;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace PrviProgram.Izgled.Sekretar
{
    public partial class IzmenaPacijenta : Window
    {
        private UpravljanjePacijentima up;
        private ObservableCollection<Model.Pacijent> pacijenti;
        private Model.Pacijent pacijent;

        public IzmenaPacijenta(ObservableCollection<Model.Pacijent> pacijenti, Model.Pacijent pacijent)
        {
            InitializeComponent();
            up = new UpravljanjePacijentima();
            this.pacijenti = pacijenti;
            this.pacijent = pacijent;

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

            Model.Pacijent noviPacijent = new Model.Pacijent();
            noviPacijent.Ime = textBoxIme.Text;
            noviPacijent.Prezime = textBoxPrezime.Text;
            noviPacijent.Jmbg = textBoxJMBG.Text;
            noviPacijent.DatumRodjenja = (DateTime)datePickerDatumRodjenja.SelectedDate;

            Drzava drzavaRodjenja = new Drzava();
            drzavaRodjenja.Ime = textBoxMestoRodjenjaDrzava.Text;
            Grad gradRodjenja = new Grad();
            gradRodjenja.Ime = textBoxMestoRodjenjaGrad.Text;
            gradRodjenja.drzava = drzavaRodjenja;

            noviPacijent.MestoRodjenja = gradRodjenja;

            Drzava drzava = new Drzava();
            drzava.Ime = textBoxDrzava.Text;
            Grad grad = new Grad();
            grad.Ime = textBoxGrad.Text;
            grad.drzava = drzava;
            Adresa adresa = new Adresa();
            adresa.Ulica = textBoxUlica.Text;
            adresa.Broj = int.Parse(textBoxBroj.Text);
            adresa.Sprat = int.Parse(textBoxSprat.Text);
            adresa.Stan = int.Parse(textBoxStan.Text);
            adresa.grad = grad;

            noviPacijent.AdresaStanovanja = adresa;

            noviPacijent.Pol = ((bool)radioButtonPolM.IsChecked ? Model.Pol.Muski : Model.Pol.Zenski);
            noviPacijent.Email = textBoxEmail.Text;
            noviPacijent.KontaktTelefon = textBoxKontaktTelefon.Text;

            noviPacijent.termin = this.pacijent.termin;
            noviPacijent.kartonPacijenta = this.pacijent.kartonPacijenta;
            noviPacijent.Korisnik = korisnik;

            if (up.IzmenaPacijenta(this.pacijent, noviPacijent) == true)
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
    }
}