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

namespace PrviProgram.Izgled.IzgledPacijent
{

    public partial class AnketirajPacijenta : Page, INotifyPropertyChanged
    {
        public Pacijent pacijent = new Pacijent();
        public List<Termin> termini = new List<Termin>();
        public List<AnketiranjePacijenta> anketa = new List<AnketiranjePacijenta>();
        public AnketiranjePacijentaRepository datotekaAnkete = new AnketiranjePacijentaRepository();

        public event PropertyChangedEventHandler PropertyChanged;

        public Termin termin { get; set; }
        public Lekar lekar { get; set; }
        public string ocenaLekara { get; set; }
        public string primedbaNaLekara { get; set; }
        public string primedbaNaPregled { get; set; }
        public string ljubaznostLekara { get; set; }
        public string znanjeLekaraOIstorijiBolesti { get; set; }
        public string objasnjenjeLekara { get; set; }
        public DispatcherTimer timer;
        public DispatcherTimer timer1;



        public AnketirajPacijenta(Pacijent pacijent)
        {
            InitializeComponent();
            
            this.pacijent = pacijent;
            this.termini = TerminiService.getInstance().sviTerminiKojiSuIzvrseni(pacijent);
 
            terminComboBox.ItemsSource = termini;
            LekarTextBlock.IsEnabled = false;
            ocenaLekaraComboBox.IsEnabled = false;
            primedbaNaLekaraComboBox.IsEnabled = false;
            primedbaNaPregledComboBox.IsEnabled = false;
            ljubaznostLekaraComboBox.IsEnabled = false;
            informacijeOBolestiPacijentaComboBox.IsEnabled = false;
            objasnjenjeNacinaLecenjaComboBox.IsEnabled = false;
            potvrdiAnketiranjeButton.IsEnabled = false;
            bolnicaAnketiranjeButton.IsEnabled = false;

            timer1 = new DispatcherTimer();
            timer1.Interval = TimeSpan.FromSeconds(1);
            timer1.Start();
            timer1.Tick += new EventHandler(timer_Tick);

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Start();
            timer.Tick += new EventHandler(timer_Tick1);


        }
        public List<BolnicaAnketiranje> citanjeAnketeBolnice()
        {
            BolnicaAnketiranjeRepository datoteka = new BolnicaAnketiranjeRepository();
            List<BolnicaAnketiranje> ankete = datoteka.CitanjeIzFajla();
            return ankete;

        }
        public void timer_Tick1(object sender, EventArgs e)
        {
            List<BolnicaAnketiranje> anketa = citanjeAnketeBolnice();
            if (anketa.Count != 0 && objasnjenjeNacinaLecenjaComboBox.SelectedItem!=null)
            {
                potvrdiAnketiranjeButton.IsEnabled = true;
                timer.Stop();
            }
        }
        public void timer_Tick(object sender, EventArgs e)
        {
            List<BolnicaAnketiranje> ankete = citanjeAnketeBolnice();
            BolnicaAnketiranjeRepository datoteka = new BolnicaAnketiranjeRepository();
            if (ankete.Count!=0)
            {
                foreach (BolnicaAnketiranje anketa in ankete)
                {
                    if (DateTime.Now.Day.Equals(anketa.datumANketiranja.AddDays(1).Day))
                    {
                        bolnicaAnketiranjeButton.IsEnabled = true;
                        ankete.Remove(anketa);
                        datoteka.UpisivanjeUFajl(ankete);
                      

                        //timer1.Interval = TimeSpan.FromDays(60);
                        break;
                    }
                }
            }
            else
            {
                bolnicaAnketiranjeButton.IsEnabled = true;
                timer1.Stop();
            }
            timer1.Stop();
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
                this.lekar = TerminiService.getInstance().lekarKojiJeZaduzenZaTermin(this.termin);
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

            Model.AnketiranjePacijenta anketiranjePacijenta = new Model.AnketiranjePacijenta(this.termin, this.lekar, this.ocenaLekara, this.primedbaNaLekara, this.primedbaNaPregled, this.ljubaznostLekara, this.znanjeLekaraOIstorijiBolesti, this.objasnjenjeLekara);
            anketa = datotekaAnkete.CitanjeIzFajla();
            anketa.Add(anketiranjePacijenta);
            datotekaAnkete.UpisivanjeUFajl(anketa);
            potvrdiAnketiranjeButton.IsEnabled = false;
            LekarTextBlock.IsEnabled = false;
            ocenaLekaraComboBox.IsEnabled = false;
            primedbaNaLekaraComboBox.IsEnabled = false;
            primedbaNaPregledComboBox.IsEnabled = false;
            ljubaznostLekaraComboBox.IsEnabled = false;
            informacijeOBolestiPacijentaComboBox.IsEnabled = false;
            objasnjenjeNacinaLecenjaComboBox.IsEnabled = false;
            otkaziAnketiranjeButton.IsEnabled = false;
            bolnicaAnketiranjeButton.IsEnabled = false;
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
