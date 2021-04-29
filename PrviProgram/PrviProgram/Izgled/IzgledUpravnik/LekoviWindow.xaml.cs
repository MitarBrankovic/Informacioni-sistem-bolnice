using Model;
using Repository;
using Service;
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

namespace PrviProgram.Izgled.IzgledUpravnik
{
    public partial class LekoviWindow : Window
    {
        private ObservableCollection<Lek> lekovi;
        private LekoviRepository lekoviRepository;
        private LekoviService lekoviService;

        public LekoviWindow()
        {
            InitializeComponent();
            lekoviRepository = new LekoviRepository();
            lekoviService = new LekoviService();
            lekovi = new ObservableCollection<Lek>(lekoviRepository.PregledSvihLekova());
            dataGridLekovi.ItemsSource = lekovi;
        }

        private void Informacije_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridLekovi.SelectedIndex != -1)
            {
                Lek lekZaPregled = (Lek)dataGridLekovi.SelectedItem;
                LekoviInformacije win = new LekoviInformacije(lekZaPregled);
                win.Show();
            }
            else{ MessageBox.Show("Morate izabrati lek!"); }
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            LekoviDodaj win = new LekoviDodaj(lekovi);
            win.Show();
        }

        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridLekovi.SelectedIndex != -1)
            {
                Lek lekZaIzmenu = (Lek)dataGridLekovi.SelectedItem;
                LekoviIzmeni win = new LekoviIzmeni(lekovi, lekZaIzmenu);
                win.Show();
            }
            else{ MessageBox.Show("Morate izabrati lek!"); }
        }

        private void Izbrisi_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridLekovi.SelectedIndex != -1)
            {
                Lek lekZaBrisanje = (Lek)dataGridLekovi.SelectedItem;
                lekoviService.BrisanjeLeka(lekZaBrisanje);
                lekoviService.BrisanjeAlternativnihLekova(lekZaBrisanje);
                lekovi.Remove(lekZaBrisanje);
            }
            else { MessageBox.Show("Morate izabrati lek!"); }
        }
    }
}
