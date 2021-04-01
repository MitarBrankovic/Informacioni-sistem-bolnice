using Logika.LogikaSekretar;
using Logika.LogikaLekar;
using PrviProgram.Izgled.Lekar;
using Logika.LogikaUpravnik;
using Model;

using PrviProgram.Izgled.Sekretar;
using RadSaDatotekama;
using System.Windows;
using PrviProgram.Izgled.IzgledPacijent;

namespace PrviProgram
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //Dugme Upravnik
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WindowUpravnik win2 = new WindowUpravnik();
            DatotekaUpravnik a = new DatotekaUpravnik("output.json");
            //a.UpisivanjeUFajl(new Sala());
            UpravljanjePacijentima up = new UpravljanjePacijentima();
            Pacijent p = new Pacijent();
            p.Jmbg = "12234";
            p.kartonPacijenta = new KartonPacijenta();
            //up.DodavanjePacijenta(p);

            UpravljanjeSalama upravlj = new UpravljanjeSalama();
            Sala s = new Sala();
            s.Sifra = "12234";
            //upravlj.DodavanjeSale(s);

            win2.Show();
        }

        //Dugme Sekretar
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            WindowSekretar win = new WindowSekretar();
            win.Show();
        }

        //Dugme Pacijent
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            LogovanjePacijenta win = new LogovanjePacijenta();
            win.Show();
        }

        //Dugme Lekar
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            WindowTermini winTermini = new WindowTermini();
            DatotekaLekar lek = new DatotekaLekar();
            UpravljanjePregledima up = new UpravljanjePregledima();
            Termin t = new Termin();
            t.SifraTermina = "asda2";

            UpravljanjePregledima upr = new UpravljanjePregledima();
            //upr.DodavanjePregleda(t);

            winTermini.Show();
        }
    }
}
