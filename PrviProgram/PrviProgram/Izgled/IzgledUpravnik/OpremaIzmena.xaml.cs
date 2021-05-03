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
    public partial class OpremaIzmena : Window
    {
        private UpravnikController upravnikController = new UpravnikController();
        private ObservableCollection<Oprema> svaOpremaIzTabele;
        private Sala trenutnaSala;
        private Oprema trenutnaOprema;
        private Oprema izmenjenaOprema = new Oprema();

        public OpremaIzmena(ObservableCollection<Oprema> opreme, Oprema oprema, Sala sala)
        {
            InitializeComponent();
            this.svaOpremaIzTabele = opreme;
            this.trenutnaOprema = oprema;
            this.trenutnaSala = sala;
            PrikazPodatakaOpreme();
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
                MessageBox.Show("Kolicina nije dobro uneta!", "Greska");
            }
            else
            {
                IzmenaPodatakaOpreme();
                IzvrsavanjeIzmeneOpreme();
                this.Close();
            }
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void PrikazPodatakaOpreme() 
        {
            Naziv.Text = trenutnaOprema.Naziv;
            Kolicina.Text = trenutnaOprema.Kolicina.ToString();
            Sala.Text = trenutnaSala.Naziv;
            String tip = trenutnaOprema.Tip.ToString();
            if (tip.Equals("Staticka"))
            {
                Tip.SelectedIndex = 0;
            }
            else if (tip.Equals("Dinamicka"))
            {
                Tip.SelectedIndex = 1;
            }
        }

        private void IzmenaPodatakaOpreme() 
        {
            izmenjenaOprema.Naziv = Naziv.Text;
            izmenjenaOprema.Kolicina = int.Parse(Kolicina.Text);
            String tip = Tip.Text;
            if (tip.Equals("Staticka"))
            {
                izmenjenaOprema.Tip = TipOpreme.Staticka;
            }
            else if (tip.Equals("Dinamicka"))
            {
                izmenjenaOprema.Tip = TipOpreme.Dinamicka;
            }
            izmenjenaOprema.NazivSale = trenutnaSala.Naziv;
        }

        private void OsvezavanjeTabele()
        {
            this.svaOpremaIzTabele.Remove(this.trenutnaOprema);
            this.svaOpremaIzTabele.Add(izmenjenaOprema);
            foreach (Oprema o in trenutnaSala.oprema.ToArray())
            {
                if (o.Naziv.Equals(this.trenutnaOprema.Naziv))
                {
                    trenutnaSala.GetOprema().Remove(o);
                }
            }
            trenutnaSala.oprema.Add(izmenjenaOprema);
        }

        private void IzvrsavanjeIzmeneOpreme() 
        {
            if (upravnikController.IzmenaOpreme(this.trenutnaOprema, izmenjenaOprema, trenutnaSala) == true)
            {
                OsvezavanjeTabele();
            }
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
