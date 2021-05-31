using Controller;
using Model;
using PrviProgram.Izgled.IzgledLekar.LekarWizard;
using PrviProgram.Repository;
using PrviProgram.Service;
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

namespace PrviProgram.Izgled.IzgledLekar
{
    /// <summary>
    /// Interaction logic for PodesavanjaWindow.xaml
    /// </summary>
    public partial class PodesavanjaWindow : Window
    {
        private PodesavanjaLekarRepository podesavanjaLekarRepository = new PodesavanjaLekarRepository();
        private PodesavanjaLekarService podesavanjaLekaraService = new PodesavanjaLekarService();
        private LekarController lekarController = new LekarController();
        private bool _isToolTipVisible;
        private Lekar lekar;
        public PodesavanjaWindow(Lekar lekar)
        {
            InitializeComponent();
            this.lekar = lekar;
            _isToolTipVisible = !(podesavanjaLekarRepository.IskljucioTooltip(lekar));
            DisableButton();
        }

        private void DisableButton()
        {
            if (!_isToolTipVisible)
            {
                Off.IsEnabled = false;
            }
            else
            {
                On.IsEnabled = false;
            }
        }

        private void Uputstvo_Click(object sender, RoutedEventArgs e)
        {
            WizardWindow wizardWindow = new WizardWindow();
            wizardWindow.Show();
            this.Close();
        }

        private void On_Click(object sender, RoutedEventArgs e)
        {
            lekarController.TooltipManipulacija(_isToolTipVisible);
            OnOffButton();
            podesavanjaLekaraService.IzmenaTooltipPodesavanja(lekar);
        }

        private void Off_Click(object sender, RoutedEventArgs e)
        {
            lekarController.TooltipManipulacija(_isToolTipVisible);
            OnOffButton();
            podesavanjaLekaraService.IzmenaTooltipPodesavanja(lekar);
        }

        private void OnOffButton()
        {
            if (On.IsEnabled.Equals(true))
            {
                On.IsEnabled = false;
                Off.IsEnabled = true;
                _isToolTipVisible = true;
            }
            else
            {
                On.IsEnabled = true;
                Off.IsEnabled = false;
                _isToolTipVisible = false;
            }
        }

        private void Zatvori_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
