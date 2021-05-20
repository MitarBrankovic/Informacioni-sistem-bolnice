using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using Controller;
using Model;
using Service;

namespace PrviProgram.Izgled.IzgledSekretar.IzgledLekari
{
    public partial class PregledSpecijalizacijeLekara : Window
    {
        private SekretarController sekretarController = new SekretarController();
        private ObservableCollection<Specijalizacija> specijalizacije;
        private Lekar lekar;


        public PregledSpecijalizacijeLekara(Lekar lekar)
        {
            InitializeComponent();
            specijalizacije = new ObservableCollection<Specijalizacija>((IEnumerable<Specijalizacija>)lekar.GetSpecijalizacija());
            this.lekar = lekar;
            dgDataBinding.ItemsSource = specijalizacije;
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            DodavanjeSpecijalizacije dodavanjeSpecijalizacije = new DodavanjeSpecijalizacije(specijalizacije);
            dodavanjeSpecijalizacije.Show();
        }

        private void Obrisi_Click(object sender, RoutedEventArgs e)
        {
            if (dgDataBinding.SelectedItem != null)
            {
                specijalizacije.Remove((Specijalizacija)dgDataBinding.SelectedItem);
            }
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            lekar.SetSpecijalizacija(new List<Specijalizacija>(specijalizacije));
            sekretarController.IzmenaLekara(lekar, lekar);
            Close();
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}