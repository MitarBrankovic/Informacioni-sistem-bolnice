using Logika.LogikaLekar;
using Logika.LogikaPacijent;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    /// Interaction logic for IzmenaTermina.xaml
    /// </summary>
    public partial class IzmenaTermina : Window
    {
        private ObservableCollection<Termin> term;
        private Pacijent pacijent;
        private Termin t;
        private Termin noviTermin = new Termin();
        int index;
        string[] niz = { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00", "16:30", "17:00", "17:30", "18:00", "18:30", "19:00", "19:30" };


        public IzmenaTermina(Termin selektovaniTermin, Pacijent p, ObservableCollection<Termin> termini)
        {
            this.pacijent = p;
            this.term = termini;
            this.t = selektovaniTermin;

            InitializeComponent();

            if (selektovaniTermin.lekar != null)
            {
                if (selektovaniTermin.lekar.Ime != null && selektovaniTermin.lekar.Prezime != null)
                {
                    ImeLekara.Text = selektovaniTermin.lekar.Ime;
                    PrezimeLekara.Text = selektovaniTermin.lekar.Prezime;
                }
            }
            
            DatumText.SelectedDate = selektovaniTermin.Datum;
            var s = vremeText as ComboBox;
            //s.SelectedIndex = Convert.ToInt32(selektovaniTermin.Vreme);

            string v = selektovaniTermin.Vreme;



            for (int i = 0; i < niz.Length; i++)
            {
                if (niz[i].Equals(v))
                {
                    index = i;
                }
            }

            vremeText.SelectedIndex = index;

            String tip = selektovaniTermin.TipTermina.ToString();
            if (tip.Equals("Pregled"))
            {
                TipTerminaText.SelectedIndex = 0;
            }
            if (tip.Equals("Kontrola"))
            {
                TipTerminaText.SelectedIndex = 1;
            }

            //TipTerminaText.SelectedIndex = (int)(selektovaniTermin.TipTermina);
        }


        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            this.term.Remove(this.t);
            Model.Lekar l = new Model.Lekar();
            l.Ime = ImeLekara.Text;
            l.Prezime = PrezimeLekara.Text;
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
            if (UpravljanjeTerminima.getInstance().IzmenaTermina(this.noviTermin, pacijent) == true)
            {
                UpravljanjePregledima.getInstance().IzmenaPregleda(this.noviTermin);
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
