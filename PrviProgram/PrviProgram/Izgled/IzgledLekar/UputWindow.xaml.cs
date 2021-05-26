using Controller;
using Model;
using PrviProgram.Repository;
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

namespace PrviProgram.Izgled.IzgledLekar
{
    /// <summary>
    /// Interaction logic for UputWindow.xaml
    /// </summary>
    public partial class UputWindow : Window
    {
        private LekarRepository lekarRepository = new LekarRepository();
        private LekarController lekarController = new LekarController();
        private UtilityService utilityService = new UtilityService();
        private TerminiService terminiService = new TerminiService();
        private TerminiRepository terminiRepository = new TerminiRepository();
        private SpecijalizacijeRepository specijalizacijeRepository = new SpecijalizacijeRepository();
        private List<string> constVreme;
        private ObservableCollection<string> vreme;
        private ObservableCollection<TipTermina> tipTermina = new ObservableCollection<TipTermina> { TipTermina.Pregled, TipTermina.Operacija, TipTermina.Kontrola };
        private Pacijent pacijent;
        private Lekar lekar;
        public UputWindow(Pacijent pacijent, Lekar lekar)
        {
            InitializeComponent();
            this.pacijent = pacijent;
            this.lekar = lekar;
            constVreme = new List<string>(utilityService.nizVremena);
            vreme = new ObservableCollection<string>(utilityService.nizVremena);
            comboBoxSpecijalizacija.ItemsSource = specijalizacijeRepository.PregledSvihSpecijalizacija();
            SelektovanaSpecijalizacija();
        }

        private void SelektovanaSpecijalizacija()
        {
            if(comboBoxSpecijalizacija.SelectedItem == null)
            {
                comboBoxLekari.IsEnabled = false;
                comboBoxTipTermina.IsEnabled = false;
                datePicker.IsEnabled = false;
                vremeText.IsEnabled = false;
            }
            else
            {
                comboBoxLekari.IsEnabled = true;
            }
        }

        private void SelektovanLekar()
        {
            comboBoxTipTermina.IsEnabled = true;
        }

        private void selektovanTipTermina()
        {
            datePicker.IsEnabled = true;
        }

        private List<Lekar> PronadjiLekareOdredjeneSpecijalizacije()
        {
            List<Lekar> lekari = lekarRepository.PregledSvihLekara();
            foreach (Lekar lekar in lekari.ToArray())
            {
                if (!ProveriSpecijalizacijuLekara(lekar) || lekar.Jmbg == this.lekar.Jmbg)
                {
                    lekari.Remove(lekar);
                }
            }
            return lekari;
        }

        private bool ProveriSpecijalizacijuLekara(Lekar zaLekara)
        {
            Lekar lekar = lekarRepository.PregledLekara(zaLekara.Jmbg);
            foreach (Specijalizacija specijalizacija in lekar.GetSpecijalizacija())
            {
                Specijalizacija specijalizacijaCombo = (Specijalizacija)comboBoxSpecijalizacija.SelectedItem;
                if (specijalizacija.Naziv.Equals(specijalizacijaCombo.Naziv))
                {
                    return true;
                }
            }
            return false;
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            Termin termin = PreuzmiTerminIzForme();
            if(terminiService.ProveraZauzetostiTermina(termin) == false)
            {
                terminiService.DodavanjeTermina(termin);
                this.Close();
            }
            else
            {
                BrisanjeZauzetogTermina(termin);
                terminiService.DodavanjeTermina(termin);
                this.Close();
            }

        }
        private void BrisanjeZauzetogTermina(Termin termin)
        {
            foreach(Termin t in terminiRepository.PregledSvihTermina())
            {
                if(t.lekar.Jmbg == termin.lekar.Jmbg && t.Datum == termin.Datum && t.Vreme == termin.Vreme)
                {
                    terminiService.BrisanjeTermina(t);
                }
            }
        }
        private Termin PreuzmiTerminIzForme()
        {
            Termin termin = new Termin(
                (DateTime)datePicker.SelectedDate,
                (TipTermina)comboBoxTipTermina.SelectedItem,
                utilityService.GenerisanjeSifre(),
                vremeText.Text,
                (Model.Lekar)comboBoxLekari.SelectedItem,
                pacijent);
            termin.sala = TerminiService.getInstance().DobavljanjeSale(termin);
            return termin;
        }

        private void Otkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void checkBoxHitanSlucaj_Checked(object sender, RoutedEventArgs e)
        {
            PostaviVremenaNaSlobodna();
        }

        private void checkBoxHitanSlucaj_Unchecked(object sender, RoutedEventArgs e)
        {
            AzurirajVreme();
        }

        private void PostaviVremenaNaSlobodna()
        {
            vreme = new ObservableCollection<string>(constVreme);
            vremeText.ItemsSource = vreme;
            vremeText.IsEnabled = true;
        }

        private void AzurirajVreme()
        {
            if ((Lekar)comboBoxLekari.SelectedItem != null && datePicker.SelectedDate != null)
            {
                vreme = new ObservableCollection<string>(lekarController.DobavljanjeSlobodnihTerminaLekara((Lekar)comboBoxLekari.SelectedItem, (DateTime)datePicker.SelectedDate));
                vremeText.ItemsSource = vreme;
                vremeText.SelectedItem = vremeText.Items[0];
            }
            else
            {
                vremeText.IsEnabled = false;
            }
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            vremeText.IsEnabled = true;
            if (checkBoxHitanSlucaj.IsChecked == false)
            {
                AzurirajVreme();
            }
            else
            {
                PostaviVremenaNaSlobodna();
            }
        }

        private void comboBoxSpecijalizacija_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelektovanaSpecijalizacija();
            comboBoxLekari.ItemsSource = PronadjiLekareOdredjeneSpecijalizacije();
            vremeText.IsEnabled = false;
        }

        private void comboBoxLekari_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelektovanLekar();
            comboBoxTipTermina.ItemsSource = tipTermina;
        }

        private void comboBoxTipTermina_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selektovanTipTermina();
        }
    }
}
