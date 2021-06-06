using Controller;
using Model;
using PrviProgram.Repository;
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
using System.Windows.Shapes;

namespace PrviProgram.Izgled.IzgledUpravnik
{
    public partial class LekoviPrimedbe : Window
    {
        private PrimedbeNaLekRepository primedbeNaLekRepository = new PrimedbeNaLekRepository();
        private ObservableCollection<PrimedbaNaLek> primedbe;
        private LekoviController lekoviController = new LekoviController();
        private ObservableCollection<Lek> lekovi;
        private LekoviRepository lekoviRepository = new LekoviRepository();

        public LekoviPrimedbe()
        {
            InitializeComponent();
            primedbe = new ObservableCollection<PrimedbaNaLek>(primedbeNaLekRepository.PregledSvihPrimedbi());
            lekovi = new ObservableCollection<Lek>(lekoviRepository.PregledSvihLekova());
            dataGridPrimedbe.ItemsSource = primedbe;
        }

        private void Prikaz_Click(object sender, RoutedEventArgs e)
        {
            PrimedbaNaLek primedba = (PrimedbaNaLek)dataGridPrimedbe.SelectedItem;
            MessageBox.Show(primedba.Primedba, "Primedba");
        }

        private void Obrisi_Click(object sender, RoutedEventArgs e)
        {
            PrimedbaNaLek primedba = (PrimedbaNaLek)dataGridPrimedbe.SelectedItem;
            lekoviController.BrisanjePrimedbe(primedba);
            primedbe.Remove(primedba);
            LekoviIzmeni win = new LekoviIzmeni(lekovi, primedba.Lek);
            win.ShowDialog();
        }

        private void Pomoc_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
            "- LCTRL/RCTRL: Kretanje kroz aplikaciju.\n" +
            "- Tab: Selektovanje dugmeta unutar tabele.\n"+

            "- ENTER/SPACE: Zatvaranje ove poruke.\n"
                , "HELP");
        }

        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Pomoc.Focus();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.RightCtrl)
            {
                if (Nazad.IsFocused || dataGridPrimedbe.IsFocused)
                    Pomoc.Focus();
                else if (!Nazad.IsFocused || !Pomoc.IsFocused)
                    Pomoc.Focus();

            }
            else if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.LeftCtrl)
            {
                if (Pomoc.IsFocused || dataGridPrimedbe.IsFocused)
                    Nazad.Focus();
                else if (!Nazad.IsFocused || !Pomoc.IsFocused)
                    Nazad.Focus();
            }
            else if (e.Key == Key.Escape)
            {
                this.Close();
            }
            else if (e.Key == Key.F1)
            {
                MessageBox.Show(
                "- LCTRL/RCTRL: Kretanje kroz aplikaciju.\n" +
                "- Tab: Selektovanje dugmeta unutar tabele.\n" +

                "- ENTER/SPACE: Zatvaranje ove poruke.\n"
                    , "HELP");
            }
        }
    }
}
