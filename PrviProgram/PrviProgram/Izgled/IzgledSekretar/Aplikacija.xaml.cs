using System;
using System.Windows;
using System.Windows.Input;
using Model;

namespace PrviProgram.Izgled.IzgledSekretar
{
    public partial class Aplikacija : Window
    {
        private Sekretar sekretar;
        private string CurrentLanguage { get; set; }
        private string CurrentTheme { get; set; }

        public Aplikacija(Sekretar sekretar)
        {
            InitializeComponent();
            this.sekretar = sekretar;
            PocetniPage pocetniPage = new PocetniPage(this.sekretar);
            frame.NavigationService.Navigate(pocetniPage);
            textBlockNaslov.Text = "Zdravo klinika";
            CurrentLanguage = "en-US";
            CurrentTheme = "Dark";
        }

        private void Tg_Btn_Unchecked(object sender, RoutedEventArgs e)
        {
            optionsBtn.IsEnabled = true;
        }

        private void Tg_Btn_Checked(object sender, RoutedEventArgs e)
        {
            optionsBtn.IsEnabled = false;
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
                    break;
            }
            tgBtn.IsChecked = false;
        }

        private void ChangeTheme_Click(object sender, RoutedEventArgs e)
        {
            App app = (App)Application.Current;
            if (CurrentTheme.Equals("Dark"))
            {
                app.ChangeTheme(new Uri("Izgled/IzgledSekretar/Themes/Light.xaml", UriKind.Relative));
                CurrentTheme = "Light";
            }
            else
            {
                app.ChangeTheme(new Uri("Izgled/IzgledSekretar/Themes/Dark.xaml", UriKind.Relative));
                CurrentTheme = "Dark";
            }
        }

        private void SwitchLanguage_Click(object sender, RoutedEventArgs e)
        {
            App app = (App)Application.Current;
            if (CurrentLanguage.Equals("en-US"))
            {
                CurrentLanguage = "sr-LATN";
            }
            else
            {
                CurrentLanguage = "en-US";
            }
            app.ChangeLanguage(CurrentLanguage);
        }

        private void ButtonPopUpLogout_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}