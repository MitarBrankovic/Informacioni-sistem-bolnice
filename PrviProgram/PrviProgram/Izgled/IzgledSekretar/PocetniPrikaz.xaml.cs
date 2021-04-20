using System.Windows;
using Model;
using PrviProgram.Izgled.IzgledSekretar.IzgledAlergeni;
using PrviProgram.Izgled.IzgledSekretar.IzgledPacijenti;
using PrviProgram.Izgled.IzgledSekretar.IzgledTermini;

namespace PrviProgram.Izgled.IzgledSekretar
{
    public partial class PocetniPrikaz : Window
    {
        public object PregledSvihTermin { get; private set; }

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

        private void Button_Click_Termini(object sender, RoutedEventArgs e)
        {
            PregledSvihTermina pregledSvihTermina = new PregledSvihTermina();
            pregledSvihTermina.Show();
        }
    }
}
