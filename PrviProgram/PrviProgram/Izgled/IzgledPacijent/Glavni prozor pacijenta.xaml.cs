using Model;
using Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PrviProgram.Izgled.IzgledPacijent
{
    /// <summary>
    /// Interaction logic for Glavni_prozor_pacijenta.xaml
    /// </summary>
    public partial class Glavni_prozor_pacijenta : Window, INotifyPropertyChanged
    {
        private bool checker;
        public Pacijent pacijent = new Pacijent();
        public Glavni_prozor_pacijenta(Pacijent pacijent )
        {
            Checker = false;

            InitializeComponent();
            this.pacijent = pacijent;
            this.DataContext = this;
            Page pocetna = new PregledTermina(pacijent);
            this.frame.NavigationService.Navigate(pocetna);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public bool Checker
        {
            get { return checker; }
            set
            {
                checker = value;
                OnPropertyChanged();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Page pocetna = new PregledTermina(pacijent);
            this.frame.NavigationService.Navigate(pocetna);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Page pocetna = new kreiranjeNotifikacije(pacijent);
            this.frame.NavigationService.Navigate(pocetna);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Page pocetna = new PregledTermina(pacijent);
            this.frame.NavigationService.Navigate(pocetna);
        }

        //anketa
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if(!AnketaService.getInstance().DaLiPostojiBarJedanIzvrsenTermin(pacijent))
            {
                MessageBox.Show("Ne možete da popunite anketu, jer nemate ni jedan izvršen pregled ili ste popunili sve ankete do sad");
            }
            else
            {
    
                Page pocetna = new AnketirajPacijenta(pacijent);
                this.frame.NavigationService.Navigate(pocetna);
            }
            
        }
    }
}
