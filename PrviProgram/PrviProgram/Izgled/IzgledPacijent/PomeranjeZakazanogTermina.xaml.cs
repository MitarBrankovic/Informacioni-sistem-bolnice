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
        public Lekar l = new Lekar();
        public Termin t;

        public DateTime trenutnoVreme = new DateTime();
        public DateTime vremeTermina = new DateTime();
        private ObservableCollection<Termin> term;
        private Pacijent pacijent;
        private Termin noviTermin = new Termin();
        string[] niz = { "08:00:00", "08:30:00", "09:00:00", "09:30:00", "10:00:00", "10:30:00", "11:00:00", "11:30:00", "12:00:00", "12:30:00", "13:00:00", "13:30:00", "14:00:00", "14:30:00", "15:00:00", "15:30:00", "16:00:00", "16:30:00", "17:00:00", "17:30:00", "18:00:00", "18:30:00", "19:00:00", "19:30:00" };


        public PomeranjeZakazanogTermina(Termin selektovaniTermin, Pacijent p, ObservableCollection<Termin> termini)
        {
            InitializeComponent();

            this.datumTermina = selektovaniTermin.Datum;
            this.term = termini;
            this.t = selektovaniTermin;
            this.pacijent = p;
            this.l.Ime = selektovaniTermin.lekar.Ime;
            this.l.Prezime = selektovaniTermin.lekar.Prezime;
            this.l.Jmbg = selektovaniTermin.lekar.Jmbg;
            DatumText.BlackoutDates.Add(new CalendarDateRange(DateTime.Today, datumTermina.AddDays(-3)));
            DatumText.BlackoutDates.Add(new CalendarDateRange(datumTermina,datumTermina));
            DatumText.BlackoutDates.Add(new CalendarDateRange(datumTermina.AddDays(3), DateTime.MaxValue));
            // vremeText.Items.RemoveAt(0);
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
        public void brisanjeComboBoxova(int[] niz)
        {
            int j = 0;
            int k = 0;
            for (int i = 0; i < niz.Length; i++)
            {

                if (niz[i] == 1)
                {
                    j = i + k;
                    vremeText.Items.RemoveAt(j);

                    k--;

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
                string jmbg = this.l.Jmbg;
                int[] noviNiz = (int[])TerminiService.getInstance().ProveraZauzetostiLekara(jmbg, (DateTime)DatumText.SelectedDate, niz);
                brisanjeComboBoxova(noviNiz);
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
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.term.Remove(this.t);
            
            this.noviTermin.lekar = l;
            this.noviTermin.Datum = (DateTime)(DatumText.SelectedDate);
            this.noviTermin.Vreme = vremeText.Text;
            noviTermin.SifraTermina = t.SifraTermina;
            noviTermin.sala = t.sala;

            this.noviTermin.pacijent = this.pacijent;

            String tip = TipTerminaText.Text;
            if (tip.Equals("Pregled"))
            {
                this.noviTermin.TipTermina = TipTermina.Pregled;
            }
            if (tip.Equals("Kontrola"))
            {
                this.noviTermin.TipTermina = TipTermina.Kontrola;
            }
            if (TerminiService.getInstance().IzmenaTermina(this.noviTermin) == true)
            {
                //  PreglediService.getInstance().IzmenaPregleda(this.noviTermin);
                this.term.Add(this.noviTermin);
            }
            this.Close();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
