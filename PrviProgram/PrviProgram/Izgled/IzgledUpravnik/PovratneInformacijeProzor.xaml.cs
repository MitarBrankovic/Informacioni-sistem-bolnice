using Model;
using Repository;
using Service;
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

namespace PrviProgram.Izgled.IzgledUpravnik
{
    /// <summary>
    /// Interaction logic for PovratneInformacijeProzor.xaml
    /// </summary>
    public partial class PovratneInformacijeProzor : UserControl
    {
        private Osoba upravnik;
        private PovratneInformacijeService povratneInformacijeService = new PovratneInformacijeService();
        private PovratneInformacijeRepository povratneInformacijeRepository = new PovratneInformacijeRepository();

        public PovratneInformacijeProzor(Upravnik upravnik)
        {
            InitializeComponent();
            this.upravnik = upravnik;
            ProveraVecPoslato();
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            PovratneInformacije povratneInformacije = new PovratneInformacije();
            if ((bool)Da.IsChecked)
                povratneInformacije.ImaProblem = true;
            else if ((bool)Ne.IsChecked)
                povratneInformacije.ImaProblem = false;

            if ((bool)Jedan.IsChecked)
                povratneInformacije.Ocena = 1;
            else if ((bool)Dva.IsChecked)
                povratneInformacije.Ocena = 2;
            else if ((bool)Tri.IsChecked)
                povratneInformacije.Ocena = 3;
            else if ((bool)Cetiri.IsChecked)
                povratneInformacije.Ocena = 4;
            else if ((bool)Pet.IsChecked)
                povratneInformacije.Ocena = 5;

            povratneInformacije.Primedba = Textbox.Text;
            povratneInformacije.Osoba = upravnik;
            if (povratneInformacijeService.DodavanjePovratneInformacije(povratneInformacije)) {
                Potvrdi.IsEnabled = false;
                MessageBox.Show("Uspesno ste poslali povratne informacije!");
            }
        }

        private void ProveraVecPoslato() {
            List<PovratneInformacije> povratne = povratneInformacijeRepository.PregledSvihPovratnih();
            foreach(PovratneInformacije p in povratne)
            {
                if (p.Osoba.Jmbg == upravnik.Jmbg)
                    Potvrdi.IsEnabled = false;
            }
        }

        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as Grid).Children.Remove(this);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Nazad.Focus();
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.RightCtrl)
            {
                if (Nazad.IsFocused)
                {
                    Jedan.Focus();
                }
                else if (Jedan.IsFocused)
                {
                    Textbox.Focus();
                }
                else if (Textbox.IsFocused)
                {
                    Potvrdi.Focus();
                }
                else if (Potvrdi.IsFocused)
                {
                    Pomoc.Focus();
                }
                else if (!Nazad.IsFocused || !Potvrdi.IsFocused || !Pomoc.IsFocused)
                {
                    Potvrdi.Focus();
                }
            }
            else if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.LeftCtrl)
            {
                if (Pomoc.IsFocused)
                {
                    Potvrdi.Focus();
                }
                else if (Potvrdi.IsFocused)
                {
                    Textbox.Focus();
                }
                else if (Textbox.IsFocused)
                {
                    Jedan.Focus();
                }
                else if (Jedan.IsFocused)
                {
                    Nazad.Focus();
                }
                else if (!Nazad.IsFocused || !Potvrdi.IsFocused || !Pomoc.IsFocused)
                {
                    Nazad.Focus();
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
            else if (e.Key == Key.Space)
                e.Handled = true;
            else if (Dva.IsFocused || Tri.IsFocused || Cetiri.IsFocused) 
            {
                if (e.Key == Key.Up)
                    e.Handled = true;
            }
            else if (Jedan.IsFocused)
            {
                if (e.Key == Key.Up || e.Key == Key.Left)
                    e.Handled = true;
            }
            else if (Pet.IsFocused)
            {
                if (e.Key == Key.Up || e.Key == Key.Right)
                    e.Handled = true;
            }
            else if (Da.IsFocused || Ne.IsFocused)
            {
                if (e.Key == Key.Down)
                    e.Handled = true;
            }
        }


        private void PrikazPomoci()
        {
            MessageBox.Show(
            "- LCTRL/RCTRL: Kretanje kroz aplikaciju.\n" +
            "- ESCAPE: Povratak na pocetni prozor. \n" +
            "- LCTRL/RCTRL: Vracanje fokusa na dugmice kada je fokus na tabeli.\n" +
            "- F1: Otvaranje Pomoc dijaloga. \n" +

            "- ENTER/SPACE: Zatvaranje ove poruke.\n"
                , "HELP");
        }

        private void Pomoc_Click(object sender, RoutedEventArgs e)
        {
            PrikazPomoci();
        }
    }
}

