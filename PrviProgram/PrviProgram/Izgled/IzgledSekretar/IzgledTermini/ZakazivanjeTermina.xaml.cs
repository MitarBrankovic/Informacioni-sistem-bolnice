﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using Model;
using Repository;
using Service;

namespace PrviProgram.Izgled.IzgledSekretar.IzgledTermini
{
    public partial class ZakazivanjeTermina : Window
    {
        private PacijentRepository pacijentRepository = new PacijentRepository();
        private LekarRepository lekarRepository = new LekarRepository();
        private Repository.SalaRepository salaRepository = new Repository.SalaRepository();
        private TerminiService terminiService = new TerminiService();
        private ObservableCollection<Termin> termini;
        public static GuestPacijent guestPacijent;
        private List<string> constVreme = new List<string>(){ "08:00:00", "08:30:00", "09:00:00", "09:30:00", "10:00:00", "10:30:00", "11:00:00", "11:30:00", "12:00:00", "12:30:00", "13:00:00", "13:30:00", "14:00:00", "14:30:00", "15:00:00", "15:30:00", "16:00:00", "16:30:00", "17:00:00", "17:30:00", "18:00:00", "18:30:00", "19:00:00", "19:30:00" };
        private ObservableCollection<string> vreme = new ObservableCollection<string> { "08:00:00", "08:30:00", "09:00:00", "09:30:00", "10:00:00", "10:30:00", "11:00:00", "11:30:00", "12:00:00", "12:30:00", "13:00:00", "13:30:00", "14:00:00", "14:30:00", "15:00:00", "15:30:00", "16:00:00", "16:30:00", "17:00:00", "17:30:00", "18:00:00", "18:30:00", "19:00:00", "19:30:00" };
        private ObservableCollection<string> tipTermina = new ObservableCollection<string> { "Operacija", "Pregled", "Kontrola" };
        public ZakazivanjeTermina(ObservableCollection<Termin> termini)
        {
            InitializeComponent();
            this.termini = termini;
            comboBoxLekari.ItemsSource = lekarRepository.PregledSvihLekara();
            comboBoxPacijenti.ItemsSource = pacijentRepository.PregledSvihPacijenata();
            vremeText.ItemsSource = vreme;
            TipTerminaText.ItemsSource = tipTermina;
            vremeText.IsEnabled = false;
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            Termin termin = new Termin();
            termin.Datum = (DateTime)(datePicker.SelectedDate);
            termin.Vreme = vremeText.Text;
            termin.lekar = (Model.Lekar)comboBoxLekari.SelectedItem;
            if (guestPacijent == null)
            {
                termin.pacijent = (Pacijent)comboBoxPacijenti.SelectedItem;
            }
            else
            {
                termin.guestPacijent = guestPacijent;
            }

            Sala novaSala = new Sala();
            novaSala = TerminiService.getInstance().dobavljanjeSale(termin);
            termin.sala = novaSala;

            Random rnd = new Random();
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
            else if (tip.Equals("Operacija"))
            {
                termin.TipTermina = TipTermina.Operacija;
            }
            if (terminiService.ProvaraZauzatostiTermina(termin) == false)
            {
                terminiService.DodavanjeTermina(termin);
                this.termini.Add(termin);
                this.Close();
            }
            else
            {
                MessageBox.Show("Termin zauzet!!");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void GuestPacijent_Click(object sender, RoutedEventArgs e)
        {
            KreiranjeGuestPacijenta kreiranjeGuestPacijenta = new KreiranjeGuestPacijenta(comboBoxPacijenti);
            kreiranjeGuestPacijenta.Show();
        }

        private void comboBoxLekari_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (checkBoxHitanSlucaj.IsChecked == false)
            {
                if ((Lekar)comboBoxLekari.SelectedItem != null && datePicker.SelectedDate != null)
                {
                    vremeText.IsEnabled = true;
                    List<string> zauzetiTermini = terminiService.ZauzetiTerminiLekaraDatuma((Lekar)comboBoxLekari.SelectedItem, (DateTime)datePicker.SelectedDate);
                    foreach (String vremeT in constVreme)
                    {
                        foreach (String zauzetiT in zauzetiTermini)
                        {
                            if (vremeT.Equals(zauzetiT))
                            {
                                vreme.Remove(vremeT);
                            }
                        }
                    }
                }
                else
                {
                    vremeText.IsEnabled = false;
                }
            }
            else
            {
                vreme = new ObservableCollection<string>(constVreme);
                vremeText.ItemsSource = vreme;
                vremeText.IsEnabled = true;
            }
        }

        private void datePicker_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (checkBoxHitanSlucaj.IsChecked == false)
            {
                if ((Lekar)comboBoxLekari.SelectedItem != null && datePicker.SelectedDate != null)
                {
                    vremeText.IsEnabled = true;
                    List<string> zauzetiTermini = terminiService.ZauzetiTerminiLekaraDatuma((Lekar)comboBoxLekari.SelectedItem, (DateTime)datePicker.SelectedDate);
                    foreach (String vremeT in constVreme)
                    {
                        foreach (String zauzetiT in zauzetiTermini)
                        {
                            if (vremeT.Equals(zauzetiT))
                            {
                                vreme.Remove(vremeT);
                            }
                        }
                    }
                }
                else
                {
                    vremeText.IsEnabled = false;
                }
            }
            else
            {
                vreme = new ObservableCollection<string>(constVreme);
                vremeText.ItemsSource = vreme;
                vremeText.IsEnabled = true;
            }
        }

        private void checkBoxHitanSlucaj_Checked(object sender, RoutedEventArgs e)
        {
            vreme = new ObservableCollection<string>(constVreme);
            vremeText.ItemsSource = vreme;
            vremeText.IsEnabled = true;
        }

        private void checkBoxHitanSlucaj_Unchecked(object sender, RoutedEventArgs e)
        {
            if ((Lekar)comboBoxLekari.SelectedItem != null && (DateTime)datePicker.SelectedDate != null)
            {
                vremeText.IsEnabled = true;
                List<string> zauzetiTermini = terminiService.ZauzetiTerminiLekaraDatuma((Lekar)comboBoxLekari.SelectedItem, (DateTime)datePicker.SelectedDate);
                foreach (String vremeT in constVreme)
                {
                    foreach (String zauzetiT in zauzetiTermini)
                    {
                        if (vremeT.Equals(zauzetiT))
                        {
                            vreme.Remove(vremeT);
                        }
                    }
                }
            }
            else
            {
                vremeText.IsEnabled = false;
            }
        }
    }
}
