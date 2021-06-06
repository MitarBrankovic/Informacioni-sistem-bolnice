using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Controller;
using Model;
using Service;

namespace PrviProgram.Izgled.IzgledUpravnik
{
    public partial class IzmenaSale : Window
    {
        private SaleController saleController = new SaleController();
        private UtilityService utilityService = new UtilityService();
        private TerminiService terminiService = new TerminiService();
        private ObservableCollection<Sala> sale;
        private Sala sala;
        private Sala izmenjenaSala = new Sala();
        private ObservableCollection<TipSale> tipSale = new ObservableCollection<TipSale> { TipSale.Operaciona, TipSale.Magacin,
            TipSale.Kancelarija, TipSale.SalaZaOdmor, TipSale.SalaSaKrevetima};

        public IzmenaSale(ObservableCollection<Sala> sale, Sala sala)
        {
            InitializeComponent();
            this.sale = sale;
            this.sala = sala;
            PrikazPodatakaSale();
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
            else if (!utilityService.IsNumber(Sprat.Text))
            {
                MessageBox.Show("Sprat nije dobro unet!", "Greska");
            }
            else
            {
                IzmenaPodatakaSale();
                IzvrsavanjeIzmene();
                this.Close();
            }
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void PrikazPodatakaSale()
        {
            Naziv.Text = sala.Naziv;
            Sifra.Text = sala.Sifra;
            Sprat.Text = sala.Sprat.ToString();
            Naziv.Text = sala.Naziv;
            Tip.ItemsSource = tipSale;
            Tip.SelectedIndex = tipSale.IndexOf(sala.Tip);
        }

        private void IzmenaPodatakaSale()
        {
            izmenjenaSala.Naziv = Naziv.Text;
            izmenjenaSala.Sifra = Sifra.Text;
            izmenjenaSala.Sprat = int.Parse(Sprat.Text);
            izmenjenaSala.SetOprema(sala.GetOprema());
            izmenjenaSala.Tip = (TipSale)Tip.SelectedItem;
        }

        private void IzvrsavanjeIzmene()
        {
            if (saleController.IzmenaSale(this.sala, izmenjenaSala))
            {
                this.sale.Remove(this.sala);
                this.sale.Add(izmenjenaSala);
                TerminiService.getInstance().IzmenaSale(this.sala, izmenjenaSala);
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
