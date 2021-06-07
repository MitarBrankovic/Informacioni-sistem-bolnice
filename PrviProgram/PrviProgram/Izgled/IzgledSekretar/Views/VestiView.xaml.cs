using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Controller;
using Model;
using Repository;

namespace PrviProgram.Izgled.IzgledSekretar.Views
{
    public partial class VestiView : Page
    {
        private SekretarController sekretarController = new SekretarController();
        private VestiRepository vestiRepository = new VestiRepository();
        private ObservableCollection<Vest> vesti;
        private Sekretar sekretar;

        public VestiView(Sekretar sekretar)
        {
            InitializeComponent();
            this.sekretar = sekretar;
            vesti = new ObservableCollection<Vest>(vestiRepository.PregledSvihVesti());
            dgDataBinding.ItemsSource = vesti;
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            Page kreiranje = new KreiranjeVestiView(vesti, sekretar);
            NavigationService.Navigate(kreiranje);
        }

        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {
            if (dgDataBinding.SelectedItem != null)
            {
                Page izmena = new IzmenaVestiView(vesti, (Vest)dgDataBinding.SelectedItem);
                NavigationService.Navigate(izmena);
            }
        }

        private void Izbrisi_Click(object sender, RoutedEventArgs e)
        {
            if (dgDataBinding.SelectedItem != null)
            {
                Vest vest = (Vest)dgDataBinding.SelectedItem;
                sekretarController.BrisanjeVesti((Vest)dgDataBinding.SelectedItem);
                vesti.Remove(vest);
            }
        }

    }
}

