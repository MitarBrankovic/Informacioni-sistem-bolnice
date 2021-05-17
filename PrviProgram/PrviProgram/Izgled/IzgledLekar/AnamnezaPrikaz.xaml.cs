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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrviProgram.Izgled.IzgledLekar
{
    /// <summary>
    /// Interaction logic for AnamnezaPrikaz.xaml
    /// </summary>
    public partial class AnamnezaPrikaz : UserControl
    {
        private PocetniPrikaz pocetniPrikaz;
        private UserControl prethodniUserControl;
        public AnamnezaPrikaz(UserControl prethodniUserControl, PocetniPrikaz pocetniPrikaz)
        {
            InitializeComponent();
            this.pocetniPrikaz = pocetniPrikaz;
            pocetniPrikaz.DugmeZaPovratakNaPrethodnuStranicu(prethodniUserControl, pocetniPrikaz, this);
        }

        private void Zavrsi_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Karton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Uput_Click(object sender, RoutedEventArgs e)
        {

        }

        private void radioButtonRecpet_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void radioButtonTerapija_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
