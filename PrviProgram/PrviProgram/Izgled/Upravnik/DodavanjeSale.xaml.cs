using Logika.LogikaUpravnik;
using Model;
using System;
using System.Collections.Generic;
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

namespace PrviProgram.Izgled.Upravnik
{
    /// <summary>
    /// Interaction logic for DodavanjeSale.xaml
    /// </summary>
    public partial class DodavanjeSale : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }


        public DodavanjeSale()
        {
            InitializeComponent();

        }


        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            Sala tempSala = new Sala();
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
                tempSala.Tip = TipSale.SalaSaKrevetrima;
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

            UpravljanjeSalama.getInstance().DodavanjeSale(tempSala);
            WindowUpravnik.getInstance().dataGridUpravnik.Items.Refresh();

            this.Hide();
        }
    }
}
