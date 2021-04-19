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
            WindowUpravnik win = new WindowUpravnik();
            win.Show();
        }

        private void IzlogujteSe_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
