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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrviProgram.Izgled.IzgledLekar.LekarWizard
{
    /// <summary>
    /// Interaction logic for LekoviWizard.xaml
    /// </summary>
    public partial class LekoviWizard : UserControl
    {
        public LekoviWizard()
        {
            InitializeComponent();
            TextBoxInformacije.Text = "Pregled lekova\n\n\n" +
                "1. Azuriraj - omogucava izmenu informacija o lekovima.\n\n" +
                "2. Primedba - otvara se prozor u kome se unosi tekst primedbe na odredjeni lek.\n\n" +
                "3. Lista lekova.\n\n" +
                "4. Informacije o izabranom leku.";
        }
    }
}
