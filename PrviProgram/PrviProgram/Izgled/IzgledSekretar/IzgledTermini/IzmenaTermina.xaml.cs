using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using Model;
using Repository;
using Service.PacijentService;

namespace PrviProgram.Izgled.IzgledSekretar.IzgledTermini
{
    public partial class IzmenaTermina : Window
    {
        private PacijentRepository pacijentRepository = new PacijentRepository();
        private LekarRepository lekarRepository = new LekarRepository();
        private Repository.SalaRepository salaRepository = new Repository.SalaRepository();
        private TerminiService terminiService = new TerminiService();
        private ObservableCollection<Termin> termini;
        private Termin termin;
        public IzmenaTermina(ObservableCollection<Termin> termini, Termin termin)
        {
            InitializeComponent();
            this.termini = termini;
            this.termin = termin;
            comboBoxLekari.ItemsSource = lekarRepository.PregledSvihLekara();
            comboBoxPacijenti.ItemsSource = pacijentRepository.PregledSvihPacijenata();
            string[] niz = { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00", "16:30", "17:00", "17:30", "18:00", "18:30", "19:00", "19:30" };
            vremeText.ItemsSource = niz;

            datePicker.SelectedDate = termin.Datum;
            comboBoxPacijenti.SelectedItem = termin.pacijent;
            comboBoxLekari.SelectedItem = termin.lekar;
            TipTerminaText.SelectedItem = termin.TipTermina;
            vremeText.SelectedItem = termin.Vreme;
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            Termin termin = new Termin();
            termin.Datum = (DateTime)(datePicker.SelectedDate);
            termin.Vreme = vremeText.Text;
            termin.lekar = (Model.Lekar)comboBoxLekari.SelectedItem;
            termin.pacijent = (Pacijent)comboBoxPacijenti.SelectedItem;

            Random rnd = new Random();
            List<Sala> sale = new List<Sala>();
            sale = salaRepository.PregledSvihSala();
            Sala tempSala = new Sala();
            if (sale[0] != null)
            {
                tempSala = sale[0];
                termin.sala = tempSala;
            }
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var Random = new Random();
            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[Random.Next(chars.Length)];
            }
            var finalString = new String(stringChars);
            termin.SifraTermina = finalString;

            String tip = TipTerminaText.Text;
            if (tip.Equals("Pregled"))
            {
                termin.TipTermina = TipTermina.Pregled;
            }
            else if (tip.Equals("Kontrola"))
            {
                termin.TipTermina = TipTermina.Kontrola;
            }

            terminiService.DodavanjeTermina(termin, termin.pacijent);
            this.termini.Add(termin);

            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
