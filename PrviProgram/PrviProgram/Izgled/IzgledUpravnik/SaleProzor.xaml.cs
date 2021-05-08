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
using System.Windows.Threading;
using Model;
using PrviProgram.Logika;
using PrviProgram.Repository;
using Repository;
using Service;
using System.Collections.ObjectModel;

namespace PrviProgram.Izgled.IzgledUpravnik
{
    public partial class SaleProzor : UserControl
    {
        private SalaRepository saleRepository = new SalaRepository();
        private TerminiRenoviranjaRepository terminiRenoviranjaRepository = new TerminiRenoviranjaRepository();
        private ObservableCollection<Sala> sale;
        public DispatcherTimer timer = new DispatcherTimer();

        public SaleProzor()
        {
            InitializeComponent();
            InicijalizacijaTimera();
            sale = new ObservableCollection<Sala>(saleRepository.PregledSvihSala());
            dataGridUpravnik.ItemsSource = sale;
        }

        private void InicijalizacijaTimera()
        {
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Start();
            timer.Tick += new EventHandler(BrisanjeTerminaRenoviranja);
        }

        public void BrisanjeTerminaRenoviranja(object sender, EventArgs e)
        {
            List<TerminRenoviranjaSale> terminiRenoviranja = terminiRenoviranjaRepository.CitanjeIzFajla();
            foreach (TerminRenoviranjaSale terminBrojac in terminiRenoviranja)
            {
                if (DateTime.Today.Equals(terminBrojac.KrajRenoviranja))
                {
                    //timer.Stop();
                    terminiRenoviranja.Remove(terminBrojac);
                    terminiRenoviranjaRepository.UpisivanjeUFajl(terminiRenoviranja);
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
                ControllerUpravnik.getInstance().BrisanjeSale((Sala)dataGridUpravnik.SelectedItem, sale);
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
            else { MessageBox.Show("Morate izabrati salu!"); }
        }

        private void Oprema_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridUpravnik.SelectedIndex != -1)
            {
                var s = new OpremaSale(sale, (Sala)dataGridUpravnik.SelectedItem);
                gridMain.Children.Clear();
                gridMain.Children.Add(s);
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

        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as Grid).Children.Remove(this);
        }
    }
}
