using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Controller;
using Model;
using Service;

namespace PrviProgram.Izgled.IzgledUpravnik
{
    public partial class DodavanjeSale : Window
    {
        private UtilityService utilityService = new UtilityService();
        private ObservableCollection<Sala> sale;
        private SaleController saleController = new SaleController();
        private Sala novaSala = new Sala();
        private ObservableCollection<TipSale> tipSale = new ObservableCollection<TipSale> { TipSale.Operaciona, TipSale.Magacin,
            TipSale.Kancelarija, TipSale.SalaZaOdmor, TipSale.SalaSaKrevetima};

        public DodavanjeSale(ObservableCollection<Sala> sale)
        {
            InitializeComponent();
            Tip.ItemsSource = tipSale;
            this.sale = sale;
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            if (Naziv.Text == "" && Sifra.Text == "" && Sprat.Text == "")
            {
                MessageBox.Show("Nisu popunjena sva polja!", "Greska");
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(Naziv.Text, "^[a-zA-Z ]"))
            {
                MessageBox.Show("Naziv nije dobro unet!", "Greska");
                Naziv.Text.Remove(Naziv.Text.Length - 1);
            }
            else if (!utilityService.IsNumber(Sprat.Text) || int.Parse(Sprat.Text) < 1)
            {
                MessageBox.Show("Sprat nije dobro unet!", "Greska");
            }
            else
            {
                FormiranjeNoveSale();
                DodavanjeNoveSale();
                this.Close();
            }
        }

        private void FormiranjeNoveSale()
        {
            novaSala.Naziv = Naziv.Text;
            novaSala.Sifra = Sifra.Text;
            novaSala.Sprat = int.Parse(Sprat.Text);
            novaSala.Tip = (TipSale)Tip.SelectedItem;
        }

        private void DodavanjeNoveSale()
        {
            if (saleController.DodavanjeSale(novaSala))
            {
                this.sale.Add(novaSala);
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Naziv.Focus();
        }
    }
}
