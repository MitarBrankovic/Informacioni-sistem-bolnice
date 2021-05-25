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
using System.Windows.Shapes;

namespace PrviProgram.Izgled.IzgledLekar
{
    /// <summary>
    /// Interaction logic for PregledAlergena.xaml
    /// </summary>

    public partial class PregledAlergena : Window
    {
        private ObservableCollection<Alergen> alergeni;
        private AlergeniRepository alergeniRepository = new AlergeniRepository();

        public PregledAlergena(Pacijent pacijent)
        {
            InitializeComponent();
            List<Alergen> alergen = new List<Alergen>();
            if (pacijent.KartonPacijenta.alergen != null)
            {
                alergen = pacijent.KartonPacijenta.alergen;
            }
            alergeni = new ObservableCollection<Alergen>(alergen);
            dgDataBinding1.ItemsSource = alergeni;
        }
    }
}
