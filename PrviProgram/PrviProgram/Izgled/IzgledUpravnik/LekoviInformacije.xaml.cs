using Model;
using Service;
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
using System.Windows.Shapes;

namespace PrviProgram.Izgled.IzgledUpravnik
{
    /// <summary>
    /// Interaction logic for LekoviInformacije.xaml
    /// </summary>
    public partial class LekoviInformacije : Window
    {
        private LekoviService lekoviService;
        private Lek lek;

        public LekoviInformacije(Lek lek)
        {
            InitializeComponent();
            lekoviService = new LekoviService();
            this.lek = lek;
            Naziv.Text = lek.Naziv;
            Sifra.Text = lek.Sifra;
            Opis.Text = lek.Opis;
            Sastojci.Text = lek.Sastojci;
            //alternativniListView.View = View.Details;
            alternativniListView.ItemsSource = lek.ZamenaZaLek;
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
