using Controller;
using Model;
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
    /// Interaction logic for PrimedbaNaLek.xaml
    /// </summary>
    public partial class PrimedbaNaLekWindow : Window
    {
        private Lek lek;
        private Lekar lekar;
        private LekarController lekarController = new LekarController();
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
                kreiranjeSifrePrimedbe(),
                lekar,
                lek,
                TextboxPrimedba.Text,
                DateTime.Today
                );
            return primedbaNaLek;
        }

        private string kreiranjeSifrePrimedbe()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var Random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[Random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;
        }
    }
}
