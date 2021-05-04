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
using System.Windows.Threading;

namespace PrviProgram.Izgled.IzgledUpravnik
{

    public partial class OpremaWindow : Window
    {
        private UpravnikController upravnikController = new UpravnikController();
        private SalaRepository saleRepository = new SalaRepository();
        private ObservableCollection<Oprema> svaOpremaIzTabele;
        private Sala trenutnaSala;
        private ObservableCollection<Sala> sveSale;
        public DispatcherTimer timer;

        public OpremaWindow(ObservableCollection<Sala> sale, Sala sala)
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
                    OpremaDinPremestanje win = new OpremaDinPremestanje(svaOpremaIzTabele, selektovanaOprema, trenutnaSala, sveSale);
                    win.Show();
                }
                else {
                    OpremaStatPremestanje win = new OpremaStatPremestanje(svaOpremaIzTabele, selektovanaOprema, trenutnaSala, sveSale);
                    win.Show();
                }
            }
            else{ MessageBox.Show("Morate izabrati opremu!"); }
        }

        private void Izbrisi_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridOprema.SelectedIndex != -1)
            {
                Oprema selektovanaOprema = (Oprema)dataGridOprema.SelectedItem;
                foreach (Oprema opremaBrojac in trenutnaSala.oprema.ToArray()) {
                    if (opremaBrojac.Naziv.Equals(selektovanaOprema.Naziv)) {
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
            else { MessageBox.Show("Morate izabrati opremu!");
            }
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            OpremaDodavanje win = new OpremaDodavanje(svaOpremaIzTabele, trenutnaSala);
            win.Show();
        }
    }
}
