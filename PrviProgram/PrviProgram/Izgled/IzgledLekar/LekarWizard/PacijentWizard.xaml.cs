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
    /// Interaction logic for PacijentWizard.xaml
    /// </summary>
    public partial class PacijentWizard : UserControl
    {
        public PacijentWizard()
        {
            InitializeComponent();
            TextBoxInformacije.Text = "Pacijenti\n\n\n" +
                "1. Karton pacijenta - dugme koje otvara stranicu koja omogucava uvid u karton pacijenta.\n\n" +
                "2. Lista pacijenata";
        }
    }
}
