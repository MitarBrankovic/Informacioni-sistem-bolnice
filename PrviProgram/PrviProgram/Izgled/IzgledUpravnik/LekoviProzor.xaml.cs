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
        private ObservableCollection<Lek> pomocnaLista;
        private LekoviRepository lekoviRepository = new LekoviRepository();
        private UpravnikController upravnikController = new UpravnikController();

        public LekoviProzor()
        {
            InitializeComponent();
            lekovi = new ObservableCollection<Lek>(lekoviRepository.PregledSvihLekova());
            pomocnaLista = new ObservableCollection<Lek>(lekovi);
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
                    Textbox.Focus();
                }
                else if (Textbox.IsFocused)
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
            }
            else if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.LeftCtrl)
            {
                if (Pomoc.IsFocused)
                {
                    Textbox.Focus();
                }
                else if (Textbox.IsFocused)
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
            }
            else if (e.Key == Key.Escape)
            {
                (this.Parent as Grid).Children.Remove(this);
            }
            else if (e.Key == Key.F1)
            {
                PrikazPomoci();
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
            else if (e.Key == Key.Space)
                e.Handled = true;
        }

        private void Pomoc_Click(object sender, RoutedEventArgs e)
        {
            PrikazPomoci();
        }

        private void PrikazPomoci()
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
            "- F3: Otvaranje dijaloga o informacijama selektovanog leka. \n" +
            "- DEL: Brisanje selektovanog leka. \n\n" +

            "- ENTER/SPACE: Zatvaranje ove poruke.\n"
                , "HELP");
        }

        private void dataGridLekovi_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (dataGridLekovi.SelectedIndex != -1)
                {
                    upravnikController.BrisanjeLeka((Lek)dataGridLekovi.SelectedItem);
                    lekovi.Remove((Lek)dataGridLekovi.SelectedItem);
                }
                else { MessageBox.Show("Morate izabrati lek!"); }
                Dodaj.Focus();
            }
        }

        private void Textbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                String search;
                search = Textbox.Text;
                String[] splited = search.Split(" ");
                if (search == "")
                {
                    ResetovanjeTabele();
                }
                else if (splited.Length == 1)
                {
                    foreach (Lek l in pomocnaLista)
                    {
                        if (!lekovi.Contains(l))
                        {
                            lekovi.Add(l);
                        }
                        if (!l.Naziv.ToLower().Contains(splited[0].ToLower()))
                        {
                            lekovi.Remove(l);
                        }
                    }
                }
            }
        }
        private void ResetovanjeTabele()
        {
            foreach (Lek l in pomocnaLista)
            {
                if (!lekovi.Contains(l))
                {
                    lekovi.Add(l);
                }
            }
        }
    }
}
