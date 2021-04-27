using Model;
using PrviProgram.Repository;
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

namespace PrviProgram.Izgled.IzgledUpravnik
{
    /// <summary>
    /// Interaction logic for RenoviranjeSale.xaml
    /// </summary>
    public partial class RenoviranjeSale : Window
    {
        private SaleService saleService;
        private SalaRepository saleRep;
        private ObservableCollection<Model.Oprema> opreme;
        private Sala selektovanaSala;
        private ObservableCollection<Model.Sala> sveSale;
        private DateTime pocetakRenoviranja;
        private DateTime krajRenoviranja;


        public RenoviranjeSale(ObservableCollection<Model.Sala> sale, Model.Sala sala)
        {
            InitializeComponent();

            saleRep = new SalaRepository();
            saleService = new SaleService();

            this.sveSale = sale;
            this.selektovanaSala = sala;

            TrenutnaSala.Text = sala.Naziv;




            TerminiRepository datoteka = new TerminiRepository();
            List<Termin> termini = datoteka.CitanjeIzFajla();

            foreach (Termin tt in termini)
            {
                if (tt.sala.Sifra.Equals(sala.Sifra))
                {
                    PocetakRenoviranja.BlackoutDates.Add(new CalendarDateRange(tt.Datum, tt.Datum));
                    KrajRenoviranja.BlackoutDates.Add(new CalendarDateRange(tt.Datum, tt.Datum));
                }
            }

        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            if (PocetakRenoviranja.Text == "" && KrajRenoviranja.Text == "")
            {
                MessageBox.Show("Nisu popunjena sva polja!", "Greska");
            }
            else{
                this.pocetakRenoviranja = (DateTime)(PocetakRenoviranja.SelectedDate);
                this.krajRenoviranja = (DateTime)(KrajRenoviranja.SelectedDate);

                if (saleService.RenoviranjeSale(selektovanaSala, this.pocetakRenoviranja, this.krajRenoviranja) == false) {
                    MessageBox.Show("Ponovo izaberite datume");

                }
            }
            this.Close();

        }

        private void PocetakRenoviranja_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void KrajRenoviranja_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
