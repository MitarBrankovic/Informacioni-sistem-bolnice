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
using Logika.LogikaSekretar;

namespace PrviProgram.Izgled.Sekretar
{
    public partial class WindowSekretar : Window
    {
        public UpravljanjePacijentima upravljanje;
        public ObservableCollection<Model.Pacijent> pacijenti;

        public WindowSekretar()
        {
            InitializeComponent();
            upravljanje = new UpravljanjePacijentima();
            pacijenti = new ObservableCollection<Model.Pacijent>(upravljanje.PregledSvihPacijenata());
            dgDataBinding.ItemsSource = pacijenti;
        }

        private void Izbrisi_Click(object sender, RoutedEventArgs e)
        {
            if ((Model.Pacijent)dgDataBinding.SelectedItem != null)
            {
                upravljanje.BrisanjePacijenta((Model.Pacijent) dgDataBinding.SelectedItem);
                pacijenti.Remove((Model.Pacijent)dgDataBinding.SelectedItem);
            }
        }

        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {
            if ((Model.Pacijent)dgDataBinding.SelectedItem != null)
            {
                IzmenaPacijenta d = new IzmenaPacijenta(pacijenti, (Model.Pacijent)dgDataBinding.SelectedItem);
                d.Show();
            }
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            Registracija d = new Registracija(pacijenti);
            d.Show();
        }
    }
}
