using System.Windows;

namespace PrviProgram
{
    public partial class MainWindow : Window
    {
        private void Prijavi_se_Click(object sender, RoutedEventArgs e)
        {
            Logovanje win = new Logovanje();
            win.Show();
        }
    }
}
