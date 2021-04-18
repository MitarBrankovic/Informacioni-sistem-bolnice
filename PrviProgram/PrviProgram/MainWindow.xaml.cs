using Model;
using PrviProgram.Izgled.IzgledPacijent;
using PrviProgram.Izgled.IzgledUpravnik;
using PrviProgram.Izgled.Lekar;
using PrviProgram.Izgled.Sekretar;
using PrviProgram.Repository;
using Service.LekarService;
using Service.SekretarService;
using Service.UpravnikService;
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

        //Dugme Upravnik
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PocetniProzor win2 = new PocetniProzor();
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
            LekarRepository lek = new LekarRepository();
            PreglediService up = new PreglediService();
            Termin t = new Termin();
            t.SifraTermina = "asda2";

            PreglediService upr = new PreglediService();
            //upr.DodavanjePregleda(t);

            winTermini.Show();
        }
    }
}
