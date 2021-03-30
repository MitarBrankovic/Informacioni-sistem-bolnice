using Logika.LogikaLekar;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using RadSaDatotekama;

namespace PrviProgram.Izgled.Lekar
{
    /// <summary>
    /// Interaction logic for DodavanjeTermina.xaml
    /// </summary>
    public partial class DodavanjeTermina : Window
    {

        private UpravljanjePregledima upr;
        private ObservableCollection<Termin> termini;

        public DodavanjeTermina(ObservableCollection<Termin> termini)
        {
            InitializeComponent();

            upr = new UpravljanjePregledima();
            this.termini = termini;
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            Termin tempTermin = new Termin();
            tempTermin.Sifra = Sifra.Text;
            //tempTermin.sala = PregledSale(Sala.Text);

            /*DatotekaSala datoteka = new DatotekaSala();
            List<Sala> sale = datoteka.CitanjeIzFajla();
            foreach (Sala s in sale)
            {
                if (s.Sifra.Equals(sala.Sifra))
                {
                    return false;
                }
            }*/

            Sala tempSala = new Sala();
            tempSala.Sifra = Sifra.Text;
            tempTermin.sala = tempSala;

            Model.Pacijent tempPacijent = new Model.Pacijent();
            tempPacijent.Jmbg = Pacijent.Text;
            tempTermin.pacijent = tempPacijent;

            String tip = TipTerm.Text;
            if (tip.Equals("Pregled"))
            {
                tempTermin.TipTermina = TipTermina.Pregled;
            }
            else if (tip.Equals("Operacija"))
            {
                tempTermin.TipTermina = TipTermina.Operacija;
            }
            else if (tip.Equals("Kontrola"))
            {
                tempTermin.TipTermina = TipTermina.Kontrola;
            }

            UpravljanjePregledima.getInstance().DodavanjePregleda(tempTermin);
            this.termini.Add(tempTermin);
            
            this.Close();
        }

        private Sala PregledSale(object sala)
        {
            throw new NotImplementedException();
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
