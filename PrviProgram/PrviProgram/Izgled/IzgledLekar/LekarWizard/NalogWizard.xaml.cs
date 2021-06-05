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
    /// Interaction logic for NalogWizard.xaml
    /// </summary>
    public partial class NalogWizard : UserControl
    {
        public NalogWizard()
        {
            InitializeComponent();
            TextBoxInformacije.Text = "Nalog\n\n\n" +
                "1. Izmeni informacije - omogucava vrsenje izmena informacija o lekaru.\n\n" +
                "2. Izloguj se - odjava sa vaseg profila.";
        }
    }
}
