﻿using Controller;
using Model;
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
        private UpravnikController upravnikController = new UpravnikController();
        private Sala trenutnaSala;
        private Oprema trenutnaOprema;
        private ObservableCollection<Sala> sveSale;
        private ObservableCollection<Oprema> svaOpremaIzTabele;
        private Oprema opremaZaPremestanje = new Oprema();
        private Oprema preostalaOprema = new Oprema();
        private Sala novaSala = new Sala();


        public OpremaDinPremestanje(ObservableCollection<Oprema> opreme, Oprema oprema, Sala sala, ObservableCollection<Sala> sale)
        {
            InitializeComponent();
            this.svaOpremaIzTabele = opreme;
            this.trenutnaOprema = oprema;
            this.trenutnaSala = sala;
            this.sveSale = sale;
            PrikazPodatakaOpreme();
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            if (Kolicina.Text == "")
            {
                MessageBox.Show("Nisu popunjena sva polja!", "Greska");
            }
            else if (IsNumber(Kolicina.Text) == false)
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
            }
            else
            {
                this.svaOpremaIzTabele.Remove(this.trenutnaOprema);
                ProveraPreostaleOpreme();
            }
            BrisanjeOpremeIzStareSale();
            DodavanjePreostaleOpremeUStaruSalu();
            DodavanjaOpremeZaPremestanjeUNovuSalu();
        }

        private void PremestanjeOpreme()
        {
            if (upravnikController.PremestanjeOpreme(opremaZaPremestanje, trenutnaSala, novaSala) == true)
            {
                OsvezavanjeTabele();
            }
            else{ MessageBox.Show("Uneli ste pogresnu kolicinu!"); }
        }

        public bool IsNumber(String st)
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
