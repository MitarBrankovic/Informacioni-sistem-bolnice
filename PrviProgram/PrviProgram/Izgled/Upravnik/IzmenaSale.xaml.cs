using Logika.LogikaUpravnik;
using Model;
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

namespace PrviProgram.Izgled.Upravnik
{
    /// <summary>
    /// Interaction logic for IzmenaSale.xaml
    /// </summary>
    public partial class IzmenaSale : Window
    {

        private Logika.LogikaUpravnik.UpravljanjeSalama upr;
        private ObservableCollection<Model.Sala> sale;
        private Model.Sala sala;


        public IzmenaSale(ObservableCollection<Model.Sala> sale, Model.Sala sala)
        {
            InitializeComponent();

            upr = new Logika.LogikaUpravnik.UpravljanjeSalama();
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

                this.sale.Remove(this.sala);

                this.sala.Naziv = Naziv.Text;
                this.sala.Sifra = Sifra.Text;
                this.sala.Sprat = int.Parse(Sprat.Text);


                String tip = Tip.Text;
                if (tip.Equals("Operaciona"))
                {
                    this.sala.Tip = TipSale.Operaciona;
                }
                else if (tip.Equals("Kancelarija"))
                {
                    this.sala.Tip = TipSale.Kancelarija;
                }
                else if (tip.Equals("Sala za odmor"))
                {
                    this.sala.Tip = TipSale.SalaZaOdmor;
                }
                else if (tip.Equals("Sala sa krevetima"))
                {
                    this.sala.Tip = TipSale.SalaSaKrevetima;
                }
                else if (tip.Equals("Prostorija"))
                {
                    this.sala.Tip = TipSale.Prostorije;
                }

                String dostupnost = Dostupnost.Text;
                if (dostupnost.Equals("Da"))
                {
                    this.sala.Dostupnost = true;
                }
                else if (dostupnost.Equals("Ne"))
                {
                    this.sala.Dostupnost = false;
                }


                if (upr.IzmenaSale(this.sala) == true)
                {
                    this.sale.Add(this.sala);
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
