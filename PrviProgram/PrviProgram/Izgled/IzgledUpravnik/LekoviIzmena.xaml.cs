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
        private LekoviService lekoviService;
        private ObservableCollection<Lek> lekovi;
        private Lek lek;
        private Lek tempLek = new Lek();
        private List<CheckBoxLek> novalista = new List<CheckBoxLek>();

        public LekoviIzmeni(ObservableCollection<Lek> lekovi, Lek lek)
        {
            InitializeComponent();
            lekoviService = new LekoviService();
            tempLek.ZamenaZaLek = new List<Lek>();
            this.lek = lek;
            this.lekovi = lekovi;
            Naziv.Text = lek.Naziv;
            Sifra.Text = lek.Sifra;
            Opis.Text = lek.Opis;
            Sastojci.Text = lek.Sastojci;
            inicijalizacijaComboBoxaSala();

            foreach (Lek l in lek.ZamenaZaLek) 
            {
                foreach (var lekBrojac in novalista)
                {
                    if (l.Sifra.Equals(lekBrojac.pomocniLek.Sifra)) 
                    {
                        lekBrojac.IsSelected = true;
                        
                    }    
                }

            }

        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            if (Naziv.Text == "" && Sifra.Text == "")
            {
                MessageBox.Show("Nisu popunjena sva polja!", "Greska"); //, MessageBoxButtons.OK, MessageBoxIcon.Error
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

                tempLek.Naziv = Naziv.Text;
                tempLek.Sifra = Sifra.Text;
                tempLek.Opis = Opis.Text;
                tempLek.Sastojci = Sastojci.Text;

                tempLek.ZamenaZaLek.Clear();
                foreach (var lekBrojac in novalista)
                {
                    if (lekBrojac.IsSelected == true)
                    {
                        tempLek.ZamenaZaLek.Add(lekBrojac.pomocniLek);
                    }
                }



                if (lekoviService.IzmenaLeka(lek, tempLek) == true)
                {
                    this.lekovi.Remove(lek);
                    this.lekovi.Add(tempLek);
                }

                this.Close();
            }
        }

        private void CheckBoxLek_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBoxLek_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void AlternativniLekovi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }




        public class CheckBoxLek
        {
            public bool IsSelected { get; set; }
            public Lek pomocniLek { get; set; }

            /*public CheckBoxLek(Lek lek)
            {
                pomocniLek = lek;
            }

            public CheckBoxLek(Lek lek, bool isSelected)
            {
                IsSelected = isSelected;
                pomocniLek = lek;
            }*/
        }


        private void inicijalizacijaComboBoxaSala()
        {
            foreach (Lek lekBrojac in lekovi)
            {
                CheckBoxLek check = new CheckBoxLek();
                check.pomocniLek = lekBrojac;
                novalista.Add(check);
                if (check.pomocniLek.Naziv.Equals(Naziv.Text))
                {
                    novalista.Remove(check);
                }
            }
            AlternativniLekovi.ItemsSource = novalista;
        }
    }
}
