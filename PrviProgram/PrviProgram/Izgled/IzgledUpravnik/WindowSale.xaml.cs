using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Model;
using PrviProgram.Logika;
using PrviProgram.Repository;
using Service.UpravnikService;

namespace PrviProgram.Izgled.IzgledUpravnik
{
    public partial class WindowUpravnik : Window
    {
        private static WindowUpravnik instance = null;
        public static WindowUpravnik getInstance()
        {
            if (instance == null)
            {
                instance = new WindowUpravnik();
            }
            return instance;
        }


        public SaleService upravljanje;
        public SalaRepository saleRep;
        public ObservableCollection<Model.Sala> sale;
        public DispatcherTimer timer;



        public WindowUpravnik()
        {
            InitializeComponent();

            upravljanje = new SaleService();
            saleRep = new SalaRepository();
            //sale = new ObservableCollection<Model.Sala>(saleRep.PregledSvihSala());

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Start();
            timer.Tick += new EventHandler(lala);

            sale = new ObservableCollection<Model.Sala>(saleRep.PregledSvihSala());


            dataGridUpravnik.ItemsSource = sale;

        }

        public void lala(object sender, EventArgs e)
        {
            TerminiPremestajaRepository datoteka = new TerminiPremestajaRepository();
            List<TerminPremestanjaOpreme> termini = datoteka.CitanjeIzFajla();

            SalaRepository datoteka1 = new SalaRepository();
          
            foreach (TerminPremestanjaOpreme t in termini)
            {
                if (DateTime.Today.Equals(t.datumPremestaja))
                {
                    OpremaService.getInstance().PremestanjeOpreme(t.oprema, t.staraSala, t.sala);
                    //timer.Stop();
                    termini.Remove(t);
                    datoteka.UpisivanjeUFajl(termini);
                    break;

                }

            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            DodavanjeSale dodavanje = new DodavanjeSale(sale);
            dodavanje.Show();

        }

        private void Izbrisi_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridUpravnik.SelectedIndex != -1)
            {
                Sala sala = new Sala();
                sala = (Model.Sala)dataGridUpravnik.SelectedItem;
                ControllerUpravnik.getInstance().BrisanjeSale(sala, sale);
            }
            else { MessageBox.Show("Morate izabrati salu!"); }

        }

        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridUpravnik.SelectedIndex != -1)
            {
                IzmenaSale izmena = new IzmenaSale(sale, (Model.Sala)dataGridUpravnik.SelectedItem);
                izmena.Show();
            }
            else
            {
                MessageBox.Show("Morate izabrati salu!");
            }
        }

        private void Oprema_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridUpravnik.SelectedIndex != -1)
            {
                OpremaWindow win = new OpremaWindow(sale, (Model.Sala)dataGridUpravnik.SelectedItem);
                win.Show();
            }
            else { MessageBox.Show("Morate izabrati salu!"); }

        }

    }
}
