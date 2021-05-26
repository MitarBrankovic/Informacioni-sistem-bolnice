using Controller;
using Model;
using PrviProgram.Service;
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

namespace PrviProgram.Izgled.IzgledLekar
{
    public partial class PrimedbaNaLekWindow : Window
    {
        private Lek lek;
        private Lekar lekar;
        private LekarController lekarController = new LekarController();
        private UtilityService utilityService = new UtilityService();
        public PrimedbaNaLekWindow(Lek lek, Lekar lekar)
        {
            InitializeComponent();
            this.lek = lek;
            this.lekar = lekar;
            OsnovneInformacije();
        }

        private void OsnovneInformacije()
        {
            TextboxNazivLeka.Text = lek.Naziv;
        }

        private void Posalji_Click(object sender, RoutedEventArgs e)
        {
            PrimedbaNaLek primedbaNaLek = kreiranjePrimedbe();
            lekarController.KreiranjePrimedbe(primedbaNaLek);
            this.Close();
        }

        private void Zatvori_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private PrimedbaNaLek kreiranjePrimedbe()
        {
            PrimedbaNaLek primedbaNaLek = new PrimedbaNaLek(

                utilityService.GenerisanjeSifre(),
                lekar,
                lek,
                TextboxPrimedba.Text,
                DateTime.Today
                );
            return primedbaNaLek;
        }
    }
}
