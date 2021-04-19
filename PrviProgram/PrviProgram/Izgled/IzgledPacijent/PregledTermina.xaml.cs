using Model;
using Service.LekarService;
using Service.PacijentService;
using System.Collections.ObjectModel;
using System.Windows;


namespace PrviProgram.Izgled.IzgledPacijent
{
    /// <summary>
    /// Interaction logic for PregledTermina.xaml
    /// </summary>
    public partial class PregledTermina : Window
    {
        //private static PregledTermina instance = null;
        //
        //public static PregledTermina getInstance(Pacijent p)
        //{
        //    if (instance == null)
        //    {
        //
        //        instance = new PregledTermina(p);
        //    }
        //    return instance;
        //}
        ObservableCollection<Termin> termini { get; set; }

        public PregledTermina(Pacijent p)
        {

            InitializeComponent();

            this.pacijent = p;
            termini = new ObservableCollection<Termin>(TerminiService.getInstance().PregledTermina(p));

            dataGridPacijenta.ItemsSource = termini;

        }
        private Pacijent pacijent;
        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            DodavanjeTermina win = new DodavanjeTermina(termini, pacijent);
            win.Show();
        }

        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridPacijenta.SelectedIndex != -1)
            {
                Termin t = (Termin)dataGridPacijenta.SelectedItem;
                var s = new IzmenaTermina(t, pacijent, termini);
                s.Show();
            }
        }

        private void Izbrisi_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridPacijenta.SelectedItem != null)
            {
                TerminiService.getInstance().BrisanjeTermina((Model.Termin)dataGridPacijenta.SelectedItem, pacijent);
                //PreglediService.getInstance().BrisanjePregleda(((Model.Termin)dataGridPacijenta.SelectedItem).SifraTermina);
                termini.Remove((Model.Termin)dataGridPacijenta.SelectedItem);
            }
            else
            {

                MessageBox.Show("Niste selektovali termin koji zelite da izbrisete!!");
            }

        }
    }


}