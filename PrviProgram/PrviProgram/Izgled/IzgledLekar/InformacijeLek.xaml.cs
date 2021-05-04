using Controller;
using Model;
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

namespace PrviProgram.Izgled.IzgledLekar
{
    /// <summary>
    /// Interaction logic for InformacijeLek.xaml
    /// </summary>
    public partial class InformacijeLek : Window
    {
        private ObservableCollection<Lek> lekovi;
        private Lek lek;
        private List<CheckBoxSelektovanLek> alternativniLekovi = new List<CheckBoxSelektovanLek>();
        private Lek izmenjenLek = new Lek();
        private UpravnikController upravnikController = new UpravnikController();
        private bool izmenaClick = false;

        public InformacijeLek(ObservableCollection<Lek> lekovi, Lek lek)
        {
            InitializeComponent();
            ProveraIzmene();
            izmenjenLek.ZamenaZaLek = new List<Lek>();
            this.lekovi = lekovi;
            this.lek = lek;
            PrikazPodatakaLeka();
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            if (ValidanUnos())
            {
                IzmenaPodatakaLeka();
                IzvrsavanjeIzmene();
                this.Close();
            }
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void PrikazPodatakaLeka()
        {
            Naziv.Text = this.lek.Naziv;
            Sifra.Text = this.lek.Sifra;
            Opis.Text = this.lek.Opis;
            Sastojci.Text = this.lek.Sastojci;
            InicijalizacijaComboBoxaAlternativnihLekova();
            PrikazAlternativnihLekova();
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

        public class CheckBoxSelektovanLek
        {
            public bool IsSelected { get; set; }
            public Lek SelektovanAlternativniLek { get; set; }
        }

        private void InicijalizacijaComboBoxaAlternativnihLekova()
        {
            foreach (Lek lekBrojac in lekovi)
            {
                CheckBoxSelektovanLek checkBoxSelektovanLek = new CheckBoxSelektovanLek();
                checkBoxSelektovanLek.SelektovanAlternativniLek = lekBrojac;
                alternativniLekovi.Add(checkBoxSelektovanLek);
                if (checkBoxSelektovanLek.SelektovanAlternativniLek.Naziv.Equals(Naziv.Text))
                {
                    alternativniLekovi.Remove(checkBoxSelektovanLek);
                }
            }
            ComboAlternativni.ItemsSource = alternativniLekovi;
        }

        private bool ValidanUnos()
        {
            if (Naziv.Text == "" && Sifra.Text == "")
            {
                MessageBox.Show("Nisu popunjena sva polja!", "Greska");
                return false;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(Naziv.Text, "^[a-zA-Z ]"))
            {
                MessageBox.Show("Naziv nije dobro unet!", "Greska");
                Naziv.Text.Remove(Naziv.Text.Length - 1);
                return false;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(Sifra.Text, "^[a-zA-Z ]"))
            {
                MessageBox.Show("Naziv nije dobro unet!", "Greska");
                Naziv.Text.Remove(Naziv.Text.Length - 1);
                return false;
            }
            else
            {
                return true;
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

        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {
            if(izmenaClick == false)
            {
                izmenaClick = true;
                ProveraIzmene();
            }
            else
            {
                izmenaClick = false;
                ProveraIzmene();
            }
        }

        private void ProveraIzmene()
        {
            if(izmenaClick == false)
            {
                Naziv.IsEnabled = false;
                Sifra.IsEnabled = false;
                Sastojci.IsEnabled = false;
                Opis.IsEnabled = false;
                ComboAlternativni.IsEnabled = false;
            }
            else
            {
                Naziv.IsEnabled = true;
                Sifra.IsEnabled = true;
                Sastojci.IsEnabled = true;
                Opis.IsEnabled = true;
                ComboAlternativni.IsEnabled = true;
            }
        }
    }
}
