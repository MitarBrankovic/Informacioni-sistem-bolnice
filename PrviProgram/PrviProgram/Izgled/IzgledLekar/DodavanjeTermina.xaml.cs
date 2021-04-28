using Model;
using PrviProgram.Repository;
using Repository;
using Service;
using Service.LekarService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
//using Logika.LogikaSekretar;

namespace PrviProgram.Izgled.IzgledLekar
{
    /// <summary>
    /// Interaction logic for DodavanjeTermina.xaml
    /// </summary>
    public partial class DodavanjeTermina : Window
    {

        private PreglediService upr;
        private PacijentiService uprPac;
        private SaleService uprSal;
        private SalaRepository saleRep;
        private ObservableCollection<Termin> termini;
        private PacijentRepository pacijentRepository= new PacijentRepository();
        private SalaRepository salaRepository = new SalaRepository();
        private TerminiService terminiService = new TerminiService();
        private Lekar lekar;

        public DodavanjeTermina(ObservableCollection<Termin> termini, Lekar lekar)
        {
            InitializeComponent();
            this.lekar = lekar;
            upr = new PreglediService();
            uprPac = new PacijentiService();
            uprSal = new SaleService();
            saleRep = new SalaRepository();
            this.termini = termini;
            ComboboxPacijent.ItemsSource = pacijentRepository.PregledSvihPacijenata();
            ComboboxSala.ItemsSource = salaRepository.PregledSvihSala();

        }

        private bool datum = false;
        private bool vreme = false;

        private void datum_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var datum1 = sender as DatePicker;

            if (datum1 == null)
            {
                datum = false;
            }
            else
            {
                datum = true;
            }

        }

        private void vreme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var vreme1 = sender as ComboBox;
            if (vreme1 == null)
            {
                vreme = false;
            }
            else
            {
                vreme = true;
            }

        }
        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            Termin tempTermin = new Termin();
            tempTermin.lekar = lekar;
            Sala tempSala = new Sala();
            tempSala = (Sala)ComboboxSala.SelectedItem;
            tempTermin.sala = tempSala;
            
            Model.Pacijent tempPacijent = new Model.Pacijent();
            tempPacijent = (Pacijent)ComboboxPacijent.SelectedItem;
            tempTermin.pacijent = tempPacijent;

            tempTermin.Vreme = vremeText.Text;
            tempTermin.Datum = (DateTime)(DatumText.SelectedDate);


            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var Random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[Random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            tempTermin.SifraTermina = finalString;


            String tip = TipTerm.Text;
            if (tip.Equals("Pregled"))
            {
                tempTermin.TipTermina = TipTermina.Pregled;
            }
            else if (tip.Equals("Operacija"))
            {
                tempTermin.TipTermina = TipTermina.Operacija;
            }
            else if (tip.Equals("Kontrola"))
            {
                tempTermin.TipTermina = TipTermina.Kontrola;
            }

            bool vecPostoji = false;

            foreach (Termin t in this.termini)
            {
                if (tempTermin.sala.Sifra.Equals(t.sala.Sifra) && tempTermin.Vreme.Equals(t.Vreme) && tempTermin.Datum.Equals(t.Datum))
                {
                    vecPostoji = true;
                    break;
                }
            }
            if (vecPostoji == false)
            {
                terminiService.DodavanjeTermina(tempTermin);
                this.termini.Add(tempTermin);
                this.Close();
            }
            else
            {
                MessageBox.Show("Ta sala je vec zauzeta!");
            }
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
