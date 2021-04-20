using Model;
using Service.UpravnikService;
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
    /// Interaction logic for OpremaStatPremestanje.xaml
    /// </summary>
    public partial class OpremaStatPremestanje : Window
    {
        private OpremaService upr;
        private ObservableCollection<Model.Oprema> opreme;
        private Sala sala;
        private Oprema opremaa;
        private ObservableCollection<Model.Sala> sale;
        private DateTime datumPremestaja;

        public OpremaStatPremestanje(ObservableCollection<Model.Oprema> opreme, Oprema oprema, Sala sala, ObservableCollection<Model.Sala> sale)
        {
            InitializeComponent();

            //TerminDatum.BlackoutDates.Add(new CalendarDateRange(DateTime.Today, DateTime.Today));

            upr = new OpremaService();
            this.opreme = opreme;
            this.opremaa = oprema;
            this.sala = sala;
            this.sale = sale;
            TrenutnaSala.Text = sala.Naziv;
            NazivOpreme.Text = oprema.Naziv;

            List<Sala> comboSale = new List<Sala>(sale);
            //comboSale.Remove(sala);
            foreach (Sala s in comboSale.ToArray())
            {
                if (s.Sifra.Equals(sala.Sifra))
                {
                    comboSale.Remove(s);
                    break;
                }
            }
            ComboSala.ItemsSource = comboSale;

        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            if (Kolicina.Text == "")
            {
                MessageBox.Show("Nisu popunjena sva polja!", "Greska");
            }
            else if (isNumber(Kolicina.Text) == false)
            {
                MessageBox.Show("Kolicina nije dobro uneta!", "Greska");
            }
            else
            {
                Oprema oprema = new Oprema();
                oprema.Naziv = NazivOpreme.Text;
                oprema.Kolicina = int.Parse(Kolicina.Text);
                oprema.Tip = TipOpreme.Staticka;
                this.datumPremestaja = (DateTime)(TerminDatum.SelectedDate);
                Sala novaSala = new Sala();
                novaSala = (Sala)ComboSala.SelectedItem;

                if (this.opremaa.Kolicina - oprema.Kolicina > -1)
                {
                    this.opreme.Remove(this.opremaa);       //refresovanje tabele stare sale
                    Oprema op = new Oprema();
                    op.Naziv = oprema.Naziv;
                    op.Tip = oprema.Tip;
                    op.Kolicina = this.opremaa.Kolicina - oprema.Kolicina;
                    if (op.Kolicina != 0)
                    {
                        this.opreme.Add(op);
                    }


                    upr.dodavanjeTermina(novaSala, this.sala, oprema, this.datumPremestaja);
                }
                else {
                    MessageBox.Show("Uneli ste pogresnu kolicinu!");
                }

                this.Close();
            }
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DatumText_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public bool isNumber(String st)
        {
            try
            {
                int.Parse(st);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
