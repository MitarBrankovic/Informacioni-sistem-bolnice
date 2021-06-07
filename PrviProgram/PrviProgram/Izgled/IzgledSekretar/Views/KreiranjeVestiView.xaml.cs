using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Controller;
using Model;
using Service;

namespace PrviProgram.Izgled.IzgledSekretar.Views
{
    public partial class KreiranjeVestiView : Page
    {
        private SekretarController sekretarController = new SekretarController();
        private UtilityService utilityService = new UtilityService();
        private ObservableCollection<Vest> vesti;
        private Sekretar autor;

        public KreiranjeVestiView(ObservableCollection<Vest> vesti, Sekretar autor)
        {
            InitializeComponent();
            this.vesti = vesti;
            this.autor = autor;
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            Vest vest = new Vest(DateTime.Now, textBoxText.Text, utilityService.GenerisanjeSifre(), autor, textBoxNaslov.Text);
            if (sekretarController.DodavanjeVesti(vest))
            {
                vesti.Add(vest);
                NavigationService.GoBack();
            }
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

    }
}
