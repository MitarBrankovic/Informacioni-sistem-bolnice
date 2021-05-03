using System.Windows;
using Model;
using PrviProgram.Izgled.IzgledSekretar.IzgledAlergeni;
using PrviProgram.Izgled.IzgledSekretar.IzgledPacijenti;
using PrviProgram.Izgled.IzgledSekretar.IzgledTermini;
using PrviProgram.Izgled.IzgledSekretar.IzgledVesti;

namespace PrviProgram.Izgled.IzgledSekretar
{
    public partial class PocetniPrikaz : Window
    {
        private Sekretar sekretar;

        public PocetniPrikaz(Sekretar sekretar)
        {
            InitializeComponent();
            this.sekretar = sekretar;
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

        private void Button_Click_Vesti(object sender, RoutedEventArgs e)
        {
            PregledSvihVesti pregledSvihVesti = new PregledSvihVesti(sekretar);
            pregledSvihVesti.Show();
        }
    }
}
