using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using Model;
using Repository;

namespace PrviProgram.Izgled.IzgledSekretar.IzgledPacijenti
{
    public partial class DodajAlergen : Window
    {
        private AlergeniRepository alergeniRepository = new AlergeniRepository();
        private ObservableCollection<Alergen> alergeni;

        public DodajAlergen(ObservableCollection<Alergen> alergeni)
        {
            InitializeComponent();
            this.alergeni = alergeni;
            InicijalizacijaCombo();
        }

        private void InicijalizacijaCombo()
        {
            List<Alergen> comboAlergeni = new List<Alergen>(alergeniRepository.PregledSvihAlergena());
            foreach (Alergen alergen in alergeni)
            {
                foreach (Alergen alergenToRemove in new List<Alergen>(comboAlergeni))
                {
                    if (alergenToRemove.Naziv.Equals(alergen.Naziv))
                    {
                        comboAlergeni.Remove(alergenToRemove);
                    }
                }
            }
            comboBoxAlergen.ItemsSource = comboAlergeni;
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxAlergen.SelectedItem != null)
            {
                alergeni.Add((Alergen)comboBoxAlergen.SelectedItem);
            }
            Close();
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
