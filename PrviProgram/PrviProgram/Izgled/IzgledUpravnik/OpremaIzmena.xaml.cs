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
        private UtilityService utilityService = new UtilityService();
        private OpremaController opremaController = new OpremaController();
        private ObservableCollection<Oprema> svaOpremaIzTabele;
        private Sala trenutnaSala;
        private Oprema trenutnaOprema;
        private Oprema izmenjenaOprema = new Oprema();
        private ObservableCollection<TipOpreme> tipOpreme = new ObservableCollection<TipOpreme> { TipOpreme.Dinamicka, TipOpreme.Staticka };


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
            else if (!utilityService.IsNumber(Kolicina.Text))
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
            Tip.ItemsSource = tipOpreme;
            Tip.SelectedIndex = tipOpreme.IndexOf(trenutnaOprema.Tip);
        }

        private void IzmenaPodatakaOpreme() 
        {
            izmenjenaOprema.Naziv = Naziv.Text;
            izmenjenaOprema.Kolicina = int.Parse(Kolicina.Text);
            izmenjenaOprema.Tip = (TipOpreme)Tip.SelectedItem;
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
            if (opremaController.IzmenaOpreme(this.trenutnaOprema, izmenjenaOprema, trenutnaSala))
            {
                OsvezavanjeTabele();
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
                Naziv.Focus();
            }
        }
    }
}
