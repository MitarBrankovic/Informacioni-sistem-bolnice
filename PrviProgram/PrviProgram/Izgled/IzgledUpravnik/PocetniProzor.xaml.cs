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
            this.Close();
        }

        private void Oprema_Click(object sender, RoutedEventArgs e)
        {
            CelaOpremaWindow win = new CelaOpremaWindow();
            win.Show();
        }

        private void Lekovi_Click(object sender, RoutedEventArgs e)
        {
            LekoviWindow win = new LekoviWindow();
            win.Show();
        }
    }
}
