using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Model;

namespace PrviProgram.Izgled.IzgledUpravnik
{
    public partial class PocetniProzor : Window
    {
        private Upravnik upravnik;
        public PocetniProzor(Upravnik upravnik)
        {
            InitializeComponent();
            this.upravnik = upravnik;
        }
                
        private void LogOutIzAplikacije()
        {
            MessageBoxResult rsltMessageBox = MessageBox.Show("Da li ste sigurni da zelite da se izlogujete?", "Log out",
        MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
            switch (rsltMessageBox)
            {
                case MessageBoxResult.Yes:
                    this.Close();
                    break;
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
        }

        private void Sale_Click(object sender, RoutedEventArgs e)
        {
            var s = new SaleProzor();
            gridMain.Children.Clear();
            gridMain.Children.Add(s);
        }

        private void IzlogujteSe_Click(object sender, RoutedEventArgs e)
        {
            LogOutIzAplikacije();
        }

        private void Oprema_Click(object sender, RoutedEventArgs e)
        {
            var s = new OpremaProzor();
            gridMain.Children.Clear();
            gridMain.Children.Add(s);
        }

        private void Lekovi_Click(object sender, RoutedEventArgs e)
        {
            var s = new LekoviProzor();
            gridMain.Children.Clear();
            gridMain.Children.Add(s);
        }

        private void Zaposleni_Click(object sender, RoutedEventArgs e)
        {
            var s = new ZaposleniProzor();
            gridMain.Children.Clear();
            gridMain.Children.Add(s);
        }

        private void Tutorijal_Click(object sender, RoutedEventArgs e)
        {
            var s = new TutorijalProzor();
            gridMain.Children.Clear();
            gridMain.Children.Add(s);
        }

        private void Podesavanja_Click_1(object sender, RoutedEventArgs e)
        {
            var s = new Podesavanja(upravnik);
            gridMain.Children.Clear();
            gridMain.Children.Add(s);
        }

        private void RadnoVreme_Click(object sender, RoutedEventArgs e)
        {
            var s = new RadnoVremeProzor();
            gridMain.Children.Clear();
            gridMain.Children.Add(s);
        }

        private void Pomoc_Click(object sender, RoutedEventArgs e)
        {
            PrikazPomoci();
        }

        private void Button_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ToolTip tooltip = (ToolTip)(sender as Control).ToolTip;
            tooltip.PlacementTarget = (UIElement)sender;
            tooltip.Placement = PlacementMode.Right;
            tooltip.PlacementRectangle = new Rect(0, (sender as Control).Height, 0, 0);
            tooltip.IsOpen = (sender as Control).IsKeyboardFocusWithin;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SaleButton.Focus();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.RightCtrl)
            {
                if (SaleButton.IsFocused)
                {
                    Zaposleni.Focus();
                }
                else if (Zaposleni.IsFocused)
                {
                    Podesavanja.Focus();
                }
                else if (Podesavanja.IsFocused)
                {
                    OpremaButton.Focus();
                }
                else if (OpremaButton.IsFocused)
                {
                    RadnoVreme.Focus();
                }
                else if (RadnoVreme.IsFocused)
                {
                    LogOutButton.Focus();
                }
                else if (LogOutButton.IsFocused)
                {
                    LekoviButton.Focus();
                }
                else if (LekoviButton.IsFocused)
                {
                    Tutorijal.Focus();
                }
                else if (Tutorijal.IsFocused)
                {
                    PovratneInformacije.Focus();
                }
                else if (PovratneInformacije.IsFocused)
                {
                    Pomoc.Focus();
                }
            }
            else if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.LeftCtrl)
            {
                if (Pomoc.IsFocused)
                {
                    PovratneInformacije.Focus();
                }
                else if (PovratneInformacije.IsFocused)
                {
                    Tutorijal.Focus();
                }
                else if (Tutorijal.IsFocused)
                {
                    LekoviButton.Focus();
                }
                else if (LekoviButton.IsFocused)
                {
                    LogOutButton.Focus();
                }
                else if (LogOutButton.IsFocused)
                {
                    RadnoVreme.Focus();
                }
                else if (RadnoVreme.IsFocused)
                {
                    OpremaButton.Focus();
                }
                else if (OpremaButton.IsFocused)
                {
                    Podesavanja.Focus();
                }
                else if (Podesavanja.IsFocused)
                {
                    Zaposleni.Focus();
                }
                else if (Zaposleni.IsFocused)
                {
                    SaleButton.Focus();
                }
            }
            else if (e.Key == Key.End)
            {
                LogOutIzAplikacije();
            }
            else if (e.Key == Key.Space)
            {
                if (!SaleButton.IsFocused || !OpremaButton.IsFocused || !LekoviButton.IsFocused || !LogOutButton.IsFocused || !Pomoc.IsFocused)
                {
                    SaleButton.Focus();
                }
            }
            else if (Pomoc.IsFocused)
            {
                if (e.Key == Key.Up || e.Key == Key.Right)
                    e.Handled = true;
            }
            else if (LogOutButton.IsFocused)
            {
                if (e.Key == Key.Down)
                    e.Handled = true;
            }
            else if (SaleButton.IsFocused || OpremaButton.IsFocused || LekoviButton.IsFocused)
            {
                if (e.Key == Key.Up)
                    e.Handled = true;
            }
        }

        private void PrikazPomoci()
        {
            MessageBox.Show(
            "- LCTRL/RCTRL: Kretanje kroz aplikaciju.\n" +
            "- N: Selektovanje MenuBar-a - FILE.\n" +
            "- ENTER: Potvrda akcije selektovanog dugmeta. \n" +
            "- SPACE: Vracanje fokusa na dugme nakon povratka na pocetni prozor.\n" +
            "- END: Izlazak iz aplikacije. \n\n" +

            "- ENTER/SPACE: Zatvaranje ove poruke.\n" +
            "NAPOMENA: Postoji mogucnost kretanja uz pomoc strelica ali se preporucuje koriscenje LCTRL/RCTRL. Menu nije u potpunosti zavrsen."
            , "HELP");
        }

        private void PovratneInformacije_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
