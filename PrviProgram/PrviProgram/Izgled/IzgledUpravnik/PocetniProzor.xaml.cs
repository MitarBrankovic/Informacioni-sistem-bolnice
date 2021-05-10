using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Model;

namespace PrviProgram.Izgled.IzgledUpravnik
{
    public partial class PocetniProzor : Window
    {
        public PocetniProzor(Upravnik upravnik)
        {
            InitializeComponent();
        }

        private void Sale_Click(object sender, RoutedEventArgs e)
        {
            var s = new SaleProzor();
            gridMain.Children.Clear();
            gridMain.Children.Add(s);
        }

        private void IzlogujteSe_Click(object sender, RoutedEventArgs e)
        {
            string sMessageBoxText = "Da li ste sigurni da zelite da se izlogujete?";
            string sCaption = "Log out";
            MessageBoxButton btnMessageBox = MessageBoxButton.YesNoCancel;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;
            MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
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

        public void Ocisti()
        {
            gridMain.Children.Clear();
        }

        private void Pomoc_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "- Use LEFT and RIGHT CTRL to move withind buttons.\n" +
                "- Use CTRL + O to select menu bar - FILE.\n" +
                "- Use ENTER to select the button.\n" +

                "- Use ENTER/SPACE to close this message.\n"

                , "HELP");
        }

        private void PodesavanjaNaloga_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Podesavanja_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OpremaPrikaz_Click(object sender, RoutedEventArgs e)
        {

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
                    OpremaButton.Focus();
                }
                else if (OpremaButton.IsFocused)
                {
                    LekoviButton.Focus();
                }
                else if (LekoviButton.IsFocused)
                {
                    Pomoc.Focus();
                }
                else if (LogOutButton.IsFocused)
                {
                    LekoviButton.Focus();
                }
                /*else if (!SaleButton.IsFocused || !OpremaButton.IsFocused || !LekoviButton.IsFocused || !LogOutButton.IsFocused || !Pomoc.IsFocused)
                {
                    SaleButton.Focus();
                }*/
                else if (SaleMenu.IsFocused || LekoviMenu.IsFocused || OpremaMenu.IsFocused || PodesavanjaMenu.IsFocused || PodesavanjaNalogaMenu.IsFocused || HelpMenu.IsFocused)
                    SaleButton.Focus();

            }
            else if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.LeftCtrl)
            {
                if (SaleButton.IsFocused)
                {
                    LogOutButton.Focus();
                }
                else if (LogOutButton.IsFocused)
                {
                    SaleButton.Focus();
                }
                else if (LekoviButton.IsFocused)
                {
                    OpremaButton.Focus();
                }
                else if (OpremaButton.IsFocused)
                {
                    SaleButton.Focus();
                }
                else if (Pomoc.IsFocused)
                {
                    LekoviButton.Focus();
                }
                else if (SaleMenu.IsFocused || LekoviMenu.IsFocused || OpremaMenu.IsFocused || PodesavanjaMenu.IsFocused || PodesavanjaNalogaMenu.IsFocused || HelpMenu.IsFocused)
                    SaleButton.Focus();
            }
            else if (e.Key == Key.End)
            {
                string sMessageBoxText = "Da li ste sigurni da zelite da se izlogujete?";
                string sCaption = "Log out";
                MessageBoxButton btnMessageBox = MessageBoxButton.YesNoCancel;
                MessageBoxImage icnMessageBox = MessageBoxImage.Warning;
                MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
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
            else if (e.Key == Key.Space)
            {
                if (!SaleButton.IsFocused || !OpremaButton.IsFocused || !LekoviButton.IsFocused || !LogOutButton.IsFocused || !Pomoc.IsFocused)
                {
                    SaleButton.Focus();
                }
            }

        }
    }
}
