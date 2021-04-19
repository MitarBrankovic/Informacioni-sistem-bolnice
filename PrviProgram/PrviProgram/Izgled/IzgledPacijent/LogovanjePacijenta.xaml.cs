using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace PrviProgram.Izgled.IzgledPacijent
{
    public partial class LogovanjePacijenta : Window
    {
        private static LogovanjePacijenta instance = null;

        public static LogovanjePacijenta getInstance()
        {
            if (instance == null)
            {

                instance = new LogovanjePacijenta();
            }
            return instance;
        }

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
                        PregledTermina win = new PregledTermina(p);
                        win.Show();
                        break;
                    }

                }

            }
            this.Close();
        }

    }
}