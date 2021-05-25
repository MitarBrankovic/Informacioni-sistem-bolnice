using Model;
using PrviProgram.Repository;
using PrviProgram.Service;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for IstorijaBolnickogLecenjaWindow.xaml
    /// </summary>
    public partial class IstorijaBolnickogLecenjaWindow : Window
    {
        private BolnickoLecenjeRepository bolnickoLecenjeRepository = new BolnickoLecenjeRepository();
        private BolnickoLecenjeService bolnickoLecenjeService = new BolnickoLecenjeService();
        private SalaRepository salaRepository = new SalaRepository();
        private List<BolnickoLecenje> bolnickoLecenjePacijenta;
        private BolnickoLecenje trenutnoBolnickoLecenje;
        private bool izmeniPritisnut = false;
        public IstorijaBolnickogLecenjaWindow(Pacijent pacijent)
        {
            InitializeComponent();
            bolnickoLecenjePacijenta = bolnickoLecenjeRepository.PregledSvihBolnickihLecenjaPacijenta(pacijent.Jmbg);
            dataGridIstorijaBolnickogLecenja.ItemsSource = bolnickoLecenjePacijenta;
            ProveraDaLiJeTrenutnoNaLecenju();
        }

        private void ProveraDaLiJeTrenutnoNaLecenju()
        {
            foreach(BolnickoLecenje bolnickoLecenje in bolnickoLecenjePacijenta)
            {
                if(bolnickoLecenje.DatumPocetka < DateTime.Now && bolnickoLecenje.DatumZavrsetka > DateTime.Now)
                {
                    trenutnoBolnickoLecenje = bolnickoLecenje;
                    PostaviVrednostPoljaSaInformacijama();
                    DozvoliMenjanjeInformacija();
                }
            }
        }

        private void DozvoliMenjanjeInformacija()
        {
            DatePickerZavrsetak.DisplayDateStart = DateTime.Today;
        }

        private void PostaviVrednostPoljaSaInformacijama()
        {

            DatePickerPocetak.SelectedDate = trenutnoBolnickoLecenje.DatumPocetka;
            DatePickerZavrsetak.SelectedDate = trenutnoBolnickoLecenje.DatumZavrsetka;
            ComboboxSobe.ItemsSource = salaRepository.SaleTipaSoba();
            PostaviComboboxSobe();
            Izmeni.IsEnabled = true;
        }

        private void PostaviComboboxSobe()
        {
            int count = 0;
            foreach(Sala sala in ComboboxSobe.Items)
            {
                if (sala.Sifra == trenutnoBolnickoLecenje.Sala.Sifra)
                {
                    ComboboxSobe.SelectedIndex = count;
                    break;
                }
                count++;
            }
        }

        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {
            if(izmeniPritisnut == false)
            {
                DatePickerZavrsetak.IsEnabled = true;
                ComboboxSobe.IsEnabled = true;
                izmeniPritisnut = true;
                Izmeni.Content = "Sačuvaj";
            }
            else
            {
                izmeniPritisnut = false;
                Izmeni.Content = "Izmeni";
                SacuvajIzmenu();
            }
        }

        private void SacuvajIzmenu()
        {
            trenutnoBolnickoLecenje.DatumZavrsetka = (DateTime)DatePickerZavrsetak.SelectedDate;
            trenutnoBolnickoLecenje.Sala = (Sala)ComboboxSobe.SelectedItem;
            bolnickoLecenjeService.IzmenaBolnickogLecenja(trenutnoBolnickoLecenje);
        }

        private void DatePickerPocetak_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DatePickerZavrsetak_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboboxSobe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
