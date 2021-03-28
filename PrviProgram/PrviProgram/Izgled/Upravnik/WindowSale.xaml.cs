using Logika.LogikaUpravnik;
using Model;
using PrviProgram.Izgled.Upravnik;
using System.Collections.Generic;
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




        List<Sala> sale = new List<Sala>();


        public WindowUpravnik()
        {
            InitializeComponent();


            sale = UpravljanjeSalama.getInstance().PregledSvihSala();


            dataGridUpravnik.ItemsSource = sale;
            /*foreach (Sala sa in sale) {
                dataGridUpravnik.Items.Add(sa);
            }*/

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            DodavanjeSale dodavanje = new DodavanjeSale();
            dataGridUpravnik.Items.Refresh();
            dodavanje.Show();

        }

        private void Izbrisi_Click(object sender, RoutedEventArgs e)
        {
            //int trenutniRed = dataGridUpravnik.SelectedIndex;
            //string oznaceniProfesor = (string)dataGridUpravnik.GetV(trenutniRed, 0);
            //UpravljanjeSalama.getInstance().BrisanjeSale(dataGridUpravnik.SelectedValuePath);
            MessageBox.Show("Uspesno ste obrisali salu!");
        }

        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
