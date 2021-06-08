using System.Windows;
using System.Windows.Controls;
using Service;

namespace PrviProgram.Izgled.IzgledSekretar.Views
{
    public partial class IzvestajView : Page
    {
        private IzvestajAbstractService izvestajService;

        public IzvestajView(IzvestajAbstractService izvestajService)
        {
            InitializeComponent();
            this.izvestajService = izvestajService;
        }

        private void ButtonIzvestaj_Click(object sender, RoutedEventArgs e)
        {
            izvestajService.IzgenerisiIzvestaj("");
            MessageBox.Show("Uspesno ste kreirali izvestaj.");
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

    }
}
