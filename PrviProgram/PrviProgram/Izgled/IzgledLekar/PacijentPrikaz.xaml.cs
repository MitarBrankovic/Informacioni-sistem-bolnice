using Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrviProgram.Izgled.IzgledLekar
{
    public partial class PacijentPrikaz : UserControl
    {
        public ObservableCollection<IzvrseniPregled> izvrseniPregledi;
        private PacijentRepository pacijentRepository = new PacijentRepository();
        private Pacijent pacijent;
        private bool azurirajPritisnut = false;
        private PocetniPrikaz pocetniPrikaz;
        private LekoviRepository lekoviRepository = new LekoviRepository();
        public PacijentPrikaz(PocetniPrikaz pocetniPrikaz, Pacijent pacijent)
        {
            InitializeComponent();
            this.pocetniPrikaz = pocetniPrikaz;
            this.pacijent = pacijentRepository.PregledPacijenta(pacijent.Jmbg);
            PopuniInformacijePacijenta();
            izvrseniPregledi = new ObservableCollection<IzvrseniPregled>(pacijentRepository.PregledPacijenta(pacijent.Jmbg).KartonPacijenta.izvrseniPregled);
            dataGridKartonPacijenta.ItemsSource = izvrseniPregledi;
            pocetniPrikaz.GoBackButtonVisibilityTrue();
            pocetniPrikaz.DodajUserControl(this);
            ComboboxLek.ItemsSource = lekoviRepository.PregledSvihLekova();
        }
        private void PopuniInformacijePacijenta()
        {
            textBoxIme.Text = pacijent.Ime;
            textBoxPrezime.Text = pacijent.Prezime;
            textBoxJMBG.Text = pacijent.Jmbg;
            datePickerDatumRodjenja.SelectedDate = pacijent.DatumRodjenja;
            radioButtonPolM.IsChecked = pacijent.Pol.Equals(Model.Pol.Muski);
            radioButtonPolZ.IsChecked = pacijent.Pol.Equals(Model.Pol.Zenski);
        }

        private void dataGridKarton_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Azuriraj.IsEnabled = true;
            if (radioButtonAnamneza.IsChecked == true)
            {
                TextboxInformacije.Text = ((IzvrseniPregled)dataGridKartonPacijenta.SelectedItem).anamneza.Opis;
            }
            else if (radioButtonRecpet.IsChecked == true)
            {
                TextboxInformacije.Text = ((IzvrseniPregled)dataGridKartonPacijenta.SelectedItem).recept.OpisLeka;
            }
            //ComboboxLek.SelectedItem = ((IzvrseniPregled)dataGridKartonPacijenta.SelectedItem).recept.Lekovi.Naziv;
            SelektujLekUComboboxu();
            BrojDana.Text = ((IzvrseniPregled)dataGridKartonPacijenta.SelectedItem).recept.VremenskiPeriodUzimanjaLeka.ToString();
        }

        private void SelektujLekUComboboxu()
        {
            int count = 0;
            foreach (Lek lek in ComboboxLek.Items)
            {
                if (lek.Sifra == ((IzvrseniPregled)dataGridKartonPacijenta.SelectedItem).recept.Lekovi.Sifra)
                {
                    ComboboxLek.SelectedIndex = count;
                    break;
                }
                count++;
            }
        }

        private void Alergeni_Click(object sender, RoutedEventArgs e)
        {
            PregledAlergena pregledAlergena = new PregledAlergena(pacijent);
            pregledAlergena.Show();
        }

        private void Azuriraj_Click(object sender, RoutedEventArgs e)
        {
            IzmenaStanjaDugmetaAzuriranje();
        }

        private void IzmenaStanjaDugmetaAzuriranje()
        {
            if (TextboxInformacije != null)
            {
                if (!azurirajPritisnut)
                {
                    azurirajPritisnut = true;
                    TextboxInformacije.IsEnabled = true;
                    BrojDana.IsEnabled = true;
                    ComboboxLek.IsEnabled = true;
                    Azuriraj.Content = "Sacuvaj";
                }
                else
                {
                    azurirajPritisnut = false;
                    TextboxInformacije.IsEnabled = false;
                    BrojDana.IsEnabled = false;
                    ComboboxLek.IsEnabled = false;
                    Azuriraj.Content = "Azuriraj";
                    AzuriranjeIzvrsenogPregleda();
                }
            }
        }

        private void AzuriranjeIzvrsenogPregleda()
        {
            IzvrseniPregled selektovaniIzvrseniPregled = ((IzvrseniPregled)dataGridKartonPacijenta.SelectedItem);
            selektovaniIzvrseniPregled = ProveraKojiRadioButtonJePritisnut(selektovaniIzvrseniPregled);
            LekAzuriran(selektovaniIzvrseniPregled);
            KartonPacijentaService.getInstance().IzvrsenaAnamneza(selektovaniIzvrseniPregled, pacijentRepository.PregledPacijenta(pacijent.Jmbg));
        }

        private void LekAzuriran(IzvrseniPregled noviIzvrseniPregled)
        {
            noviIzvrseniPregled.recept.Lekovi = (Lek)ComboboxLek.SelectedItem;
            noviIzvrseniPregled.recept.VremenskiPeriodUzimanjaLeka = int.Parse(BrojDana.Text);
        }

        private IzvrseniPregled ProveraKojiRadioButtonJePritisnut(IzvrseniPregled noviIzvrseniPregled)
        {
            if (radioButtonAnamneza.IsChecked == true)
            {
                noviIzvrseniPregled.anamneza.Opis = TextboxInformacije.Text;
            }
            else if (radioButtonRecpet.IsChecked == true)
            {
                noviIzvrseniPregled.recept.OpisLeka = TextboxInformacije.Text;
            }
            return noviIzvrseniPregled;
        }

        private void ResetAzurirajDugme()
        {
            if (azurirajPritisnut == true)
            {
                azurirajPritisnut = false;
                Azuriraj.Content = "Azuriraj";
                TextboxInformacije.IsEnabled = false;
            }
        }

        private void radioButtonAnamneza_Checked(object sender, RoutedEventArgs e)
        {
            if ((IzvrseniPregled)dataGridKartonPacijenta.SelectedItem != null)
                TextboxInformacije.Text = ((IzvrseniPregled)dataGridKartonPacijenta.SelectedItem).anamneza.Opis;
            ResetAzurirajDugme();
        }

        private void radioButtonRecpet_Checked(object sender, RoutedEventArgs e)
        {
            if ((IzvrseniPregled)dataGridKartonPacijenta.SelectedItem != null)
                TextboxInformacije.Text = ((IzvrseniPregled)dataGridKartonPacijenta.SelectedItem).recept.OpisLeka;
            ResetAzurirajDugme();
        }

        private void BrojDana_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void BolnickoLecenje_Click(object sender, RoutedEventArgs e)
        {
            IstorijaBolnickogLecenjaWindow istorijaBolnickogLecenjaWindow = new IstorijaBolnickogLecenjaWindow(pacijent);
            istorijaBolnickogLecenjaWindow.Show();
        }
    }
}
