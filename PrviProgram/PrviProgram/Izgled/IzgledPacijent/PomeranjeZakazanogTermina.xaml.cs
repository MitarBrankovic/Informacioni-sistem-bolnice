using Model;
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

        public DateTime trenutnoVreme = new DateTime();
        public DateTime vremeTermina = new DateTime();

        public PomeranjeZakazanogTermina(Termin selektovaniTermin, Pacijent p, ObservableCollection<Termin> termini)
        {
            InitializeComponent();
            datumTermina = selektovaniTermin.Datum;
            DatumText.BlackoutDates.Add(new CalendarDateRange(DateTime.Today, datumTermina.AddDays(-3)));
            DatumText.BlackoutDates.Add(new CalendarDateRange(datumTermina,datumTermina));
            DatumText.BlackoutDates.Add(new CalendarDateRange(datumTermina.AddDays(3), DateTime.MaxValue));

            //DatePicker dp = new DatePicker();
            //this.trenutniDatum = DateTime.Now;
            //this.datumTermina = selektovaniTermin.Datum;
            //this.vremeTermina = Convert.ToDateTime(selektovaniTermin.Vreme);
            //this.trenutnoVreme = DateTime.Now;
            //DateTime noviDatum = datumTermina.AddDays(-3);
            //DateTime noviDatum2 = datumTermina.AddDays(-1);
            //CalendarDateRange cdr = new CalendarDateRange(noviDatum, noviDatum2);
            //DatumText.BlackoutDates.Add(cdr);
            //dp.DisplayDateStart = new DateTime(noviDatum.Year, noviDatum.Month, noviDatum.Day);
            //dp.DisplayDateEnd = new DateTime(noviDatum2.Year, noviDatum2.Month, noviDatum2.Day);
            //stackDatum.Children.Add(dp);

            // DatumText.BlackoutDates.Add(new CalendarDateRange(new DateTime(noviDatum.Year, noviDatum.Month, noviDatum.Day), new DateTime(noviDatum2.Year, noviDatum2.Month, noviDatum2.Day)));
            //DatumText.DisplayDateStart = new DateTime(noviDatum.Year, noviDatum.Month, noviDatum.Day);
            //DatumText.DisplayDateEnd= new DateTime(noviDatum2.Year, noviDatum2.Month, noviDatum2.Day);

        }

        

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
