using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Model;
using Repository;
using Service;

namespace PrviProgram.Izgled.IzgledSekretar.IzgledTermini
{
    public partial class IzmenaTermina : Window
    {
        private LekarRepository lekarRepository = new LekarRepository();
        private Repository.SalaRepository salaRepository = new Repository.SalaRepository();
        private TerminiService terminiService = new TerminiService();
        private ObservableCollection<Termin> termini;
        private ObservableCollection<string> vreme = new ObservableCollection<string> { "08:00:00", "08:30:00", "09:00:00", "09:30:00", "10:00:00", "10:30:00", "11:00:00", "11:30:00", "12:00:00", "12:30:00", "13:00:00", "13:30:00", "14:00:00", "14:30:00", "15:00:00", "15:30:00", "16:00:00", "16:30:00", "17:00:00", "17:30:00", "18:00:00", "18:30:00", "19:00:00", "19:30:00" };
        private ObservableCollection<string> tipTermina = new ObservableCollection<string> { "Operacija", "Pregled", "Kontrola" };
        private Termin termin;
        public IzmenaTermina(ObservableCollection<Termin> termini, Termin termin)
        {
            InitializeComponent();
            this.termini = termini;
            this.termin = termin;
            comboBoxLekari.ItemsSource = lekarRepository.PregledSvihLekara();

            if (termin.pacijent != null)
            {
                ObservableCollection<Pacijent> pacijents = new ObservableCollection<Pacijent>();
                pacijents.Add(termin.pacijent);
                comboBoxPacijenti.ItemsSource = pacijents;
            }
            else
            {
                ObservableCollection<GuestPacijent> guestPacijents = new ObservableCollection<GuestPacijent>();
                guestPacijents.Add(termin.guestPacijent);
                comboBoxPacijenti.ItemsSource = guestPacijents;
            }
            comboBoxPacijenti.SelectedIndex = 0;
            comboBoxPacijenti.IsEnabled = false;

            vremeText.ItemsSource = vreme;
            TipTerminaText.ItemsSource = tipTermina;

            datePicker.SelectedDate = termin.Datum;
            TipTerminaText.SelectedIndex = tipTermina.IndexOf(termin.TipTermina.ToString());
            vremeText.SelectedIndex = vreme.IndexOf(termin.Vreme);

            int i = 0;
            foreach(Model.Lekar a in comboBoxLekari.Items.Cast<Model.Lekar>())
            {
                if (a.Jmbg.Equals(termin.lekar.Jmbg))
                {
                    comboBoxLekari.SelectedIndex = i;
                }
                i++;
            }
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            Termin termin = new Termin();
            termin.Datum = (DateTime)(datePicker.SelectedDate);
            termin.Vreme = vremeText.Text;
            termin.lekar = (Model.Lekar)comboBoxLekari.SelectedItem;
            if (this.termin.pacijent != null)
            {
                termin.pacijent = (Pacijent)comboBoxPacijenti.SelectedItem;
            }
            else
            {
                termin.guestPacijent = (GuestPacijent) comboBoxPacijenti.SelectedItem;
            }

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

            String tip = (String)TipTerminaText.SelectedItem;
            if (tip.Equals("Pregled"))
            {
                termin.TipTermina = TipTermina.Pregled;
            }
            else if (tip.Equals("Kontrola"))
            {
                termin.TipTermina = TipTermina.Kontrola;
            }
            else if (tip.Equals("Operacija"))
            {
                termin.TipTermina = TipTermina.Operacija;
            }

            terminiService.IzmenaTermina(termin, termin.pacijent);
            this.termini.Remove(this.termin);
            this.termini.Add(termin);

            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
