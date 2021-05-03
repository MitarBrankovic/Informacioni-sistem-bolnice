using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PrviProgram.Izgled.IzgledLekar
{
    /// <summary>
    /// Interaction logic for PocetniPrikaz.xaml
    /// </summary>
    public partial class PocetniPrikaz : Window
    {
        private Lekar lekar;
        public PocetniPrikaz(Lekar lekar)
        {
            InitializeComponent();
            this.lekar = lekar;
        }

        private void Raspored_Click(object sender, RoutedEventArgs e)
        {
            WindowTermini win = new WindowTermini(lekar);
            win.Show();
        }

        private void PregledLekova_Click(object sender, RoutedEventArgs e)
        {
            WindowLekovi win = new WindowLekovi(lekar);
            win.Show();
        }
    }
}
