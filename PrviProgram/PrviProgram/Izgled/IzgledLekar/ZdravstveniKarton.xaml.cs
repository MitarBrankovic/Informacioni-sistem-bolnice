using Model;
using PrviProgram.Repository;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ZdravstveniKarton.xaml
    /// </summary>
    public partial class ZdravstveniKarton : Window
    {

        public ObservableCollection<IzvrseniPregled> izvrseniPregledi;
        public ObservableCollection<Termin> termini;
        public PacijentRepository pacijentRepository = new PacijentRepository();
        private TerminiRepository terminiRepository = new TerminiRepository();
        private Pacijent pacijent;
        public ZdravstveniKarton(Pacijent pacijent, ObservableCollection<Termin> termini)
        {
            InitializeComponent();
            this.pacijent = pacijent;
            this.termini = termini;
            izvrseniPregledi = new ObservableCollection<IzvrseniPregled>(pacijentRepository.PregledPacijenta(pacijent.Jmbg).kartonPacijenta.izvrseniPregled);
            dataGridKarton.ItemsSource = izvrseniPregledi;
        }

        private void dataGridKarton_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Azuriraj_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridKarton.SelectedIndex != -1)
            {
                IzvrseniPregled izvrseniPregled = (IzvrseniPregled)dataGridKarton.SelectedItem;
                IzvrsavanjeAnamneze anamneza = new IzvrsavanjeAnamneze(izvrseniPregled, pacijent, termini, terminiRepository.PregledTermina(izvrseniPregled.Sifra));
                anamneza.Show();
            }
            else
            {
                MessageBox.Show("Morate izabrati termin!");
            }
        }

        private void Alergeni_Click(object sender, RoutedEventArgs e)
        {
            PregledAlergena pregledAlergena = new PregledAlergena();
            pregledAlergena.Show();
        }
    }
}
