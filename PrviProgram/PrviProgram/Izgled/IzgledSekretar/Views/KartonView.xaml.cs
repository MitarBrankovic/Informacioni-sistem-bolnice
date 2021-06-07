using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Controller;
using Model;
using Repository;


namespace PrviProgram.Izgled.IzgledSekretar.Views
{
    public partial class KartonView : Page
    {
        private SekretarController sekretarController = new SekretarController();
        private AlergeniRepository alergeniRepository = new AlergeniRepository();
        private ObservableCollection<Alergen> alergeni;
        private Pacijent pacijent;
        private KartonPacijenta kartonPacijenta;

        public KartonView(Pacijent pacijent)
        {
            InitializeComponent();
            this.pacijent = pacijent;
            kartonPacijenta = pacijent.KartonPacijenta;
            alergeni = new ObservableCollection<Alergen>((IEnumerable<Alergen>)kartonPacijenta.GetAlergen());
            dgDataBinding.ItemsSource = alergeni;
            InicijalizacijaCombo();
            textBoxIme.Text = pacijent.Ime;
            textBoxPrezime.Text = pacijent.Prezime;
            textBoxJMBG.Text = pacijent.Jmbg;
            datePickerDatumRodjenja.SelectedDate = pacijent.DatumRodjenja;
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

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxAlergen.SelectedItem != null)
            {
                alergeni.Add((Alergen)comboBoxAlergen.SelectedItem);
                InicijalizacijaCombo();
            }
        }

        private void Obrisi_Click(object sender, RoutedEventArgs e)
        {
            if (dgDataBinding.SelectedItem != null)
            {
                alergeni.Remove((Alergen)dgDataBinding.SelectedItem);
                InicijalizacijaCombo();
            }
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            pacijent.KartonPacijenta.SetAlergen(new List<Alergen>(alergeni));
            sekretarController.IzmenaPacijenta(pacijent, pacijent);
            NavigationService.GoBack();
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

    }
}
