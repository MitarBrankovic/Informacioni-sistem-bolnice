using Model;
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
    /// Interaction logic for InformacijePacijent.xaml
    /// </summary>
    public partial class InformacijePacijent : Window
    {

        private ObservableCollection<Termin> termini;
        private Termin termin;
        public InformacijePacijent(ObservableCollection<Termin> termini, Termin termin)
        {
            InitializeComponent();

            this.termini = termini;
            this.termin = termin;

            textBoxIme.Text = termin.pacijent.Ime;
            textBoxPrezime.Text = termin.pacijent.Prezime;
            textBoxJMBG.Text = termin.pacijent.Jmbg;
            datePickerDatumRodjenja.SelectedDate = termin.pacijent.DatumRodjenja;

            textBoxMestoRodjenjaGrad.Text = termin.pacijent.MestoRodjenja.Ime;
            textBoxMestoRodjenjaDrzava.Text = termin.pacijent.MestoRodjenja.drzava.Ime;

            textBoxUlica.Text = termin.pacijent.AdresaStanovanja.Ulica;
            textBoxBroj.Text = termin.pacijent.AdresaStanovanja.Broj.ToString();
            textBoxGrad.Text = termin.pacijent.AdresaStanovanja.grad.Ime;
            textBoxDrzava.Text = termin.pacijent.AdresaStanovanja.grad.drzava.Ime;

            radioButtonPolM.IsChecked = termin.pacijent.Pol.Equals(Model.Pol.Muski);
            radioButtonPolZ.IsChecked = termin.pacijent.Pol.Equals(Model.Pol.Zenski);

            textBoxEmail.Text = termin.pacijent.Email;
            textBoxKontaktTelefon.Text = termin.pacijent.KontaktTelefon;
        }

        private void zdravstveniKarton_Click(object sender, RoutedEventArgs e)
        {
            ZdravstveniKarton zdravstveniKarton = new ZdravstveniKarton(termin.pacijent);
            zdravstveniKarton.Show();
        }

        private void zatviri_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
