using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PrviProgram.Izgled.IzgledLekar
{
    /// <summary>
    /// Interaction logic for WindowLekovi.xaml
    /// </summary>
    public partial class WindowLekovi : Window
    {
        private ObservableCollection<Lek> lekovi;
        private LekoviRepository lekoviRepository = new LekoviRepository();
        private Lekar lekar;
        public WindowLekovi(Lekar lekar)
        {
            InitializeComponent();
            this.lekar = lekar;
            lekovi = new ObservableCollection<Lek>(lekoviRepository.PregledSvihLekova());
            dataGridLekovi.ItemsSource = lekovi;
        }
        private void Informacije_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridLekovi.SelectedIndex != -1)
            {
                InformacijeLek informacijeLek = new InformacijeLek(lekovi, (Lek)dataGridLekovi.SelectedItem);
                informacijeLek.Show();
            }
            else
            {
                MessageBox.Show("Morate izabrati lek!");
            }
        }

        private void Primedba_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridLekovi.SelectedIndex != -1)
            {
                PrimedbaNaLekWindow primedbaNaLek = new PrimedbaNaLekWindow((Lek)dataGridLekovi.SelectedItem, lekar);
                primedbaNaLek.Show();
            }
            else
            {
                MessageBox.Show("Morate izabrati lek!");
            }
        }
    }
}
