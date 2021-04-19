using System.Collections.Generic;
using System.Windows;
using Model;
using Repository;

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
