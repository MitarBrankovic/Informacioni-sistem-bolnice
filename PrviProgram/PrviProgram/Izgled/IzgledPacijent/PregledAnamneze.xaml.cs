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
        public PregledAnamneze(Pacijent selektovaniPacijent)
        {
            InitializeComponent();
            this.izvrseniPregledi = new ObservableCollection<IzvrseniPregled>(selektovaniPacijent.kartonPacijenta.izvrseniPregled);
           
            dataGridPacijenta.ItemsSource = izvrseniPregledi;
        }
    }
}
