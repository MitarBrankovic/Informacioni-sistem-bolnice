using Model;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PrviProgram.Izgled.IzgledLekar
{
    /// <summary>
    /// Interaction logic for PovratneInformacijeWindow.xaml
    /// </summary>
    public partial class PovratneInformacijeWindow : Window
    {
        private Osoba lekar;
        private PovratneInformacijeService povratneInformacijeService = new PovratneInformacijeService();
        private PovratneInformacijeRepository povratneInformacijeRepository = new PovratneInformacijeRepository();
        public PovratneInformacijeWindow(Lekar lekar)
        {
            InitializeComponent();
            this.lekar = lekar;
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Potvrdi_Click_1(object sender, RoutedEventArgs e)
        {
            PovratneInformacije povratneInformacije = new PovratneInformacije();
            if ((bool)Da.IsChecked)
                povratneInformacije.ImaProblem = true;
            else if ((bool)Ne.IsChecked)
                povratneInformacije.ImaProblem = false;

            if ((bool)Jedan.IsChecked)
                povratneInformacije.Ocena = 1;
            else if ((bool)Dva.IsChecked)
                povratneInformacije.Ocena = 2;
            else if ((bool)Tri.IsChecked)
                povratneInformacije.Ocena = 3;
            else if ((bool)Cetiri.IsChecked)
                povratneInformacije.Ocena = 4;
            else if ((bool)Pet.IsChecked)
                povratneInformacije.Ocena = 5;

            povratneInformacije.Primedba = Textbox.Text;
            povratneInformacije.Osoba = lekar;
            if (povratneInformacijeService.DodavanjePovratneInformacije(povratneInformacije))
            {
                Potvrdi.IsEnabled = false;
                MessageBox.Show("Uspesno ste poslali povratne informacije!");
                this.Close();
            }
        }
    }
}
