using Model;
using PrviProgram.Izgled.IzgledLekar;
using Service;
using Service.LekarService;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace PrviProgram.Izgled.IzgledLekar
{
    /// <summary>
    /// Interaction logic for WindowTermini.xaml
    /// </summary>
    public partial class WindowTermini : Window
    {
        //List<Termin> termini = new List<Termin>();
        public PreglediService upravljanje;
        public ObservableCollection<Termin> termini;
        private TerminiService terminiService = new TerminiService();
        private IzvrseniPregled izvrseniPregled;

        public WindowTermini(Lekar lekar)
        {
            InitializeComponent();

            upravljanje = new PreglediService();
            termini = new ObservableCollection<Termin>(upravljanje.PregledSvihPregledaLekara(lekar));
            dataGridLekar.ItemsSource = termini;
            //termini = UpravljanjePregledima.getInstance().PregledSvihPregleda();
            //dataGridLekar.ItemsSource = termini;
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            DodavanjeTermina dodavanje = new DodavanjeTermina(termini);
            dodavanje.Show();
        }

        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridLekar.SelectedIndex != -1)
            {
                IzmenaTermina izmena = new IzmenaTermina(termini, (Termin)dataGridLekar.SelectedItem);
                izmena.Show();
            }
            else
            {
                MessageBox.Show("Morate izabrati termin!");
            }
        }

        private void Izbrisi_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridLekar.SelectedItem != null)
            {
                Termin termin = (Termin)dataGridLekar.SelectedItem;
                terminiService.BrisanjeTermina(termin);
                termini.Remove(termin);
                MessageBox.Show("Uspesno ste obrisali termin!");
            }
            else
            {
                MessageBox.Show("Morate izabrati termin!");
            }


        }
        private void Informacije_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridLekar.SelectedIndex != -1)
            {
                InformacijePacijent info = new InformacijePacijent(termini, (Termin)dataGridLekar.SelectedItem);
                info.Show();
            }
            else
            {
                MessageBox.Show("Morate izabrati termin!");

            }
        }


        private void Anamneza_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridLekar.SelectedIndex != -1)
            {
                Termin termin = (Termin)dataGridLekar.SelectedItem;
                izvrseniPregled = new IzvrseniPregled();
                izvrseniPregled.Lekar = termin.lekar;
                izvrseniPregled.Datum = termin.Datum;
                izvrseniPregled.TipTermina = termin.TipTermina;
                izvrseniPregled.Sifra = termin.SifraTermina;

                IzvrsavanjeAnamneze anamneza = new IzvrsavanjeAnamneze(izvrseniPregled, termin.pacijent);
                anamneza.Show();
            }
            else
            {
                MessageBox.Show("Morate izabrati termin!");

            }
        }


        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        
    }
}
