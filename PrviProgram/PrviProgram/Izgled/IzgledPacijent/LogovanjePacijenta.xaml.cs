using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Model;
using RadSaDatotekama;

namespace PrviProgram.Izgled.IzgledPacijent
{
    /// <summary>
    /// Interaction logic for LogovanjePacijenta.xaml
    /// </summary>
    public partial class LogovanjePacijenta : Window
    {
        public LogovanjePacijenta()
        {
            InitializeComponent();
        }

        private void LogovanjeButton_Click(object sender, RoutedEventArgs e)
        {


            string ime = korisnickoImeText.Text;

            Console.WriteLine(ime);
            string sifra = sifraText.Text;
            Console.WriteLine(sifra);
            DatotekaPacijent datoteka = new DatotekaPacijent();
            List<Pacijent> pacijenti = datoteka.CitanjeIzFajla();

            foreach (Pacijent p in pacijenti)
            {
                if (p.Korisnik != null)
                {
                    if (p.Korisnik.KorisnickoIme.Equals(ime) && p.Korisnik.Lozinka.Equals(sifra))
                    {
                        PregledTermina win = PregledTermina.getInstance(p);
                        win.Show();
                    }

                }

            }

        }

    }
}