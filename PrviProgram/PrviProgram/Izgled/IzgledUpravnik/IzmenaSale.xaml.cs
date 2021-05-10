using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Controller;
using Model;
using Service;
using Service.LekarService;

namespace PrviProgram.Izgled.IzgledUpravnik
{
    public partial class IzmenaSale : Window
    {
        private UpravnikController upravnikController = new UpravnikController();
        private ObservableCollection<Sala> sale;
        private Sala sala;
        private Sala izmenjenaSala = new Sala();

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
                MessageBox.Show("Nisu popunjena sva polja!", "Greska"); //, MessageBoxButtons.OK, MessageBoxIcon.Error
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
            String tip = sala.Tip.ToString();
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
            else if (tip.Equals("Magacin"))
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

        private void IzmenaPodatakaSale()
        {
            izmenjenaSala.Naziv = Naziv.Text;
            izmenjenaSala.Sifra = Sifra.Text;
            izmenjenaSala.Sprat = int.Parse(Sprat.Text);
            //novaSala.oprema = sala.GetOprema();
            izmenjenaSala.SetOprema(sala.GetOprema());


            String tip = Tip.Text;
            if (tip.Equals("Operaciona"))
            {
                izmenjenaSala.Tip = TipSale.Operaciona;
            }
            else if (tip.Equals("Kancelarija"))
            {
                izmenjenaSala.Tip = TipSale.Kancelarija;
            }
            else if (tip.Equals("Sala za odmor"))
            {
                izmenjenaSala.Tip = TipSale.SalaZaOdmor;
            }
            else if (tip.Equals("Sala sa krevetima"))
            {
                izmenjenaSala.Tip = TipSale.SalaSaKrevetima;
            }
            else if (tip.Equals("Magacin"))
            {
                izmenjenaSala.Tip = TipSale.Magacin;
            }

            String dostupnost = Dostupnost.Text;
            if (dostupnost.Equals("Da"))
            {
                izmenjenaSala.Dostupnost = true;
            }
            else if (dostupnost.Equals("Ne"))
            {
                izmenjenaSala.Dostupnost = false;
            }
        }

        private void IzvrsavanjeIzmene()
        {
            if (upravnikController.IzmenaSale(this.sala, izmenjenaSala) == true)
            {
                this.sale.Remove(this.sala);
                this.sale.Add(izmenjenaSala);
                TerminiService.getInstance().IzmenaSale(this.sala, izmenjenaSala);
                PreglediService.getInstance().IzmenaSale(this.sala, izmenjenaSala);
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
