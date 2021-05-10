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
    /// <summary>
    /// Interaction logic for LekoviProzor.xaml
    /// </summary>
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
            win.Show();
        }

        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridLekovi.SelectedIndex != -1)
            {
                LekoviIzmeni win = new LekoviIzmeni(lekovi, (Lek)dataGridLekovi.SelectedItem);
                win.Show();
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
                win.Show();
            }
            else { MessageBox.Show("Morate izabrati lek!"); }
        }

        private void Neodobreni_Click(object sender, RoutedEventArgs e)
        {
            LekoviNeodobreni win = new LekoviNeodobreni();
            win.Show();
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
                    Neodobreni.Focus();
                }
                else if (Neodobreni.IsFocused)
                {
                    Pomoc.Focus();
                }
                else if (Nazad.IsFocused)
                {
                    Dodaj.Focus();
                }
                else if (dataGridLekovi.IsFocused)
                {
                    Dodaj.Focus();
                }
                else if (!Dodaj.IsFocused || !Izmeni.IsFocused || !Izbrisi.IsFocused || !Informacije.IsFocused || !Neodobreni.IsFocused || !Nazad.IsFocused)
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
                    Neodobreni.Focus();
                }
                else if (Neodobreni.IsFocused)
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
                    Dodaj.Focus();
                }
                else if (!Dodaj.IsFocused || !Izmeni.IsFocused || !Izbrisi.IsFocused || !Informacije.IsFocused || !Neodobreni.IsFocused || !Nazad.IsFocused)
                {
                    Dodaj.Focus();
                }
                else if (SaleMenu.IsFocused || LekoviMenu.IsFocused || OpremaMenu.IsFocused || PodesavanjaMenu.IsFocused || PodesavanjaNalogaMenu.IsFocused || HelpMenu.IsFocused)
                    Dodaj.Focus();
            }
            else if (e.Key == Key.Escape)
            {
                (this.Parent as Grid).Children.Remove(this);
            }
            else if (e.Key == Key.F1)
            {
                MessageBox.Show(
                    "- Use LEFT and RIGHT CTRL to move withind buttons.\n" +
                    "- Use CTRL + O to select menu bar - FILE.\n" +
                    "- Use ENTER to select the button.\n" +

                    "- Use ENTER/SPACE to close this message.\n"

                    , "HELP");
            }
            else if (e.Key == Key.F2)
            {
                if (dataGridLekovi.SelectedIndex != -1)
                {
                    LekoviIzmeni win = new LekoviIzmeni(lekovi, (Lek)dataGridLekovi.SelectedItem);
                    win.Show();
                }
                else { MessageBox.Show("Morate izabrati lek!"); }
            }
            else if (e.Key == Key.F3)
            {
                if (dataGridLekovi.SelectedIndex != -1)
                {
                    upravnikController.BrisanjeLeka((Lek)dataGridLekovi.SelectedItem);
                    lekovi.Remove((Lek)dataGridLekovi.SelectedItem);
                }
                else { MessageBox.Show("Morate izabrati lek!"); }
            }
            else if (Pomoc.IsFocused || Nazad.IsFocused)
            {
                if (e.Key == Key.Up || e.Key == Key.Down || e.Key == Key.Left || e.Key == Key.Right)
                    e.Handled = true;
            }
            else if (Dodaj.IsFocused || Izmeni.IsFocused || Izbrisi.IsFocused || Informacije.IsFocused || Neodobreni.IsFocused)
            {
                if (e.Key == Key.Up)
                    e.Handled = true;
            }
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

        }
    }
}
