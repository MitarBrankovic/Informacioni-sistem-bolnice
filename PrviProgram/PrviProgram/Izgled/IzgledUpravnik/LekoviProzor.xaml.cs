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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Controller;
using Model;
using Repository;
using System.Collections.ObjectModel;

namespace PrviProgram.Izgled.IzgledUpravnik
{
    public partial class LekoviProzor : UserControl
    {
        private ObservableCollection<Lek> lekovi;
        private LekoviRepository lekoviRepository = new LekoviRepository();
        private UpravnikController upravnikController = new UpravnikController();

        public LekoviProzor()
        {
            InitializeComponent();
            lekovi = new ObservableCollection<Lek>(lekoviRepository.PregledSvihLekova());
            dataGridLekovi.ItemsSource = lekovi;
        }

        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as Grid).Children.Remove(this);
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            LekoviDodaj win = new LekoviDodaj(lekovi);
            win.ShowDialog();
        }

        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridLekovi.SelectedIndex != -1)
            {
                LekoviIzmeni win = new LekoviIzmeni(lekovi, (Lek)dataGridLekovi.SelectedItem);
                win.ShowDialog();
            }
            else { MessageBox.Show("Morate izabrati lek!"); }
        }

        private void Izbrisi_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridLekovi.SelectedIndex != -1)
            {
                upravnikController.BrisanjeLeka((Lek)dataGridLekovi.SelectedItem);
                lekovi.Remove((Lek)dataGridLekovi.SelectedItem);
            }
            else { MessageBox.Show("Morate izabrati lek!"); }
        }

        private void Informacije_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridLekovi.SelectedIndex != -1)
            {
                Lek lek = (Lek)dataGridLekovi.SelectedItem;
                LekoviInformacije win = new LekoviInformacije(lekoviRepository.PregledLeka(lek.Sifra));
                win.ShowDialog();
            }
            else { MessageBox.Show("Morate izabrati lek!"); }
        }

        private void Primedbe_Click(object sender, RoutedEventArgs e)
        {
            LekoviPrimedbe win = new LekoviPrimedbe();
            win.ShowDialog();
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DodajLek_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Dodaj.Focus();
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
                    Informacije.Focus();
                }
                else if (Informacije.IsFocused)
                {
                    Primedbe.Focus();
                }
                else if (Primedbe.IsFocused)
                {
                    Pomoc.Focus();
                }
                else if (Nazad.IsFocused)
                {
                    Dodaj.Focus();
                }
                else if (dataGridLekovi.IsFocused)
                {
                    Izmeni.Focus();
                }
                else if (!Dodaj.IsFocused || !Izmeni.IsFocused || !Izbrisi.IsFocused || !Informacije.IsFocused || !Primedbe.IsFocused || !Nazad.IsFocused)
                {
                    Dodaj.Focus();
                }
                else if (SaleMenu.IsFocused || LekoviMenu.IsFocused || OpremaMenu.IsFocused || PodesavanjaMenu.IsFocused || PodesavanjaNalogaMenu.IsFocused || HelpMenu.IsFocused)
                    Dodaj.Focus();

            }
            else if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.LeftCtrl)
            {
                if (Pomoc.IsFocused)
                {
                    Primedbe.Focus();
                }
                else if (Primedbe.IsFocused)
                {
                    Informacije.Focus();
                }
                else if (Informacije.IsFocused)
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
                else if (dataGridLekovi.IsFocused)
                {
                    Izmeni.Focus();
                }
                else if (!Dodaj.IsFocused && !Izmeni.IsFocused && !Izbrisi.IsFocused && !Informacije.IsFocused && !Primedbe.IsFocused && !Nazad.IsFocused)
                {
                    Dodaj.Focus();
                }
                else if (SaleMenu.IsFocused || LekoviMenu.IsFocused || OpremaMenu.IsFocused || PodesavanjaMenu.IsFocused || PodesavanjaNalogaMenu.IsFocused || HelpMenu.IsFocused)
                    Dodaj.Focus();
            }
            else if (e.Key == Key.M)
            {
                File.Focus();
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
                if (dataGridLekovi.SelectedIndex != -1)
                {
                    LekoviIzmeni win = new LekoviIzmeni(lekovi, (Lek)dataGridLekovi.SelectedItem);
                    win.ShowDialog();
                }
                else { MessageBox.Show("Morate izabrati lek!"); }
                Dodaj.Focus();
            }
            else if (e.Key == Key.F3)
            {
                if (dataGridLekovi.SelectedIndex != -1)
                {
                    upravnikController.BrisanjeLeka((Lek)dataGridLekovi.SelectedItem);
                    lekovi.Remove((Lek)dataGridLekovi.SelectedItem);
                }
                else { MessageBox.Show("Morate izabrati lek!"); }
                Dodaj.Focus();
            }
            else if (e.Key == Key.F4)
            {
                if (dataGridLekovi.SelectedIndex != -1)
                {
                    Lek lek = (Lek)dataGridLekovi.SelectedItem;
                    LekoviInformacije win = new LekoviInformacije(lekoviRepository.PregledLeka(lek.Sifra));
                    win.ShowDialog();
                }
                else { MessageBox.Show("Morate izabrati lek!"); }
                Dodaj.Focus();
            }
            else if (Pomoc.IsFocused || Nazad.IsFocused)
            {
                if (e.Key == Key.Up || e.Key == Key.Down || e.Key == Key.Left || e.Key == Key.Right)
                    e.Handled = true;
            }
            else if (Dodaj.IsFocused || Izmeni.IsFocused || Izbrisi.IsFocused || Informacije.IsFocused)
            {
                if (e.Key == Key.Up)
                    e.Handled = true;
            }
            else if (Primedbe.IsFocused)
            {
                if (e.Key == Key.Up || e.Key == Key.Right)
                    e.Handled = true;
            }
            else if (Dodaj.IsFocused || Izmeni.IsFocused || Izbrisi.IsFocused || Informacije.IsFocused || Primedbe.IsFocused)
            {
                if (e.Key == Key.Up)
                    e.Handled = true;
            }
            else if (e.Key == Key.Space || e.Key == Key.N)
                e.Handled = true;
        }

        private void PodesavanjaNalogaMenu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PodesavanjaMenu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaleMenu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OpremaMenu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LekoviMenu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HelpMenu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Pomoc_Click(object sender, RoutedEventArgs e)
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
    }
}
