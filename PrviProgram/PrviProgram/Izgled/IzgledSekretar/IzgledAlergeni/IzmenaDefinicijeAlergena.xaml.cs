using System.Collections.ObjectModel;
using System.Windows;
using Controller;
using Model;

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
            Alergen alergen = new Alergen(textBoxAlergen.Text);
            if (sekretarController.IzmenaAlergena(this.alergen, alergen))
            {
                int index = alergeni.IndexOf(this.alergen);
                alergeni.Remove(this.alergen);
                alergeni.Insert(index, alergen);
            }
            Close();
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
