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
            List<Alergen> al = new List<Alergen>(alergeniRepository.PregledSvihAlergena());
            foreach (Alergen a in this.alergeni)
            {
                foreach (Alergen aa in al.ToArray())
                {
                    if (aa.Naziv.Equals(a.Naziv))
                    {
                        al.Remove(aa);
                    }
                }
            }
            comboBoxAlergen.ItemsSource = al;
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxAlergen.SelectedItem != null)
            {
                alergeni.Add((Alergen) comboBoxAlergen.SelectedItem);
            }
            this.Close();
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
