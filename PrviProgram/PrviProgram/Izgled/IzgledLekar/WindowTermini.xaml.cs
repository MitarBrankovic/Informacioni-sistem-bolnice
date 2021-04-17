using Model;
using PrviProgram.Logika.Controllers;
using Service.LekarService;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace PrviProgram.Izgled.Lekar
{
    /// <summary>
    /// Interaction logic for WindowTermini.xaml
    /// </summary>
    public partial class WindowTermini : Window
    {
        private static WindowTermini instance = null;
        public static WindowTermini getInstance()
        {
            if (instance == null)
            {
                instance = new WindowTermini();
            }
            return instance;
        }

        //List<Termin> termini = new List<Termin>();
        public PreglediService upravljanje;
        public ObservableCollection<Termin> termini;
        public ControllerLekar controllerLekar;

        public WindowTermini()
        {
            InitializeComponent();

            upravljanje = new PreglediService();
            termini = new ObservableCollection<Termin>(upravljanje.PregledSvihPregleda());
            controllerLekar = new ControllerLekar();
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
            if (dataGridLekar.SelectedIndex != -1)
            {
                ControllerLekar.getInstance().BrisanjeTermina(((Termin)dataGridLekar.SelectedItem), termini);
                MessageBox.Show("Uspesno ste obrisali termin!");
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
