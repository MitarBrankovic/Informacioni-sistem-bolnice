using Controller;
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
    public partial class LekoviPrikaz : UserControl
    {
        private LekoviRepository lekoviRepository = new LekoviRepository();
        private LekoviController lekoviController = new LekoviController();
        private ObservableCollection<Lek> lekovi;
        private Lek lek;
        private ObservableCollection<CheckBoxSelektovanLek> alternativniLekovi = new ObservableCollection<CheckBoxSelektovanLek>();
        private Lekar lekar;
        private bool azurirajPritisnut = false;
        private Lek izmenjenLek;

        public LekoviPrikaz(PocetniPrikaz pocetniPrikaz, Lekar lekar)
        {
            InitializeComponent();
            this.lekar = lekar;
            lekovi = new ObservableCollection<Lek>(lekoviRepository.PregledSvihLekova());
            Izmeni.IsEnabled = false;
            IzmenaStanjaTextBoxova();
            dataGridLekovi.ItemsSource = lekovi;
        }

        private void Primedba_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridLekovi.SelectedIndex != -1)
            {
                PrimedbaNaLekWindow primedbaNaLek = new PrimedbaNaLekWindow((Lek)dataGridLekovi.SelectedItem, lekar);
                primedbaNaLek.ShowDialog();
            }
            else
            {
                MessageBox.Show("Morate izabrati lek!");
            }
        }

        private void dataGridLekovi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lek = (Lek)dataGridLekovi.SelectedItem;
            Izmeni.IsEnabled = true;
            PrikazPodatakaLeka();
            if(azurirajPritisnut == true)
            {
                IzmenaStanjaDugmetaAzuriranje();
            }
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
            foreach (Lek lek in this.lek.ZamenaZaLek)
            {
                SelektujAlternativneLekove(lek);
            }
        }

        private void SelektujAlternativneLekove(Lek lek)
        {
            foreach (var lekBrojac in alternativniLekovi)
            {
                if (lek.Sifra.Equals(lekBrojac.SelektovanAlternativniLek.Sifra))
                {
                    lekBrojac.IsSelected = true;
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
            IzmenaStanjaDugmetaAzuriranje();
            IzmenaStanjaTextBoxova();
        }

        private void IzmenaStanjaDugmetaAzuriranje()
        {
            if (dataGridLekovi.SelectedIndex != -1)
            {
                if (!azurirajPritisnut)
                {
                    azurirajPritisnut = true;
                    Izmeni.Content = "Sacuvaj";
                }
                else
                {
                    azurirajPritisnut = false;
                    Izmeni.Content = "Azuriraj";
                    IzmenaPodatakaLeka();
                    IzvrsavanjeIzmene();
                }
            }
        }

        private void IzmenaPodatakaLeka()
        {
            izmenjenLek = new Lek(Sifra.Text, Naziv.Text, Sastojci.Text, Opis.Text);
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
            if (lekoviController.IzmenaLeka(lek, izmenjenLek) == true)
            {
                Lek selektovaniLek = lekovi[dataGridLekovi.SelectedIndex];
                selektovaniLek.Naziv = izmenjenLek.Naziv;
                selektovaniLek.Opis = izmenjenLek.Opis;
                selektovaniLek.Sastojci = izmenjenLek.Sastojci;
                selektovaniLek.Sifra = izmenjenLek.Sifra;
            }
        }

        private void IzmenaStanjaTextBoxova()
        {
            if (azurirajPritisnut)
            {
                Naziv.IsEnabled = true;
                Sifra.IsEnabled = true;
                Sastojci.IsEnabled = true;
                Opis.IsEnabled = true;
                ComboAlternativni.IsEnabled = true;
            }
            else
            {
                Naziv.IsEnabled = false;
                Sifra.IsEnabled = false;
                Sastojci.IsEnabled = false;
                Opis.IsEnabled = false;
                ComboAlternativni.IsEnabled = false;
            }
        }
    }
}
