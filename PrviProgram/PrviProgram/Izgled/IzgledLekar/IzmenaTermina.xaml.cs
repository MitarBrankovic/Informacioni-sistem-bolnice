﻿using Model;
using PrviProgram.Repository;
using Repository;
using Service;
using Service.LekarService;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace PrviProgram.Izgled.IzgledLekar
{
    /// <summary>
    /// Interaction logic for IzmenaTermina.xaml
    /// </summary>
    public partial class IzmenaTermina : Window
    {
        private ObservableCollection<Termin> termini;
        private TerminiService terminiService = new TerminiService();
        private PacijentRepository pacijentRepository = new PacijentRepository();
        private SalaRepository salaRepository = new SalaRepository();
        private ObservableCollection<Termin> terminiPacijent;
        private Termin termin;
        int index;
        string[] niz = { "08:00", "08:30", "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00", "16:30", "17:00", "17:30", "18:00", "18:30", "19:00", "19:30" };


        public IzmenaTermina(ObservableCollection<Termin> termini, Termin termin)
        {
            InitializeComponent();
            this.termini = termini;
            this.termin = termin;
            this.terminiPacijent = new ObservableCollection<Termin>(terminiService.PregledTermina(this.termin.pacijent));
            PopunjavanjePoljaSaPodacima();

        }


        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            this.termini.Remove(this.termin);

            Termin tempTermin = new Termin((DateTime)(DatumText.SelectedDate), SelektovaniTipTermina(), termin.SifraTermina,
                vremeText.Text, termin.lekar, IzmenjeniPacijent());
            
            Sala tempSala = new Sala();
            tempSala = (Sala)ComboboxSala.SelectedItem;
            tempTermin.sala = tempSala;

            if (!terminiService.ProvaraZauzatostiTermina(tempTermin))
            {
                terminiService.IzmenaTermina(tempTermin);
                this.termini.Add(tempTermin);
                this.Close();
            }
            else MessageBox.Show("Greska!");            
        }



        private Pacijent IzmenjeniPacijent()
        {
            Model.Pacijent tempPacijent = new Model.Pacijent();
            tempPacijent = pacijentRepository.PregledPacijenta(((Pacijent)ComboboxPacijent.SelectedItem).Jmbg);
            termin.pacijent = tempPacijent;
            return tempPacijent;
        }

        private TipTermina SelektovaniTipTermina()
        {
            String tip = TipTerm.Text;
            if (tip.Equals("Pregled"))
                return TipTermina.Pregled;
            else if (tip.Equals("Operacija"))
                return TipTermina.Operacija;
            else
                return TipTermina.Kontrola;
        }

        private void PopunjavanjePoljaSaPodacima()
        {
            ComboboxPacijent.ItemsSource = pacijentRepository.PregledSvihPacijenata();
            ComboboxSala.ItemsSource = salaRepository.PregledSvihSala();
            Sifra.Text = termin.SifraTermina;
            DatumText.SelectedDate = termin.Datum;
            ComboboxSala.SelectedItem = termin.sala;
            ComboboxPacijent.SelectedItem = termin.pacijent;
            PostaviComboboxVremeNaPostojecuVrednost();
            PostaviComboboxTipTerminaNaPostojecuVrednost();


        }
        private void PostaviComboboxTipTerminaNaPostojecuVrednost()
        {
            String tip = termin.TipTermina.ToString();
            switch (tip)
            {
                case "Pregled":
                    TipTerm.SelectedIndex = 0;
                    break;
                case "Operacija":
                    TipTerm.SelectedIndex = 1;
                    break;
                case "Kontrola":
                    TipTerm.SelectedIndex = 2;
                    break;
            }
        }

        private void PostaviComboboxVremeNaPostojecuVrednost()
        {
            string vremeTermina = termin.Vreme;
            for (int i = 0; i < niz.Length; i++)
            {
                if (niz[i].Equals(vremeTermina))
                {
                    index = i;
                }
            }
            vremeText.SelectedIndex = index;
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
