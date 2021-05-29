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

    public partial class ZaposleniProzor : UserControl
    {
        private ObservableCollection<Osoba> zaposlenii;
        private LekarRepository lekarRepository = new LekarRepository();
        private SekretarRepository sekretarRepository = new SekretarRepository();
        private SekretarController sekretarController = new SekretarController();
        private SekretarService sekretarService = new SekretarService();

        public ZaposleniProzor()
        {
            InitializeComponent();
            Inicijalizacija();
            dataGridZaposleni.ItemsSource = zaposlenii;
        }


        private void Inicijalizacija() 
        {
            List<Osoba> zaposleni = new List<Osoba>();
            List<Lekar> lekari = lekarRepository.PregledSvihLekara();
            List<Sekretar> sekretari = sekretarRepository.PregledSvihSekretara();
            zaposleni.AddRange(lekari);
            zaposleni.AddRange(sekretari);
            zaposlenii = new ObservableCollection<Osoba>(zaposleni);
        }

        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridZaposleni.SelectedIndex != -1)
            {
                ZaposleniIzmeni win = new ZaposleniIzmeni(zaposlenii, (Osoba)dataGridZaposleni.SelectedItem);
                win.ShowDialog();
            }
            else { MessageBox.Show("Morate izabrati zaposlenog!"); }
        }

        private void Izbrisi_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridZaposleni.SelectedIndex != -1)
            {
                Osoba selektovanaOsoba = (Osoba)dataGridZaposleni.SelectedItem;
                if (selektovanaOsoba.Korisnik.TipKorisnika == TipKorisnika.Lekar)
                {
                    sekretarController.BrisanjeLekara(lekarRepository.PregledLekara(selektovanaOsoba.Jmbg));
                    zaposlenii.Remove(selektovanaOsoba);
                }
                else if (selektovanaOsoba.Korisnik.TipKorisnika == TipKorisnika.Sekretar)
                {
                    sekretarService.BrisanjeSekretara(sekretarRepository.PregledSekretara(selektovanaOsoba.Jmbg));
                    zaposlenii.Remove(selektovanaOsoba);
                }
            }
            else { MessageBox.Show("Morate izabrati zaposlenog!"); }
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            ZaposleniDodaj win = new ZaposleniDodaj(zaposlenii);
            win.ShowDialog();
        }

        private void Informacije_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as Grid).Children.Remove(this);
        }

        private void Pomoc_Click(object sender, RoutedEventArgs e)
        {
            PrikazPomoci();
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
                    Pomoc.Focus();
                }
                else if (Nazad.IsFocused)
                {
                    Dodaj.Focus();
                }
                else if (dataGridZaposleni.IsFocused)
                {
                    Izmeni.Focus();
                }
                else if (!Dodaj.IsFocused || !Izmeni.IsFocused || !Izbrisi.IsFocused || !Informacije.IsFocused || !Nazad.IsFocused)
                {
                    Dodaj.Focus();
                }

            }
            else if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.LeftCtrl)
            {
                if (Pomoc.IsFocused)
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
                else if (dataGridZaposleni.IsFocused)
                {
                    Izmeni.Focus();
                }
                else if (!Dodaj.IsFocused && !Izmeni.IsFocused && !Izbrisi.IsFocused && !Informacije.IsFocused && !Nazad.IsFocused)
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
                if (dataGridZaposleni.SelectedIndex != -1)
                {
                    ZaposleniIzmeni win = new ZaposleniIzmeni(zaposlenii, (Osoba)dataGridZaposleni.SelectedItem);
                    win.ShowDialog();
                }
                else { MessageBox.Show("Morate izabrati zaposlenog!"); }
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
            else if (Dodaj.IsFocused || Izmeni.IsFocused || Izbrisi.IsFocused || Informacije.IsFocused)
            {
                if (e.Key == Key.Up)
                    e.Handled = true;
            }
            else if (e.Key == Key.Space || e.Key == Key.N)
                e.Handled = true;
        }

        private void dataGridZaposleni_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (dataGridZaposleni.SelectedIndex != -1)
                {
                    Osoba selektovanaOsoba = (Osoba)dataGridZaposleni.SelectedItem;
                    if (selektovanaOsoba.Korisnik.TipKorisnika == TipKorisnika.Lekar)
                    {
                        sekretarController.BrisanjeLekara(lekarRepository.PregledLekara(selektovanaOsoba.Jmbg));
                        zaposlenii.Remove(selektovanaOsoba);
                    }
                    else if (selektovanaOsoba.Korisnik.TipKorisnika == TipKorisnika.Sekretar)
                    {
                        sekretarService.BrisanjeSekretara(sekretarRepository.PregledSekretara(selektovanaOsoba.Jmbg));
                        zaposlenii.Remove(selektovanaOsoba);
                    }
                }
                else { MessageBox.Show("Morate izabrati zaposlenog!"); }
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
            "- F2: Otvaranje dijaloga izmene selektovanog zaposlenog. \n" +
            "- F3: Otvaranje dijaloga o informacijama selektovanog zaposlenog. \n" +
            "- DEL: Brisanje selektovanog zaposlenog. \n\n" +

            "- ENTER/SPACE: Zatvaranje ove poruke.\n"
                , "HELP");
        }
    }
}
