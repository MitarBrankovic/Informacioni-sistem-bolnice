using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Model;
using PrviProgram.Izgled.IzgledLekar.LekarWizard;
using PrviProgram.Izgled.IzgledPacijent;
using PrviProgram.Izgled.IzgledSekretar;
using PrviProgram.Izgled.IzgledUpravnik;
using PrviProgram.Repository;
using PrviProgram.Service;
using Repository;

namespace PrviProgram
{
    public partial class Logovanje : Window
    {
        private PacijentRepository pacijentRepository = new PacijentRepository();
        private UpravnikRepository upravnikRepository = new UpravnikRepository();
        private SekretarRepository sekretarRepository = new SekretarRepository();
        private PodesavanjaLekarService podesavanjaLekaraService = new PodesavanjaLekarService();
        private PodesavanjaLekarRepository podesavanjaLekarRepository = new PodesavanjaLekarRepository();
        private ILekarRepository lekarRepository = new LekarRepository();

        public Logovanje()
        {
            InitializeComponent();
            korisnickoImeText.Focus();
        }

        private void LogovanjeButton_Click(object sender, RoutedEventArgs e)
        {
            List<Korisnik> korisnici = UcitaKorisnike();
            foreach (Korisnik korisnik in korisnici)
            {
                if (korisnik.KorisnickoIme.Equals(korisnickoImeText.Text) && korisnik.Lozinka.Equals(sifraText.Password))
                {
                    UlogujSe(korisnik);
                    break;
                }
            }
            Close();
        }

        private void UlogujSe(Korisnik korisnik)
        {
            switch (korisnik.TipKorisnika)
            {
                case TipKorisnika.Pacijent:
                    Pacijent pacijent = pacijentRepository.CitanjeIzFajla().First(o => o.Korisnik.KorisnickoIme == korisnik.KorisnickoIme);
                    if (pacijent != null)
                    {
                        Glavni_prozor_pacijenta glavniProzor = new Glavni_prozor_pacijenta(pacijent);
                        glavniProzor.Show();
                    }
                    break;
                case TipKorisnika.Upravnik:
                    Upravnik upravnik = upravnikRepository.CitanjeIzFajla().First(o => o.Korisnik.KorisnickoIme == korisnik.KorisnickoIme);
                    if (upravnik != null)
                    {
                        PocetniProzor win = new PocetniProzor(upravnik);
                        win.Show();
                    }
                    break;
                case TipKorisnika.Sekretar:
                    Sekretar sekretar = sekretarRepository.CitanjeIzFajla().First(o => o.Korisnik.KorisnickoIme == korisnik.KorisnickoIme);
                    if (sekretar != null)
                    {
                        PocetniPrikaz win = new PocetniPrikaz(sekretar);
                        win.Show();
                        Aplikacija aplikacija = new Aplikacija(sekretar);
                        aplikacija.Show();
                    }
                    break;
                case TipKorisnika.Lekar:
                    Lekar lekar = lekarRepository.CitanjeIzFajla().First(o => o.Korisnik.KorisnickoIme == korisnik.KorisnickoIme);
                    if (lekar != null)
                    {
                        Izgled.IzgledLekar.PocetniPrikaz win = new Izgled.IzgledLekar.PocetniPrikaz(lekar);
                        win.Show();
                        ProveraDaLiJePrviPutUlogovan(lekar);
                    }
                    break;
            }
        }

        private void ProveraDaLiJePrviPutUlogovan(Lekar lekar)
        {
            if (!podesavanjaLekarRepository.PogledaoWizard(lekar))
            {
                WizardWindow wizardWindow = new WizardWindow();
                wizardWindow.Show();
                podesavanjaLekaraService.IzmenaWizardPodesavanja(lekar);
            }
        }

        private List<Korisnik> UcitaKorisnike()
        {
            List<Korisnik> korisnici = new List<Korisnik>();
            korisnici.AddRange(pacijentRepository.PregledSvihKorisnika());
            korisnici.AddRange(upravnikRepository.PregledSvihKorisnika());
            korisnici.AddRange(sekretarRepository.PregledSvihKorisnika());
            korisnici.AddRange(lekarRepository.PregledSvihKorisnika());
            return korisnici;
        }

    }
}