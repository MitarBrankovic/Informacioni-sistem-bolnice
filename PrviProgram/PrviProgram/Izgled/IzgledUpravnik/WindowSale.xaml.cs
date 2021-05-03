using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Model;
using PrviProgram.Logika;
using PrviProgram.Repository;
using Repository;
using Service;

namespace PrviProgram.Izgled.IzgledUpravnik
{
    public partial class WindowUpravnik : Window
    {
        private static WindowUpravnik instance = null;
        public static WindowUpravnik GetInstance()
        {
            if (instance == null)
            {
                instance = new WindowUpravnik();
            }
            return instance;
        }
        public SaleService saleService;
        public SalaRepository saleRepository;
        public ObservableCollection<Sala> sale;
        public DispatcherTimer timer = new DispatcherTimer();

        public WindowUpravnik()
        {
            InitializeComponent();
            saleService = new SaleService();
            saleRepository = new SalaRepository();
            InicijalizacijaTimera();
            sale = new ObservableCollection<Sala>(saleRepository.PregledSvihSala());
            dataGridUpravnik.ItemsSource = sale;
        }

        private void InicijalizacijaTimera()
        {
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Start();
            timer.Tick += new EventHandler(IzvrsavanjePremestanjaOpreme);
            timer.Tick += new EventHandler(BrisanjeTerminaRenoviranja);
        }

        public void IzvrsavanjePremestanjaOpreme(object sender, EventArgs e)
        {
            TerminiPremestajaRepository datoteka = new TerminiPremestajaRepository();
            List<TerminPremestanjaOpreme> termini = datoteka.CitanjeIzFajla();        
            foreach (TerminPremestanjaOpreme terminBrojac in termini)
            {
                if (DateTime.Today.Equals(terminBrojac.datumPremestaja))
                {
                    OpremaService.GetInstance().PremestanjeOpreme(terminBrojac.oprema, terminBrojac.staraSala, terminBrojac.sala);
                    //timer.Stop();
                    termini.Remove(terminBrojac);
                    datoteka.UpisivanjeUFajl(termini);
                    break;
                }
            }
        }

        public void BrisanjeTerminaRenoviranja(object sender, EventArgs e) {
            TerminiRenoviranjaRepository datoteka = new TerminiRenoviranjaRepository();
            List<TerminRenoviranjaSale> terminiRenoviranja = datoteka.CitanjeIzFajla();
            foreach (TerminRenoviranjaSale terminBrojac in terminiRenoviranja)
            {
                if (DateTime.Today.Equals(terminBrojac.KrajRenoviranja))
                {
                    //timer.Stop();
                    terminiRenoviranja.Remove(terminBrojac);
                    datoteka.UpisivanjeUFajl(terminiRenoviranja);
                    break;
                }
            }
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
                sala = (Sala)dataGridUpravnik.SelectedItem;
                ControllerUpravnik.getInstance().BrisanjeSale(sala, sale);
            }
            else { MessageBox.Show("Morate izabrati salu!"); }
        }

        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridUpravnik.SelectedIndex != -1)
            {
                IzmenaSale izmena = new IzmenaSale(sale, (Sala)dataGridUpravnik.SelectedItem);
                izmena.Show();
            }
            else{ MessageBox.Show("Morate izabrati salu!"); }
        }

        private void Oprema_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridUpravnik.SelectedIndex != -1)
            {
                OpremaWindow win = new OpremaWindow(sale, (Sala)dataGridUpravnik.SelectedItem);
                win.Show();
            }
            else { MessageBox.Show("Morate izabrati salu!"); }
        }

        private void Renoviranje_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridUpravnik.SelectedIndex != -1)
            {
                RenoviranjeSale win = new RenoviranjeSale(sale, (Sala)dataGridUpravnik.SelectedItem);
                win.Show();
            }
            else { MessageBox.Show("Morate izabrati salu!"); }
        }
    }
}
