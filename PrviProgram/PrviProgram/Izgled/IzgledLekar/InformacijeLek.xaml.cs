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
    /// Interaction logic for InformacijeLek.xaml
    /// </summary>
    public partial class InformacijeLek : Window
    {
        public InformacijeLek(ObservableCollection<Lek> lekovi, Lek lek)
        {
            InitializeComponent();
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
