using Controller;
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
    public partial class OpremaDodavanje : Window
    {
        private UpravnikController upravnikController = new UpravnikController();
        private ObservableCollection<Oprema> svaOpremaIzTabele;
        private Sala trenutnaSala;
        private Oprema novaOprema = new Oprema();

        public OpremaDodavanje(ObservableCollection<Oprema> opreme, Sala sala)
        {
            InitializeComponent();
            this.svaOpremaIzTabele = opreme;
            this.trenutnaSala = sala;
            Sala.Text = sala.Naziv;
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
            else if (IsNumber(Kolicina.Text) == false)
            {
                MessageBox.Show("Kolicina nije dobro unet!", "Greska");
            }
            else
            {
                FormiranjeNoveOpreme();
                DodavanjeNoveOpreme();
                this.Close();
            }
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void FormiranjeNoveOpreme() 
        {
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
            novaOprema.NazivSale = trenutnaSala.Naziv;
        }

        private void DodavanjeNoveOpreme() 
        {
            if (upravnikController.DodavanjeOpreme(novaOprema, trenutnaSala) == true)
            {
                this.svaOpremaIzTabele.Add(novaOprema);
                trenutnaSala.oprema.Add(novaOprema);
            }
        }

        public bool IsNumber(String textBox)
        {
            try
            {
                int.Parse(textBox);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
