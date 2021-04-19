using System.Collections.ObjectModel;
using System.Windows;
using Controller;
using Model;

namespace PrviProgram.Izgled.IzgledSekretar.IzgledAlergeni
{

    public partial class DefinisanjeAlergena : Window
    {
        private SekretarController sekretarController = new SekretarController();
        private ObservableCollection<Alergen> alergeni;
        public DefinisanjeAlergena(ObservableCollection<Alergen> alergeni)
        {
            InitializeComponent();
            this.alergeni = alergeni;
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            Alergen alergen = new Alergen();
            alergen.Naziv = textBoxAlergen.Text;
            if (sekretarController.DodavanjeAlergena(alergen) == true)
            {
                this.alergeni.Add(alergen);
            }
            this.Close();
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
