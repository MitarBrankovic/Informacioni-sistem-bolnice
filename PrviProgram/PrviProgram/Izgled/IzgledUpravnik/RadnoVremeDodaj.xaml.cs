using Model;
using PrviProgram.Service;
using Repository;
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
    /// Interaction logic for RadnoVremeDodaj.xaml
    /// </summary>
    public partial class RadnoVremeDodaj : Window
    {
        private LekarRepository lekarRepository = new LekarRepository();
        private UtilityService utilityService = new UtilityService();
        private ObservableCollection<string> vremeTermina;
        private ObservableCollection<RadnoVremeLekara> radnaVremena;
        private RadnoVremeService radnoVremeService = new RadnoVremeService();

        public RadnoVremeDodaj(ObservableCollection<RadnoVremeLekara> radnaVremena)
        {
            InitializeComponent();
            this.radnaVremena = radnaVremena;
            vremeTermina = new ObservableCollection<string>(utilityService.nizVremena);
            InicijalizacijaCombo();
        }

        private void InicijalizacijaCombo()
        {
            ComboLekar.ItemsSource = lekarRepository.PregledSvihLekara();
            ComboPocetno.ItemsSource = vremeTermina;
            ComboKrajnje.ItemsSource = vremeTermina;
        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            RadnoVremeLekara radnoVremeLekara = new RadnoVremeLekara((Lekar)ComboLekar.SelectedItem, ComboPocetno.Text,
                ComboKrajnje.Text, (DateTime)(PocetniDatum.SelectedDate), (DateTime)(KrajnjiDatum.SelectedDate));

            DateTime pocetak = (DateTime)PocetniDatum.SelectedDate;
            DateTime kraj = (DateTime)KrajnjiDatum.SelectedDate;
            if (DateTime.Compare(pocetak, kraj) > 0)
            {
                MessageBox.Show("Datumi nisu dobro selektovani!", "Greska");
            }
            else 
            {
                if (radnoVremeService.DodavanjeLeka(radnoVremeLekara))
                {
                    radnaVremena.Add(radnoVremeLekara);
                    this.Close();
                }
                else { MessageBox.Show("Podaci nisu dobro uneti!"); }
            }
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
