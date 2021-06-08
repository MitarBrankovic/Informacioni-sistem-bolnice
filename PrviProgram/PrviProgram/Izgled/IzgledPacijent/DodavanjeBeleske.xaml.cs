using Model;
using PrviProgram.ViewModel;
using Service;
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

namespace PrviProgram.Izgled.IzgledPacijent
{
    /// <summary>
    /// Interaction logic for DodavanjeBeleske.xaml
    /// </summary>
    public partial class DodavanjeBeleske : Window
    {
        private IzvrseniPreglediViewModel izvrseniPreglediViewModel;

        public DodavanjeBeleske(IzvrseniPregled selektovanaAnamneza, ObservableCollection<IzvrseniPregled> izvrseniPregledi, Pacijent selektovaniPacijent)
        {
            izvrseniPreglediViewModel = new IzvrseniPreglediViewModel(selektovanaAnamneza, izvrseniPregledi, selektovaniPacijent);
            InitializeComponent();
            this.DataContext = izvrseniPreglediViewModel;
        }
          

    }
}
