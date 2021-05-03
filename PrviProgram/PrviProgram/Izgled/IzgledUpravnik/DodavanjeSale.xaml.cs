using System;
using System.Collections.ObjectModel;
using System.Windows;
using Controller;
using Model;
using Service;

namespace PrviProgram.Izgled.IzgledUpravnik
{
    public partial class DodavanjeSale : Window
    {
        private ObservableCollection<Sala> sale;
        private UpravnikController upravnikController = new UpravnikController();
        private Sala novaSala = new Sala();

        public DodavanjeSale(ObservableCollection<Sala> sale)
        {
            InitializeComponent();
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
            else if (IsNumber(Sprat.Text) == false)
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
            String tip = Tip.Text;
            if (tip.Equals("Operaciona"))
            {
                novaSala.Tip = TipSale.Operaciona;
            }
            else if (tip.Equals("Kancelarija"))
            {
                novaSala.Tip = TipSale.Kancelarija;
            }
            else if (tip.Equals("Sala za odmor"))
            {
                novaSala.Tip = TipSale.SalaZaOdmor;
            }
            else if (tip.Equals("Sala sa krevetima"))
            {
                novaSala.Tip = TipSale.SalaSaKrevetima;
            }
            else if (tip.Equals("Magacin"))
            {
                novaSala.Tip = TipSale.Magacin;
            }
            String dostupnost = Dostupnost.Text;
            if (dostupnost.Equals("Da"))
            {
                novaSala.Dostupnost = true;
            }
            else if (dostupnost.Equals("Ne"))
            {
                novaSala.Dostupnost = false;
            }
        }

        private void DodavanjeNoveSale()
        {
            if (upravnikController.DodavanjeSale(novaSala) == true)
            {
                this.sale.Add(novaSala);
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
