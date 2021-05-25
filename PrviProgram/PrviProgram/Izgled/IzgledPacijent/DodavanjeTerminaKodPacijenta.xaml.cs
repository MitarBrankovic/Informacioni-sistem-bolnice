using Model;
using PrviProgram.Service;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PrviProgram.Izgled.IzgledPacijent
{
    /// <summary>
    /// Interaction logic for DodavanjeTerminaKodPacijenta.xaml
    /// </summary>
    public partial class DodavanjeTerminaKodPacijenta : Window
    {
        public ObservableCollection<Termin> termini;
        public Pacijent pacijent;
        public List<Lekar> lekari = new List<Lekar>();
        public DateTime minDatum = new DateTime();
        public DateTime maxDatum = new DateTime();
        public Lekar selektovaniLekar = new Lekar();
        public string selektovaniPrioritet;
        public DodavanjeTerminaKodPacijenta(ObservableCollection<Termin> termini, Pacijent pacijent)
        {
            InitializeComponent();

            this.termini = termini;
            this.pacijent = pacijent;
            lekarComboBox.ItemsSource = citanjeLekara();
            PostavljanjeButtonNaFalse();

        }
        public void PostavljanjeButtonNaFalse()
        {
            DoDatumText.IsEnabled = false;
            lekarComboBox.IsEnabled = false;
            PrioritetComboBox.IsEnabled = false;
            PotvrdiButton.IsEnabled = false;
        }
         
        public List<Lekar> citanjeLekara()
        {
            LekarRepository datotekaLekar = new LekarRepository();
            this.lekari = datotekaLekar.CitanjeIzFajla();
            return lekari;

        }
       

        private void PrioritetComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PrioritetComboBox.SelectedItem.Equals(""))
            {
                PotvrdiButton.IsEnabled = false;
            }
            else
            {
                
                PotvrdiButton.IsEnabled = true;
            }


        }

        private void lekarComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lekarComboBox.SelectedItem.Equals(""))
            {
                PrioritetComboBox.IsEnabled = false;
            }
            else
            {
                this.selektovaniLekar = (Lekar)lekarComboBox.SelectedItem;
                PrioritetComboBox.IsEnabled = true;
            }

        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {

            if(TerminiService.getInstance().ProveraZauzetostiKodLekara(minDatum,maxDatum,selektovaniLekar))
            {
                
                List<Termin> termini11 = new List<Termin>(TerminiService.getInstance().SviSlobodniTermini(minDatum, maxDatum, selektovaniLekar,(TipTermina)TipTerminaText.SelectedItem));
                TerminiService.getInstance().terminiSlobodni = new List<Termin>();
              
                IzborPregleda prozor = new IzborPregleda(termini11,termini,pacijent);
                prozor.Show();
                this.Close();
            }
            else
            {
                string selektovani = PrioritetComboBox.Text;
                if(selektovani.Equals("Lekar"))
                {
                    string tipTermina = TipTerminaText.Text;
                    List<Termin> termini11 = new List<Termin>(TerminiService.getInstance().ProveraVremenaKodLekara(minDatum, maxDatum, selektovaniLekar, (TipTermina)TipTerminaText.SelectedItem));
                    TerminiService.getInstance().terminiSlobodni = new List<Termin>();
               
                    IzborPregleda prozor = new IzborPregleda(termini11, termini, pacijent);
                    prozor.Show();
                    this.Close();
                }
                else
                {
                    string tipTermina = TipTerminaText.Text;
                    List<Termin> termini11 = new List<Termin>(TerminiService.getInstance().ProveraLekaraKodVremena(minDatum, maxDatum, selektovaniLekar, (TipTermina)TipTerminaText.SelectedItem));
                    TerminiService.getInstance().terminiSlobodni = new List<Termin>();
                    
                    IzborPregleda prozor = new IzborPregleda(termini11, termini, pacijent);
                    prozor.Show();
                    this.Close();


                }
              
            }

        }

        private void otkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OdDatumText_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if(OdDatumText.Text.Equals(""))
            {
                DoDatumText.IsEnabled = false;
            }
            else
            {
                this.minDatum =(DateTime) OdDatumText.SelectedDate;
                DoDatumText.IsEnabled = true;
            }
        }

        private void DoDatumText_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DoDatumText.Text.Equals(""))
            {
                lekarComboBox.IsEnabled = false;
            }
            else
            {
                this.maxDatum = (DateTime)DoDatumText.SelectedDate;
                lekarComboBox.IsEnabled = true;
            }

        }
    }
}
