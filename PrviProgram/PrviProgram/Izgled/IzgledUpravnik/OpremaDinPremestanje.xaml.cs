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
    public partial class OpremaDinPremestanje : Window
    {
        private UtilityService utilityService = new UtilityService();
        private UpravnikController upravnikController = new UpravnikController();
        private Sala trenutnaSala;
        private Oprema trenutnaOprema;
        private ObservableCollection<Oprema> svaOpremaIzTabele;
        private Oprema opremaZaPremestanje = new Oprema();
        private Oprema preostalaOprema = new Oprema();
        private Sala novaSala = new Sala();
        private SalaRepository salaRepository = new SalaRepository();
        private List<Sala> sveSale = new List<Sala>();

        public OpremaDinPremestanje(ObservableCollection<Oprema> opreme, Oprema oprema, Sala sala)
        {
            InitializeComponent();
            this.svaOpremaIzTabele = opreme;
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
            else if(int.Parse(Kolicina.Text) < 0) {
                MessageBox.Show("Kolicina nije dobro uneta!", "Greska");
            }
            else
            {
                novaSala = (Sala)ComboSala.SelectedItem;
                FormiranjeOpremeZaPremestanje();
                PremestanjeOpreme();
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
            List<Sala> comboSale = salaRepository.PregledSvihSala();
            foreach (Sala salaBrojac in comboSale.ToArray())
            {
                if (salaBrojac.Sifra.Equals(trenutnaSala.Sifra))
                {
                    comboSale.Remove(salaBrojac);
                    break;
                }
            }
            ComboSala.ItemsSource = comboSale;
        }

        private void FormiranjeOpremeZaPremestanje()
        {
            opremaZaPremestanje.Naziv = NazivOpreme.Text;
            opremaZaPremestanje.Kolicina = int.Parse(Kolicina.Text);
            opremaZaPremestanje.Tip = TipOpreme.Dinamicka;
            opremaZaPremestanje.NazivSale = novaSala.Naziv;
        }

        private void ProveraPreostaleOpreme()
        {
            preostalaOprema.Naziv = opremaZaPremestanje.Naziv;
            preostalaOprema.Tip = opremaZaPremestanje.Tip;
            preostalaOprema.Kolicina = this.trenutnaOprema.Kolicina - opremaZaPremestanje.Kolicina;
            preostalaOprema.NazivSale = this.trenutnaSala.Naziv;
            if (preostalaOprema.Kolicina != 0)
            {
                this.svaOpremaIzTabele.Add(preostalaOprema);
            }
        }

        private void BrisanjeOpremeIzStareSale()
        {
            foreach (Oprema o in trenutnaSala.oprema.ToArray())
            {
                if (o.Naziv.Equals(opremaZaPremestanje.Naziv))
                {
                    trenutnaSala.GetOprema().Remove(o);
                }
            }
        }
        private void DodavanjePreostaleOpremeUStaruSalu() 
        {
            Oprema preostalaOprema = new Oprema();
            preostalaOprema.Naziv = opremaZaPremestanje.Naziv;
            preostalaOprema.Tip = opremaZaPremestanje.Tip;
            preostalaOprema.Kolicina = this.trenutnaOprema.Kolicina - opremaZaPremestanje.Kolicina;
            preostalaOprema.NazivSale = this.trenutnaSala.Naziv;
            trenutnaSala.oprema.Add(preostalaOprema);
        }

        private void DodavanjaOpremeZaPremestanjeUNovuSalu()
        {
            foreach (Sala s in sveSale)
            {
                if (s.Sifra.Equals(novaSala.Sifra))
                {
                    s.oprema.Add(opremaZaPremestanje);
                }
            }
        }

        private void OsvezavanjeTabele()
        {
            if (opremaZaPremestanje.Kolicina == 0)
            {
                this.svaOpremaIzTabele.Remove(this.trenutnaOprema);
                this.svaOpremaIzTabele.Add(opremaZaPremestanje);
            }
            else
            {
                this.svaOpremaIzTabele.Remove(this.trenutnaOprema);
                //this.svaOpremaIzTabele.Add(opremaZaPremestanje);
                ProveraPostojanjaOpremeUNovojSali(opremaZaPremestanje);
                ProveraPreostaleOpreme();
            }
            BrisanjeOpremeIzStareSale();
            DodavanjePreostaleOpremeUStaruSalu();
            DodavanjaOpremeZaPremestanjeUNovuSalu();
        }

        private bool ProveraPostojanjaOpremeUNovojSali(Oprema opremaZaPremestanje)
        {
            List<Oprema> svaOprema = new List<Oprema>(this.svaOpremaIzTabele);
            foreach (Oprema opremaBrojac in svaOprema)
            {
                if (opremaBrojac.Naziv.Equals(opremaZaPremestanje.Naziv) && opremaBrojac.NazivSale.Equals(opremaZaPremestanje.NazivSale))
                {
                    this.svaOpremaIzTabele.Remove(opremaBrojac);
                    Oprema prebacenaOprema = new Oprema(opremaZaPremestanje.Naziv, opremaBrojac.Kolicina + opremaZaPremestanje.Kolicina, opremaZaPremestanje.Tip, opremaBrojac.NazivSale);
                    this.svaOpremaIzTabele.Add(prebacenaOprema);
                    return true;
                }
            }
            this.svaOpremaIzTabele.Add(opremaZaPremestanje);
            return true;
        }

        private void PremestanjeOpreme()
        {
            if (upravnikController.PremestanjeOpreme(opremaZaPremestanje, trenutnaSala, novaSala))
            {
                OsvezavanjeTabele();
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
