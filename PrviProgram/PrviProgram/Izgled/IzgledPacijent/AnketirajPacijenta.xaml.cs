using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Repository;
using PrviProgram.Repository;
using System.ComponentModel;
using System.Windows.Threading;
using Controller;

namespace PrviProgram.Izgled.IzgledPacijent
{

    public partial class AnketirajPacijenta : Page, INotifyPropertyChanged
    {
        public Pacijent pacijent = new Pacijent();
        public List<Termin> termini = new List<Termin>();
        public TerminiLekarController terminiLekar;
        public List<AnketiranjePacijenta> anketa = new List<AnketiranjePacijenta>();
        public AnketiranjePacijentaRepository datotekaAnkete = new AnketiranjePacijentaRepository();
        public BolnicaAnketiranjeRepository datotekaBolnica = new BolnicaAnketiranjeRepository();

        public event PropertyChangedEventHandler PropertyChanged;

        public Termin termin { get; set; }
        public Lekar lekar { get; set; }
        public string ocenaLekara { get; set; }
        public string primedbaNaLekara { get; set; }
        public string primedbaNaPregled { get; set; }
        public string ljubaznostLekara { get; set; }
        public string znanjeLekaraOIstorijiBolesti { get; set; }
        public string objasnjenjeLekara { get; set; }
        public DispatcherTimer timerZaOtkljcavanjeDugmetaAnketirajBolnicu;
        public DispatcherTimer timerZaZakljucavanjeDugmetaAnkeetirajBolnicu;



        public AnketirajPacijenta(Pacijent pacijent)
        {
            InitializeComponent();
            
            this.pacijent = pacijent;
            this.termini = TerminiZaAnketiranjeService.getInstance().SviTerminiKojiSuIzvrseni(pacijent);
            inicijalizacijaTimeraZaOtkljucavanjeDugmetaBolnica();
            inicijalizacijaTimeraZaZakljucavanjeDugmetaBolnica();



        }
        public void inicijalizacijaTimeraZaZakljucavanjeDugmetaBolnica()
        {
            timerZaZakljucavanjeDugmetaAnkeetirajBolnicu = new DispatcherTimer();
            timerZaZakljucavanjeDugmetaAnkeetirajBolnicu.Interval = TimeSpan.FromSeconds(1);
            timerZaZakljucavanjeDugmetaAnkeetirajBolnicu.Start();
            timerZaZakljucavanjeDugmetaAnkeetirajBolnicu.Tick += new EventHandler(ProveraDaLiTrebaOtkljucatiBolnicaAnketiraj);
        }
        public void inicijalizacijaTimeraZaOtkljucavanjeDugmetaBolnica()
        {
            timerZaOtkljcavanjeDugmetaAnketirajBolnicu = new DispatcherTimer();
            timerZaOtkljcavanjeDugmetaAnketirajBolnicu.Interval = TimeSpan.FromSeconds(1);
            timerZaOtkljcavanjeDugmetaAnketirajBolnicu.Start();
            timerZaOtkljcavanjeDugmetaAnketirajBolnicu.Tick += new EventHandler(OtkljucavanjeDugmetaAnketirajBolnicu);
        }
        public void PostavljanjeButtonaNaFalse()
        {
            LekarTextBlock.IsEnabled = false;
            ocenaLekaraComboBox.IsEnabled = false;
            primedbaNaLekaraComboBox.IsEnabled = false;
            primedbaNaPregledComboBox.IsEnabled = false;
            ljubaznostLekaraComboBox.IsEnabled = false;
            informacijeOBolestiPacijentaComboBox.IsEnabled = false;
            objasnjenjeNacinaLecenjaComboBox.IsEnabled = false;
            potvrdiAnketiranjeButton.IsEnabled = false;
            bolnicaAnketiranjeButton.IsEnabled = false;

        }
        public List<BolnicaAnketiranje> citanjeAnketeBolnice()
        {
            BolnicaAnketiranjeRepository datoteka = new BolnicaAnketiranjeRepository();
            List<BolnicaAnketiranje> ankete = datoteka.CitanjeIzFajla();
            return ankete;

        }
        public void OtkljucavanjeDugmetaAnketirajBolnicu(object sender, EventArgs e)
        {
            List<BolnicaAnketiranje> anketa = citanjeAnketeBolnice();
            if (anketa.Count != 0 && objasnjenjeNacinaLecenjaComboBox.SelectedItem!=null)
            {
                potvrdiAnketiranjeButton.IsEnabled = true;
                timerZaOtkljcavanjeDugmetaAnketirajBolnicu.Stop();
            }
        }
        public void ProveraDaLiTrebaOtkljucatiBolnicaAnketiraj(object sender, EventArgs e)
        {
            List<BolnicaAnketiranje> ankete = citanjeAnketeBolnice();
            
            if (ankete.Count!=0)
            {
                ProveraKadaJePoslednjiPutBolnicaAnketirana();
            }
            else
            {
                bolnicaAnketiranjeButton.IsEnabled = true;
                timerZaZakljucavanjeDugmetaAnkeetirajBolnicu.Stop();
            }
            timerZaZakljucavanjeDugmetaAnkeetirajBolnicu.Stop();
        }
        public void ProveraKadaJePoslednjiPutBolnicaAnketirana()
        {
            List<BolnicaAnketiranje> ankete = citanjeAnketeBolnice();
            foreach (BolnicaAnketiranje anketa in ankete)
            {
                if (DateTime.Now.Day.Equals(anketa.DatumANketiranja.AddDays(1).Day))
                {
                    bolnicaAnketiranjeButton.IsEnabled = true;
                    ankete.Remove(anketa);
                    datotekaBolnica.UpisivanjeUFajl(ankete);
                    break;
                }
            }

        }

        private void terminComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(terminComboBox.SelectedItem.Equals(""))
            {
                LekarTextBlock.IsEnabled = false;
            }
            else
            {
                this.termin = (Termin)terminComboBox.SelectedItem;
                this.lekar = terminiLekar.LekarKojiJeZaduzenZaTermin(this.termin);
                LekarTextBlock.Text = lekar.ToString();         
                ocenaLekaraComboBox.IsEnabled = true; 
            }
        }

        private void primedbaNaLekaraComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(primedbaNaLekaraComboBox.Text.Equals(""))
            {
                primedbaNaPregledComboBox.IsEnabled = false;
            }
            else
            {
                primedbaNaPregledComboBox.IsEnabled = true;
                this.primedbaNaLekara = primedbaNaLekaraComboBox.Text;
            }
        }

        private void primedbaNaPregledComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(primedbaNaPregledComboBox.Text.Equals(""))
            {
                ljubaznostLekaraComboBox.IsEnabled = false;
            }
            else
            {
                this.primedbaNaPregled = primedbaNaPregledComboBox.Text;
                ljubaznostLekaraComboBox.IsEnabled = true;

            }
        }

        private void ljubaznostLekaraComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ljubaznostLekaraComboBox.SelectedItem.Equals(""))
            {
                informacijeOBolestiPacijentaComboBox.IsEnabled = false;
            }
            else
            {
                informacijeOBolestiPacijentaComboBox.IsEnabled = true;
                this.ljubaznostLekara = ljubaznostLekaraComboBox.SelectedItem.ToString();

            }
        }

        private void informacijeOBolestiPacijentaComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(informacijeOBolestiPacijentaComboBox.SelectedItem.Equals(""))
            {
                objasnjenjeNacinaLecenjaComboBox.IsEnabled = false;
            }
            else
            {
                objasnjenjeNacinaLecenjaComboBox.IsEnabled = true;
                this.znanjeLekaraOIstorijiBolesti =informacijeOBolestiPacijentaComboBox.SelectedItem.ToString();
            }
        }

        private void objasnjenjeNacinaLecenjaComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(objasnjenjeNacinaLecenjaComboBox.SelectedItem.Equals(""))
            {
                potvrdiAnketiranjeButton.IsEnabled = false;
            }
            else
            {
               // potvrdiAnketiranjeButton.IsEnabled = true;
                this.objasnjenjeLekara = objasnjenjeNacinaLecenjaComboBox.SelectedItem.ToString();
            }
        }

        private void bolnicaAnketiranjeButton_Click(object sender, RoutedEventArgs e)
        {
            AnketiranjeBolnice window = new AnketiranjeBolnice();
            window.Show();
         
        }

        private void potvrdiAnketiranjeButton_Click(object sender, RoutedEventArgs e)
        {
            AnketiranjePacijenta anketiranjePacijenta = new AnketiranjePacijenta(this.termin, this.lekar, this.ocenaLekara, this.primedbaNaLekara, this.primedbaNaPregled, this.ljubaznostLekara, this.znanjeLekaraOIstorijiBolesti, this.objasnjenjeLekara);
            anketa = datotekaAnkete.CitanjeIzFajla();
            anketa.Add(anketiranjePacijenta);
            datotekaAnkete.UpisivanjeUFajl(anketa);
            PostavljanjeButtonaNaFalse();
            terminComboBox.IsEnabled = false;

        }

        private void otkaziAnketiranjeButton_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void ocenaLekaraComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ocenaLekaraComboBox.SelectedItem.Equals(""))
            {
                primedbaNaLekaraComboBox.IsEnabled = false;
            }
            else
            {
                this.ocenaLekara = ocenaLekaraComboBox.SelectedItem.ToString();
                primedbaNaLekaraComboBox.IsEnabled = true;

            }
        }
    }
}
