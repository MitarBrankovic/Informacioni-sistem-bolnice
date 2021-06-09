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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrviProgram.Izgled.IzgledPacijent
{
    /// <summary>
    /// Interaction logic for PovratneInformacijePage.xaml
    /// </summary>
    public partial class PovratneInformacijePage : Page
    {
        private Osoba pacijent;
        private PovratneInformacijeService povratneInformacijeService = new PovratneInformacijeService();
        private PovratneInformacijeRepository povratneInformacijeRepository = new PovratneInformacijeRepository();
        public PovratneInformacijePage(Pacijent pacijent)
        {
            InitializeComponent();
            this.pacijent = pacijent;
            ProveraVecPoslato();
        }

        private void PotvrdiButton_Click(object sender, RoutedEventArgs e)
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
            povratneInformacije.Osoba = pacijent;
            if (povratneInformacijeService.DodavanjePovratneInformacije(povratneInformacije))
            {
                PotvrdiButton.IsEnabled = false;
                MessageBox.Show("Uspesno ste poslali povratne informacije!");
            }

        }
        private void ProveraVecPoslato()
        {
            List<PovratneInformacije> povratne = povratneInformacijeRepository.PregledSvihPovratnih();
            foreach (PovratneInformacije p in povratne)
            {
                if (p.Osoba.Jmbg.Equals(pacijent.Jmbg))
                    PotvrdiButton.IsEnabled = false;
            }
        }
    }
}
