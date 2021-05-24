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
        public DateTime datumTermina { get; set; }

        public DateTime trenutnoVreme { get; set; }
        public DateTime vremeTermina { get; set; }
        public DateTime vremenotifikacije { get; set; }

        ObservableCollection<Termin> termini { get; set; }

        public NotifikacijeObavestenjaRepository not = new NotifikacijeObavestenjaRepository();
        public List<NotifikacijePacijenta> notifikacije = new List<NotifikacijePacijenta>();
        public List<NotifikacijePacijenta> trenutna = new List<NotifikacijePacijenta>();
        public DispatcherTimer timer;
        public DispatcherTimer timer1;
        public DispatcherTimer timerZaAntiTrollMehanizam;
        public AntiTrollRepository datotekaAnitTrollMehanizma = new AntiTrollRepository();
        public DispatcherTimer timerZaOtkljucavanjeAntiTrollMehanizma;


        public PregledTermina(Pacijent p)
        {

            InitializeComponent();
            this.notifikacije = not.CitanjeIzFajla();
            this.pacijent = p;
            termini = new ObservableCollection<Termin>(TerminiService.getInstance().PregledTermina(p));

            timer1 = new DispatcherTimer();
            timer1.Interval = TimeSpan.FromSeconds(1);
            timer1.Start();
            timer1.Tick += new EventHandler(timer_Tick);
            InicijalizacijaTimera();
            InicijalizacijaTimeraZaOtkljucavanjeAntiTrollMehanizma();

            dataGridPacijenta.ItemsSource = termini;

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
              
                if (antiTrollPacijenta.daLiJeBanovan == true && antiTrollPacijenta.vremeBanovanja.AddDays(5).Day.Equals(DateTime.Now.Day))
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
                if(antiTrollPacijenta.brojacDodavanihTermina>=3 || antiTrollPacijenta.brojacIzmenenjenihTermina>=3 || antiTrollPacijenta.brojacOtkazanihTermina>=3 && antiTrollPacijenta.pacijent.Jmbg.Equals(pacijent.Jmbg) && antiTrollPacijenta.daLiJeBanovan==true)
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
        private void timer_Tick(object sender, EventArgs e)
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            int tim = 0;
            foreach (NotifikacijePacijenta n in this.notifikacije)
            {

                if (n.pacijent.Jmbg.Equals(this.pacijent.Jmbg))
                {
                    if (DateTime.Today >= n.PocetakDatuma && DateTime.Today <= n.KrajDatuma)
                    {
                        this.vremenotifikacije = Convert.ToDateTime(n.vremeObavestenja);

                        if ((DateTime.Now.Hour == vremenotifikacije.Hour) && (DateTime.Now.Minute == vremenotifikacije.Minute))
                        {
                            this.trenutna.Add(n);
                            if (tim <1)
                            {
                                timer.Start();
                                timer.Tick += new EventHandler(timer_Tick1);
                                ++tim;
                               // timer1.Stop();
                            }
                        }
                   
                    }
                    else
                    {
                        timer1.Stop();
                    }
                }
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void timer_Tick1(object sender, EventArgs e)
        {
            timer1.Stop();
            string nova="";
            foreach (NotifikacijePacijenta n in trenutna)
            {
                nova += n.opisNotifikacije + " ";
                
            }
            MessageBox.Show(nova);
            timer.Stop();
            timer1.Interval = TimeSpan.FromMinutes(1);
            timer1.Start();
            
        }
     
        private Pacijent pacijent;
        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            DodavanjeTerminaKodPacijenta win = new DodavanjeTerminaKodPacijenta(termini, pacijent);
            win.Show();
        }

        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridPacijenta.SelectedIndex != -1)
            {
                Termin t = (Termin)dataGridPacijenta.SelectedItem;
                var s = new IzmenaTermina(t, pacijent, termini);
                s.Show();
            }
        }

        private void Izbrisi_Click(object sender, RoutedEventArgs e)
        {
            if ((Model.Termin) dataGridPacijenta.SelectedItem != null)
            {
                TerminiService.getInstance().BrisanjeTermina((Model.Termin)dataGridPacijenta.SelectedItem);
                //PreglediService.getInstance().BrisanjePregleda(((Model.Termin)dataGridPacijenta.SelectedItem).SifraTermina);
                termini.Remove((Model.Termin)dataGridPacijenta.SelectedItem);
                AntiTrollService.getInstance().PovecavanjeBrojacaPriOtkazivanjuTermina(pacijent);
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
                this.trenutniDatum = DateTime.Now;
                this.datumTermina = selektovaniTermin.Datum;
                this.vremeTermina = Convert.ToDateTime(selektovaniTermin.Vreme);
                this.trenutnoVreme = DateTime.Now;
                DateTime ceoDatumTermina = new DateTime(datumTermina.Year, datumTermina.Month, datumTermina.Day, vremeTermina.Hour, vremeTermina.Minute, vremeTermina.Second);
                DateTime ceoTrenutniDatum = new DateTime(trenutniDatum.Year, trenutniDatum.Month, trenutniDatum.Day, trenutnoVreme.Hour, trenutnoVreme.Minute, trenutnoVreme.Second);
                TimeSpan razlika = ceoDatumTermina - ceoTrenutniDatum;
                if (razlika.TotalDays>1)
                {

                    var s = new PomeranjeZakazanogTermina(selektovaniTermin, pacijent, termini);
                    s.Show();
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