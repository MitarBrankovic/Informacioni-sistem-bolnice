using Controller;
using Model;
using Repository;
using Service;
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

    public partial class kreiranjeNotifikacije : Page
    {
        public DateTime pocetakDatum;
        public DateTime krajDatum;
        public Pacijent pacijent;
        public NotifikacijeController notifikacijeController;

        public PacijentControler pacijentControler = new PacijentControler();
        public List<NotifikacijePacijenta> notifikacije=new List<NotifikacijePacijenta>();
        public List<Pacijent> pacijenti = new List<Pacijent>();
        public List<Recept> recepti = new List<Recept>();
        public NotifikacijeService notifikacija = new NotifikacijeService();
        
        public kreiranjeNotifikacije(Pacijent pacijent)
        {
            InitializeComponent();
            krajDatumText.IsEnabled = false;
            potvrdiButton.IsEnabled = false;
            this.pacijent = pacijent;
            this.recepti = notifikacija.PregledRecepata(pacijent);
            foreach(Recept recepti in recepti)
            {
                listaLekova.Items.Add(recepti.Lekovi);
            }
            OpisLeka.IsEnabled = false;
            potvrdiButton.IsEnabled = false;
        }

        private void pocetakDatum_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if(pocetakDatumText.Text.Equals(""))
            {
                krajDatumText.IsEnabled = false;
            }
            else
            {
                krajDatumText.IsEnabled = true;
                this.krajDatum = (DateTime)pocetakDatumText.SelectedDate;
                krajDatumText.BlackoutDates.Add(new CalendarDateRange(DateTime.Today, (DateTime)pocetakDatumText.SelectedDate));
            }
        }

        private void krajDatum_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if(krajDatumText.Text.Equals(""))
            {
                potvrdiButton.IsEnabled = false;
            }
            else
            {
                this.pocetakDatum = (DateTime)krajDatumText.SelectedDate;
                potvrdiButton.IsEnabled = true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Recept recept = new Recept(OpisLeka.Text);

            NotifikacijePacijenta novaNotifikacijaPacijenta = new NotifikacijePacijenta(this.pacijent, pocetakDatum, krajDatum, recept, comboBoxVreme.Text, "Morate popiti " + listaLekova.SelectedItem.ToString() + " za 1h.");
            notifikacijeController.DodavanjeNotifikacije(novaNotifikacijaPacijenta);
            potvrdiButton.IsEnabled = false;
            otkaziButton.IsEnabled = false;
        
        }

        private void listaLekova_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(listaLekova.SelectedItem.ToString().Equals(""))
            {
                OpisLeka.IsEnabled = false;
            }
            else
            {
                Recept recept= new Recept();
                OpisLeka.IsEnabled = true;
                OpisLeka.Text = notifikacijeController.PronadjiOpis(this.pacijent, recept);
            }
        }

        private void otkaziButton_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void OpisLeka_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
 