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
    public partial class RenoviranjeSale : Window
    {
        private SaleService saleService = new SaleService();
        private Sala selektovanaSala;
        private TerminiRepository terminiRepository = new TerminiRepository();

        public RenoviranjeSale(ObservableCollection<Sala> sale, Sala sala)
        {
            InitializeComponent();
            this.selektovanaSala = sala;
            TrenutnaSala.Text = sala.Naziv;
            ProveraIIzbacivanjeDatumaPregleda();
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
                if (saleService.RenoviranjeSale(selektovanaSala, (DateTime)(PocetakRenoviranja.SelectedDate), (DateTime)(KrajRenoviranja.SelectedDate)) == false) {
                    MessageBox.Show("Ponovo izaberite datume");
                }
            }
            this.Close();
        }

        private void ProveraIIzbacivanjeDatumaPregleda() 
        {
            List<Termin> termini = terminiRepository.CitanjeIzFajla();
            foreach (Termin tt in termini)
            {
                if (tt.sala.Sifra.Equals(selektovanaSala.Sifra))
                {
                    PocetakRenoviranja.BlackoutDates.Add(new CalendarDateRange(tt.Datum, tt.Datum));
                    KrajRenoviranja.BlackoutDates.Add(new CalendarDateRange(tt.Datum, tt.Datum));
                }
            }
        }
    }
}
