using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Controller;
using Model;
using Repository;


namespace PrviProgram.Izgled.IzgledSekretar.Views
{
    public partial class PacijentiView : Page
    {
        private SekretarController sekretarController = new SekretarController();
        public PacijentRepository pacijentRepository = new PacijentRepository();
        public ObservableCollection<Pacijent> pacijenti;

        public PacijentiView()
        {
            InitializeComponent();
            pacijenti = new ObservableCollection<Pacijent>(pacijentRepository.PregledSvihPacijenata());
            dgDataBinding.ItemsSource = pacijenti;
        }

        private void Izbrisi_Click(object sender, RoutedEventArgs e)
        {
            if ((Pacijent)dgDataBinding.SelectedItem != null)
            {
                sekretarController.BrisanjePacijenta((Pacijent)dgDataBinding.SelectedItem);
                pacijenti.Remove((Pacijent)dgDataBinding.SelectedItem);
            }
        }

        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {
            if ((Pacijent)dgDataBinding.SelectedItem != null)
            {
                Page izmena = new IzmenaPacijentaView(pacijenti, (Pacijent)dgDataBinding.SelectedItem);
                NavigationService.Navigate(izmena);
            }
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            Page registracija = new RegistracijaPacijentaView(pacijenti);
            NavigationService.Navigate(registracija);
            
        }

        private void Zdravstveni_karton_Click(object sender, RoutedEventArgs e)
        {
            if (dgDataBinding.SelectedItem != null)
            {
                Page karton = new KartonView((Pacijent)dgDataBinding.SelectedItem);
                NavigationService.Navigate(karton);
            }
        }

        private void TextBox_TextChanged(object sender, RoutedEventArgs e)
        {
            pacijenti.Clear();
            ObservableCollection<Pacijent> pretrazeniPacijenti = new ObservableCollection<Pacijent>(pacijentRepository.PretragaPacijent(TextboxSearch.Text));
            dgDataBinding.ItemsSource = pretrazeniPacijenti;
        }

    }
}
