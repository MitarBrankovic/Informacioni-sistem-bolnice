using System.Collections.ObjectModel;
using System.Windows;
using PrviProgram.Izgled.IzgledSekretar;
using Service.SekretarService;

namespace PrviProgram.Izgled.IzgledSekretar.IzgledPacijenti
{
    public partial class PrikazPacijenta : Window
    {
        public PacijentiService upravljanje;
        public ObservableCollection<Model.Pacijent> pacijenti;

        public PrikazPacijenta()
        {
            InitializeComponent();
            upravljanje = new PacijentiService();
            pacijenti = new ObservableCollection<Model.Pacijent>(upravljanje.PregledSvihPacijenata());
            dgDataBinding.ItemsSource = pacijenti;
        }

        private void Izbrisi_Click(object sender, RoutedEventArgs e)
        {
            if ((Model.Pacijent)dgDataBinding.SelectedItem != null)
            {
                upravljanje.BrisanjePacijenta((Model.Pacijent)dgDataBinding.SelectedItem);
                pacijenti.Remove((Model.Pacijent)dgDataBinding.SelectedItem);
            }
        }

        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {
            if ((Model.Pacijent)dgDataBinding.SelectedItem != null)
            {
                IzmenaPacijenta d = new IzmenaPacijenta(pacijenti, (Model.Pacijent)dgDataBinding.SelectedItem);
                d.Show();
            }
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            Registracija d = new Registracija(pacijenti);
            d.Show();
        }
    }
}
