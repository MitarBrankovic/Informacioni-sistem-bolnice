using Model;
using PrviProgram.Repository;
using System;
using System.Collections.Generic;
using System.Windows;

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
            PacijentRepository datoteka = new PacijentRepository();
            List<Pacijent> pacijenti = datoteka.CitanjeIzFajla();

            foreach (Pacijent p in pacijenti)
            {
                if (p.Korisnik != null)
                {
                    if (p.Korisnik.KorisnickoIme.Equals(ime) && p.Korisnik.Lozinka.Equals(sifra))
                    {
                        PregledTermina win = PregledTermina.getInstance(p);
                        win.Show();
                        break;
                    }

                }

            }
            this.Close();
        }

    }
}