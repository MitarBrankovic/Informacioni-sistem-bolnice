using System;
using System.Collections.ObjectModel;
using System.Windows;
using Model;
using Service.UpravnikService;

namespace PrviProgram.Izgled.IzgledUpravnik
{
    public partial class DodavanjeSale : Window
    {
        private SaleService upr;
        private ObservableCollection<Model.Sala> sale;

        public DodavanjeSale(ObservableCollection<Model.Sala> sale)
        {
            InitializeComponent();
            upr = new SaleService();
            this.sale = sale;
        }


        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            Sala tempSala = new Sala();

            if (Naziv.Text == "" && Sifra.Text == "" && Sprat.Text == "")
            {
                MessageBox.Show("Nisu popunjena sva polja!", "Greska"); //, MessageBoxButtons.OK, MessageBoxIcon.Error
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(Naziv.Text, "^[a-zA-Z ]"))
            {
                MessageBox.Show("Naziv nije dobro unet!", "Greska");
                Naziv.Text.Remove(Naziv.Text.Length - 1);
            }
            else if (isNumber(Sprat.Text) == false)
            {
                MessageBox.Show("Sprat nije dobro unet!", "Greska");
            }
            else
            {

                tempSala.Naziv = Naziv.Text;
                tempSala.Sifra = Sifra.Text;
                tempSala.Sprat = int.Parse(Sprat.Text);

                String tip = Tip.Text;
                if (tip.Equals("Operaciona"))
                {
                    tempSala.Tip = TipSale.Operaciona;
                }
                else if (tip.Equals("Kancelarija"))
                {
                    tempSala.Tip = TipSale.Kancelarija;
                }
                else if (tip.Equals("Sala za odmor"))
                {
                    tempSala.Tip = TipSale.SalaZaOdmor;
                }
                else if (tip.Equals("Sala sa krevetima"))
                {
                    tempSala.Tip = TipSale.SalaSaKrevetima;
                }
                else if (tip.Equals("Magacin"))
                {
                    tempSala.Tip = TipSale.Magacin;
                }

                String dostupnost = Dostupnost.Text;
                if (dostupnost.Equals("Da"))
                {
                    tempSala.Dostupnost = true;
                }
                else if (dostupnost.Equals("Ne"))
                {
                    tempSala.Dostupnost = false;
                }

                if (upr.DodavanjeSale(tempSala) == true)
                {
                    this.sale.Add(tempSala);
                }

                this.Close();
            }
        }

        public bool isNumber(String st)
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
