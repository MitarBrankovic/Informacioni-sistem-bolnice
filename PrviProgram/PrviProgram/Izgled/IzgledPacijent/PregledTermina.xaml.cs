using Logika.LogikaLekar;
using Logika.LogikaPacijent;
using Model;
using RadSaDatotekama;
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
using System.Windows.Shapes;


namespace PrviProgram.Izgled.IzgledPacijent
{
    /// <summary>
    /// Interaction logic for PregledTermina.xaml
    /// </summary>
    public partial class PregledTermina : Window
    {
        private static PregledTermina instance = null;

        public static PregledTermina getInstance(Pacijent p)
        {
            if (instance == null)
            {

                instance = new PregledTermina(p);
            }
            return instance;
        }
        ObservableCollection<Termin> termini { get; set; }

        public PregledTermina(Pacijent p)
        {

            InitializeComponent();

            this.pacijent = p;
            termini = new ObservableCollection<Termin>(UpravljanjeTerminima.getInstance().PregledTermina(p));

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
                UpravljanjeTerminima.getInstance().BrisanjeTermina((Model.Termin)dataGridPacijenta.SelectedItem, pacijent);
                UpravljanjePregledima.getInstance().BrisanjePregleda(((Model.Termin)dataGridPacijenta.SelectedItem).SifraTermina);
                termini.Remove((Model.Termin)dataGridPacijenta.SelectedItem);
            }
            else
            {

                MessageBox.Show("Niste selektovali termin koji zelite da izbrisete!!");
            }

        }
    }


}