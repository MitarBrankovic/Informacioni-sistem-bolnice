using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using Controller;
using Model;

namespace PrviProgram.Izgled.IzgledSekretar.IzgledPacijenti
{

    public partial class ZdravstveniKartonPacijenta : Window
    {
        private SekretarController sekretarController = new SekretarController();
        private ObservableCollection<Alergen> alergeni;
        private Pacijent pacijent;
        private KartonPacijenta kartonPacijenta;

        public ZdravstveniKartonPacijenta(Pacijent pacijent)
        {
            InitializeComponent();
            this.pacijent = pacijent;
            kartonPacijenta = pacijent.kartonPacijenta;
            alergeni = new ObservableCollection<Alergen>((IEnumerable<Alergen>)kartonPacijenta.GetAlergen());
            dgDataBinding.ItemsSource = alergeni;
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            DodajAlergen dodajAlergen = new DodajAlergen(alergeni);
            dodajAlergen.Show();
        }

        private void Obrisi_Click(object sender, RoutedEventArgs e)
        {
            if (dgDataBinding.SelectedItem != null)
            {
                alergeni.Remove((Alergen)dgDataBinding.SelectedItem);
            }
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            pacijent.kartonPacijenta.SetAlergen(new List<Alergen>(alergeni));
            sekretarController.IzmenaPacijenta(pacijent, pacijent);
            Close();
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
