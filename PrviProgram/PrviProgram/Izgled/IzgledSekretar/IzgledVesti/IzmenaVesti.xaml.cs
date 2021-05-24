using System.Collections.ObjectModel;
using System.Windows;
using Controller;
using Model;

namespace PrviProgram.Izgled.IzgledSekretar.IzgledVesti
{
    public partial class IzmenaVesti : Window
    {
        private SekretarController sekretarController = new SekretarController();
        private ObservableCollection<Vest> vesti;
        private Vest vest;

        public IzmenaVesti(ObservableCollection<Vest> vesti, Vest vest)
        {
            InitializeComponent();
            this.vesti = vesti;
            this.vest = vest;
            textBoxNaslov.Text = vest.Naslov;
            textBoxText.Text = vest.Tekst;
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            Vest vest = new Vest(this.vest.Datum, textBoxText.Text, this.vest.SifraVesti, this.vest.Autor, textBoxNaslov.Text);
            if (sekretarController.IzmenaVesti(vest))
            {
                int indexVesti = vesti.IndexOf(this.vest);
                vesti.Remove(this.vest);
                vesti.Insert(indexVesti, vest);
                Close();
            }
        }

        private void Otkazi_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}