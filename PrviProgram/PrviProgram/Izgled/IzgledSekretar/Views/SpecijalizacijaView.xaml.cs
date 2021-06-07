using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Controller;
using Model;
using Repository;

namespace PrviProgram.Izgled.IzgledSekretar.Views
{
    public partial class SpecijalizacijaView : Page
    {
        private SekretarController sekretarController = new SekretarController();
        private SpecijalizacijeRepository specijalizacijeRepository = new SpecijalizacijeRepository();
        private ObservableCollection<Specijalizacija> specijalizacije;
        private Lekar lekar;


        public SpecijalizacijaView(Lekar lekar)
        {
            InitializeComponent();
            specijalizacije = new ObservableCollection<Specijalizacija>((IEnumerable<Specijalizacija>)lekar.GetSpecijalizacija());
            this.lekar = lekar;
            dgDataBinding.ItemsSource = specijalizacije;
            InicijalizacijaCombo();

        }

        private void InicijalizacijaCombo()
        {
            List<Specijalizacija> comboSpecijalizacije = new List<Specijalizacija>(specijalizacijeRepository.PregledSvihSpecijalizacija());
            foreach (Specijalizacija specijalizacija in specijalizacije)
            {
                foreach (Specijalizacija specijalizacijaToRemove in new List<Specijalizacija>(comboSpecijalizacije))
                {
                    if (specijalizacijaToRemove.Naziv.Equals(specijalizacija.Naziv))
                    {
                        comboSpecijalizacije.Remove(specijalizacijaToRemove);
                    }
                }
            }
            comboBoxSpecijalizacija.ItemsSource = comboSpecijalizacije;
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            Specijalizacija specijalizacija = new Specijalizacija(comboBoxSpecijalizacija.Text);
            if (SpecijalizacijaNePostoji(specijalizacija.Naziv))
            {
                specijalizacije.Add(specijalizacija);
                sekretarController.DodavanjeSpecijalizacije(specijalizacija);
                InicijalizacijaCombo();
            }
        }

        private bool SpecijalizacijaNePostoji(string naziv)
        {
            foreach (Specijalizacija specijalizacija in specijalizacije)
            {
                if (specijalizacija.Naziv.Equals(naziv))
                {
                    return false;
                }
            }
            return true;
        }

        private void Obrisi_Click(object sender, RoutedEventArgs e)
        {
            if (dgDataBinding.SelectedItem != null)
            {
                specijalizacije.Remove((Specijalizacija)dgDataBinding.SelectedItem);
                InicijalizacijaCombo();
            }
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            lekar.SetSpecijalizacija(new List<Specijalizacija>(specijalizacije));
            sekretarController.IzmenaLekara(lekar, lekar);
            NavigationService.GoBack();
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

    }
}