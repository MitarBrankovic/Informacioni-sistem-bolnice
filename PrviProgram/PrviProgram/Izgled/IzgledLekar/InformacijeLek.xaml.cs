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


        public InformacijeLek(ObservableCollection<Lek> lekovi, Lek lek)
        {
            InitializeComponent();
            this.lekovi = lekovi;
            this.lek = lek;
            PrikazPodatakaLeka();
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {

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
    }
}
