using Model;
using Repository;
using Service;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Tables;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PrviProgram.Izgled.IzgledUpravnik
{
    public partial class RadnoVremeIzvestaj : Window
    {
        private LekarRepository lekarRepository = new LekarRepository();
        //private IzvestajUpravnikService izvestajUpravnikService = new IzvestajUpravnikService();

        public RadnoVremeIzvestaj()
        {
            InitializeComponent();
            InicijalizacijaCombo();
        }

        private void InicijalizacijaCombo()
        {
            ComboLekar.ItemsSource = lekarRepository.PregledSvihLekara();
        }

        private void Konvertuj_Click(object sender, RoutedEventArgs e)
        {
            Lekar lekar = (Lekar)ComboLekar.SelectedItem;
            IzvestajAbstractService izvestajAbstractService = new IzvestajUpravnikService();
            izvestajAbstractService.IzgenerisiIzvestaj(lekar.Jmbg);
            //izvestajUpravnikService.IzgenerisiIzvestaj(lekar.Jmbg);
            this.Close();
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }
    }
}
