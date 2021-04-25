using System.Collections.ObjectModel;
using System.Windows;
using Model;
using PrviProgram.Repository;
using Service;

namespace PrviProgram.Izgled.IzgledSekretar.IzgledTermini
{
    public partial class PregledSvihTermina : Window
    {
        private TerminiRepository terminiRepository = new TerminiRepository();
        private TerminiService terminiService = new TerminiService();
        private ObservableCollection<Termin> termini;

        public PregledSvihTermina()
        {
            InitializeComponent();
            termini = new ObservableCollection<Termin>(terminiRepository.CitanjeIzFajla());
            dataGridPacijenta.ItemsSource = termini;
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            ZakazivanjeTermina zakazivanjeTermina = new ZakazivanjeTermina(termini);
            zakazivanjeTermina.Show();
        }

        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridPacijenta.SelectedItem != null)
            {
                Termin termin = (Termin)dataGridPacijenta.SelectedItem;
                IzmenaTermina izmenaTermina = new IzmenaTermina(termini, termin);
                izmenaTermina.Show();
            }
        }

        private void Izbrisi_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridPacijenta.SelectedItem != null)
            {
                Termin termin = (Termin)dataGridPacijenta.SelectedItem;
                terminiService.BrisanjeTermina(termin);
                termini.Remove(termin);
            }
        }
    }
}
