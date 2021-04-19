using System.Windows;
using Model;
using PrviProgram.Izgled.IzgledSekretar.IzgledAlergeni;
using PrviProgram.Izgled.IzgledSekretar.IzgledPacijenti;

namespace PrviProgram.Izgled.IzgledSekretar
{
    public partial class PocetniPrikaz : Window
    {
        public PocetniPrikaz(Sekretar sekretar)
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
