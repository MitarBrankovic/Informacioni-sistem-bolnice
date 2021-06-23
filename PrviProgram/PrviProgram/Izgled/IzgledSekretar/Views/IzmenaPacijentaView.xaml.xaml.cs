using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Controller;
using Model;
using Repository;
using Service;

namespace PrviProgram.Izgled.IzgledSekretar.Views
{
    public partial class IzmenaPacijentaView : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private SekretarController sekretarController = new SekretarController();
        private DrzaveRepository drzaveRepository = new DrzaveRepository();
        private GradoviRepository gradoviRepository = new GradoviRepository();
        private UtilityService utilityService = new UtilityService();
        private ObservableCollection<Pacijent> pacijenti;
        private Pacijent pacijent;

        public IzmenaPacijentaView(ObservableCollection<Pacijent> pacijenti, Pacijent pacijent)
        {
            InitializeComponent();
            this.pacijenti = pacijenti;
            this.pacijent = pacijent;
            InicijalizacijaCombo();
            InicijalizacijaForme(pacijent);
            DataContext = this;
        }
        private void InicijalizacijaCombo()
        {
            textBoxMestoRodjenjaGrad.ItemsSource = gradoviRepository.PregledSvihGradova();
            textBoxGrad.ItemsSource = gradoviRepository.PregledSvihGradova();
            textBoxMestoRodjenjaDrzava.ItemsSource = drzaveRepository.PregledSvihDrzava();
            textBoxDrzava.ItemsSource = drzaveRepository.PregledSvihDrzava();
        }

        private void InicijalizacijaForme(Pacijent pacijent)
        {
            ime = pacijent.Ime;
            prezime = pacijent.Prezime;
            jmbg = pacijent.Jmbg;
            datePickerDatumRodjenja.SelectedDate = pacijent.DatumRodjenja;
            mestoRodjenjaGrad = pacijent.MestoRodjenja.Ime;
            mestoRodjenjaDrzava = pacijent.MestoRodjenja.drzava.Ime;
            ulica = pacijent.AdresaStanovanja.Ulica;
            broj = pacijent.AdresaStanovanja.Broj.ToString();
            sprat = pacijent.AdresaStanovanja.Sprat.ToString();
            stan = pacijent.AdresaStanovanja.Stan.ToString();
            mestoStanovanjaGrad = pacijent.AdresaStanovanja.grad.Ime;
            mestoStanovanjaDrzava = pacijent.AdresaStanovanja.grad.drzava.Ime;
            radioButtonPolM.IsChecked = pacijent.Pol.Equals(Pol.Muski);
            radioButtonPolZ.IsChecked = pacijent.Pol.Equals(Pol.Zenski);
            email = pacijent.Email;
            kontakt = pacijent.KontaktTelefon;
            korisnickoIme = pacijent.Korisnik.KorisnickoIme;
            textBoxLozinka.Password = pacijent.Korisnik.Lozinka;
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            Korisnik korisnik = new Korisnik(KorisnickoIme, textBoxLozinka.Password, TipKorisnika.Pacijent);

            Adresa adresaStanovanja = new Adresa(Ulica, utilityService.IntParser(Broj),
                                     utilityService.IntParser(Sprat),
                                     utilityService.IntParser(Stan),
                                     GradIzForme(MestoStanovanjaDrzava, MestoStanovanjaGrad));

            Osoba osoba = new Osoba(GradIzForme(MestoRodjenjaDrzava, MestoRodjenjaGrad),
                        korisnik, adresaStanovanja, Ime, Prezime, Email,
                        Jmbg, datePickerDatumRodjenja.SelectedDate.GetValueOrDefault(),
                        (bool)radioButtonPolM.IsChecked ? Pol.Muski : Pol.Zenski, Kontakt);

            Pacijent noviPacijent = new Pacijent(osoba, pacijent.GetTermin(), pacijent.KartonPacijenta);

            if (sekretarController.IzmenaPacijenta(pacijent, noviPacijent))
            {
                int index = pacijenti.IndexOf(pacijent);
                pacijenti.Remove(pacijent);
                pacijenti.Insert(index, noviPacijent);
                NavigationService.GoBack();
            }
        }

        private Grad GradIzForme(string drzava, string grad)
        {
            Drzava drzavaDTO = new Drzava(drzava);
            Grad gradDTO = new Grad(grad, drzavaDTO);
            sekretarController.DodavanjeDrzave(drzavaDTO);
            sekretarController.DodavanjeGrada(gradDTO);
            return gradDTO;
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void textBoxGrad_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Grad grad = (Grad)textBoxGrad.SelectedItem;
            if (grad != null)
            {
                textBoxDrzava.Text = grad.drzava.Ime;
            }
        }

        private void textBoxMestoRodjenjaGrad_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Grad grad = (Grad)textBoxMestoRodjenjaGrad.SelectedItem;
            if (grad != null)
            {
                textBoxMestoRodjenjaDrzava.Text = grad.drzava.Ime;
            }
        }

        public string Ime
        {
            get
            {
                return ime;
            }
            set
            {
                if (value != ime)
                {
                    ime = value;
                    OnPropertyChanged("Ime");
                }
            }
        }
        public string Prezime
        {
            get
            {
                return prezime;
            }
            set
            {
                if (value != prezime)
                {
                    prezime = value;
                    OnPropertyChanged("Prezime");
                }
            }
        }
        public string Jmbg
        {
            get
            {
                return jmbg;
            }
            set
            {
                if (value != jmbg)
                {
                    jmbg = value;
                    OnPropertyChanged("Jmbg");
                }
            }
        }
        public string MestoRodjenjaGrad
        {
            get
            {
                return mestoRodjenjaGrad;
            }
            set
            {
                if (value != mestoRodjenjaGrad)
                {
                    mestoRodjenjaGrad = value;
                    OnPropertyChanged("MestoRodjenjaGrad");
                }
            }
        }
        public string MestoRodjenjaDrzava
        {
            get
            {
                return mestoRodjenjaDrzava;
            }
            set
            {
                if (value != mestoRodjenjaDrzava)
                {
                    mestoRodjenjaDrzava = value;
                    OnPropertyChanged("MestoRodjenjaDrzava");
                }
            }
        }
        public string Ulica
        {
            get
            {
                return ulica;
            }
            set
            {
                if (value != ulica)
                {
                    ulica = value;
                    OnPropertyChanged("Ulica");
                }
            }
        }
        public string Broj
        {
            get
            {
                return broj;
            }
            set
            {
                if (value != broj)
                {
                    broj = value;
                    OnPropertyChanged("Broj");
                }
            }
        }
        public string Sprat
        {
            get
            {
                return sprat;
            }
            set
            {
                if (value != sprat)
                {
                    sprat = value;
                    OnPropertyChanged("Sprat");
                }
            }
        }
        public string Stan
        {
            get
            {
                return stan;
            }
            set
            {
                if (value != stan)
                {
                    stan = value;
                    OnPropertyChanged("Stan");
                }
            }
        }
        public string MestoStanovanjaGrad
        {
            get
            {
                return mestoStanovanjaGrad;
            }
            set
            {
                if (value != mestoStanovanjaGrad)
                {
                    mestoStanovanjaGrad = value;
                    OnPropertyChanged("MestoStanovanjaGrad");
                }
            }
        }
        public string MestoStanovanjaDrzava
        {
            get
            {
                return mestoStanovanjaDrzava;
            }
            set
            {
                if (value != mestoStanovanjaDrzava)
                {
                    mestoStanovanjaDrzava = value;
                    OnPropertyChanged("MestoStanovanjaDrzava");
                }
            }
        }
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                if (value != email)
                {
                    email = value;
                    OnPropertyChanged("Email");
                }
            }
        }
        public string Kontakt
        {
            get
            {
                return kontakt;
            }
            set
            {
                if (value != kontakt)
                {
                    kontakt = value;
                    OnPropertyChanged("Kontakt");
                }
            }
        }
        public string KorisnickoIme
        {
            get
            {
                return korisnickoIme;
            }
            set
            {
                if (value != korisnickoIme)
                {
                    korisnickoIme = value;
                    OnPropertyChanged("KorisnickoIme");
                }
            }
        }
        public string Lozinka
        {
            get
            {
                return lozinka;
            }
            set
            {
                if (value != lozinka)
                {
                    lozinka = value;
                    OnPropertyChanged("Lozinka");
                }
            }
        }

        private string ime;
        private string prezime;
        private string jmbg;
        private string mestoRodjenjaGrad;
        private string mestoRodjenjaDrzava;
        private string ulica;
        private string broj;
        private string sprat;
        private string stan;
        private string mestoStanovanjaGrad;
        private string mestoStanovanjaDrzava;
        private string email;
        private string kontakt;
        private string korisnickoIme;
        private string lozinka;

    }
}
