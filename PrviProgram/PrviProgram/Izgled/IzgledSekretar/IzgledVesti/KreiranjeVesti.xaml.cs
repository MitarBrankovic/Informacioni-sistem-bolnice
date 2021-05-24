using System;
using System.Collections.ObjectModel;
using System.Windows;
using Controller;
using Model;
using Service;

namespace PrviProgram.Izgled.IzgledSekretar.IzgledVesti
{

    public partial class KreiranjeVesti : Window
    {
        private SekretarController sekretarController = new SekretarController();
        private UtilityService utilityService = new UtilityService();
        private ObservableCollection<Vest> vesti;
        private Sekretar autor;

        public KreiranjeVesti(ObservableCollection<Vest> vesti, Sekretar autor)
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
                Close();
            }
        }

        private void Otkazi_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
