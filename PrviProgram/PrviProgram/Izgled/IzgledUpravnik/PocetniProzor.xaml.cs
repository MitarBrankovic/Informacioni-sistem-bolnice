using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
            this.Close();
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

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {

        }
    }
}
