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
    public partial class RadnoVremeIzmeni : Window
    {
        private LekarRepository lekarRepository = new LekarRepository();
        private UtilityService utilityService = new UtilityService();
        private ObservableCollection<string> vremeTermina;
        private ObservableCollection<RadnoVremeLekara> radnaVremena;
        private RadnoVremeService radnoVremeService = new RadnoVremeService();
        private RadnoVremeLekara radnoVremeLekara = new RadnoVremeLekara();

        public RadnoVremeIzmeni(ObservableCollection<RadnoVremeLekara> radnaVremena, RadnoVremeLekara radnoVremeLekara)
        {
            InitializeComponent();
            this.radnoVremeLekara = radnoVremeLekara;
            this.radnaVremena = radnaVremena;
            vremeTermina = new ObservableCollection<string>(utilityService.nizVremena);
            InicijalizacijaCombo();
            PrikazPodataka();
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

        private void PrikazPodataka()
        {
            PocetniDatum.SelectedDate = radnoVremeLekara.PocetniDatum;
            PocetniDatum.IsEnabled = false;
            KrajnjiDatum.SelectedDate = radnoVremeLekara.KrajnjiDatum;
            ComboLekar.SelectedItem = radnoVremeLekara.Lekar;
            ComboPocetno.SelectedIndex = vremeTermina.IndexOf(radnoVremeLekara.PocetakRadnogVremena);
            ComboKrajnje.SelectedIndex = vremeTermina.IndexOf(radnoVremeLekara.KrajRadnogVremena);
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            RadnoVremeLekara novoRadnoVreme = new RadnoVremeLekara((Lekar)ComboLekar.SelectedItem, ComboPocetno.Text,
            ComboKrajnje.Text, (DateTime)(PocetniDatum.SelectedDate), (DateTime)(KrajnjiDatum.SelectedDate));

            if (radnoVremeService.IzmenaLeka(radnoVremeLekara, novoRadnoVreme))
            {
                radnaVremena.Remove(radnoVremeLekara);
                radnaVremena.Add(novoRadnoVreme);
                this.Close();
            }
            else { MessageBox.Show("Podaci nisu dobro uneti!"); }
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
