using Model;
using Service.LogikaGeneralno;
using Service.SekretarService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace PrviProgram.Izgled.Sekretar
{
    public partial class Registracija : Window
    {
        private PacijentiService upravljanjePacijentima;
        private GradoviService upravljanjeGradovima;
        private DrzaveService upravljanjeDrzavama;
        private ObservableCollection<Pacijent> pacijenti;

        public Registracija(ObservableCollection<Pacijent> pacijenti)
        {
            InitializeComponent();
            upravljanjePacijentima = new PacijentiService();
            upravljanjeGradovima = new GradoviService();
            upravljanjeDrzavama = new DrzaveService();
            this.pacijenti = pacijenti;
            textBoxMestoRodjenjaGrad.ItemsSource = upravljanjeGradovima.PregledSvihGradova();
            textBoxGrad.ItemsSource = upravljanjeGradovima.PregledSvihGradova();
            textBoxMestoRodjenjaDrzava.ItemsSource = upravljanjeDrzavama.PregledSvihDrzava();
            textBoxDrzava.ItemsSource = upravljanjeDrzavama.PregledSvihDrzava();
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            Korisnik korisnik = new Korisnik();
            korisnik.KorisnickoIme = textBoxKorisnickoIme.Text;
            korisnik.Lozinka = textBoxLozinka.Password;

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
            upravljanjeDrzavama.DodavanjeDrzave(drzavaRodjenja);
            upravljanjeGradovima.DodavanjeGrada(gradRodjenja);

            Drzava drzava = new Drzava();
            drzava.Ime = textBoxDrzava.Text;
            Grad grad = new Grad();
            grad.Ime = textBoxGrad.Text;
            grad.drzava = drzava;

            upravljanjeDrzavama.DodavanjeDrzave(drzava);
            upravljanjeGradovima.DodavanjeGrada(grad);

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

            if (upravljanjePacijentima.DodavanjePacijenta(pacijent) == true)
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