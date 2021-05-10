using Model;
using Repository;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrviProgram.Izgled.IzgledLekar
{
    /// <summary>
    /// Interaction logic for LekoviPrikaz.xaml
    /// </summary>
    public partial class LekoviPrikaz : UserControl
    {
        private ObservableCollection<Lek> lekovi;
        private Lek lek;
        private LekoviRepository lekoviRepository = new LekoviRepository();
        private List<CheckBoxSelektovanLek> alternativniLekovi = new List<CheckBoxSelektovanLek>();
        private Lekar lekar;
        public LekoviPrikaz(PocetniPrikaz pocetniPrikaz, Lekar lekar)
        {
            InitializeComponent();
            this.lekar = lekar;
            lekovi = new ObservableCollection<Lek>(lekoviRepository.PregledSvihLekova());
            dataGridLekovi.ItemsSource = lekovi;
        }

        private void Primedba_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridLekovi.SelectedIndex != -1)
            {
                PrimedbaNaLekWindow primedbaNaLek = new PrimedbaNaLekWindow((Lek)dataGridLekovi.SelectedItem, lekar);
                primedbaNaLek.Show();
            }
            else
            {
                MessageBox.Show("Morate izabrati lek!");
            }
        }

        private void dataGridLekovi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lek = (Lek)dataGridLekovi.SelectedItem;
            PrikazPodatakaLeka();
        }

        private void PrikazPodatakaLeka()
        {
            Naziv.Text = this.lek.Naziv;
            Sifra.Text = this.lek.Sifra;
            Opis.Text = lek.Opis;
            Sastojci.Text = lek.Sastojci;
            AnuliranjeAlternativnihLekova();
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
        private void AnuliranjeAlternativnihLekova()
        {
            alternativniLekovi.Clear();
            ComboAlternativni.ItemsSource = alternativniLekovi;
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

        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
