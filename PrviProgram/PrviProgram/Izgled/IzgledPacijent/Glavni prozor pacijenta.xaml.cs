using Model;
using PrviProgram.Repository;
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
using System.Windows.Threading;

namespace PrviProgram.Izgled.IzgledPacijent
{
    /// <summary>
    /// Interaction logic for Glavni_prozor_pacijenta.xaml
    /// </summary>
    public partial class Glavni_prozor_pacijenta : Window, INotifyPropertyChanged
    {
        private bool checker;
        public Pacijent pacijent = new Pacijent();
        public DispatcherTimer timerZaAntiTrollMehanizam;
        public DispatcherTimer timerZaOtkljucavanjeAntiTrollMehanizma;
        public AntiTrollRepository datotekaAnitTrollMehanizma = new AntiTrollRepository();
        public Glavni_prozor_pacijenta(Pacijent pacijent )
        {
            Checker = false;

            InitializeComponent();
            this.pacijent = pacijent;
            this.DataContext = this;
            Page pocetna = new PregledTermina(pacijent);
            InicijalizacijaTimera();
            InicijalizacijaTimeraZaOtkljucavanjeAntiTrollMehanizma();
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

        private void InicijalizacijaTimera()
        {
            timerZaAntiTrollMehanizam = new DispatcherTimer();
            timerZaAntiTrollMehanizam.Interval = TimeSpan.FromSeconds(1);
            timerZaAntiTrollMehanizam.Start();
            timerZaAntiTrollMehanizam.Tick += new EventHandler(ProveraDaLiJePacijentMaliciozan);
        }
        private void InicijalizacijaTimeraZaOtkljucavanjeAntiTrollMehanizma()
        {
            timerZaOtkljucavanjeAntiTrollMehanizma = new DispatcherTimer();
            timerZaOtkljucavanjeAntiTrollMehanizma.Interval = TimeSpan.FromSeconds(1);
            timerZaOtkljucavanjeAntiTrollMehanizma.Start();
            timerZaOtkljucavanjeAntiTrollMehanizma.Tick += new EventHandler(OtkljucavanjeAntiTrollMehanizma);
        }
        private void OtkljucavanjeAntiTrollMehanizma(object sender, EventArgs e)
        {
            List<AntiTrollPacijenta> antiTrollPacijenata = datotekaAnitTrollMehanizma.CitanjeIzFajla();
            foreach (AntiTrollPacijenta antiTrollPacijenta in antiTrollPacijenata)
            {
                DateTime noviDatum = antiTrollPacijenta.vremeBanovanja.AddDays(5);
                if (antiTrollPacijenta.daLiJeBanovan == true && noviDatum.Day.Equals(DateTime.Now.Day))
                {
                    PostavljanjeButtonaNaTrue();
                    antiTrollPacijenata.Remove(antiTrollPacijenta);
                    datotekaAnitTrollMehanizma.UpisivanjeUFajl(antiTrollPacijenata);
                    timerZaOtkljucavanjeAntiTrollMehanizma.Stop();
                    break;

                }

            }
        }

        private void ProveraDaLiJePacijentMaliciozan(object sender, EventArgs e)
        {
            List<AntiTrollPacijenta> antiTrollPacijenata = datotekaAnitTrollMehanizma.CitanjeIzFajla();
            foreach (AntiTrollPacijenta antiTrollPacijenta in antiTrollPacijenata)
            {
                if (antiTrollPacijenta.brojacDodavanihTermina >= 3 || antiTrollPacijenta.brojacIzmenenjenihTermina >= 3 || antiTrollPacijenta.brojacOtkazanihTermina >= 3 && antiTrollPacijenta.pacijent.Jmbg.Equals(pacijent.Jmbg))
                {
                    PostavljanjeButtonaNaFalse();
                    antiTrollPacijenata.Remove(antiTrollPacijenta);
                    upisivanjeUFajlMalicioznogPacijenta(antiTrollPacijenta, antiTrollPacijenata);
                    timerZaAntiTrollMehanizam.Stop();
                    break;
                }
            }

        }
        public void upisivanjeUFajlMalicioznogPacijenta(AntiTrollPacijenta antiTrollPacijenta, List<AntiTrollPacijenta> antiTrollPacijenata)
        {
            AntiTrollPacijenta antiTroll = new AntiTrollPacijenta(antiTrollPacijenta.brojacDodavanihTermina, antiTrollPacijenta.brojacIzmenenjenihTermina, antiTrollPacijenta.brojacOtkazanihTermina, antiTrollPacijenta.pacijent, DateTime.Now, true);
            antiTrollPacijenata.Add(antiTroll);
            datotekaAnitTrollMehanizma.UpisivanjeUFajl(antiTrollPacijenata);

        }

        public void PostavljanjeButtonaNaFalse()
        {
            pregledTerminaButton.IsEnabled = false;
            notifikacijeButton.IsEnabled = false;
            anketaButton.IsEnabled = false;
            izmenProfilaButton.IsEnabled = false;
            obavestenjaButton.IsEnabled = false;

        }
        public void PostavljanjeButtonaNaTrue()
        {
            pregledTerminaButton.IsEnabled = true;
            notifikacijeButton.IsEnabled = true;
            anketaButton.IsEnabled = true;
            izmenProfilaButton.IsEnabled = true;
            obavestenjaButton.IsEnabled = true;

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
