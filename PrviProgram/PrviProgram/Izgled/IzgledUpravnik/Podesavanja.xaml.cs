using Model;
using PrviProgram.ViewModel;
using Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrviProgram.Izgled.IzgledUpravnik
{
    public partial class Podesavanja : UserControl
    {
        public PodesavanjaViewModel podesavanjaViewModel;

        public Podesavanja(Upravnik upravnik)
        {
            podesavanjaViewModel = new PodesavanjaViewModel(upravnik, this);
            InitializeComponent();
            this.DataContext = podesavanjaViewModel;
        }

        #region Navigacija kroz prozor
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Da.Focus();
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.RightCtrl)
            {
                if (Da.IsFocused)
                {
                    Ne.Focus();
                }
                else if (Ne.IsFocused)
                {
                    Pomoc.Focus();
                }
                else if (Nazad.IsFocused)
                {
                    Izmena.Focus();
                }
                else if (Izmena.IsFocused)
                {
                    Da.Focus();
                }
            }
            else if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.LeftCtrl)
            {
                if (Pomoc.IsFocused)
                {
                    Ne.Focus();
                }
                else if (Ne.IsFocused)
                {
                    Da.Focus();
                }
                else if (Da.IsFocused)
                {
                    Izmena.Focus();
                }
                else if (Izmena.IsFocused)
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
                MessageBox.Show(
                "- LCTRL/RCTRL: Kretanje kroz aplikaciju.\n" +
                "- M: Selektovanje MenuBar-a - FILE.\n" +
                "- ENTER: Potvrda akcije selektovanog dugmeta. \n" +
                "- ESCAPE: Povratak na pocetni prozor. \n" +
                "- LCTRL/RCTRL: Vracanje fokusa na dugmice kada je fokus na tabeli.\n" +
                "- F1: Otvaranje Pomoc dijaloga. \n" +

                "- ENTER/SPACE: Zatvaranje ove poruke.\n"
                , "HELP");
            }
            else if (e.Key == Key.Space || e.Key == Key.N)
                e.Handled = true;
            else if (Pomoc.IsFocused || Nazad.IsFocused || Da.IsFocused || Ne.IsFocused || Izmena.IsFocused)
            {
                if (e.Key == Key.Up || e.Key == Key.Down || e.Key == Key.Left || e.Key == Key.Right)
                    e.Handled = true;
            }
        }

        private void Button_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UtilityService utilityService = new UtilityService();
            utilityService.InicijalizacijaToolTipa(sender);
        }
        #endregion
    }
}
