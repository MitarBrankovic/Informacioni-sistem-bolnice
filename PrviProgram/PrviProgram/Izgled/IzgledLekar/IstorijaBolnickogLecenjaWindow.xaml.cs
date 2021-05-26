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
    public partial class IstorijaBolnickogLecenjaWindow : Window
    {
        private BolnickoLecenjeService bolnickoLecenjeService = new BolnickoLecenjeService();
        private BolnickoLecenjeRepository bolnickoLecenjeRepository = new BolnickoLecenjeRepository();
        private SalaRepository salaRepository = new SalaRepository();
        private List<BolnickoLecenje> bolnickoLecenjePacijenta;
        private BolnickoLecenje trenutnoBolnickoLecenje;
        private Pacijent pacijent;
        private bool izmeniPritisnut = false;
        public IstorijaBolnickogLecenjaWindow(Pacijent pacijent)
        {
            InitializeComponent();
            this.pacijent = pacijent;
            bolnickoLecenjePacijenta = bolnickoLecenjeRepository.PregledSvihBolnickihLecenjaPacijenta(pacijent.Jmbg);
            dataGridIstorijaBolnickogLecenja.ItemsSource = bolnickoLecenjePacijenta;
            ProveraDaLiJeTrenutnoNaLecenju();
        }

        private void ProveraDaLiJeTrenutnoNaLecenju()
        {
            if (bolnickoLecenjeService.ProveraDaLiJePacijentTrenutnoNaBolnickomLecenju(pacijent) != null)
            {
                trenutnoBolnickoLecenje = bolnickoLecenjeService.ProveraDaLiJePacijentTrenutnoNaBolnickomLecenju(pacijent);
                PostaviVrednostPoljaSaInformacijama();
                DozvoliMenjanjeInformacija();
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
