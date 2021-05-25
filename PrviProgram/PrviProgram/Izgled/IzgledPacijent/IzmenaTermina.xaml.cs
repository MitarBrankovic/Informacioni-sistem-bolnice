using Controller;
using Model;
using PrviProgram.Service;
using Service;
using Service.LekarService;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace PrviProgram.Izgled.IzgledPacijent
{
    /// <summary>
    /// Interaction logic for IzmenaTermina.xaml
    /// </summary>
    public partial class IzmenaTermina : Window
    {
        private ObservableCollection<Termin> termini;
        private Pacijent pacijent;
        private Termin termin;
        private Termin noviTermin = new Termin();
        private int indexVremena;
        private string[] nizVremena = { "08:00:00", "08:30:00", "09:00:00", "09:30:00", "10:00:00", "10:30:00", "11:00:00", "11:30:00", "12:00:00", "12:30:00", "13:00:00", "13:30:00", "14:00:00", "14:30:00", "15:00:00", "15:30:00", "16:00:00", "16:30:00", "17:00:00", "17:30:00", "18:00:00", "18:30:00", "19:00:00", "19:30:00" };
        private PacijentControler pacijentControler = new PacijentControler();

        public IzmenaTermina(Termin selektovaniTermin, Pacijent pacijent, ObservableCollection<Termin> termini)
        {
            

            InitializeComponent();
            this.pacijent = pacijent;
            this.termini = termini;
            this.termin = selektovaniTermin;
            PrikazPodatakaTermina();

        }

        public void PrikazPodatakaTermina()
        {
            if (termin.lekar != null && termin.lekar.Ime != null && termin.lekar.Prezime != null)
            {
                ImeLekara.Text = termin.lekar.Ime;
                PrezimeLekara.Text = termin.lekar.Prezime;
            }
            DatumText.SelectedDate = termin.Datum;
            TrazenjeOdgovarajucegVremena();
            String tipTermina = termin.TipTermina.ToString();
            if (tipTermina.Equals("Pregled"))
            {
                TipTerminaText.SelectedIndex = 0;
            }
            if (tipTermina.Equals("Kontrola"))
            {
                TipTerminaText.SelectedIndex = 1;
            }
        }

        public void TrazenjeOdgovarajucegVremena()
        {
            string v = termin.Vreme;
            for (int i = 0; i < nizVremena.Length; i++)
            {
                if (nizVremena[i].Equals(v))
                {
                    indexVremena = i;
                    vremeText.SelectedIndex = indexVremena;
                    break;
                }
            }
        }
        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            this.termini.Remove(this.termin);
            Lekar lekar = new Lekar(ImeLekara.Text, PrezimeLekara.Text);
            Termin noviTermin = new Termin((DateTime)(DatumText.SelectedDate), (TipTermina)TipTerminaText.SelectedItem, termin.SifraTermina, termin.sala, vremeText.Text, lekar, pacijent);
            if (pacijentControler.IzmenaTermina(this.noviTermin))
            {
                this.termini.Add(this.noviTermin);
                pacijentControler.PovecavanjeBrojacaPriIzmeniTermina(pacijent);
            }
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
