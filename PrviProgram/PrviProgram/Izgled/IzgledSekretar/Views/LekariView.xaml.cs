using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Controller;
using Model;
using Repository;

namespace PrviProgram.Izgled.IzgledSekretar.Views
{
    public partial class LekariView : Page
    {
        private SekretarController sekretarController = new SekretarController();
        private LekarRepository lekarRepository = new LekarRepository();
        private ObservableCollection<Lekar> lekari;

        public LekariView()
        {
            InitializeComponent();
            lekari = new ObservableCollection<Lekar>(lekarRepository.PregledSvihLekara());
            dgDataBinding.ItemsSource = lekari;
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            Page registracija = new RegistracijaLekaraView(lekari);
            NavigationService.Navigate(registracija);
        }

        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {
            if ((Lekar)dgDataBinding.SelectedItem != null)
            {
                Page izmena = new IzmenaLekaraView(lekari, (Lekar)dgDataBinding.SelectedItem);
                NavigationService.Navigate(izmena);
            }
        }

        private void Izbrisi_Click(object sender, RoutedEventArgs e)
        {
            if ((Lekar)dgDataBinding.SelectedItem != null)
            {
                sekretarController.BrisanjeLekara((Lekar)dgDataBinding.SelectedItem);
                lekari.Remove((Lekar)dgDataBinding.SelectedItem);
            }
        }

        private void Specijalizacija_Click(object sender, RoutedEventArgs e)
        {
            if ((Lekar)dgDataBinding.SelectedItem != null)
            {
                Page specijalizacija = new SpecijalizacijaView((Lekar)dgDataBinding.SelectedItem);
                NavigationService.Navigate(specijalizacija);
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            lekari.Clear();
            ObservableCollection<Lekar> pretrazeniLekari = new ObservableCollection<Lekar>(lekarRepository.PretragaLekara(TextboxSearch.Text));
            dgDataBinding.ItemsSource = pretrazeniLekari;
        }

    }
}

