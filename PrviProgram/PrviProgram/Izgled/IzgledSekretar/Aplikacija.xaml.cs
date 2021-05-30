using System;
using System.Windows;
using System.Windows.Input;
using Model;

namespace PrviProgram.Izgled.IzgledSekretar
{
    public partial class Aplikacija : Window
    {
        private Sekretar sekretar;

        public Aplikacija(Sekretar sekretar)
        {
            InitializeComponent();
            this.sekretar = sekretar;
            PocetniPage pocetniPage = new PocetniPage(this.sekretar);
            frame.NavigationService.Navigate(pocetniPage);
            textBlockNaslov.Text = "Zdravo klinika";
        }

        private void Tg_Btn_Unchecked(object sender, RoutedEventArgs e)
        {
            //header.Opacity = 1;
            //frame.Opacity = 1;
            //optionsBtn.IsEnabled = true;
        }

        private void Tg_Btn_Checked(object sender, RoutedEventArgs e)
        {
            //header.Opacity = 0.3;
            //frame.Opacity = 0.3;
            //optionsBtn.IsEnabled = false;
        }

        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tgBtn.IsChecked = false;
        }

        private void LV_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            switch (lv.SelectedIndex)
            {
                case 0:
                    PocetniPage pocetniPage = new PocetniPage(sekretar);
                    frame.NavigationService.Navigate(pocetniPage);
                    textBlockNaslov.Text = "Zdravo klinika";
                    break;
                case 1:
                    break;
                case 5:
                    Close();
                    break;
            }
            tgBtn.IsChecked = false;
        }

        private void ChangeTheme_Click(object sender, RoutedEventArgs e)
        {
            App app = (App)Application.Current;
            if (themeButton.Content.ToString() == "Tema tamna")
            {
                app.ChangeTheme(new Uri("Izgled/IzgledSekretar/Themes/Dark.xaml", UriKind.Relative));
                themeButton.Content = "Tema svetla";
            }
            else
            {
                app.ChangeTheme(new Uri("Izgled/IzgledSekretar/Themes/Light.xaml", UriKind.Relative));
                themeButton.Content = "Tema tamna";
            }
        }

    }
}