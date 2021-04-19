using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Model;
using PrviProgram.Izgled.IzgledPacijent;
using PrviProgram.Izgled.IzgledSekretar;
using Repository;

namespace PrviProgram
{
    public partial class Logovanje : Window
    {
        public Logovanje()
        {
            InitializeComponent();
        }

        private void LogovanjeButton_Click(object sender, RoutedEventArgs e)
        {
            string ime = korisnickoImeText.Text;
            string sifra = sifraText.Text;

            List<Korisnik> korisnici = new List<Korisnik>();
            PacijentRepository pacijentRepository = new PacijentRepository();
            List<Korisnik> pacijenti = pacijentRepository.PregledSvihKorisnika();
            UpravnikRepository upravnikRepository = new UpravnikRepository();
            List<Korisnik> upravnici = upravnikRepository.PregledSvihKorisnika();
            SekretarRepository sekretarRepository = new SekretarRepository();
            List<Korisnik> sekretari = sekretarRepository.PregledSvihKorisnika();
            LekarRepository lekarRepository = new LekarRepository();
            List<Korisnik> lekari = lekarRepository.PregledSvihKorisnika();

            korisnici.AddRange(pacijenti);
            korisnici.AddRange(upravnici);
            korisnici.AddRange(sekretari);
            korisnici.AddRange(lekari);


            foreach (Korisnik k in korisnici)
            {
                if (k.KorisnickoIme.Equals(ime) && k.Lozinka.Equals(sifra))
                {
                    if (k.TipKorisnika == TipKorisnika.Pacijent)
                    {
                        Pacijent pacijent = pacijentRepository.CitanjeIzFajla().First(o => o.Korisnik.KorisnickoIme == k.KorisnickoIme);
                        if (pacijent != null)
                        {
                            PregledTermina win = new PregledTermina(pacijent);
                            win.Show();
                        }
                        break;
                    }
                    else if (k.TipKorisnika == TipKorisnika.Upravnik)
                    {
                        Model.Upravnik upravnik = upravnikRepository.CitanjeIzFajla().First(o => o.Korisnik.KorisnickoIme == k.KorisnickoIme);
                        if (upravnik != null)
                        {
                            //PregledTermina win = PregledTermina.getInstance(pacijent);
                            //win.Show();
                        }
                        break;
                    }
                    else if (k.TipKorisnika == TipKorisnika.Sekretar)
                    {
                        Sekretar sekretar = sekretarRepository.CitanjeIzFajla().First(o => o.Korisnik.KorisnickoIme == k.KorisnickoIme);
                        if (sekretar != null)
                        {
                            PocetniPrikaz win = new PocetniPrikaz(sekretar);
                            win.Show();
                        }
                        break;
                    }
                    else if (k.TipKorisnika == TipKorisnika.Lekar)
                    {
                        Model.Lekar lekar = lekarRepository.CitanjeIzFajla().First(o => o.Korisnik.KorisnickoIme == k.KorisnickoIme);
                        if (lekar != null)
                        {
                            //PregledTermina win = PregledTermina.getInstance(pacijent);
                            //win.Show();
                        }
                        break;
                    }
                }
            }

            this.Close();
        }

    }
}