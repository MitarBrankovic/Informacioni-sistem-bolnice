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
    public partial class LekoviInformacije : Window
    {
        private Lek lek;

        public LekoviInformacije(Lek lek)
        {
            InitializeComponent();
            this.lek = lek;
            PrikazPodataka();
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void PrikazPodataka()
        {
            Naziv.Text = this.lek.Naziv;
            Sifra.Text = this.lek.Sifra;
            Opis.Text = this.lek.Opis;
            Sastojci.Text = this.lek.Sastojci;
            alternativniListView.ItemsSource = this.lek.ZamenaZaLek;
        }
    }
}
