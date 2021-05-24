using System.Collections.ObjectModel;
using System.Windows;
using Controller;
using Model;
using Repository;
using Service;

namespace PrviProgram.Izgled.IzgledSekretar.IzgledPacijenti
{
    public partial class IzmenaPacijenta : Window
    {
        private SekretarController sekretarController = new SekretarController();
        private DrzaveRepository drzaveRepository = new DrzaveRepository();
        private GradoviRepository gradoviRepository = new GradoviRepository();
        private UtilityService utilityService = new UtilityService();
        private ObservableCollection<Pacijent> pacijenti;
        private Pacijent pacijent;

        public IzmenaPacijenta(ObservableCollection<Pacijent> pacijenti, Pacijent pacijent)
        {
            InitializeComponent();
            this.pacijenti = pacijenti;
            this.pacijent = pacijent;
            InicijalizacijaCombo();
            InicijalizacijaForme(pacijent);
        }
        private void InicijalizacijaCombo()
        {
            textBoxMestoRodjenjaGrad.ItemsSource = gradoviRepository.PregledSvihGradova();
            textBoxGrad.ItemsSource = gradoviRepository.PregledSvihGradova();
            textBoxMestoRodjenjaDrzava.ItemsSource = drzaveRepository.PregledSvihDrzava();
            textBoxDrzava.ItemsSource = drzaveRepository.PregledSvihDrzava();
        }

        private void InicijalizacijaForme(Pacijent pacijent)
        {
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
            radioButtonPolM.IsChecked = pacijent.Pol.Equals(Pol.Muski);
            radioButtonPolZ.IsChecked = pacijent.Pol.Equals(Pol.Zenski);
            textBoxEmail.Text = pacijent.Email;
            textBoxKontaktTelefon.Text = pacijent.KontaktTelefon;
            textBoxKorisnickoIme.Text = pacijent.Korisnik.KorisnickoIme;
            textBoxLozinka.Password = pacijent.Korisnik.Lozinka;
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            Korisnik korisnik = new Korisnik(textBoxKorisnickoIme.Text, textBoxLozinka.Password, TipKorisnika.Pacijent);

            Adresa adresaStanovanja = new Adresa(textBoxUlica.Text, utilityService.IntParser(textBoxBroj.Text),
                                     utilityService.IntParser(textBoxSprat.Text),
                                     utilityService.IntParser(textBoxStan.Text),
                                     GradIzForme(textBoxDrzava.Text, textBoxGrad.Text));

            Osoba osoba = new Osoba(GradIzForme(textBoxMestoRodjenjaDrzava.Text, textBoxMestoRodjenjaGrad.Text),
                        korisnik, adresaStanovanja, textBoxIme.Text, textBoxPrezime.Text, textBoxEmail.Text,
                        textBoxJMBG.Text, datePickerDatumRodjenja.SelectedDate.GetValueOrDefault(),
                        (bool)radioButtonPolM.IsChecked ? Pol.Muski : Pol.Zenski, textBoxKontaktTelefon.Text);

            Pacijent noviPacijent = new Pacijent(osoba, pacijent.GetTermin(), pacijent.kartonPacijenta);

            if (sekretarController.IzmenaPacijenta(pacijent, noviPacijent))
            {
                int index = pacijenti.IndexOf(pacijent);
                pacijenti.Remove(pacijent);
                pacijenti.Insert(index, noviPacijent);
                Close();
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

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            Close();
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