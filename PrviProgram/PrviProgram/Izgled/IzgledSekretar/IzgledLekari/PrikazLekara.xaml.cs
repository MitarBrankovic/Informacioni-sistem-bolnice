using System.Collections.ObjectModel;
using System.Windows;
using Controller;
using Model;
using Repository;

namespace PrviProgram.Izgled.IzgledSekretar.IzgledLekari
{
    public partial class PrikazLekara : Window
    {
        private SekretarController sekretarController = new SekretarController();
        private LekarRepository lekarRepository = new LekarRepository();
        private ObservableCollection<Lekar> lekari;

        public PrikazLekara()
        {
            InitializeComponent();
            lekari = new ObservableCollection<Lekar>(lekarRepository.PregledSvihLekara());
            dgDataBinding.ItemsSource = lekari;
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            DodavanjeLekara dodavanjeLekara = new DodavanjeLekara(lekari);
            dodavanjeLekara.Show();
        }

        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {
            if ((Lekar)dgDataBinding.SelectedItem != null)
            {
                IzmenaLekara izmenaLekara = new IzmenaLekara(lekari, (Lekar)dgDataBinding.SelectedItem);
                izmenaLekara.Show();
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
        private void Specijalizacije_Click(object sender, RoutedEventArgs e)
        {
            if ((Lekar)dgDataBinding.SelectedItem != null)
            {
                PregledSpecijalizacijeLekara pregledSpecijalizacijeLekara = new PregledSpecijalizacijeLekara((Lekar)dgDataBinding.SelectedItem);
                pregledSpecijalizacijeLekara.Show();
            }
        }

    }
}
