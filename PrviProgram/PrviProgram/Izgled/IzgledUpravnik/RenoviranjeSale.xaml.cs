using Controller;
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
        private UpravnikController upravnikController = new UpravnikController();
        private Sala selektovanaSala;
        private TerminiRepository terminiRepository = new TerminiRepository();

        public RenoviranjeSale(Sala sala)
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
            if (PocetakRenoviranja.Text == "" && KrajRenoviranja.Text == "" && TextNaziv1.Text == "" && TextNaziv2.Text == "" && TextSifra1.Text == "" && TextSifra2.Text == "")
            {
                MessageBox.Show("Nisu popunjena sva polja!", "Greska");
            }
            else
            {
                TerminRenoviranjaSale terminRenoviranjaSale = FormiranjeTerminaRenoviranja();
                if (!upravnikController.RenoviranjeSale(terminRenoviranjaSale))
                    MessageBox.Show("Ponovo izaberite datume");
            }
            this.Close();
        }

        private TerminRenoviranjaSale FormiranjeTerminaRenoviranja()
        {
            Sala prvaSala = new Sala(selektovanaSala.Tip, TextNaziv1.Text, selektovanaSala.Sprat, TextSifra1.Text);
            Sala drugaSala = new Sala(selektovanaSala.Tip, TextNaziv2.Text, selektovanaSala.Sprat, TextSifra2.Text);
            prvaSala.oprema = selektovanaSala.oprema;
            foreach (Oprema opremaBrojac in prvaSala.oprema)
            {
                opremaBrojac.NazivSale = prvaSala.Naziv;
            }
            drugaSala.oprema = new List<Oprema>();
            TerminRenoviranjaSale terminRenoviranjaSale = new TerminRenoviranjaSale(selektovanaSala, (DateTime)(PocetakRenoviranja.SelectedDate),
                (DateTime)(KrajRenoviranja.SelectedDate), prvaSala, drugaSala);
            terminRenoviranjaSale.TipRenoviranja = TipRenoviranja.razdvajanje;
            return terminRenoviranjaSale;
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

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
            else if (e.Key == Key.Space)
            {
                PocetakRenoviranja.Focus();
            }
        }
    }
}
