using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Controller;
using Model;
using PrviProgram.Repository;
using System.Windows.Threading;
using System.Collections.ObjectModel;

namespace PrviProgram.Izgled.IzgledUpravnik
{
    public partial class OpremaSale : UserControl
    {
        private UpravnikController upravnikController = new UpravnikController();
        private SalaRepository saleRepository = new SalaRepository();
        private ObservableCollection<Oprema> svaOpremaIzTabele;
        private Sala trenutnaSala;
        private ObservableCollection<Sala> sveSale;
        public DispatcherTimer timer;

        public OpremaSale(ObservableCollection<Sala> sale, Sala sala)
        {
            InitializeComponent();
            this.sveSale = sale;
            this.trenutnaSala = sala;
            svaOpremaIzTabele = new ObservableCollection<Oprema>(saleRepository.PregledSvihOpremaPoSali(sala));
            dataGridOprema.ItemsSource = svaOpremaIzTabele;
            labela.Content = sala.Naziv;
        }

        private void Prebaci_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridOprema.SelectedIndex != -1)
            {
                Oprema selektovanaOprema = (Oprema)dataGridOprema.SelectedItem;
                if (selektovanaOprema.Tip == TipOpreme.Dinamicka)
                {
                    OpremaDinPremestanje win = new OpremaDinPremestanje(svaOpremaIzTabele, selektovanaOprema, trenutnaSala);
                    win.Show();
                }
                else
                {
                    OpremaStatPremestanje win = new OpremaStatPremestanje(svaOpremaIzTabele, selektovanaOprema, trenutnaSala);
                    win.Show();
                }
            }
            else { MessageBox.Show("Morate izabrati opremu!"); }
        }

        private void Izbrisi_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridOprema.SelectedIndex != -1)
            {
                Oprema selektovanaOprema = (Oprema)dataGridOprema.SelectedItem;
                foreach (Oprema opremaBrojac in trenutnaSala.oprema.ToArray())
                {
                    if (opremaBrojac.Naziv.Equals(selektovanaOprema.Naziv))
                    {
                        trenutnaSala.GetOprema().Remove(opremaBrojac);
                    }
                }
                upravnikController.BrisanjeOpreme(selektovanaOprema, trenutnaSala);
                svaOpremaIzTabele.Remove(selektovanaOprema);
            }
            else { MessageBox.Show("Morate izabrati opremu!"); }
        }

        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridOprema.SelectedIndex != -1)
            {
                OpremaIzmena win = new OpremaIzmena(svaOpremaIzTabele, (Oprema)dataGridOprema.SelectedItem, trenutnaSala);
                win.Show();
            }
            else
            {
                MessageBox.Show("Morate izabrati opremu!");
            }
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            OpremaDodavanje win = new OpremaDodavanje(svaOpremaIzTabele);
            win.Show();
        }

        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            var s = new SaleProzor();
            gridMain.Children.Clear();
            gridMain.Children.Add(s);
            //var m = (this.Parent as Grid);
            //(m.Parent as Grid).Children.Remove(this);
            //(this.Parent as Grid).Children.Remove(this);
        }
    }
}
