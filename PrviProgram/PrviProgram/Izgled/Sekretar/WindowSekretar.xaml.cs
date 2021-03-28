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

namespace PrviProgram.Izgled.Sekretar
{
    public partial class WindowSekretar : Window
    {
        public WindowSekretar()
        {
            InitializeComponent();
        }

        private void Izbrisi_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            Registracija d = new Registracija();
            d.Show();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
