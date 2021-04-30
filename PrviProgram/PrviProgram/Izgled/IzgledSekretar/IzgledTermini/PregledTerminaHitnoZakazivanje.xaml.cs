using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using Model;
using Service;

namespace PrviProgram.Izgled.IzgledSekretar.IzgledTermini
{
    public partial class PregledTerminaHitnoZakazivanje : Window
    {
        private TerminiService terminiService = new TerminiService();
        private ObservableCollection<Termin> sviTermini;
        private ObservableCollection<Termin> termini;
        private Termin termin;

        public PregledTerminaHitnoZakazivanje(ObservableCollection<Termin> termini, Termin termin)
        {
            InitializeComponent();
            this.termin = termin;
            this.sviTermini = termini;
            this.termini = InicijalizujTermine();
            dataGridPacijenta.ItemsSource = this.termini;
        }

        public ObservableCollection<Termin> InicijalizujTermine()
        {
            List<Termin> termini = new List<Termin>(PregledSvihZauzetihTerminaSpecijalizacijeLekara());
            return new ObservableCollection<Termin>(SortirajTerminePoVremenuPomeranja(termini));
        }

        public List<Termin> PregledSvihZauzetihTerminaSpecijalizacijeLekara()
        {
            List<Termin> terminiLekara = new List<Termin>();
            string[] terminVreme = termin.Vreme.Split(":");
            int terminSat = int.Parse(terminVreme[0]);
            int terminMinut = int.Parse(terminVreme[1]);
            foreach (Termin t in sviTermini)
            {
                string[] tVreme = t.Vreme.Split(":");
                int tSat = int.Parse(tVreme[0]);
                int tMinut = int.Parse(tVreme[1]);
                if (// Uslov za specijalizaciju lekara termin.lekar.spec == spec
                    tSat >= terminSat && t.Datum.ToString("d").Equals(termin.Datum.ToString("d"))
                    && !(terminMinut == 30 && tSat == terminSat && tMinut == 0))
                {
                    terminiLekara.Add(t);
                }
            }
            return terminiLekara;
        }

        public List<Termin> SortirajTerminePoVremenuPomeranja(List<Termin> termini)
        {
            return termini;
        }

        private void ZakaziTerminButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OtkaziTerminButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridPacijenta.SelectedItem != null)
            {
                Termin termin = (Termin)dataGridPacijenta.SelectedItem;
                terminiService.BrisanjeTermina(termin);
                this.termini.Remove(termin);
                this.sviTermini.Remove(termin);
            }
        }

        private void PomeriTerminButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridPacijenta.SelectedItem != null)
            {
                Termin termin = (Termin)dataGridPacijenta.SelectedItem;
                PomeranjeZauzetihTermina pomeranjeZauzetihTermina = new PomeranjeZauzetihTermina(sviTermini, termini, termin);
                pomeranjeZauzetihTermina.Show();
            }
        }

    }
}