using Model;
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

namespace PrviProgram.Izgled.IzgledLekar
{
    /// <summary>
    /// Interaction logic for AlternativniLekWindow.xaml
    /// </summary>
    public partial class AlternativniLekWindow : Window
    {
        private Lek lek;
        AnamnezaPrikaz anamnezaPrikaz;
        public AlternativniLekWindow(AnamnezaPrikaz anamnezaPrikaz, Lek lek)
        {
            InitializeComponent();
            this.anamnezaPrikaz = anamnezaPrikaz;
            dataGridAlternativniLek.ItemsSource = lek.ZamenaZaLek;
            Potrvrdi.IsEnabled = false;
        }

        private void Potrvrdi_Click(object sender, RoutedEventArgs e)
        {
            anamnezaPrikaz.IzaberiZamenuZaLek((Lek)dataGridAlternativniLek.SelectedItem);
            this.Close();
        }

        private void Zatvori_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void dataGridAlternativniLek_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Potrvrdi.IsEnabled = true;
        }
    }
}
