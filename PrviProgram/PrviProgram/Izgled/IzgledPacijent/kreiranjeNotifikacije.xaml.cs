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
        
        public kreiranjeNotifikacije(Pacijent p)
        {

            
            InitializeComponent();
            LekText.Text = "Brufen";
            OpisLeka.Text = "Morate ga koristiti svaki dan u 18h";
            endDate.IsEnabled = false;
            potvrdiButton.IsEnabled = false;
            this.pp = p;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            NotifikacijeObavestenjaRepository datoteka = new NotifikacijeObavestenjaRepository();
            List<NotifikacijePacijenta> notifikacije = datoteka.CitanjeIzFajla();
            foreach (NotifikacijePacijenta n in notifikacije)
            {
                if (n.pacijent.Jmbg.Equals(this.pp.Jmbg))
                {
                    MessageBox.Show(n.opisNotifikacije);
                }
            }

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
            notifikacija.pacijent = this.pp;
            Recept r = new Recept();
            r.Lekovi = LekText.Text;
            r.OpisLeka = OpisLeka.Text;
            notifikacija.recept = r;
            notifikacija.KrajDatuma = end;
            notifikacija.PocetakDatuma = start;
            notifikacija.vremeObavestenja = comboBoxVreme.Text;
            notifikacija.opisNotifikacije="Morate popiti"+LekText.Text+" za 1h.";
            this.notifikacije.Add(notifikacija);
            not.DodavanjeNotifikacije(notifikacije);
            this.Close();
             

        }
    }
}
 