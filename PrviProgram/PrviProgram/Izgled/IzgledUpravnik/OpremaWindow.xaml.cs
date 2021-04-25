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
        public SaleService upravljanje;
        public SalaRepository saleRep;
        public ObservableCollection<Model.Oprema> opreme;
        public Sala sala;
        OpremaService opremaService;
        public ObservableCollection<Model.Sala> sale;
        public DispatcherTimer timer;

        public OpremaWindow(ObservableCollection<Model.Sala> sale, Model.Sala sala)
        {
            InitializeComponent();

            saleRep = new SalaRepository();

            this.sale = sale;
            this.sala = sala;


            upravljanje = new SaleService();
            opremaService = new OpremaService();
            opreme = new ObservableCollection<Model.Oprema>(saleRep.PregledSvihOpremaPoSali(sala));

         

            dataGridOprema.ItemsSource = opreme;


            labela.Content = sala.Naziv;
        }


        private void Prebaci_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridOprema.SelectedIndex != -1)
            {
                Oprema op = (Oprema)dataGridOprema.SelectedItem;
                if (op.Tip == TipOpreme.Dinamicka)
                {
                    OpremaDinPremestanje win = new OpremaDinPremestanje(opreme, op, sala, sale);
                    win.Show();
                }
                else {
                    OpremaStatPremestanje win = new OpremaStatPremestanje(opreme, op, sala, sale);
                    win.Show();
                }
            }
            else
            {
                MessageBox.Show("Morate izabrati opremu!");
            }

        }

        private void Izbrisi_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridOprema.SelectedIndex != -1)
            {
                Oprema op = (Oprema)dataGridOprema.SelectedItem;
                foreach (Oprema o in sala.oprema.ToArray()) {
                    if (o.Naziv.Equals(op.Naziv)) {
                        sala.GetOprema().Remove(o);
                    }
                }
                opremaService.BrisanjeOpreme(op, sala);
                opreme.Remove(op);
            }
            else { MessageBox.Show("Morate izabrati opremu!"); }
        }

        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridOprema.SelectedIndex != -1)
            {
                Oprema op = (Oprema)dataGridOprema.SelectedItem;
                OpremaIzmena win = new OpremaIzmena(opreme, op, sala);
                win.Show();
            }
            else { MessageBox.Show("Morate izabrati opremu!");
            }

        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            OpremaDodavanje win = new OpremaDodavanje(opreme, sala);
            win.Show();
        }
    }
}
