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
    public partial class OpremaStatPremestanje : Window
    {
        private UtilityService utilityService = new UtilityService();
        private OpremaService opremaService = new OpremaService();
        private Sala trenutnaSala;
        private Oprema trenutnaOprema;
        private Sala novaSala = new Sala();
        private SalaRepository salaRepository = new SalaRepository();
        private Oprema opremaZaPremestanje;
        private List<Sala> sveSale = new List<Sala>();

        public OpremaStatPremestanje(Oprema oprema, Sala sala)
        {
            InitializeComponent();
            this.trenutnaOprema = oprema;
            this.trenutnaSala = sala;
            sveSale = salaRepository.PregledSvihSala();
            PrikazPodatakaOpreme();
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            if (Kolicina.Text == "")
            {
                MessageBox.Show("Nisu popunjena sva polja!", "Greska");
            }
            else if (!utilityService.IsNumber(Kolicina.Text))
            {
                MessageBox.Show("Kolicina nije dobro uneta!", "Greska");
            }
            else if (int.Parse(Kolicina.Text) < 1)
            {
                MessageBox.Show("Kolicina nije dobro uneta!", "Greska");
            }
            else
            {
                novaSala = (Sala)ComboSala.SelectedItem;
                FormiranjeStatickogPremestanja();
                this.Close();
            }
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void PrikazPodatakaOpreme()
        {
            TrenutnaSala.Text = trenutnaSala.Naziv;
            NazivOpreme.Text = trenutnaOprema.Naziv;
            List<Sala> comboSale = new List<Sala>(sveSale);
            foreach (Sala s in comboSale.ToArray())
            {
                if (s.Sifra.Equals(trenutnaSala.Sifra))
                {
                    comboSale.Remove(s);
                    break;
                }
            }
            ComboSala.ItemsSource = comboSale;
        }

        private void FormiranjeStatickogPremestanja()
        {
            opremaZaPremestanje = new Oprema(NazivOpreme.Text, int.Parse(Kolicina.Text), TipOpreme.Staticka, novaSala.Naziv);
            if (this.trenutnaOprema.Kolicina - opremaZaPremestanje.Kolicina > -1)
            {
                opremaService.DodavanjeTermina(new TerminPremestanjaOpreme(opremaZaPremestanje, novaSala,
                    this.trenutnaSala, (DateTime)(TerminDatum.SelectedDate)));
            }
            else{ MessageBox.Show("Uneli ste pogresnu kolicinu!"); }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
            else if (e.Key == Key.Space)
            {
                Kolicina.Focus();
            }
        }
    }
}
