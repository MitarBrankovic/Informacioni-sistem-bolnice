using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using Controller;
using Model;
using Repository;
using Service;

namespace PrviProgram.Izgled.IzgledSekretar.IzgledPacijenti
{
    public partial class Registracija : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private string ime;
        private string preime;
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
                return preime;
            }
            set
            {
                if (value != preime)
                {
                    preime = value;
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

        private SekretarController sekretarController = new SekretarController();
        private DrzaveRepository drzaveRepository = new DrzaveRepository();
        private GradoviRepository gradoviRepository = new GradoviRepository();
        private UtilityService utilityService = new UtilityService();
        private ObservableCollection<Pacijent> pacijenti;

        public Registracija(ObservableCollection<Pacijent> pacijenti)
        {
            InitializeComponent();
            this.pacijenti = pacijenti;
            InicijalizacijaCombo();
            DataContext = this;
        }

        private void InicijalizacijaCombo()
        {
            textBoxMestoRodjenjaGrad.ItemsSource = gradoviRepository.PregledSvihGradova();
            textBoxGrad.ItemsSource = gradoviRepository.PregledSvihGradova();
            textBoxMestoRodjenjaDrzava.ItemsSource = drzaveRepository.PregledSvihDrzava();
            textBoxDrzava.ItemsSource = drzaveRepository.PregledSvihDrzava();
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            Korisnik korisnik = new Korisnik(textBoxKorisnickoIme.Text, textBoxLozinka.Password, TipKorisnika.Pacijent);

            Adresa adresaStanovanja = new Adresa(textBoxUlica.Text, utilityService.IntParser(textBoxBroj.Text),
                                                 utilityService.IntParser(textBoxSprat.Text),
                                                 utilityService.IntParser(textBoxStan.Text),
                                                 GradIzForme(textBoxDrzava.Text, textBoxGrad.Text));

            Osoba osoba = new Osoba(GradIzForme(textBoxMestoRodjenjaDrzava.Text, textBoxMestoRodjenjaGrad.Text),
                                    korisnik, adresaStanovanja, textBoxIme.Text, textBoxPrezime.Text, textBoxEmail.Text,
                                    textBoxJMBG.Text, datePickerDatumRodjenja.SelectedDate.GetValueOrDefault(),
                                    (bool)radioButtonPolM.IsChecked ? Pol.Muski : Pol.Zenski, textBoxKontaktTelefon.Text);

            Pacijent pacijent = new Pacijent(osoba, new List<Termin>(), new KartonPacijenta());

            if (sekretarController.DodavanjePacijenta(pacijent))
            {
                pacijenti.Add(pacijent);
                Close();
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
            Close();
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

    }
}