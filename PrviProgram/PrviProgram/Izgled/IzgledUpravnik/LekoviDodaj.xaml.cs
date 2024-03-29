﻿using Controller;
using Model;
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

namespace PrviProgram.Izgled.IzgledUpravnik
{
    public partial class LekoviDodaj : Window
    {
        private ObservableCollection<Lek> lekovi;
        private Lek noviLek;
        private List<CheckBoxSelektovanLek> alternativniLekovi = new List<CheckBoxSelektovanLek>();
        private LekoviController lekoviController = new LekoviController();

        public LekoviDodaj(ObservableCollection<Lek> lekovi)
        {
            InitializeComponent();            
            this.lekovi = lekovi;
            InicijalizacijaComboBoxaSala();
        }
        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            if (Naziv.Text == "" && Sifra.Text == "")
            {
                MessageBox.Show("Nisu popunjena sva polja!", "Greska");
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(Naziv.Text, "^[a-zA-Z ]"))
            {
                MessageBox.Show("Naziv nije dobro unet!", "Greska");
                Naziv.Text.Remove(Naziv.Text.Length - 1);
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(Sifra.Text, "^[a-zA-Z ]"))
            {
                MessageBox.Show("Naziv nije dobro unet!", "Greska");
                Naziv.Text.Remove(Naziv.Text.Length - 1);
            }
            else
            {
                FormiranjeNovogLeka();
                DodavanjeNovogLeka();
                this.Close();
            }
        }

        private void FormiranjeNovogLeka() 
        {
            noviLek = new Lek(Sifra.Text, Naziv.Text, Sastojci.Text, Opis.Text);
            noviLek.ZamenaZaLek = new List<Lek>();
            foreach (var lekBrojac in alternativniLekovi)
            {
                if (lekBrojac.IsSelected)
                {
                    noviLek.ZamenaZaLek.Add(lekBrojac.SelektovanAlternativniLek);
                }
            }
        }

        private void DodavanjeNovogLeka()
        {
            if (lekoviController.DodavanjeLeka(noviLek))
            {
                this.lekovi.Add(noviLek);
            }
        }

        public class CheckBoxSelektovanLek
        {
            public bool IsSelected { get; set; }
            public Lek SelektovanAlternativniLek { get; set; }
        }

        private void InicijalizacijaComboBoxaSala()
        {
            foreach (Lek lekBrojac in lekovi)
            {
                CheckBoxSelektovanLek check = new CheckBoxSelektovanLek();
                check.SelektovanAlternativniLek = lekBrojac;
                alternativniLekovi.Add(check);
            }
            ComboAlternativni.ItemsSource = alternativniLekovi;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
            else if (e.Key == Key.Space)
            {
                Naziv.Focus();
            }
        }
    }
}
