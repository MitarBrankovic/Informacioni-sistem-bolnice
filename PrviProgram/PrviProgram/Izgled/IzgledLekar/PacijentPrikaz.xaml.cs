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
    /// <summary>
    /// Interaction logic for PacijentPrikaz.xaml
    /// </summary>
    public partial class PacijentPrikaz : UserControl
    {
        private ObservableCollection<Termin> termini;
        public ObservableCollection<IzvrseniPregled> izvrseniPregledi;
        private PacijentRepository pacijentRepository = new PacijentRepository();
        private Termin termin;
        private Pacijent pacijent;
        private bool azurirajPritisnut = false;
        private PocetniPrikaz pocetniPrikaz;
        private UserControl prethodniUserControl;
        public PacijentPrikaz(UserControl prethodniUserControl, PocetniPrikaz pocetniPrikaz, Pacijent pacijent)
        {
            InitializeComponent();
            this.pocetniPrikaz = pocetniPrikaz;
            this.pacijent = pacijentRepository.PregledPacijenta(pacijent.Jmbg);
            PopuniInformacijePacijenta();
            izvrseniPregledi = new ObservableCollection<IzvrseniPregled>(pacijentRepository.PregledPacijenta(pacijent.Jmbg).kartonPacijenta.izvrseniPregled);
            dataGridKartonPacijenta.ItemsSource = izvrseniPregledi;
            pocetniPrikaz.DugmeVisibilityTrue();
            pocetniPrikaz.setUserControl(this, prethodniUserControl);
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
            else if (radioButtonTerapija.IsChecked == true)
            {
                TextboxInformacije.Text = ((IzvrseniPregled)dataGridKartonPacijenta.SelectedItem).terapija.Opis;
            }
            else if (radioButtonRecpet.IsChecked == true)
            {
                TextboxInformacije.Text = ((IzvrseniPregled)dataGridKartonPacijenta.SelectedItem).recept.Lekovi;
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
                    Azuriraj.Content = "Sacuvaj";
                }
                else
                {
                    azurirajPritisnut = false;
                    TextboxInformacije.IsEnabled = false;
                    Azuriraj.Content = "Azuriraj";
                    AzuriranjeIzvrsenogPregleda();
                }
            }
        }

        private void AzuriranjeIzvrsenogPregleda()
        {
            IzvrseniPregled selektovaniIzvrseniPregled = ((IzvrseniPregled)dataGridKartonPacijenta.SelectedItem);
            selektovaniIzvrseniPregled = ProveraKojiRadioButtonJePritisnut(selektovaniIzvrseniPregled);
            KartonPacijentaService.getInstance().IzvrsenaAnamneza(selektovaniIzvrseniPregled, pacijentRepository.PregledPacijenta(pacijent.Jmbg));
        }

        private IzvrseniPregled ProveraKojiRadioButtonJePritisnut(IzvrseniPregled noviIzvrseniPregled)
        {
            if (radioButtonAnamneza.IsChecked == true)
            {
                noviIzvrseniPregled.anamneza.Opis = TextboxInformacije.Text;
            }
            else if (radioButtonTerapija.IsChecked == true)
            {
                noviIzvrseniPregled.terapija.Opis = TextboxInformacije.Text;
            }
            else if (radioButtonRecpet.IsChecked == true)
            {
                noviIzvrseniPregled.recept.Lekovi = TextboxInformacije.Text;
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
                TextboxInformacije.Text = ((IzvrseniPregled)dataGridKartonPacijenta.SelectedItem).recept.Lekovi;
            ResetAzurirajDugme();
        }

        private void radioButtonTerapija_Checked(object sender, RoutedEventArgs e)
        {
            if ((IzvrseniPregled)dataGridKartonPacijenta.SelectedItem != null)
                TextboxInformacije.Text = ((IzvrseniPregled)dataGridKartonPacijenta.SelectedItem).terapija.Opis;
            if (azurirajPritisnut == true)
                ResetAzurirajDugme();
        }

    }
}
