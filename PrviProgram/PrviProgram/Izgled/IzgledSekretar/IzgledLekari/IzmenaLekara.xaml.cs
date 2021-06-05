using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using Controller;
using Model;
using Repository;
using Service;

namespace PrviProgram.Izgled.IzgledSekretar.IzgledLekari
{
    public partial class IzmenaLekara : Window
    {
        private SekretarController sekretarController = new SekretarController();
        private DrzaveRepository drzaveRepository = new DrzaveRepository();
        private GradoviRepository gradoviRepository = new GradoviRepository();
        private UtilityService utilityService = new UtilityService();
        private ObservableCollection<Lekar> lekari;
        private ObservableCollection<Specijalizacija> specijalizacije = new ObservableCollection<Specijalizacija>();
        private Lekar lekar;

        public IzmenaLekara(ObservableCollection<Lekar> lekari, Lekar lekar)
        {
            InitializeComponent();
            this.lekari = lekari;
            this.lekar = lekar;
            InicijalizacijaCombo();
            InicijalizacijaForme();

        }

        private void InicijalizacijaCombo()
        {
            textBoxMestoRodjenjaGrad.ItemsSource = gradoviRepository.PregledSvihGradova();
            textBoxGrad.ItemsSource = gradoviRepository.PregledSvihGradova();
            textBoxMestoRodjenjaDrzava.ItemsSource = drzaveRepository.PregledSvihDrzava();
            textBoxDrzava.ItemsSource = drzaveRepository.PregledSvihDrzava();
        }

        private void InicijalizacijaForme()
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
            radioButtonPolM.IsChecked = lekar.Pol.Equals(Pol.Muski);
            radioButtonPolZ.IsChecked = lekar.Pol.Equals(Pol.Zenski);
            textBoxEmail.Text = lekar.Email;
            textBoxKontaktTelefon.Text = lekar.KontaktTelefon;
            textBoxKorisnickoIme.Text = lekar.Korisnik.KorisnickoIme;
            textBoxLozinka.Password = lekar.Korisnik.Lozinka;
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
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
                int index = lekari.IndexOf(lekar);
                lekari.Remove(lekar);
                lekari.Insert(index, noviLekar);
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
