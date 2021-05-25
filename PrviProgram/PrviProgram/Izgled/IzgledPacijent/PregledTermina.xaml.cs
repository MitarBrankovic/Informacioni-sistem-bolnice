using Controller;
using Model;
using PrviProgram.Repository;
using PrviProgram.Service;
using Repository;
using Service;
using Service.LekarService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace PrviProgram.Izgled.IzgledPacijent
{
    public partial class PregledTermina : Page
    {
        public DateTime trenutniDatum { get; set; }
        public DateTime selektovaniTermin { get; set; }
        public DateTime trenutnoVreme { get; set; }
        public DateTime vremeTermina { get; set; }
        public DateTime vremenotifikacije { get; set; }

        public ObservableCollection<Termin> termini { get; set; }
        public NotifikacijeObavestenjaRepository notifikacijeDatoteka = new NotifikacijeObavestenjaRepository();
        public List<NotifikacijePacijenta> notifikacije = new List<NotifikacijePacijenta>();
        public List<NotifikacijePacijenta> notifikacijeKojeSuUIstoVreme = new List<NotifikacijePacijenta>();
        public DispatcherTimer timerZaPrikazNotifikacije;
        public DispatcherTimer timerZaNotifikacijuLeka;
        public DispatcherTimer timerZaAntiTrollMehanizam;
        public AntiTrollRepository datotekaAnitTrollMehanizma = new AntiTrollRepository();
        public DispatcherTimer timerZaOtkljucavanjeAntiTrollMehanizma;
        public PacijentControler pacijentController = new PacijentControler();


        public PregledTermina(Pacijent p)
        {
            InitializeComponent();
            this.notifikacije = notifikacijeDatoteka.CitanjeIzFajla();
            this.pacijent = p;
            termini = new ObservableCollection<Termin>(pacijentController.PregledTermina(p));
            InicijalizacijaTimeraZaNotifikacijuLeka();
            InicijalizacijaTimera();
            InicijalizacijaTimeraZaOtkljucavanjeAntiTrollMehanizma();
            dataGridPacijenta.ItemsSource = termini;
        }
        private void InicijalizacijaTimeraZaNotifikacijuLeka()
        {
            timerZaNotifikacijuLeka = new DispatcherTimer();
            timerZaNotifikacijuLeka.Interval = TimeSpan.FromSeconds(10000);
            timerZaNotifikacijuLeka.Start();
            timerZaNotifikacijuLeka.Tick += new EventHandler(ProveravaDaLiTrenutnoImaNotifikacija);
        }
        private void InicijalizacijaTimera()
        {
            timerZaAntiTrollMehanizam = new DispatcherTimer();
            timerZaAntiTrollMehanizam.Interval = TimeSpan.FromSeconds(1000);
            timerZaAntiTrollMehanizam.Start();
            timerZaAntiTrollMehanizam.Tick += new EventHandler(ProveraDaLiJePacijentMaliciozan);
        }

        private void InicijalizacijaTimeraZaOtkljucavanjeAntiTrollMehanizma()
        {
            timerZaOtkljucavanjeAntiTrollMehanizma = new DispatcherTimer();
            timerZaOtkljucavanjeAntiTrollMehanizma.Interval = TimeSpan.FromSeconds(1000);
            timerZaOtkljucavanjeAntiTrollMehanizma.Start();
            timerZaOtkljucavanjeAntiTrollMehanizma.Tick += new EventHandler(OtkljucavanjeAntiTrollMehanizma);
        }
        private void OtkljucavanjeAntiTrollMehanizma(object sender, EventArgs e)
        {
            List<AntiTrollPacijenta> antiTrollPacijenata = datotekaAnitTrollMehanizma.CitanjeIzFajla();
            foreach (AntiTrollPacijenta antiTrollPacijenta in antiTrollPacijenata)
            {
                if (antiTrollPacijenta.DaLiJeBanovan == true && antiTrollPacijenta.VremeBanovanja.AddDays(5).Day.Equals(DateTime.Now.Day))
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
            foreach(AntiTrollPacijenta antiTrollPacijenta in antiTrollPacijenata)
            {
                if(antiTrollPacijenta.BrojacDodavanihTermina>=3 || antiTrollPacijenta.BrojacIzmenenjenihTermina>=3 || antiTrollPacijenta.BrojacOtkazanihTermina>=3 && antiTrollPacijenta.pacijent.Jmbg.Equals(pacijent.Jmbg) && antiTrollPacijenta.DaLiJeBanovan==true)
                {
                    PostavljanjeButtonaNaFalse();
                    timerZaAntiTrollMehanizam.Stop();
                }
            }
        }
        public void PostavljanjeButtonaNaTrue()
        {
            DodajTerminButton.IsEnabled = true;
            IzmeniTerminButton.IsEnabled = true;
            IzbrisiButton.IsEnabled = true;
            PomeranjeZakazanogTerminaButton.IsEnabled = true;
        }

        public void PostavljanjeButtonaNaFalse()
        {
            DodajTerminButton.IsEnabled = false;
            IzmeniTerminButton.IsEnabled = false;
            IzbrisiButton.IsEnabled = false;
            PomeranjeZakazanogTerminaButton.IsEnabled = false;
        }
        private void ProveravaDaLiTrenutnoImaNotifikacija(object sender, EventArgs e)
        {
            timerZaPrikazNotifikacije = new DispatcherTimer();
            timerZaPrikazNotifikacije.Interval = TimeSpan.FromSeconds(1);
            int brojNotifikacija = 0;
            foreach (NotifikacijePacijenta notifikacija in this.notifikacije)
            {
                if (notifikacija.Pacijent.Jmbg.Equals(this.pacijent.Jmbg) && DateTime.Today >= notifikacija.PocetakDatuma 
                    && DateTime.Today <= notifikacija.KrajDatuma && (DateTime.Now.Hour == Convert.ToDateTime(notifikacija.VremeObavestenja).Hour) 
                    && (DateTime.Now.Minute == Convert.ToDateTime(notifikacija.VremeObavestenja).Minute))
                {
                    this.notifikacijeKojeSuUIstoVreme.Add(notifikacija);
                    if (brojNotifikacija < 1)
                    {
                        timerZaPrikazNotifikacije.Start();
                        timerZaPrikazNotifikacije.Tick += new EventHandler(PrikazNotifikacijeZaLek);
                        ++brojNotifikacija;
                    }
                }
            } 
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void PrikazNotifikacijeZaLek(object sender, EventArgs e)
        {
            timerZaNotifikacijuLeka.Stop();
            string opisNotifikacije="";
            foreach (NotifikacijePacijenta notifikacija in notifikacijeKojeSuUIstoVreme)
            {
                opisNotifikacije += notifikacija.OpisNotifikacije + " ";
                
            }
            MessageBox.Show(opisNotifikacije);
            timerZaPrikazNotifikacije.Stop();
            timerZaNotifikacijuLeka.Interval = TimeSpan.FromMinutes(1);
            timerZaNotifikacijuLeka.Start();
            
        }
     
        private Pacijent pacijent;
        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            DodavanjeTerminaKodPacijenta prozor = new DodavanjeTerminaKodPacijenta(termini, pacijent);
            prozor.Show();
        }

        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridPacijenta.SelectedIndex != -1)
            {
                Termin termin = (Termin)dataGridPacijenta.SelectedItem;
                IzmenaTermina prozor = new IzmenaTermina(termin, pacijent, termini);
                prozor.Show();
            }
        }

        private void Izbrisi_Click(object sender, RoutedEventArgs e)
        {
            if ((Model.Termin) dataGridPacijenta.SelectedItem != null)
            {
                pacijentController.BrisanjeTermina((Model.Termin)dataGridPacijenta.SelectedItem);
                termini.Remove((Model.Termin)dataGridPacijenta.SelectedItem);
                pacijentController.PovecavanjeBrojacaPriOtkazivanjuTermina(pacijent);
            }
            else
            {
                MessageBox.Show("Niste selektovali termin koji zelite da izbrisete!!");
            }

        }

        private void Pomeraj_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridPacijenta.SelectedIndex != -1)
            {
                Termin selektovaniTermin = (Termin)dataGridPacijenta.SelectedItem;
                DateTime ceoDatumTermina = new DateTime(selektovaniTermin.Datum.Year,selektovaniTermin.Datum.Month
                    ,selektovaniTermin.Datum.Day, Convert.ToDateTime(selektovaniTermin.Vreme).Hour,
                    Convert.ToDateTime(selektovaniTermin.Vreme).Minute, Convert.ToDateTime(selektovaniTermin.Vreme).Second);
                DateTime ceoTrenutniDatum = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 
                    DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                if ((ceoDatumTermina - ceoTrenutniDatum).TotalDays>1)
                {
                    PomeranjeZakazanogTermina prozor = new PomeranjeZakazanogTermina(selektovaniTermin, pacijent, termini);
                    prozor.Show();
                }
                else
                {
                    MessageBox.Show("NE MOZETE DA POMERITE OVAJ DATUM",
              "Error");
                }
            }

        }
    }


}