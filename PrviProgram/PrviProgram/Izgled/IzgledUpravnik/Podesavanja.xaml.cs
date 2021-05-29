using Model;
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
        private bool isToolTipVisible = true;
        private UtilityService utilityService = new UtilityService();
        private Upravnik upravnik;

        public Podesavanja(Upravnik upravnik)
        {
            InitializeComponent();
            this.upravnik = upravnik;
        }

        private void Da_Click(object sender, RoutedEventArgs e)
        {
            this.isToolTipVisible = true;
            Style style = new Style(typeof(ToolTip));
            style.Setters.Add(new Setter(UIElement.VisibilityProperty, Visibility.Collapsed));
            style.Seal();
            foreach (Window window in Application.Current.Windows)
            {
                window.Resources.Remove(typeof(ToolTip));
                isToolTipVisible = true;
            }
        }

        private void Ne_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult rsltMessageBox = MessageBox.Show("Are you sure you want to disable tooltips?", "Tooltips",
                MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
            switch (rsltMessageBox)
            {
                case MessageBoxResult.Yes:
                    this.isToolTipVisible = false;

                    Style style = new Style(typeof(ToolTip));
                    style.Setters.Add(new Setter(UIElement.VisibilityProperty, Visibility.Collapsed));
                    style.Seal();

                    foreach (Window window in Application.Current.Windows)
                    {
                        window.Resources.Add(typeof(ToolTip), style);
                        isToolTipVisible = false;
                    }
                    break;
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
        }

        private void Izmena_Click(object sender, RoutedEventArgs e)
        {
            PodesavanjaNalogaUpravnika win = new PodesavanjaNalogaUpravnika(upravnik);
            win.ShowDialog();
        }

        private void Pomoc_Click(object sender, RoutedEventArgs e)
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

        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as Grid).Children.Remove(this);
        }

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
            utilityService.InicijalizacijaToolTipa(sender);
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            utilityService.LogOutUpravnikaUserControl(this);
        }
    }
}
