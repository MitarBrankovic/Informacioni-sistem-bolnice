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
    /// Interaction logic for KartonPacijentaWizard.xaml
    /// </summary>
    public partial class KartonPacijentaWizard : UserControl
    {
        public KartonPacijentaWizard()
        {
            InitializeComponent();
            TextBoxInformacije.Text = "Karton pacijenta\n\n\n" +
                "1. Lista izvrsenih pregleda, kontrola ili operacija.\n\n" +
                "2. Lek - lekar ima mogucnost izmene leka, ukoliko je prethodno pritisnuo dugme azuriraj.\n\n" +
                "3. Broj dana konzumiranja leka - lekar ima uvid u broj dana konzumacije leka. Ukoliko je pritisnuo azuriraj, ovaj broj moze da promeni.\n\n" +
                "4. Selektovanjem anamneze u tekstualnom polju se prikazuje anamneza. Ukoliko se selektuje recept u tekstualnom polju se prikazuje recept.\n\n" +
                "5. Tekstualno polje.\n\n" +
                "6. Azuriraj - dugme koje omoguava izmenu informacija o izvrsenom pregledu. Kada se izvrse promene, potrebno je pritisnuti dugme \"Sacuvaj\".\n\n" +
                "7. Bolnicko lecenje - uvid u istoriju bolnickog lecenja pacijenta.\n\n" +
                "8. Alergeni";
        }
    }
}
