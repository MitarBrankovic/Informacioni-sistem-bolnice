using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using Controller;
using Model;
using Repository;
using Service;

namespace PrviProgram.Izgled.IzgledSekretar.IzgledLekari
{
    public partial class DodavanjeLekara : Window
    {
        private SekretarController sekretarController = new SekretarController();
        private DrzaveRepository drzaveRepository = new DrzaveRepository();
        private GradoviRepository gradoviRepository = new GradoviRepository();
        private UtilityService utilityService = new UtilityService();
        private ObservableCollection<Lekar> lekari;
        private ObservableCollection<Specijalizacija> specijalizacije = new ObservableCollection<Specijalizacija>();

        public DodavanjeLekara(ObservableCollection<Lekar> lekari)
        {
            InitializeComponent();
            this.lekari = lekari;
            InicijalizacijaCombo();
        }

        private void InicijalizacijaCombo()
        {
            textBoxMestoRodjenjaGrad.ItemsSource = gradoviRepository.PregledSvihGradova();
            textBoxGrad.ItemsSource = gradoviRepository.PregledSvihGradova();
            textBoxMestoRodjenjaDrzava.ItemsSource = drzaveRepository.PregledSvihDrzava();
            textBoxDrzava.ItemsSource = drzaveRepository.PregledSvihDrzava();
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            Korisnik korisnik = new Korisnik(textBoxKorisnickoIme.Text, textBoxLozinka.Password, TipKorisnika.Lekar);

            Drzava drzavaRodjenja = new Drzava(textBoxMestoRodjenjaDrzava.Text);
            Grad gradRodjenja = new Grad(textBoxMestoRodjenjaGrad.Text, drzavaRodjenja);
            sekretarController.DodavanjeDrzave(drzavaRodjenja);
            sekretarController.DodavanjeGrada(gradRodjenja);

            Drzava drzavaStanovanja = new Drzava(textBoxDrzava.Text);
            Grad gradStanovanja = new Grad(textBoxGrad.Text, drzavaStanovanja);
            sekretarController.DodavanjeDrzave(drzavaStanovanja);
            sekretarController.DodavanjeGrada(gradStanovanja);

            Adresa adresaStanovanja = new Adresa(textBoxUlica.Text, utilityService.IntParser(textBoxBroj.Text),
                                                 utilityService.IntParser(textBoxSprat.Text),
                                                 utilityService.IntParser(textBoxStan.Text), gradStanovanja);

            Osoba osoba = new Osoba(gradRodjenja, korisnik, adresaStanovanja, textBoxIme.Text, textBoxPrezime.Text,
                                    textBoxEmail.Text, textBoxJMBG.Text,
                                    datePickerDatumRodjenja.SelectedDate.GetValueOrDefault(),
                                    (bool)radioButtonPolM.IsChecked ? Pol.Muski : Pol.Zenski, textBoxKontaktTelefon.Text);

            Lekar lekar = new Lekar(osoba, new List<Specijalizacija>(specijalizacije));

            if (sekretarController.DodavanjeLekara(lekar))
            {
                lekari.Add(lekar);
            }
            Close();
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
