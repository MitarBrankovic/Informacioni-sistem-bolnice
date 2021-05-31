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
    /// Interaction logic for AnamnezaWizard.xaml
    /// </summary>
    public partial class AnamnezaWizard : UserControl
    {
        public AnamnezaWizard()
        {
            InitializeComponent();
            TextBoxInformacije.Text = "Izvrsavanje anamneze\n\n\n" +
                "1. Osnovne informacije o pacijentu.\n\n" +
                "2. Polje za unos anamneze.\n\n" +
                "3. Odabir leka.\n\n" +
                "4. Polje za unos broja koji se odnosi na broj dana koliko pacijent treba da konzumira lek.\n\n" +
                "5. Polje za opis konzumacije leka. Odnosi se na nacin na koji se konzumira lek.\n\n" +
                "6. Karton pacijenta - dugme koje otvara stranicu koja omogucava uvid u karton pacijenta.\n\n" +
                "7. Uput na bolnicko lecenje - dugme koje otvara prozor na kome se nalaze polja vezana za informacije o datumima dolaska i odlaska pacijenta, kao i sobi u kojoj ce biti smesten" +
                "8. Uput za specijalistu - otvara prozor za upucivanje pacijenta kod specijaliste.\n\n" +
                "9. Zavrsi anamnezu - anamneza je kreirana.";
        }
    }
}
