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
            Alergen alergen = new Alergen(textBoxAlergen.Text);
            if (sekretarController.DodavanjeAlergena(alergen))
            {
                alergeni.Add(alergen);
            }
            Close();
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
