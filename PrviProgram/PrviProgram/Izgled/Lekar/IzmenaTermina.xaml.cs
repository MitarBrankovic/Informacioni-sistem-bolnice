using Logika.LogikaLekar;
using Logika.LogikaPacijent;
using Model;
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

namespace PrviProgram.Izgled.Lekar
{
    /// <summary>
    /// Interaction logic for IzmenaTermina.xaml
    /// </summary>
    public partial class IzmenaTermina : Window
    {

        private UpravljanjePregledima upr;
        private ObservableCollection<Termin> termini;
        private Termin termin;

        public IzmenaTermina(ObservableCollection<Termin> termini, Termin termin)
        {
            InitializeComponent();

            upr = new UpravljanjePregledima();
            this.termini = termini;
            this.termin = termin;

            Pacijent.Text = termin.pacijent.Jmbg;
            Sifra.Text = termin.Sifra;
            Datum.Text = termin.Datum.ToString();
            Sala.Text = termin.sala.Sifra;

            String tip = termin.TipTermina.ToString();
            if (tip.Equals("Pregled"))
            {
                TipTerm.SelectedIndex = 0;
            }
            else if (tip.Equals("Operacija"))
            {
                TipTerm.SelectedIndex = 1;
            }
            else if (tip.Equals("Kontrola"))
            {
                TipTerm.SelectedIndex = 2;
            }
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            this.termini.Remove(this.termin);

            Sala tempSala = new Sala();
            tempSala.Sifra = Sifra.Text;
            this.termin.sala = tempSala;

            Model.Pacijent tempPacijent = new Model.Pacijent();
            tempPacijent.Jmbg = Pacijent.Text;
            this.termin.pacijent = tempPacijent;

            this.termin.Sifra = Sifra.Text;
            //this.termin.Datum = Datum.Text;

            String tip = TipTerm.Text;
            if (tip.Equals("Pregled"))
            {
                this.termin.TipTermina = TipTermina.Pregled;
            }
            else if (tip.Equals("Operacija"))
            {
                this.termin.TipTermina = TipTermina.Operacija;

            }
            else if (tip.Equals("Kontrola"))
            {
                this.termin.TipTermina = TipTermina.Kontrola;

            }

            upr.IzmenaPregleda(this.termin);
            this.termini.Add(this.termin);

            this.Close();
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
