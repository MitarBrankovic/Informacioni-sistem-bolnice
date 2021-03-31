using Logika.LogikaLekar;
using Logika.LogikaSekretar;
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
using Logika.LogikaUpravnik;
using Logika.LogikaPacijent;
//using Logika.LogikaSekretar;

namespace PrviProgram.Izgled.Lekar
{
    /// <summary>
    /// Interaction logic for DodavanjeTermina.xaml
    /// </summary>
    public partial class DodavanjeTermina : Window
    {

        private UpravljanjePregledima upr;
        private UpravljanjePacijentima uprPac;
        private UpravljanjeSalama uprSal;
        private ObservableCollection<Termin> termini;

        public DodavanjeTermina(ObservableCollection<Termin> termini)
        {
            InitializeComponent();

            upr = new UpravljanjePregledima();
            uprPac = new UpravljanjePacijentima();
            uprSal = new UpravljanjeSalama();
            this.termini = termini;

        }

        bool datum = false;
        bool vreme = false;

        private void datum_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var datum1 = sender as DatePicker;

            if (datum1 == null)
            {
                datum = false;
            }
            else
            {
                datum = true;
            }

        }

        private void vreme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var vreme1 = sender as ComboBox;
            if (vreme1 == null)
            {
                vreme = false;
            }
            else
            {
                vreme = true;
            }

        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            Termin tempTermin = new Termin();
            tempTermin.SifraTermina = Sifra.Text;
            

            Sala tempSala = new Sala();
            tempSala = uprSal.PregledSale(Sala.Text);
            tempTermin.sala = tempSala;
            

            Model.Pacijent tempPacijent = new Model.Pacijent();
            tempPacijent = uprPac.PregledPacijenta(Pacijent.Text);
            tempTermin.pacijent = tempPacijent;

            tempTermin.Vreme = vremeText.Text;
            tempTermin.Datum = (DateTime)(DatumText.SelectedDate);

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
            UpravljanjeTerminima.getInstance().DodavanjeTermina(tempTermin, tempPacijent);

            this.termini.Add(tempTermin);
            
            this.Close();
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
