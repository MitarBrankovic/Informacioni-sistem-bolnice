using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Model;

namespace PrviProgram.Izgled.IzgledSekretar.IzgledTermini
{
    public partial class KreiranjeGuestPacijenta : Window
    {
        private ComboBox comboBox;

        public KreiranjeGuestPacijenta(ComboBox comboBox)
        {
            InitializeComponent();
            this.comboBox = comboBox;
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            GuestPacijent guestPacijent = new GuestPacijent();
            guestPacijent.Ime = textBoxIme.Text;
            guestPacijent.Prezime = textBoxPrezime.Text;
            guestPacijent.Jmbg = textBoxJMBG.Text;
            guestPacijent.Pol = ((bool)radioButtonPolM.IsChecked ? Pol.Muski : Pol.Zenski);
            guestPacijent.KontaktTelefon = textBoxKontaktTelefon.Text;
            ZakazivanjeTermina.guestPacijent = guestPacijent;
            ObservableCollection<GuestPacijent> guestPacijents = new ObservableCollection<GuestPacijent>();
            guestPacijents.Add(guestPacijent);
            comboBox.ItemsSource = guestPacijents;
            comboBox.SelectedIndex = 0;
            this.Close();
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
