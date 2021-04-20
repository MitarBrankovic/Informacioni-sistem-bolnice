using Model;
using Repository;
using Service.PacijentService;
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

namespace PrviProgram.Izgled.IzgledPacijent
{
   
    public partial class kreiranjeNotifikacije : Window
    {
        public DateTime end;
        public DateTime start;
        public Pacijent pp;
        public NotifikacijePacijenta notifikacija = new NotifikacijePacijenta();
        public NotifikacijeService not = new NotifikacijeService();
        public List<NotifikacijePacijenta> notifikacije=new List<NotifikacijePacijenta>();
        List<Pacijent> pacijenti = new List<Pacijent>();
        List<Recept> recepti = new List<Recept>();
        
        public kreiranjeNotifikacije(Pacijent p)
        {

            
            InitializeComponent();
            
            endDate.IsEnabled = false;
            potvrdiButton.IsEnabled = false;
            this.pp = p;
            this.recepti=not.pregledRecepata(p);
            foreach(Recept r in recepti)
            {
                listaLekova.Items.Add(r.Lekovi);
            }
            OpisLeka.IsEnabled = false;
            potvrdiButton.IsEnabled = false;
            
        }

        private void startDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if(startDate.Text.Equals(""))
            {
                endDate.IsEnabled = false;
            }
            else
            {
                endDate.IsEnabled = true;
                this.start = (DateTime)startDate.SelectedDate;
                endDate.BlackoutDates.Add(new CalendarDateRange(DateTime.Today, (DateTime)startDate.SelectedDate));
            }
        }

        private void endDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if(endDate.Text.Equals(""))
            {
                potvrdiButton.IsEnabled = false;
            }
            else
            {
                this.end = (DateTime)endDate.SelectedDate;
                potvrdiButton.IsEnabled = true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.notifikacija.pacijent = this.pp;
            Recept r = new Recept();
            r.Lekovi = listaLekova.SelectedItem.ToString();
            r.OpisLeka = OpisLeka.Text;
            this.notifikacija.recept = r;
            this. notifikacija.KrajDatuma = end;
           this. notifikacija.PocetakDatuma = start;
           this.notifikacija.vremeObavestenja = comboBoxVreme.Text;
           this. notifikacija.opisNotifikacije="Morate popiti "+listaLekova.SelectedItem.ToString()+" za 1h.";
     
            not.DodavanjeNotifikacije(notifikacija);
            this.Close();

        }

        private void listaLekova_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(listaLekova.SelectedItem.ToString().Equals(""))
            {
                OpisLeka.IsEnabled = false;
            }
            else
            {
                Recept r1 = new Recept();
                r1.Lekovi = listaLekova.SelectedItem.ToString();
                OpisLeka.IsEnabled = true;
                OpisLeka.Text = not.PronadjiOpis(r1, this.pp);
            }
        }
    }
}
 