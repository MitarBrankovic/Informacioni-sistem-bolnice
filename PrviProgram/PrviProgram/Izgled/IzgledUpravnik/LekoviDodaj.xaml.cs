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
    /// <summary>
    /// Interaction logic for LekoviDodaj.xaml
    /// </summary>
    public partial class LekoviDodaj : Window
    {
        private ObservableCollection<Lek> lekovi;
        private LekoviService lekoviService;
        Lek tempLek = new Lek();

        private List<CheckBoxLek> novalista = new List<CheckBoxLek>();

        public LekoviDodaj(ObservableCollection<Lek> lekovi)
        {
            InitializeComponent();
            tempLek.ZamenaZaLek = new List<Lek>();
            lekoviService = new LekoviService();
            this.lekovi = lekovi;
            inicijalizacijaComboBoxaSala();
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


                foreach (var lekBrojac in novalista)
                {
                    if (lekBrojac.IsSelected == true)
                    {
                        tempLek.ZamenaZaLek.Add(lekBrojac.pomocniLek);
                    }
                }



                if (lekoviService.DodavanjeLeka(tempLek) == true)
                {
                    this.lekovi.Add(tempLek);
                }

                this.Close();
            }
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
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
            List<Lek> comboLekovi = new List<Lek>(lekovi);
            foreach (Lek lekBrojac in lekovi) 
            {
                if (lekBrojac.Naziv.Equals(Naziv.Text)) {
                    comboLekovi.Remove(lekBrojac);
                }
            }

            foreach (Lek lekBrojac in lekovi)
            {
                CheckBoxLek check = new CheckBoxLek();
                check.pomocniLek = lekBrojac;
                novalista.Add(check);
            }



            AlternativniLekovi.ItemsSource = novalista;
        }


        private void nova()
        {
            foreach (Lek lekBrojac in lekovi)
            {
                CheckBoxLek check = new CheckBoxLek();
                check.pomocniLek = lekBrojac;
                novalista.Add(check);
            }

        }


        private void AlternativniLekovi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
