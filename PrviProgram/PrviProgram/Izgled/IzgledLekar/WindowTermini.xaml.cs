using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Logika.LogikaLekar;
using Logika.LogikaPacijent;
using Model;
using PrviProgram.Izgled.Lekar;
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
        public UpravljanjePregledima upravljanje;
        public ObservableCollection<Termin> termini;


        public WindowTermini()
        {
            InitializeComponent();

            upravljanje = new UpravljanjePregledima();
            termini = new ObservableCollection<Termin>(upravljanje.PregledSvihPregleda());

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
                upravljanje.BrisanjePregleda(((Termin)dataGridLekar.SelectedItem).SifraTermina);
                UpravljanjeTerminima.getInstance().BrisanjeTermina((Model.Termin)dataGridLekar.SelectedItem, ((Model.Termin)dataGridLekar.SelectedItem).pacijent);
                //UpravljanjePregledima.getInstance().BrisanjePregleda(((Model.Termin)dataGridLekar.SelectedItem).SifraTermina);

                termini.Remove((Termin)dataGridLekar.SelectedItem);
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
