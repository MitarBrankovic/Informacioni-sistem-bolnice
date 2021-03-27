using Logika.LogikaSekretar;
using Model;
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
            Window1 win2 = new Window1();
            DatotekaUpravnik a = new DatotekaUpravnik("output.json");
            a.UpisivanjeUFajl(new Sala());
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
    }
}
