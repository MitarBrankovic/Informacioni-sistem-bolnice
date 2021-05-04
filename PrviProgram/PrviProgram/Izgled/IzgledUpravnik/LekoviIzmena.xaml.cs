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
    public partial class LekoviIzmeni : Window
    {
        private ObservableCollection<Lek> lekovi;
        private Lek lek;
        private Lek izmenjenLek = new Lek();
        private List<CheckBoxSelektovanLek> alternativniLekovi = new List<CheckBoxSelektovanLek>();
        private UpravnikController upravnikController = new UpravnikController();

        public LekoviIzmeni(ObservableCollection<Lek> lekovi, Lek lek)
        {
            InitializeComponent();
            izmenjenLek.ZamenaZaLek = new List<Lek>();
            this.lek = lek;
            this.lekovi = lekovi;
            PrikazPodatakaLeka();
            InicijalizacijaComboBoxaSala();
            PrikazAlternativnihLekova();
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
                IzmenaPodatakaLeka();
                IzvrsavanjeIzmene();
                this.Close();
            }
        }

        private void IzmenaPodatakaLeka() 
        {
            izmenjenLek.Naziv = Naziv.Text;
            izmenjenLek.Sifra = Sifra.Text;
            izmenjenLek.Opis = Opis.Text;
            izmenjenLek.Sastojci = Sastojci.Text;
            izmenjenLek.ZamenaZaLek.Clear();
            foreach (var lekBrojac in alternativniLekovi)
            {
                if (lekBrojac.IsSelected == true)
                {
                    izmenjenLek.ZamenaZaLek.Add(lekBrojac.SelektovanAlternativniLek);
                }
            }
        }

        private void IzvrsavanjeIzmene()
        {
            if (upravnikController.IzmenaLeka(lek, izmenjenLek) == true)
            {
                this.lekovi.Remove(lek);
                this.lekovi.Add(izmenjenLek);
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
                if (check.SelektovanAlternativniLek.Naziv.Equals(Naziv.Text))
                {
                    alternativniLekovi.Remove(check);
                }
            }
            ComboAlternativni.ItemsSource = alternativniLekovi;
        }

        private void PrikazPodatakaLeka()
        {
            Naziv.Text = this.lek.Naziv;
            Sifra.Text = this.lek.Sifra;
            Opis.Text = this.lek.Opis;
            Sastojci.Text = this.lek.Sastojci;
        }

        private void PrikazAlternativnihLekova()
        {
            foreach (Lek l in this.lek.ZamenaZaLek)
            {
                foreach (var lekBrojac in alternativniLekovi)
                {
                    if (l.Sifra.Equals(lekBrojac.SelektovanAlternativniLek.Sifra))
                    {
                        lekBrojac.IsSelected = true;
                    }
                }
            }
        }
    }
}