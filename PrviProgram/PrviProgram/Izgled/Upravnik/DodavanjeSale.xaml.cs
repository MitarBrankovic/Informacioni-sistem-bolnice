using Logika.LogikaUpravnik;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
//using System.Windows.Forms;

namespace PrviProgram.Izgled.Upravnik
{
    /// <summary>
    /// Interaction logic for DodavanjeSale.xaml
    /// </summary>
    public partial class DodavanjeSale : Window
    {
        private Logika.LogikaUpravnik.UpravljanjeSalama upr;
        private ObservableCollection<Model.Sala> sale;

        public DodavanjeSale(ObservableCollection<Model.Sala> sale)
        {
            InitializeComponent();
            upr = new Logika.LogikaUpravnik.UpravljanjeSalama();
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
                else if (tip.Equals("Prostorija"))
                {
                    tempSala.Tip = TipSale.Prostorije;
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

                //UpravljanjeSalama.getInstance().DodavanjeSale(tempSala);

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
