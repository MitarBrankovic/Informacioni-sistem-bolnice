using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Model;
using PrviProgram.Repository;
using Service;

namespace PrviProgram.Izgled.IzgledSekretar.Views
{
    public partial class TerminiView : Page
    {
        private TerminiRepository terminiRepository = new TerminiRepository();
        private TerminiService terminiService = new TerminiService();
        private ObservableCollection<Termin> termini;

        public TerminiView()
        {
            InitializeComponent();
            termini = new ObservableCollection<Termin>(terminiRepository.CitanjeIzFajla());
            dgDataBinding.ItemsSource = termini;
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            Page zakazivanje = new ZakazivanjeTerminaView(termini);
            NavigationService.Navigate(zakazivanje);
        }

        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {
            if (dgDataBinding.SelectedItem != null)
            {
                Page izmena = new IzmenaTerminaView(termini, (Termin)dgDataBinding.SelectedItem);
                NavigationService.Navigate(izmena);
            }
        }

        private void Izbrisi_Click(object sender, RoutedEventArgs e)
        {
            if (dgDataBinding.SelectedItem != null)
            {
                Termin termin = (Termin)dgDataBinding.SelectedItem;
                terminiService.BrisanjeTermina(termin);
                termini.Remove(termin);
            }
        }

    }
}
