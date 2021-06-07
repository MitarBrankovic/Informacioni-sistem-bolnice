using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Model;
using Service;

namespace PrviProgram.Izgled.IzgledSekretar.Views
{
    public partial class TerminiZauzetiView : Page
    {
        private TerminiService terminiService = new TerminiService();
        private ObservableCollection<Termin> sviTermini;
        private ObservableCollection<Termin> termini;

        public TerminiZauzetiView(ObservableCollection<Termin> termini, Termin termin)
        {
            InitializeComponent();
            this.sviTermini = termini;
            this.termini = new ObservableCollection<Termin>(PregledSvihZauzetihTerminaZaLekaraOdVremena(termin));
            dgDataBinding.ItemsSource = this.termini;
        }

        public List<Termin> PregledSvihZauzetihTerminaZaLekaraOdVremena(Termin termin)
        {
            List<Termin> terminiZaLekara = new List<Termin>();
            string[] terminVreme = termin.Vreme.Split(":");
            foreach (Termin zauzetTermin in sviTermini)
            {
                string[] tVreme = zauzetTermin.Vreme.Split(":");
                if (zauzetTermin.lekar.Jmbg.Equals(termin.lekar.Jmbg)
                    && int.Parse(tVreme[0]) >= int.Parse(terminVreme[0])
                    && zauzetTermin.Datum.Date.Equals(termin.Datum.Date)
                    && !(int.Parse(terminVreme[1]) == 30 && int.Parse(tVreme[0]) == int.Parse(terminVreme[0]) && int.Parse(tVreme[1]) == 0))
                {
                    terminiZaLekara.Add(zauzetTermin);
                }
            }
            return terminiZaLekara;
        }

        private void PomeriTerminButton_Click(object sender, RoutedEventArgs e)
        {
            if (dgDataBinding.SelectedItem != null)
            {
                Termin termin = (Termin)dgDataBinding.SelectedItem;
                Page pomeranje = new PomeranjeTerminaView(sviTermini, termini, termin);
                NavigationService.Navigate(pomeranje);
            }
        }

        private void OtkaziTerminButton_Click(object sender, RoutedEventArgs e)
        {
            if (dgDataBinding.SelectedItem != null)
            {
                Termin termin = (Termin)dgDataBinding.SelectedItem;
                terminiService.BrisanjeTermina(termin);
                this.termini.Remove(termin);
                this.sviTermini.Remove(termin);
            }
        }
        
        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

    }
}
