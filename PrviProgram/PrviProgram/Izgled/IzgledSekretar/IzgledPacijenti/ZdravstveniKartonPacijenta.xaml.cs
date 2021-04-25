using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using Controller;
using Model;
using Service;

namespace PrviProgram.Izgled.IzgledSekretar.IzgledPacijenti
{

    public partial class ZdravstveniKartonPacijenta : Window
    {
        private KartonPacijenta kartonPacijenta;
        private ObservableCollection<Alergen> alergeni;
        private Pacijent pacijent;
        private SekretarController sekretarController = new SekretarController();
        private PacijentiService pacijentiService = new PacijentiService();

        public ZdravstveniKartonPacijenta(Pacijent pacijent)
        {
            InitializeComponent();
            this.kartonPacijenta = pacijent.kartonPacijenta;
            this.alergeni = new ObservableCollection<Alergen>((System.Collections.Generic.IEnumerable<Alergen>)kartonPacijenta.GetAlergen());
            this.pacijent = pacijent;
            dgDataBinding.ItemsSource = alergeni;
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            DodajAlergen dodajAlergen = new DodajAlergen(this.alergeni);
            dodajAlergen.Show();
        }

        private void Obrisi_Click(object sender, RoutedEventArgs e)
        {
            if (dgDataBinding.SelectedItem != null)
            {
                this.alergeni.Remove((Alergen)dgDataBinding.SelectedItem);
            }
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            pacijent.kartonPacijenta.SetAlergen(new List<Alergen>(alergeni));
            //sekretarController.IzmenaPacijenta(this.pacijent, this.pacijent);
            pacijentiService.IzmenaPacijenta(this.pacijent, this.pacijent);
            // TODO: Resiti repository za kartone
            // TODO: Implementirati controler
            this.Close();
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
