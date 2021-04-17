using Model;
using Service.LekarService;
using Service.UpravnikService;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace PrviProgram.Izgled.Upravnik
{
    /// <summary>
    /// Interaction logic for IzmenaSale.xaml
    /// </summary>
    public partial class IzmenaSale : Window
    {

        private SaleService upr;
        private ObservableCollection<Model.Sala> sale;
        private Model.Sala sala;


        public IzmenaSale(ObservableCollection<Model.Sala> sale, Model.Sala sala)
        {
            InitializeComponent();

            upr = new SaleService();
            this.sale = sale;
            this.sala = sala;

            Naziv.Text = sala.Naziv;
            Sifra.Text = sala.Sifra;
            Sprat.Text = sala.Sprat.ToString();
            Naziv.Text = sala.Naziv;

            String tip = sala.Tip.ToString();
            //MessageBox.Show(tip);
            if (tip.Equals("Operaciona"))
            {
                Tip.SelectedIndex = 0;
            }
            else if (tip.Equals("Kancelarija"))
            {
                Tip.SelectedIndex = 1;
            }
            else if (tip.Equals("SalaZaOdmor"))
            {
                Tip.SelectedIndex = 2;
            }
            else if (tip.Equals("SalaSaKrevetima"))
            {
                Tip.SelectedIndex = 3;
            }
            else if (tip.Equals("Prostorije"))
            {
                Tip.SelectedIndex = 4;
            }

            String dostupnost = sala.Dostupnost.ToString();
            if (dostupnost.Equals("True"))
            {
                Dostupnost.SelectedIndex = 0;
            }
            else if (dostupnost.Equals("False"))
            {
                Dostupnost.SelectedIndex = 1;
            }

        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {

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
                Sala novaSala = new Sala();


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
                else if (tip.Equals("Prostorija"))
                {
                    novaSala.Tip = TipSale.Prostorije;
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


                if (upr.IzmenaSale(this.sala, novaSala) == true)
                {
                    this.sale.Remove(this.sala);
                    this.sale.Add(novaSala);
                    Service.PacijentService.TerminiService.getInstance().IzmenaSale(this.sala, novaSala);
                    PreglediService.getInstance().IzmenaSale(this.sala, novaSala);
                }

                this.Close();
            }
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
