using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Model;
using Repository;

namespace PrviProgram.Izgled.IzgledSekretar.Views
{
    public partial class AlergeniView : Page
    {
        private AlergeniRepository alergeniRepository = new AlergeniRepository();
        private ObservableCollection<Alergen> alergeni;

        public AlergeniView()
        {
            InitializeComponent();
            alergeni = new ObservableCollection<Alergen>(alergeniRepository.PregledSvihAlergena());
            dgDataBinding.ItemsSource = alergeni;
            Delete.IsEnabled = false;
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            Alergen alergen = new Alergen(textBoxAlergen.Text);
            alergeni.Add(alergen);
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            alergeniRepository.UpisivanjeUFajl(new List<Alergen>(alergeni));
            NavigationService.GoBack();
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Izbrisi_Click(object sender, RoutedEventArgs e)
        {
            if (dgDataBinding.SelectedItem != null)
            {
                alergeni.Remove((Alergen)dgDataBinding.SelectedItem);
            }
        }

        private void DgDataBinding_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgDataBinding.SelectedItem != null)
            {
                Delete.IsEnabled = true;
                textBlockEditAleren.IsEnabled = true;
            }
            else
            {
                Delete.IsEnabled = false;
                textBlockEditAleren.IsEnabled = false;
            }
        }

    }
}
