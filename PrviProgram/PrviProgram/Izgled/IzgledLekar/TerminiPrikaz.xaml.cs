using Model;
using Service;
using Service.LekarService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrviProgram.Izgled.IzgledLekar
{
    /// <summary>
    /// Interaction logic for TerminiPrikaz.xaml
    /// </summary>
    public partial class TerminiPrikaz : UserControl
    {
        public PreglediService upravljanje;
        public ObservableCollection<Termin> termini;
        private TerminiService terminiService = new TerminiService();
        private IzvrseniPregled izvrseniPregled;
        private Lekar lekar;
        private StackPanel parent;
        private PocetniPrikaz pocetniPrikaz;
        public TerminiPrikaz(PocetniPrikaz pocetniPrikaz, StackPanel parent, Lekar lekar)
        {
            InitializeComponent();
            this.pocetniPrikaz = pocetniPrikaz;
            this.parent = parent;
            this.lekar = lekar;
            upravljanje = new PreglediService();
            termini = new ObservableCollection<Termin>(upravljanje.PregledSvihPregledaLekara(lekar));
            dataGridLekar.ItemsSource = termini;
            //termini = UpravljanjePregledima.getInstance().PregledSvihPregleda();
            //dataGridLekar.ItemsSource = termini;
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            DodavanjeTermina dodavanje = new DodavanjeTermina(termini, lekar);
            dodavanje.Show();
        }

        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {
            Termin termin = (Termin)dataGridLekar.SelectedItem;

            if (termin.izvrsen != true)
            {
                if (dataGridLekar.SelectedIndex != -1)
                {
                    IzmenaTermina izmena = new IzmenaTermina(termini, (Termin)dataGridLekar.SelectedItem);
                    izmena.Show();
                }
                else
                {
                    MessageBox.Show("Morate izabrati termin!");
                }
            }
            else
            {
                MessageBox.Show("Ovaj termin ste vec izvrsili!");
            }
        }

        private void Izbrisi_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridLekar.SelectedItem != null)
            {
                Termin termin = (Termin)dataGridLekar.SelectedItem;
                terminiService.BrisanjeTermina(termin);
                termini.Remove(termin);
                MessageBox.Show("Uspesno ste obrisali termin!");
            }
            else
            {
                MessageBox.Show("Morate izabrati termin!");
            }


        }
        private void Informacije_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridLekar.SelectedIndex != -1)
            {
                //InformacijePacijent info = new InformacijePacijent(termini, (Termin)dataGridLekar.SelectedItem);
                //info.Show();
                PacijentPrikaz pacijentPrikaz = new PacijentPrikaz(this, pocetniPrikaz, ((Termin)dataGridLekar.SelectedItem).pacijent);
                parent.Children.Remove(this);
                //this.Visibility = Visibility.Collapsed;
                parent.Children.Add(pacijentPrikaz);
            }
            else
            {
                MessageBox.Show("Morate izabrati termin!");

            }
        }


        private void Anamneza_Click(object sender, RoutedEventArgs e)
        {
            Termin termin = (Termin)dataGridLekar.SelectedItem;

            if (termin.izvrsen != true)
            {
                if (dataGridLekar.SelectedIndex != -1)
                {
                    ObservableCollection<IzvrseniPregled> izvrseniPregledi = new ObservableCollection<IzvrseniPregled>();
                    IzvrsavanjeAnamneze anamneza = new IzvrsavanjeAnamneze(izvrseniPregledi, izvrseniPregled, termin.pacijent, termini, termin);
                    anamneza.Show();
                }
                else
                {
                    MessageBox.Show("Morate izabrati termin!");
                }
            }
            else
            {
                MessageBox.Show("Ovaj termin ste vec izvrsili!");
            }
        }


        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


    }
}
