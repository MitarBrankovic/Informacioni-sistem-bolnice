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
            GuestPacijent guestPacijent = new GuestPacijent(textBoxIme.Text, textBoxPrezime.Text, textBoxJMBG.Text,
                                                            textBoxKontaktTelefon.Text,
                                                            (bool)radioButtonPolM.IsChecked ? Pol.Muski : Pol.Zenski);
            ZakazivanjeTermina.guestPacijent = guestPacijent;
            ObservableCollection<GuestPacijent> guestPacijents = new ObservableCollection<GuestPacijent>();
            guestPacijents.Add(guestPacijent);
            comboBox.ItemsSource = guestPacijents;
            comboBox.SelectedIndex = 0;
            Close();
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
