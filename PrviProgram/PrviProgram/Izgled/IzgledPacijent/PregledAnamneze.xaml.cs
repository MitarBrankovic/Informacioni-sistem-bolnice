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

namespace PrviProgram.Izgled.IzgledPacijent
{
    /// <summary>
    /// Interaction logic for PregledAnamneze.xaml
    /// </summary>
    public partial class PregledAnamneze : Page
    {
        public ObservableCollection<Pacijent> pacijenti { get; set; }
        public PacijentRepository datotekaPacijent = new PacijentRepository();
        public ObservableCollection<IzvrseniPregled> izvrseniPregledi { get; set; }
        public Pacijent selektovaniPacijent = new Pacijent();
        public PregledAnamneze(Pacijent selektovaniPacijent)
        {
            InitializeComponent();
            this.izvrseniPregledi = new ObservableCollection<IzvrseniPregled>(selektovaniPacijent.KartonPacijenta.izvrseniPregled);
            this.selektovaniPacijent = selektovaniPacijent;
            dataGridAnamneze.ItemsSource = izvrseniPregledi;
        }

        private void DodajBeleskuName_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridAnamneze.SelectedIndex != -1)
            {
             
                DodavanjeBeleske prozorZaDodavanjeBeleske = new DodavanjeBeleske((IzvrseniPregled)dataGridAnamneze.SelectedItem, izvrseniPregledi,selektovaniPacijent);
                prozorZaDodavanjeBeleske.Show();
            }
          
        }

        private void Notifikacija_Beleske_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridAnamneze.SelectedIndex != -1)
            {
                KreiranjeNotifikacijaBelske prozor = new KreiranjeNotifikacijaBelske((IzvrseniPregled)dataGridAnamneze.SelectedItem, selektovaniPacijent);
                prozor.Show();
            }

        }
    }
}
