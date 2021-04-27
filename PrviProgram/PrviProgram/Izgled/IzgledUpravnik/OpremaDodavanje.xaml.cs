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
    /// Interaction logic for OpremaDodavanje.xaml
    /// </summary>
    public partial class OpremaDodavanje : Window
    {
        private OpremaService upr;
        private ObservableCollection<Model.Oprema> opreme;
        private Sala sala;

        public OpremaDodavanje(ObservableCollection<Model.Oprema> opreme, Sala sala)
        {
            InitializeComponent();
            upr = new OpremaService();
            this.opreme = opreme;
            this.sala = sala;

            Sala.Text = sala.Naziv;
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            Oprema tempOprema = new Oprema();

            if (Naziv.Text == "" && Kolicina.Text == "")
            {
                MessageBox.Show("Nisu popunjena sva polja!", "Greska"); //, MessageBoxButtons.OK, MessageBoxIcon.Error
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(Naziv.Text, "^[a-zA-Z ]"))
            {
                MessageBox.Show("Naziv nije dobro unet!", "Greska");
                Naziv.Text.Remove(Naziv.Text.Length - 1);
            }
            else if (isNumber(Kolicina.Text) == false)
            {
                MessageBox.Show("Kolicina nije dobro unet!", "Greska");
            }
            else
            {

                tempOprema.Naziv = Naziv.Text;
                tempOprema.Kolicina = int.Parse(Kolicina.Text);
                
                String tip = Tip.Text;
                if (tip.Equals("Staticka"))
                {
                    tempOprema.Tip = TipOpreme.Staticka;
                }
                else if (tip.Equals("Dinamicka"))
                {
                    tempOprema.Tip = TipOpreme.Dinamicka;
                }

                tempOprema.NazivSale = sala.Naziv;


                if (upr.DodavanjeOpreme(tempOprema, sala) == true)
                {
                    this.opreme.Add(tempOprema);
                    sala.oprema.Add(tempOprema);
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
