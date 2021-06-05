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
    /// Interaction logic for RasporedWizard.xaml
    /// </summary>
    public partial class RasporedWizard : UserControl
    {
        public RasporedWizard()
        {
            InitializeComponent();
            TextBoxInformacije.Text = "Raspored\n\n\n" +
                "1. Karton pacijenta - dugme koje otvara stranicu koja omogucava uvid u karton pacijenta.\n\n" +
                "2. Anamneza - dugme koje otvara stranicu za izvrsavanje anamneze.\n\n" +
                "3. Dodaj - dodavanje termina.\n\n" +
                "4. Izmeni - izmena predstojeceg termina.\n\n" +
                "5. Izbrisi - brisanje termina.\n\n" +
                "6. Lista termina.";
        }
    }
}
