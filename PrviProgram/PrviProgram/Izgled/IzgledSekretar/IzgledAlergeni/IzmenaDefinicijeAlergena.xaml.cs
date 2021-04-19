using System.Collections.ObjectModel;
using System.Windows;
using Model;
using Controller;

namespace PrviProgram.Izgled.IzgledSekretar.IzgledAlergeni
{
    public partial class IzmenaDefinicijeAlergena : Window
    {
        private SekretarController sekretarController = new SekretarController();
        private ObservableCollection<Alergen> alergeni;
        private Alergen alergen;
        public IzmenaDefinicijeAlergena(ObservableCollection<Alergen> alergeni, Alergen alergen)
        {
            InitializeComponent();
            this.alergeni = alergeni;
            this.alergen = alergen;
            textBoxAlergen.Text = alergen.Naziv;
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            Alergen alergen = new Alergen();
            alergen.Naziv = textBoxAlergen.Text;
            if (sekretarController.IzmenaAlergena(this.alergen, alergen) == true)
            {
                int index = this.alergeni.IndexOf(this.alergen);
                alergeni.Remove(this.alergen);
                alergeni.Insert(index, alergen);
            }
            this.Close();
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
