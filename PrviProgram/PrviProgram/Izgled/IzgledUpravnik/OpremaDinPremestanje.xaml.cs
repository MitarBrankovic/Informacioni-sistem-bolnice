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
    /// Interaction logic for OpremaDinPremestanje.xaml
    /// </summary>
    public partial class OpremaDinPremestanje : Window
    {
        private OpremaService upr;
        private ObservableCollection<Model.Oprema> opreme;
        private Sala sala;
        private Oprema opremaa;
        private ObservableCollection<Model.Sala> sale;


        public OpremaDinPremestanje(ObservableCollection<Model.Oprema> opreme, Oprema oprema, Sala sala, ObservableCollection<Model.Sala> sale)
        {
            InitializeComponent();

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
                oprema.Tip = TipOpreme.Dinamicka;

                Sala novaSala = new Sala();
                novaSala = (Sala)ComboSala.SelectedItem;


                if (upr.PremestanjeOpreme(oprema, sala, novaSala) == true)
                {
                    if (oprema.Kolicina == 0)
                    {
                        this.opreme.Remove(this.opremaa);
                    }
                    else
                    {
                        this.opreme.Remove(this.opremaa);
                        Oprema op = new Oprema();
                        op.Naziv = oprema.Naziv;
                        op.Tip = oprema.Tip;
                        op.Kolicina = this.opremaa.Kolicina - oprema.Kolicina;

                        this.opreme.Add(op);
                    }

                    foreach (Oprema o in sala.oprema.ToArray())
                    {
                        if (o.Naziv.Equals(oprema.Naziv))
                        {
                            sala.GetOprema().Remove(o);
                            //sala.GetOprema().Add(oprema);
                        }
                    }
                    Oprema opr = new Oprema();
                    opr.Naziv = oprema.Naziv;
                    opr.Tip = oprema.Tip;
                    opr.Kolicina = this.opremaa.Kolicina - oprema.Kolicina;
                    sala.oprema.Add(opr);
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
