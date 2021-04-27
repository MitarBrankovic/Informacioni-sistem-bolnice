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
    /// <summary>
    /// Interaction logic for OpremaIzmena.xaml
    /// </summary>
    public partial class OpremaIzmena : Window
    {
        private OpremaService upr;
        private ObservableCollection<Model.Oprema> opreme;
        private Sala sala;
        private Oprema oprema;

        public OpremaIzmena(ObservableCollection<Model.Oprema> opreme, Oprema oprema, Sala sala)
        {
            InitializeComponent();

            upr = new OpremaService();
            this.opreme = opreme;
            this.oprema = oprema;
            this.sala = sala;

            Naziv.Text = oprema.Naziv;
            Kolicina.Text = oprema.Kolicina.ToString();
            Sala.Text = sala.Naziv;

            String tip = oprema.Tip.ToString();

            if (tip.Equals("Staticka"))
            {
                Tip.SelectedIndex = 0;
            }
            else if (tip.Equals("Dinamicka"))
            {
                Tip.SelectedIndex = 1;
            }

        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            if (Naziv.Text == "" && Kolicina.Text == "")
            {
                MessageBox.Show("Nisu popunjena sva polja!", "Greska");
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(Naziv.Text, "^[a-zA-Z ]"))
            {
                MessageBox.Show("Naziv nije dobro unet!", "Greska");
                Naziv.Text.Remove(Naziv.Text.Length - 1);
            }
            else if (isNumber(Kolicina.Text) == false)
            {
                MessageBox.Show("Kolicina nije dobro uneta!", "Greska");
            }
            else
            {
                Oprema novaOprema = new Oprema();

                novaOprema.Naziv = Naziv.Text;
                novaOprema.Kolicina = int.Parse(Kolicina.Text);
                
                String tip = Tip.Text;
                if (tip.Equals("Staticka"))
                {
                    novaOprema.Tip = TipOpreme.Staticka;
                }
                else if (tip.Equals("Dinamicka"))
                {
                    novaOprema.Tip = TipOpreme.Dinamicka;
                }
                novaOprema.NazivSale = sala.Naziv;

                if (upr.IzmenaOpreme(this.oprema, novaOprema, sala) == true)
                {
                    this.opreme.Remove(this.oprema);
                    this.opreme.Add(novaOprema);

                    foreach (Oprema o in sala.oprema.ToArray())
                    {
                        if (o.Naziv.Equals(this.oprema.Naziv))
                        {
                            sala.GetOprema().Remove(o);
                        }
                    }
                    sala.oprema.Add(novaOprema);
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
