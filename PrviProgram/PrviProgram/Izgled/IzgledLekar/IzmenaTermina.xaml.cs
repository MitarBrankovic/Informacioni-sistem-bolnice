using Logika.LogikaLekar;
using Logika.LogikaPacijent;
using Logika.LogikaSekretar;
using Logika.LogikaUpravnik;
using Model;
using PrviProgram.Logika.Controllers;
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

        private UpravljanjePacijentima UpravljanjePacijentima;
        private UpravljanjeSalama UpravljanjeSalama;
        private UpravljanjeTerminima UpravljanjeTerminima;
        private UpravljanjePregledima UpravljanjePregledima;
        private ObservableCollection<Termin> termini;

        private ObservableCollection<Termin> terminiPacijent;
        private Termin termin;
        int index;
        string[] niz = { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00", "16:30", "17:00", "17:30", "18:00", "18:30", "19:00", "19:30" };


        public IzmenaTermina(ObservableCollection<Termin> termini, Termin termin)
        {
            InitializeComponent();

            UpravljanjePacijentima = new UpravljanjePacijentima();
            UpravljanjeSalama = new UpravljanjeSalama();
            UpravljanjeTerminima = new UpravljanjeTerminima();
            UpravljanjePregledima = new UpravljanjePregledima();

            this.termini = termini;
            this.termin = termin;

            this.terminiPacijent = new ObservableCollection<Termin>(UpravljanjeTerminima.PregledTermina(this.termin.pacijent));


            if (termin.pacijent != null)
            {
                if (UpravljanjePacijentima.PregledPacijenta(termin.pacijent.Jmbg) != null)
                {
                    Pacijent.Text = termin.pacijent.Jmbg;
                }
            }

            Sifra.Text = termin.SifraTermina;
            DatumText.SelectedDate = termin.Datum;
            //Datum.Text = termin.Datum.ToString();

            if (termin.sala != null)
            {
                if (UpravljanjeSalama.PregledSale(termin.sala.Sifra) != null)
                {
                    //if (uprSal.PregledSale(termin.sala.Sifra).Dostupnost == true)
                    //{
                        Sala.Text = termin.sala.Sifra;
                    //}
                }
            }


            string v = termin.Vreme;
            for (int i = 0; i < niz.Length; i++)
            {
                if (niz[i].Equals(v))
                {
                    index = i;
                }
            }
            vremeText.SelectedIndex = index;


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
            tempSala = UpravljanjeSalama.PregledSale(Sala.Text);
            termin.sala = tempSala;
            this.termin.sala = tempSala;

            Model.Pacijent tempPacijent = new Model.Pacijent();
            tempPacijent = UpravljanjePacijentima.PregledPacijenta(Pacijent.Text);
            termin.pacijent = tempPacijent;

            this.termin.SifraTermina = Sifra.Text;

            this.termin.Datum = (DateTime)DatumText.SelectedDate;
            
            this.termin.Vreme = vremeText.Text;

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

            ControllerLekar.getInstance().IzmenaTermina(this.termin, tempPacijent);

            this.termini.Add(this.termin);

            this.Close();
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DatumText_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void vremeText_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
