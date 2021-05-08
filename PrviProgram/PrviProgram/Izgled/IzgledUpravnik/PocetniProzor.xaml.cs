using System.Windows;
using Model;

namespace PrviProgram.Izgled.IzgledUpravnik
{
    public partial class PocetniProzor : Window
    {
        public PocetniProzor(Upravnik upravnik)
        {
            InitializeComponent();
        }

        private void Sale_Click(object sender, RoutedEventArgs e)
        {
            var s = new SaleProzor();
            gridMain.Children.Clear();
            gridMain.Children.Add(s);
        }

        private void IzlogujteSe_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Oprema_Click(object sender, RoutedEventArgs e)
        {
            var s = new OpremaProzor();
            gridMain.Children.Clear();
            gridMain.Children.Add(s);
        }

        private void Lekovi_Click(object sender, RoutedEventArgs e)
        {
            var s = new LekoviProzor();
            gridMain.Children.Clear();
            gridMain.Children.Add(s);
        }

        public void Ocisti()
        {
            gridMain.Children.Clear();
        }
    }
}
