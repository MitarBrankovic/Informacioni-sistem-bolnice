using System.Collections.ObjectModel;
using System.Windows;
using Controller;
using Model;
using Repository;

namespace PrviProgram.Izgled.IzgledSekretar.IzgledAlergeni
{
    public partial class PrikazAlergena : Window
    {
        private SekretarController sekretarController = new SekretarController();
        private AlergeniRepository alergeniRepository = new AlergeniRepository();
        private ObservableCollection<Alergen> alergeni;

        public PrikazAlergena()
        {
            InitializeComponent();
            alergeni = new ObservableCollection<Alergen>(alergeniRepository.PregledSvihAlergena());
            dgDataBinding.ItemsSource = alergeni;
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            DefinisanjeAlergena definisanjeAlergena = new DefinisanjeAlergena(alergeni);
            definisanjeAlergena.Show();
        }

        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {
            if ((Alergen)dgDataBinding.SelectedItem != null)
            {
                IzmenaDefinicijeAlergena izmenaDefinicijeAlergena = new IzmenaDefinicijeAlergena(alergeni, (Alergen)dgDataBinding.SelectedItem);
                izmenaDefinicijeAlergena.Show();
            }
        }

        private void Izbrisi_Click(object sender, RoutedEventArgs e)
        {
            sekretarController.BrisanjeAlergena((Alergen)dgDataBinding.SelectedItem);
            alergeni.Remove((Alergen)dgDataBinding.SelectedItem);
        }

    }
}
