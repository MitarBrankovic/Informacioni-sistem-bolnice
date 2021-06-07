using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Model;
using PrviProgram.Izgled.IzgledSekretar.Views;

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
                    Page vesti = new VestiView(sekretar);
                    frame.NavigationService.Navigate(vesti);
                    textBlockNaslov.Text = "Vesti";
                    break;
                case 2:
                    Page termini = new TerminiView();
                    frame.NavigationService.Navigate(termini);
                    textBlockNaslov.Text = "Termini";
                    break;
                case 3:
                    Page hitan = new ZakazivanjeHitnogTerminaView();
                    frame.NavigationService.Navigate(hitan);
                    textBlockNaslov.Text = "Hitan termin";
                    break;
                case 4:
                    Page pacijenti = new PacijentiView();
                    frame.NavigationService.Navigate(pacijenti);
                    textBlockNaslov.Text = "Pacijenti";
                    break;
                case 5:
                    Page lekari = new LekariView();
                    frame.NavigationService.Navigate(lekari);
                    textBlockNaslov.Text = "Lekari";
                    break;
                case 6:
                    Page alergeni = new AlergeniView();
                    frame.NavigationService.Navigate(alergeni);
                    textBlockNaslov.Text = "Alergeni";
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

        private void FeedbackButton_Click(object sender, RoutedEventArgs e)
        {
            Page feedback = new PovratneInformacijeView(sekretar);
            frame.NavigationService.Navigate(feedback);
            textBlockNaslov.Text = "Povratne informacije";
        }
        
        private void ButtonPopUpLogout_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}