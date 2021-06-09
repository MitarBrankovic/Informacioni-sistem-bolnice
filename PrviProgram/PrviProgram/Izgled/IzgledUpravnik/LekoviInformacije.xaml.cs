using Model;
using PrviProgram.ViewModel;
using Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PrviProgram.Izgled.IzgledUpravnik
{
    public partial class LekoviInformacije : Window
    {
        public LekoviInformacijeViewModel lekoviInformacijeViewModel;
        public LekoviInformacije(Lek lek)
        {
            lekoviInformacijeViewModel = new LekoviInformacijeViewModel(lek, this);
            InitializeComponent();
            this.DataContext = lekoviInformacijeViewModel;
            alternativniListView.ItemsSource = lek.ZamenaZaLek;
        }

        #region Navigacija kroz prozor
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }
        #endregion
    }
}
