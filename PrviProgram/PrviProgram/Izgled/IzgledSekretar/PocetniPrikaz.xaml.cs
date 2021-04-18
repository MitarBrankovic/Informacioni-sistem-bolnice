using System.Windows;
using PrviProgram.Izgled.IzgledSekretar.IzgledAlergeni;

namespace PrviProgram.Izgled.IzgledSekretar
{
    public partial class PocetniPrikaz : Window
    {
        public PocetniPrikaz()
        {
            InitializeComponent();
        }

        private void Button_Click_Pacijenti(object sender, RoutedEventArgs e)
        {
            PrikazPacijenta prikazPacijenta = new PrikazPacijenta();
            prikazPacijenta.Show();
        }

        private void Button_Click_Alergeni(object sender, RoutedEventArgs e)
        {
            PrikazAlergena prikazAlergena = new PrikazAlergena();
            prikazAlergena.Show();
        }
    }
}
