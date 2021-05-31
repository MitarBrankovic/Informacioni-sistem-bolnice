using Controller;
using Model;
using Service;
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
    /// Interaction logic for PomeranjeZakazanogTermina.xaml
    /// </summary>
    public partial class PomeranjeZakazanogTermina : Window
    {
        public DateTime trenutniDatum = new DateTime();
        public DateTime datumTermina=new DateTime();

        public Lekar lekar = new Lekar();
        public Termin termin=new Termin();


        public DateTime trenutnoVreme = new DateTime();
        public DateTime vremeTermina = new DateTime();
        private ObservableCollection<Termin> termini;
        private Pacijent pacijent;
        private Termin noviTermin = new Termin();
        string[] nizVremena = { "08:00:00", "08:30:00", "09:00:00", "09:30:00", 
            "10:00:00", "10:30:00", "11:00:00", "11:30:00", "12:00:00", 
            "12:30:00", "13:00:00", "13:30:00", "14:00:00", "14:30:00", 
            "15:00:00", "15:30:00", "16:00:00", "16:30:00", "17:00:00", 
            "17:30:00", "18:00:00", "18:30:00", "19:00:00", "19:30:00" };
        public PacijentControler pacijentControler = new PacijentControler();


        public PomeranjeZakazanogTermina(Termin selektovaniTermin, Pacijent pacijent, ObservableCollection<Termin> termini)
        {
            InitializeComponent();

            this.datumTermina = selektovaniTermin.Datum;
            this.termini = termini;
            this.termin = selektovaniTermin;
            this.pacijent = pacijent;
            this.lekar.Ime = selektovaniTermin.lekar.Ime;
            this.lekar.Prezime = selektovaniTermin.lekar.Prezime;
            this.lekar.Jmbg = selektovaniTermin.lekar.Jmbg;
            DatumText.BlackoutDates.Add(new CalendarDateRange(DateTime.Today, datumTermina.AddDays(-3)));
            DatumText.BlackoutDates.Add(new CalendarDateRange(datumTermina,datumTermina));
            DatumText.BlackoutDates.Add(new CalendarDateRange(datumTermina.AddDays(3), DateTime.MaxValue));
            PotvrdiDugme.IsEnabled = false;
            vremeText.IsEnabled = false;
            ImeLekara.Text = selektovaniTermin.lekar.Ime;
            PrezimeLekara.Text = selektovaniTermin.lekar.Prezime;
            TipTerminaText.IsEnabled = false;
            String tip = selektovaniTermin.TipTermina.ToString();
            if (tip.Equals("Pregled"))
            {
                TipTerminaText.SelectedIndex = 0;
            }
            if (tip.Equals("Kontrola"))
            {
                TipTerminaText.SelectedIndex = 1;
            }

        }

        private void vremeText_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(vremeText.SelectedItem.Equals(""))
            {
                PotvrdiDugme.IsEnabled = false;
            }
            else
            {
                PotvrdiDugme.IsEnabled = true;
            }
        }
        public void brisanjeComboBoxova(int[] nizVremena)
        {
            int indexObrisanogVremena = 0;
            int brojacObrisanihVremena = 0;
            for (int iteratorPetlje = 0; iteratorPetlje < nizVremena.Length; iteratorPetlje++)
            {
                if (nizVremena[iteratorPetlje] == 1)
                {
                    indexObrisanogVremena = iteratorPetlje + brojacObrisanihVremena;
                    vremeText.Items.RemoveAt(indexObrisanogVremena);
                    brojacObrisanihVremena--;
                }
            }
        }
        
        private void DatumText_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if(DatumText.Text.Equals(""))
            {
                vremeText.IsEnabled = false;
            }
            else
            {
                int[] noviNizSlobodnihIZauzetihVremena = pacijentControler.ProveraZauzetostiLekara(this.lekar.Jmbg, (DateTime)DatumText.SelectedDate, nizVremena);
                brisanjeComboBoxova(noviNizSlobodnihIZauzetihVremena);
                if (vremeText.Items.IsEmpty)
                {
                    vremeText.IsEnabled = false;
                }
                else
                {
                    vremeText.IsEnabled = true;
                }
            }

        }

        //dugme potvrdi
        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            this.termini.Remove(this.termin);
            Termin noviTermin = new Termin ((DateTime)(DatumText.SelectedDate), (TipTermina)TipTerminaText.SelectedItem, termin.SifraTermina, termin.sala, vremeText.Text,lekar, pacijent);
            if (pacijentControler.IzmenaTermina(this.noviTermin))
            { 
                this.termini.Add(this.noviTermin);
            }
            this.Close();

        }

        private void Otkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
