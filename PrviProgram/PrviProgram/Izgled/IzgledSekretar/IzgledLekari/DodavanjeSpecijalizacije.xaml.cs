using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using Controller;
using Model;
using Repository;

namespace PrviProgram.Izgled.IzgledSekretar.IzgledLekari
{
    public partial class DodavanjeSpecijalizacije : Window
    {
        private SekretarController sekretarController = new SekretarController();
        private SpecijalizacijeRepository specijalizacijeRepository = new SpecijalizacijeRepository();
        private ObservableCollection<Specijalizacija> specijalizacije;

        public DodavanjeSpecijalizacije(ObservableCollection<Specijalizacija> specijalizacije)
        {
            InitializeComponent();
            this.specijalizacije = specijalizacije;
            InicijalizacijaCombo(specijalizacije);
        }

        private void InicijalizacijaCombo(ObservableCollection<Specijalizacija> specijalizacije)
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

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            Specijalizacija specijalizacija = new Specijalizacija(comboBoxSpecijalizacija.Text);
            if (SpecijalizacijaNePostoji(specijalizacija.Naziv))
            {
                specijalizacije.Add(specijalizacija);
                sekretarController.DodavanjeSpecijalizacije(specijalizacija);
                Close();
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

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
