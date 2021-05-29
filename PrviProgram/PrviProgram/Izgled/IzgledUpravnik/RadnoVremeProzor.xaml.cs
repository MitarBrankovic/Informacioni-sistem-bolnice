using Controller;
using Model;
using PrviProgram.Service;
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

namespace PrviProgram.Izgled.IzgledUpravnik
{
    public partial class RadnoVremeProzor : UserControl
    {
        private ObservableCollection<RadnoVremeLekara> radnaVremenaLekara;
        private RadnoVremeLekaraRepository radnoVremeLekaraRepository = new RadnoVremeLekaraRepository();
        private RadnoVremeService radnoVremeService = new RadnoVremeService();
        private UpravnikController upravnikController = new UpravnikController();

        public RadnoVremeProzor()
        {
            InitializeComponent();
            radnaVremenaLekara = new ObservableCollection<RadnoVremeLekara>(radnoVremeLekaraRepository.PregledSvihRadnihVremena());
            dataGridRadnaVremena.ItemsSource = radnaVremenaLekara;
        }

        private void Izvestaj_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Kalendar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as Grid).Children.Remove(this);
        }

        private void Pomoc_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Izbrisi_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridRadnaVremena.SelectedIndex != -1)
            {
                radnoVremeService.BrisanjeLeka((RadnoVremeLekara)dataGridRadnaVremena.SelectedItem);
                radnaVremenaLekara.Remove((RadnoVremeLekara)dataGridRadnaVremena.SelectedItem);
            }
            else { MessageBox.Show("Morate izabrati radno vreme!"); }
        }

        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridRadnaVremena.SelectedIndex != -1)
            {
                RadnoVremeIzmeni win = new RadnoVremeIzmeni(radnaVremenaLekara, (RadnoVremeLekara)dataGridRadnaVremena.SelectedItem);
                win.ShowDialog();
            }
            else { MessageBox.Show("Morate izabrati radno vreme!"); }
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            RadnoVremeDodaj win = new RadnoVremeDodaj(radnaVremenaLekara);
            win.ShowDialog();
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.RightCtrl)
            {
                if (Dodaj.IsFocused)
                {
                    Izmeni.Focus();
                }
                else if (Izmeni.IsFocused)
                {
                    Izbrisi.Focus();
                }
                else if (Izbrisi.IsFocused)
                {
                    Izvestaj.Focus();
                }
                else if (Izvestaj.IsFocused)
                {
                    Kalendar.Focus();
                }
                else if (Kalendar.IsFocused)
                {
                    Pomoc.Focus();
                }
                else if (Nazad.IsFocused)
                {
                    Dodaj.Focus();
                }
                else if (dataGridRadnaVremena.IsFocused)
                {
                    Izmeni.Focus();
                }
                else if (!Dodaj.IsFocused || !Izmeni.IsFocused || !Izbrisi.IsFocused || !Izvestaj.IsFocused || !Kalendar.IsFocused || !Nazad.IsFocused)
                {
                    Dodaj.Focus();
                }
            }
            else if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.LeftCtrl)
            {
                if (Pomoc.IsFocused)
                {
                    Kalendar.Focus();
                }
                else if (Kalendar.IsFocused)
                {
                    Izvestaj.Focus();
                }
                else if (Izvestaj.IsFocused)
                {
                    Izbrisi.Focus();
                }
                else if (Izbrisi.IsFocused)
                {
                    Izmeni.Focus();
                }
                else if (Izmeni.IsFocused)
                {
                    Dodaj.Focus();
                }
                else if (Dodaj.IsFocused)
                {
                    Nazad.Focus();
                }
                else if (dataGridRadnaVremena.IsFocused)
                {
                    Izmeni.Focus();
                }
                else if (!Dodaj.IsFocused && !Izmeni.IsFocused && !Izbrisi.IsFocused && !Izvestaj.IsFocused && !Kalendar.IsFocused && !Nazad.IsFocused)
                {
                    Dodaj.Focus();
                }
            }
            else if (e.Key == Key.Escape)
            {
                (this.Parent as Grid).Children.Remove(this);
            }
            else if (e.Key == Key.F1)
            {
                MessageBox.Show(
                "- LCTRL/RCTRL: Kretanje kroz aplikaciju.\n" +
                "- M: Selektovanje MenuBar-a - FILE.\n" +
                "- ENTER: Potvrda akcije selektovanog dugmeta. \n" +
                "- ESCAPE: Povratak na pocetni prozor. \n" +
                "- DOWN: Selektovanje tabele kada dugmici iznad tabele imaju fokus. \n" +
                "- LCTRL/RCTRL: Vracanje fokusa na dugmice kada je fokus na tabeli.\n" +
                "- F1: Otvaranje Pomoc dijaloga. \n" +
                "- F2: Otvaranje dijaloga izmene selektovanog leka. \n" +
                "- F3: Brisanje selektovanog leka. \n" +
                "- F4: Otvaranje dijaloga o informacijama selektovanog leka. \n\n" +

                "- ENTER/SPACE: Zatvaranje ove poruke.\n"
                    , "HELP");
            }
            else if (e.Key == Key.F2)
            {
                if (dataGridRadnaVremena.SelectedIndex != -1)
                {
                    RadnoVremeIzmeni win = new RadnoVremeIzmeni(radnaVremenaLekara, (RadnoVremeLekara)dataGridRadnaVremena.SelectedItem);
                    win.ShowDialog();
                }
                else { MessageBox.Show("Morate izabrati radno vreme!"); }
                Dodaj.Focus();
            }
            else if (e.Key == Key.F3)
            {
                if (dataGridRadnaVremena.SelectedIndex != -1)
                {
                    radnoVremeService.BrisanjeLeka((RadnoVremeLekara)dataGridRadnaVremena.SelectedItem);
                    radnaVremenaLekara.Remove((RadnoVremeLekara)dataGridRadnaVremena.SelectedItem);
                }
                else { MessageBox.Show("Morate izabrati radno vreme!"); }
                Dodaj.Focus();
            }
            else if (e.Key == Key.F4)
            {
                if (dataGridRadnaVremena.SelectedIndex != -1)
                {
                    Lek lek = (Lek)dataGridRadnaVremena.SelectedItem;
                    //LekoviIzvestaj win = new LekoviIzvestaj(lekoviRepository.PregledLeka(lek.Sifra));
                   // win.ShowDialog();
                }
                else { MessageBox.Show("Morate izabrati radno vreme!"); }
                Dodaj.Focus();
            }
            else if (Pomoc.IsFocused || Nazad.IsFocused)
            {
                if (e.Key == Key.Up || e.Key == Key.Down || e.Key == Key.Left || e.Key == Key.Right)
                    e.Handled = true;
            }
            else if (Dodaj.IsFocused || Izmeni.IsFocused || Izbrisi.IsFocused || Izvestaj.IsFocused)
            {
                if (e.Key == Key.Up)
                    e.Handled = true;
            }
            else if (Kalendar.IsFocused)
            {
                if (e.Key == Key.Up || e.Key == Key.Right)
                    e.Handled = true;
            }
            else if (Dodaj.IsFocused || Izmeni.IsFocused || Izbrisi.IsFocused || Izvestaj.IsFocused || Kalendar.IsFocused)
            {
                if (e.Key == Key.Up)
                    e.Handled = true;
            }
            else if (e.Key == Key.Space || e.Key == Key.N)
                e.Handled = true;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
