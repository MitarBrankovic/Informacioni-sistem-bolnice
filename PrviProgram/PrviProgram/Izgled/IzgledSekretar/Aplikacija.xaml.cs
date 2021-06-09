using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Model;
using PrviProgram.Izgled.IzgledSekretar.Views;
using Service;

namespace PrviProgram.Izgled.IzgledSekretar
{
    public partial class Aplikacija : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private Sekretar sekretar;
        private string CurrentLanguage { get; set; }
        private string CurrentTheme { get; set; }
        private string currentTitle;
        
        public string CurrentTitle
        {
            get
            {
                return currentTitle;
            }
            set
            {
                if (value != currentTitle)
                {
                    currentTitle = value;
                    OnPropertyChanged("CurrentTitle");
                }
            }
        }

        public Aplikacija(Sekretar sekretar)
        {
            InitializeComponent();
            this.sekretar = sekretar;
            PocetniPage pocetniPage = new PocetniPage(this.sekretar);
            frame.NavigationService.Navigate(pocetniPage);
            CurrentTitle = TranslationSource.Instance["Clinic"];
            CurrentLanguage = "en-US";
            CurrentTheme = "Light";
            this.DataContext = this;
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
                    CurrentTitle = TranslationSource.Instance["Clinic"];
                    PocetniPage pocetniPage = new PocetniPage(sekretar);
                    frame.NavigationService.Navigate(pocetniPage);
                    break;
                case 1:
                    CurrentTitle = TranslationSource.Instance["News"];
                    Page vesti = new VestiView(sekretar);
                    frame.NavigationService.Navigate(vesti);
                    break;
                case 2:
                    CurrentTitle = TranslationSource.Instance["Terms"];
                    Page termini = new TerminiView();
                    frame.NavigationService.Navigate(termini);
                    break;
                case 3:
                    CurrentTitle = TranslationSource.Instance["Emergency term"];
                    Page hitan = new ZakazivanjeHitnogTerminaView();
                    frame.NavigationService.Navigate(hitan);
                    break;
                case 4:
                    CurrentTitle = TranslationSource.Instance["Patients"];
                    Page pacijenti = new PacijentiView();
                    frame.NavigationService.Navigate(pacijenti);
                    break;
                case 5:
                    CurrentTitle = TranslationSource.Instance["Doctors"];
                    Page lekari = new LekariView();
                    frame.NavigationService.Navigate(lekari);
                    break;
                case 6:
                    CurrentTitle = TranslationSource.Instance["Allergens"];
                    Page alergeni = new AlergeniView();
                    frame.NavigationService.Navigate(alergeni);
                    break;
                case 7:
                    CurrentTitle = TranslationSource.Instance["Reports"];
                    Page izvestaj = new IzvestajView(new IzvestajSekretarService());
                    frame.NavigationService.Navigate(izvestaj);
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
            CurrentTitle = TranslationSource.Instance["Feedback"];
            Page feedback = new PovratneInformacijeView(sekretar);
            frame.NavigationService.Navigate(feedback);
        }
        
        private void ButtonPopUpLogout_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}