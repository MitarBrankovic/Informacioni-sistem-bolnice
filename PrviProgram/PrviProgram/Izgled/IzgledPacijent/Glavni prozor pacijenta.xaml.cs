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
        public BeleskaRepository datotekaBeleska = new BeleskaRepository();
        public DispatcherTimer timerZaNotifikacijuBeleska;
        public AntiTrollRepository datotekaAnitTrollMehanizma = new AntiTrollRepository();
        public List<Beleska> beleskeKojeSuUIstoVreme = new List<Beleska>();
        public DispatcherTimer timerZaPrikazNotifikacije;
        public Glavni_prozor_pacijenta(Pacijent pacijent )
        {
            Checker = false;

            InitializeComponent();
            this.pacijent = pacijent;
            this.DataContext = this;
            InicijalizacijaTimera();
            InicijalizacijaTimeraZaOtkljucavanjeAntiTrollMehanizma();
            InicijalizacijaTimeraZaNotifikacijuBeleska();
            
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
        private void InicijalizacijaTimeraZaNotifikacijuBeleska()
        {
            timerZaNotifikacijuBeleska = new DispatcherTimer();
            timerZaNotifikacijuBeleska.Interval = TimeSpan.FromSeconds(10000);
            timerZaNotifikacijuBeleska.Start();
            timerZaNotifikacijuBeleska.Tick += new EventHandler(ObavestenjeZaBelesku);
        }
    

        private void ObavestenjeZaBelesku(object sender, EventArgs e)
        {
            timerZaPrikazNotifikacije = new DispatcherTimer();
            timerZaPrikazNotifikacije.Interval = TimeSpan.FromSeconds(100000);
            int brojNotifikacija = 0;
            List<Beleska> beleske = datotekaBeleska.CitanjeIzFajla();
            foreach(Beleska beleska in beleske)
            {
                if(beleska.Jmbg.Equals(pacijent.Jmbg) && DateTime.Today>=beleska.PocetakStizanjaNotifikacije &&
                    DateTime.Today<=beleska.KrajStizanjaNotifikacije && DateTime.Now.Hour==Convert.ToDateTime(beleska.VremeObavestenja).Hour && 
                    DateTime.Now.Minute == Convert.ToDateTime(beleska.VremeObavestenja).Minute)
                {
                    this.beleskeKojeSuUIstoVreme.Add(beleska);
                    if (brojNotifikacija < 1)
                    {
                        brojNotifikacija++;
                        timerZaPrikazNotifikacije.Start();
                        timerZaPrikazNotifikacije.Tick += new EventHandler(OpisNotifikacijeBeleska);
                    }
                }
            }

        }
        private void OpisNotifikacijeBeleska(object sender, EventArgs e)
        {
            timerZaNotifikacijuBeleska.Stop();
            string noviOpisNotifikacije = "";
            foreach (Beleska beleska in beleskeKojeSuUIstoVreme)
            {
                noviOpisNotifikacije += beleska.OpisBeleske + " ";

            }
            MessageBox.Show(noviOpisNotifikacije);
            timerZaPrikazNotifikacije.Stop();
            timerZaNotifikacijuBeleska.Stop();
            timerZaNotifikacijuBeleska.Interval = TimeSpan.FromMinutes(1);
            timerZaNotifikacijuBeleska.Start();

        }


        private void InicijalizacijaTimera()
        {
            timerZaAntiTrollMehanizam = new DispatcherTimer();
            timerZaAntiTrollMehanizam.Interval = TimeSpan.FromSeconds(10000);
            timerZaAntiTrollMehanizam.Start();
            timerZaAntiTrollMehanizam.Tick += new EventHandler(ProveraDaLiJePacijentMaliciozan);
        }
        private void InicijalizacijaTimeraZaOtkljucavanjeAntiTrollMehanizma()
        {
            timerZaOtkljucavanjeAntiTrollMehanizma = new DispatcherTimer();
            timerZaOtkljucavanjeAntiTrollMehanizma.Interval = TimeSpan.FromSeconds(10000);
            timerZaOtkljucavanjeAntiTrollMehanizma.Start();
            timerZaOtkljucavanjeAntiTrollMehanizma.Tick += new EventHandler(OtkljucavanjeAntiTrollMehanizma);
        }
        private void OtkljucavanjeAntiTrollMehanizma(object sender, EventArgs e)
        {
            List<AntiTrollPacijenta> antiTrollPacijenata = datotekaAnitTrollMehanizma.CitanjeIzFajla();
            foreach (AntiTrollPacijenta antiTrollPacijenta in antiTrollPacijenata)
            {
               if (antiTrollPacijenta.DaLiJeBanovan == true && antiTrollPacijenta.VremeBanovanja.AddDays(5).Date.Equals(DateTime.Now.Date))
                {
                    antiTrollPacijenata.Remove(antiTrollPacijenta);
                    OtkljucavanjeSistema(antiTrollPacijenata);
                    break;
                }
            }
        }

        public void OtkljucavanjeSistema(List<AntiTrollPacijenta> antiTrollPacijenata)
        {
            PostavljanjeButtonaNaTrue();
            datotekaAnitTrollMehanizma.UpisivanjeUFajl(antiTrollPacijenata);
            timerZaOtkljucavanjeAntiTrollMehanizma.Stop();
        }

        private void ProveraDaLiJePacijentMaliciozan(object sender, EventArgs e)
        {
            List<AntiTrollPacijenta> antiTrollPacijenata = datotekaAnitTrollMehanizma.CitanjeIzFajla();
            foreach (AntiTrollPacijenta antiTrollPacijenta in antiTrollPacijenata)
            {
                if ((antiTrollPacijenta.BrojacDodavanihTermina >= 3 || antiTrollPacijenta.BrojacIzmenenjenihTermina >= 3 || antiTrollPacijenta.BrojacOtkazanihTermina >= 3 )&& antiTrollPacijenta.pacijent.Jmbg.Equals(pacijent.Jmbg) && antiTrollPacijenta.DaLiJeBanovan==false)
                {
                    antiTrollPacijenata.Remove(antiTrollPacijenta);
                    UpisivanjeUFajlMalicioznogPacijentaIZakljucavanjeSistema(antiTrollPacijenta, antiTrollPacijenata);
                    break;
                }
                 if(antiTrollPacijenta.BrojacDodavanihTermina >= 3 || antiTrollPacijenta.BrojacIzmenenjenihTermina >= 3 || antiTrollPacijenta.BrojacOtkazanihTermina >= 3 && antiTrollPacijenta.pacijent.Jmbg.Equals(pacijent.Jmbg) && antiTrollPacijenta.DaLiJeBanovan == true)
                 {
                    PostavljanjeButtonaNaFalse();
                }            
            }
        }
        public void UpisivanjeUFajlMalicioznogPacijentaIZakljucavanjeSistema(AntiTrollPacijenta antiTrollPacijenta, List<AntiTrollPacijenta> antiTrollPacijenata)
        {
            PostavljanjeButtonaNaFalse();
            AntiTrollPacijenta antiTroll = new AntiTrollPacijenta(antiTrollPacijenta.BrojacDodavanihTermina, antiTrollPacijenta.BrojacIzmenenjenihTermina, antiTrollPacijenta.BrojacOtkazanihTermina, antiTrollPacijenta.pacijent, DateTime.Now, true);
            antiTrollPacijenata.Add(antiTroll);
            datotekaAnitTrollMehanizma.UpisivanjeUFajl(antiTrollPacijenata);
            timerZaAntiTrollMehanizam.Stop();
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
            if(!TerminiZaAnketiranjeService.getInstance().DaLiPostojiBarJedanIzvrsenTermin(pacijent))
            {
                MessageBox.Show("Ne možete da popunite anketu, jer nemate ni jedan izvršen pregled ili ste popunili sve ankete do sad");
            }
            else
            {
    
                Page pocetna = new AnketirajPacijenta(pacijent);
                this.frame.NavigationService.Navigate(pocetna);
            }
            
        }

        private void izmenProfilaButton_Click(object sender, RoutedEventArgs e)
        {
            Page pocetna = new PregledAnamneze(pacijent);
            this.frame.NavigationService.Navigate(pocetna);
        }

        private void obavestenjaButton_Click(object sender, RoutedEventArgs e)
        {
            Page pocetna = new TerapijaPage(pacijent);
            this.frame.NavigationService.Navigate(pocetna);

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            this.Close();
            Logovanje win = new Logovanje();
            win.Show();

        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Page pocetna = new PovratneInformacijePage(pacijent);
            this.frame.NavigationService.Navigate(pocetna);
        }
    }
}
