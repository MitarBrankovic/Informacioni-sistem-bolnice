using Logika.LogikaUpravnik;
using Model;
using PrviProgram.Izgled.Upravnik;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace PrviProgram
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class WindowUpravnik : Window
    {
        private static WindowUpravnik instance = null;
        public static WindowUpravnik getInstance()
        {
            if (instance == null)
            {
                instance = new WindowUpravnik();
            }
            return instance;
        }


        public UpravljanjeSalama upravljanje;
        public ObservableCollection<Model.Sala> sale; 



        public WindowUpravnik()
        {
            InitializeComponent();

            upravljanje = new UpravljanjeSalama();
            sale = new ObservableCollection<Model.Sala>(upravljanje.PregledSvihSala());


            dataGridUpravnik.ItemsSource = sale;

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            DodavanjeSale dodavanje = new DodavanjeSale(sale);
            dodavanje.Show();

        }

        private void Izbrisi_Click(object sender, RoutedEventArgs e)
        {
            upravljanje.BrisanjeSale((Model.Sala)dataGridUpravnik.SelectedItem);
            sale.Remove((Model.Sala)dataGridUpravnik.SelectedItem);
            MessageBox.Show("Uspesno ste obrisali salu!");
        }

        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {
            IzmenaSale izmena = new IzmenaSale(sale, (Model.Sala)dataGridUpravnik.SelectedItem);
            izmena.Show();
        }

    }
}
