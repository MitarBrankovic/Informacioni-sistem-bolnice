using System.Windows.Controls;
using Repository;
using Service;
using Model;

namespace PrviProgram.Izgled.IzgledSekretar.Views
{
    public partial class PovratneInformacijeView : Page
    {
        private PovratneInformacijeService informacijeService = new PovratneInformacijeService();
        private PovratneInformacijeRepository informacijeRepository = new PovratneInformacijeRepository();
        private Osoba sekretar;

        public PovratneInformacijeView(Sekretar sekretar)
        {
            InitializeComponent();
            this.sekretar = sekretar;
            ProveraVecPoslato();
        }

        private void ProveraVecPoslato()
        {
            if (informacijeRepository.PregledPovratneInformacije(sekretar.Jmbg) != null)
            {
                Submit.IsEnabled = false;
            }
        }

        private void Odustani_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Potvrdi_Click(object sender, System.Windows.RoutedEventArgs e)
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

            povratneInformacije.Primedba = textBoxText.Text;
            povratneInformacije.Osoba = sekretar;
            if (informacijeService.DodavanjePovratneInformacije(povratneInformacije))
            {
                Submit.IsEnabled = false;
            }
        }

    }
}
