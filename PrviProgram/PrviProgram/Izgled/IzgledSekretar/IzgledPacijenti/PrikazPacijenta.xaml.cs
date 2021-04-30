using System.Collections.ObjectModel;
using System.Windows;
using Model;
using Service;
using Repository;

namespace PrviProgram.Izgled.IzgledSekretar.IzgledPacijenti
{
    public partial class PrikazPacijenta : Window
    {
        public PacijentiService pacijentiService = new PacijentiService();
        public PacijentRepository pacijentRepository = new PacijentRepository();
        public ObservableCollection<Model.Pacijent> pacijenti;

        public PrikazPacijenta()
        {
            InitializeComponent();
            pacijenti = new ObservableCollection<Model.Pacijent>(pacijentRepository.PregledSvihPacijenata());
            dgDataBinding.ItemsSource = pacijenti;
        }

        private void Izbrisi_Click(object sender, RoutedEventArgs e)
        {
            if ((Model.Pacijent)dgDataBinding.SelectedItem != null)
            {
                pacijentiService.BrisanjePacijenta((Model.Pacijent)dgDataBinding.SelectedItem);
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

        private void Zdravstveni_karton_Click(object sender, RoutedEventArgs e)
        {
            if (dgDataBinding.SelectedItem != null)
            {
                ZdravstveniKartonPacijenta zdravstveniKarton = new ZdravstveniKartonPacijenta((Pacijent)dgDataBinding.SelectedItem);
                zdravstveniKarton.Show();
            }
        }
    }
}
