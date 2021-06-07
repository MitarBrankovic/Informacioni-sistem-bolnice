using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Model;

namespace PrviProgram.Izgled.IzgledSekretar.Views
{
    public partial class KreiranjeGuestPacijentaView : Page
    {
        private ComboBox comboBox;

        public KreiranjeGuestPacijentaView(ComboBox comboBox)
        {
            InitializeComponent();
            this.comboBox = comboBox;
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            GuestPacijent guestPacijent = new GuestPacijent(textBoxIme.Text, textBoxPrezime.Text, textBoxJMBG.Text,
                                                            textBoxKontaktTelefon.Text,
                                                            (bool)radioButtonPolM.IsChecked ? Pol.Muski : Pol.Zenski);
            ZakazivanjeTerminaView.guestPacijent = guestPacijent;
            ZakazivanjeHitnogTerminaView.guestPacijent = guestPacijent;
            ObservableCollection<GuestPacijent> guestPacijents = new ObservableCollection<GuestPacijent>();
            guestPacijents.Add(guestPacijent);
            comboBox.ItemsSource = guestPacijents;
            comboBox.SelectedIndex = 0;
            NavigationService.GoBack();
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

    }
}
