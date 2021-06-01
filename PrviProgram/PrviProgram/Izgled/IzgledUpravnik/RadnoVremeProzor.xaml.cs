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
        private ObservableCollection<RadnoVremeLekara> pomocnaLista;
        private RadnoVremeLekaraRepository radnoVremeLekaraRepository = new RadnoVremeLekaraRepository();
        private RadnoVremeService radnoVremeService = new RadnoVremeService();
        private UpravnikController upravnikController = new UpravnikController();

        public RadnoVremeProzor()
        {
            InitializeComponent();
            radnaVremenaLekara = new ObservableCollection<RadnoVremeLekara>(radnoVremeLekaraRepository.PregledSvihRadnihVremena());
            pomocnaLista = new ObservableCollection<RadnoVremeLekara>(radnaVremenaLekara);
            dataGridRadnaVremena.ItemsSource = radnaVremenaLekara;
        }

        private void Izvestaj_Click(object sender, RoutedEventArgs e)
        {
            RadnoVremeIzvestaj win = new RadnoVremeIzvestaj(radnaVremenaLekara);
            win.ShowDialog();
        }

        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as Grid).Children.Remove(this);
        }

        private void Pomoc_Click(object sender, RoutedEventArgs e)
        {
            PrikazPomoci();
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
                    foreach (RadnoVremeLekara r in pomocnaLista)
                    {
                        if (!radnaVremenaLekara.Contains(r))
                        {
                            radnaVremenaLekara.Add(r);
                        }
                        if (!r.Lekar.Ime.ToLower().Contains(splited[0].ToLower()))
                        {
                            radnaVremenaLekara.Remove(r);
                        }
                    }
                }
                else if (splited.Length == 2)
                {
                    foreach (RadnoVremeLekara r in pomocnaLista)
                    {
                        if (!radnaVremenaLekara.Contains(r))
                        {
                            radnaVremenaLekara.Add(r);
                        }
                        if (!r.Lekar.Ime.ToLower().Contains(splited[0].ToLower()) || !r.Lekar.Prezime.ToLower().Contains(splited[1].ToLower()))
                        {
                            radnaVremenaLekara.Remove(r);
                        }
                    }
                }
            }
        }

        private void ResetovanjeTabele()
        {
            foreach (RadnoVremeLekara r in pomocnaLista)
            {
                if (!radnaVremenaLekara.Contains(r))
                {
                    radnaVremenaLekara.Add(r);
                }
            }
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
                else if (dataGridRadnaVremena.IsFocused)
                {
                    Izmeni.Focus();
                }
                else if (!Dodaj.IsFocused || !Izmeni.IsFocused || !Izbrisi.IsFocused || !Izvestaj.IsFocused || !Nazad.IsFocused)
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
                else if (!Dodaj.IsFocused && !Izmeni.IsFocused && !Izbrisi.IsFocused && !Izvestaj.IsFocused && !Nazad.IsFocused)
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

                }
                else { MessageBox.Show("Morate izabrati radno vreme!"); }
                Dodaj.Focus();
            }
            else if (Pomoc.IsFocused || Nazad.IsFocused)
            {
                if (e.Key == Key.Up || e.Key == Key.Down || e.Key == Key.Left || e.Key == Key.Right)
                    e.Handled = true;
            }
            else if (Dodaj.IsFocused || Izmeni.IsFocused || Izbrisi.IsFocused)
            {
                if (e.Key == Key.Up)
                    e.Handled = true;
            }
            else if (Izvestaj.IsFocused)
            {
                if (e.Key == Key.Up || e.Key == Key.Right)
                    e.Handled = true;
            }
            else if (Dodaj.IsFocused || Izmeni.IsFocused || Izbrisi.IsFocused || Izvestaj.IsFocused)
            {
                if (e.Key == Key.Up)
                    e.Handled = true;
            }
            else if (e.Key == Key.Space || e.Key == Key.N)
                e.Handled = true;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Dodaj.Focus();
        }

        private void dataGridRadnaVremena_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (dataGridRadnaVremena.SelectedIndex != -1)
                {
                    radnoVremeService.BrisanjeLeka((RadnoVremeLekara)dataGridRadnaVremena.SelectedItem);
                    radnaVremenaLekara.Remove((RadnoVremeLekara)dataGridRadnaVremena.SelectedItem);
                }
                else { MessageBox.Show("Morate izabrati radno vreme!"); }
                Dodaj.Focus();
            }
        }

        private void PrikazPomoci()
        {
            MessageBox.Show(
            "- LCTRL/RCTRL: Kretanje kroz aplikaciju.\n" +
            "- ENTER: Potvrda akcije selektovanog dugmeta. \n" +
            "- ESCAPE: Povratak na pocetni prozor. \n" +
            "- DOWN: Selektovanje tabele kada dugmici iznad tabele imaju fokus. \n" +
            "- LCTRL/RCTRL: Vracanje fokusa na dugmice kada je fokus na tabeli.\n" +
            "- F1: Otvaranje Pomoc dijaloga. \n" +
            "- F2: Otvaranje dijaloga izmene selektovanog radnog vremena. \n" +
            "- F3: Otvaranje dijaloga o informacijama selektovanog radnog vremena. \n" +
            "- DEL: Brisanje selektovanog radnog vremena. \n\n" +

            "- ENTER/SPACE: Zatvaranje ove poruke.\n"
                , "HELP");
        }

    }
}
