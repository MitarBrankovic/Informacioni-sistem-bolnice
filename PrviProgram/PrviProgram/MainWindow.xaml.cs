using System.Windows;
using PrviProgram.Izgled.IzgledPacijent;
using PrviProgram.Izgled.IzgledSekretar;
using PrviProgram.Izgled.Lekar;

namespace PrviProgram
{
    public partial class MainWindow : Window
    {
        private void Prijavi_se_Click(object sender, RoutedEventArgs e)
        {
            LogovanjePacijenta win = LogovanjePacijenta.getInstance();
            win.Show();
        
        }
    }
}
