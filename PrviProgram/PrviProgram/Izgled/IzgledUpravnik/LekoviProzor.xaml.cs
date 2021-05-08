using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Controller;
using Model;
using Repository;
using System.Collections.ObjectModel;

namespace PrviProgram.Izgled.IzgledUpravnik
{
    /// <summary>
    /// Interaction logic for LekoviProzor.xaml
    /// </summary>
    public partial class LekoviProzor : UserControl
    {
        private ObservableCollection<Lek> lekovi;
        private LekoviRepository lekoviRepository = new LekoviRepository();
        private UpravnikController upravnikController = new UpravnikController();

        public LekoviProzor()
        {
            InitializeComponent();
            lekovi = new ObservableCollection<Lek>(lekoviRepository.PregledSvihLekova());
            dataGridLekovi.ItemsSource = lekovi;
        }

        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as Grid).Children.Remove(this);
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
                LekoviIzmeni win = new LekoviIzmeni(lekovi, (Lek)dataGridLekovi.SelectedItem);
                win.Show();
            }
            else { MessageBox.Show("Morate izabrati lek!"); }
        }

        private void Izbrisi_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridLekovi.SelectedIndex != -1)
            {
                upravnikController.BrisanjeLeka((Lek)dataGridLekovi.SelectedItem);
                lekovi.Remove((Lek)dataGridLekovi.SelectedItem);
            }
            else { MessageBox.Show("Morate izabrati lek!"); }
        }

        private void Informacije_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridLekovi.SelectedIndex != -1)
            {
                Lek lek = (Lek)dataGridLekovi.SelectedItem;
                LekoviInformacije win = new LekoviInformacije(lekoviRepository.PregledLeka(lek.Sifra));
                win.Show();
            }
            else { MessageBox.Show("Morate izabrati lek!"); }
        }

        private void Neodobreni_Click(object sender, RoutedEventArgs e)
        {
            LekoviNeodobreni win = new LekoviNeodobreni();
            win.Show();
        }

        private void Pomoc_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Lekovi_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PodesavanjaNaloga_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Podesavanja_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Sale_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OpremaPrikaz_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DodajLek_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
