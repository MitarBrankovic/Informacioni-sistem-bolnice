using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Controller;
using Model;
using Repository;
using Service;

namespace PrviProgram.Izgled.IzgledSekretar.Views
{
    public partial class RegistracijaPacijentaView : Page
    {
        private SekretarController sekretarController = new SekretarController();
        private DrzaveRepository drzaveRepository = new DrzaveRepository();
        private GradoviRepository gradoviRepository = new GradoviRepository();
        private UtilityService utilityService = new UtilityService();
        private ObservableCollection<Pacijent> pacijenti;

        public RegistracijaPacijentaView(ObservableCollection<Pacijent> pacijenti)
        {
            InitializeComponent();
            this.pacijenti = pacijenti;
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
            Korisnik korisnik = new Korisnik(textBoxKorisnickoIme.Text, textBoxLozinka.Password, TipKorisnika.Pacijent);

            Adresa adresaStanovanja = new Adresa(textBoxUlica.Text, utilityService.IntParser(textBoxBroj.Text),
                                                 utilityService.IntParser(textBoxSprat.Text),
                                                 utilityService.IntParser(textBoxStan.Text),
                                                 GradIzForme(textBoxDrzava.Text, textBoxGrad.Text));

            Osoba osoba = new Osoba(GradIzForme(textBoxMestoRodjenjaDrzava.Text, textBoxMestoRodjenjaGrad.Text),
                                    korisnik, adresaStanovanja, textBoxIme.Text, textBoxPrezime.Text, textBoxEmail.Text,
                                    textBoxJMBG.Text, datePickerDatumRodjenja.SelectedDate.GetValueOrDefault(),
                                    (bool)radioButtonPolM.IsChecked ? Pol.Muski : Pol.Zenski, textBoxKontaktTelefon.Text);

            Pacijent pacijent = new Pacijent(osoba, new List<Termin>(), new KartonPacijenta());

            if (sekretarController.DodavanjePacijenta(pacijent))
            {
                pacijenti.Add(pacijent);
                NavigationService.GoBack();
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
            NavigationService.GoBack();
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
