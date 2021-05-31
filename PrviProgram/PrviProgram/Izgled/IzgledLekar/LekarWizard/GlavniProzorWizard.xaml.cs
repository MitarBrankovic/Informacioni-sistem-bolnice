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
    /// Interaction logic for GlavniProzorWizard.xaml
    /// </summary>
    public partial class GlavniProzorWizard : UserControl
    {
        public GlavniProzorWizard()
        {
            InitializeComponent();
            TextBoxInformacije.Text = "Glavni prozor\n\n\n" +
                "1. Raspored - otvara se stranica na kojoj se nalazi lista termina. Na ovoj stranici moguce je dodavati termine, vrsiti izmene, kao i brisanje\n\n" +
                "2. Pacijent - otvara se stranica na kojoj se nalazi lista pacijenata. Na ovoj stranici moguce je pregledati karton pacijenta.\n\n" +
                "3. Pregled lekova - otvara se stranica sa listom lekova. Na ovoj stranici moguce je pregledati informacije lekova, menjati ih, kao i pisati primedbe.\n\n" +
                "4. Nalog - otvara se stranica sa informacijama o lekaru. Na ovoj stranici omoguceno vam je menjanje sopstvenih informacija, kao i korisnickog imena i lozinke.\n\n" +
                "5. Pomoc - otvara se prozor na kome mozete da ukljucite i iskljucite tooltip-ove, kao i da ponovo pogledati Wizard tutorijal.";
        }
    }
}
