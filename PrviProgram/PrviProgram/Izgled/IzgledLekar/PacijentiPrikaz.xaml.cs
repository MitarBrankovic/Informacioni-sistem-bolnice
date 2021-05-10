using Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrviProgram.Izgled.IzgledLekar
{
    /// <summary>
    /// Interaction logic for PacijentiPrikaz.xaml
    /// </summary>
    public partial class PacijentiPrikaz : UserControl
    {
        public PacijentRepository pacijentRepository = new PacijentRepository();
        public ObservableCollection<Model.Pacijent> pacijenti;
        private PocetniPrikaz pocetniPrikaz;
        private StackPanel parent;
        public PacijentiPrikaz(PocetniPrikaz pocetniPrikaz, StackPanel parent)
        {
            InitializeComponent();
            this.pocetniPrikaz = pocetniPrikaz;
            this.parent = parent;
            pacijenti = new ObservableCollection<Model.Pacijent>(pacijentRepository.PregledSvihPacijenata());
            dgDataBinding.ItemsSource = pacijenti;
        }

        private void Informacije_Click(object sender, RoutedEventArgs e)
        {
            if (dgDataBinding.SelectedIndex != -1)
            {
                
                PacijentPrikaz pacijentPrikaz = new PacijentPrikaz(this, pocetniPrikaz, (Pacijent)dgDataBinding.SelectedItem);
                parent.Children.Remove(this);
                parent.Children.Add(pacijentPrikaz);
            }
            else
            {
                MessageBox.Show("Morate izabrati pacijenta!");

            }
        }
    }
}
