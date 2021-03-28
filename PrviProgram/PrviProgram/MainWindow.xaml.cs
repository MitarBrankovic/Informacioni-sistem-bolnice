using Logika.LogikaSekretar;
using Model;
using PrviProgram.Izgled.Pacijent;
using RadSaDatotekama;
using System.Windows;

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WindowUpravnik win2 = new WindowUpravnik();
            DatotekaUpravnik a = new DatotekaUpravnik("output.json");
            //a.UpisivanjeUFajl(new Sala());
            UpravljanjePacijentima up = new UpravljanjePacijentima();
            Pacijent p = new Pacijent();
            p.Jmbg = "12234";
            p.kartonPacijenta = new KartonPacijenta();
            up.DodavanjePacijenta(p);
            win2.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Logovanje win = new Logovanje();
            win.Show();
        }
    }
}
