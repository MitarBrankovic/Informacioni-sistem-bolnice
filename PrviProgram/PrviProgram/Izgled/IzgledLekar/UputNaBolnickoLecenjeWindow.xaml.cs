using Controller;
using Model;
using PrviProgram.Repository;
using PrviProgram.Service;
using Service;
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
    /// Interaction logic for UputNaBolnickoLecenjeWindow.xaml
    /// </summary>
    public partial class UputNaBolnickoLecenjeWindow : Window
    {
        private SalaRepository salaRepository = new SalaRepository();
        private UtilityService utilityService = new UtilityService();
        private BolnickoLecenjeService bolnickoLecenjeService = new BolnickoLecenjeService();
        private LekarController lekarController = new LekarController();
        private Pacijent pacijent;
        public UputNaBolnickoLecenjeWindow(Pacijent pacijent)
        {
            InitializeComponent();
            this.pacijent = pacijent;
            PopuniComboboxSobama();
        }

        private void PopuniComboboxSobama()
        {
            ComboboxSobe.ItemsSource = salaRepository.SaleTipaSoba();
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            BolnickoLecenje bolnickoLecenje = new BolnickoLecenje(
                utilityService.GenerisanjeSifre(), 
                pacijent, 
                (Sala)ComboboxSobe.SelectedItem, 
                (DateTime)DatePickerPocetak.SelectedDate, 
                (DateTime)DatePickerZavrsetak.SelectedDate);
            lekarController.DodavanjeBolnickogLecenja(bolnickoLecenje);
            this.Close();
        }

        private void Zatvori_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DatePickerPocetak_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePickerZavrsetak.IsEnabled = true;
            DatePickerZavrsetak.DisplayDateStart = DatePickerPocetak.SelectedDate;
            ProveraDaLiSuSvaPoljaPopunjena();
        }

        private void DatePickerZavrsetak_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ProveraDaLiSuSvaPoljaPopunjena();
        }

        private void ComboboxSobe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ProveraDaLiSuSvaPoljaPopunjena();
        }

        private void ProveraDaLiSuSvaPoljaPopunjena()
        {
            if (DatePickerPocetak.SelectedDate == null || DatePickerZavrsetak == null || ComboboxSobe.SelectedItem == null)
                Potvrdi.IsEnabled = false;
            else
                Potvrdi.IsEnabled = true;
        }
    }
}
