using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PrviProgram.Izgled.IzgledLekar.LekarWizard
{
    /// <summary>
    /// Interaction logic for WizardWindow.xaml
    /// </summary>
    public partial class WizardWindow : Window
    {
        private GlavniProzorWizard GlavniProzorWizard = new GlavniProzorWizard();
        private RasporedWizard RasporedWizard = new RasporedWizard();
        private AnamnezaWizard AnamnezaWizard = new AnamnezaWizard();
        private KartonPacijentaWizard KartonPacijentaWizard = new KartonPacijentaWizard();
        private PacijentWizard PacijentWizard = new PacijentWizard();
        private NalogWizard NalogWizard = new NalogWizard();
        private LekoviWizard LekoviWizard = new LekoviWizard();
        private List<UserControl> wizardList = new List<UserControl>();

        private int trenutniWizard = 0;
        public WizardWindow()
        {
            InitializeComponent();
            IncijalizacijaRedosledaWizarda();
            ButtonManipulation();
            PostaviUserControl();
        }

        private void IncijalizacijaRedosledaWizarda()
        {
            wizardList.Add(GlavniProzorWizard);
            wizardList.Add(RasporedWizard);
            wizardList.Add(AnamnezaWizard);
            wizardList.Add(KartonPacijentaWizard);
            wizardList.Add(PacijentWizard);
            wizardList.Add(LekoviWizard);
            wizardList.Add(NalogWizard);
        }

        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            trenutniWizard--;
            PostaviUserControl();
            ButtonManipulation();
        }

        private void Dalje_Click(object sender, RoutedEventArgs e)
        {
            trenutniWizard++;
            PostaviUserControl();
            ButtonManipulation();
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void PostaviUserControl()
        {
            if(trenutniWizard == wizardList.Count)
            {
                this.Close();
            }
            else
            {
            ContentArea.Children.Clear();
            ContentArea.Children.Add(wizardList[trenutniWizard]);
            }
        }

        private void ButtonManipulation()
        {
            if(trenutniWizard == 0)
            {
                Nazad.IsEnabled = false;
            }
            else
            {
                Nazad.IsEnabled = true;
            }
            
            if(trenutniWizard == wizardList.Count-1)
            {
                Dalje.Content = "Završi";
            }
            else
            {
                Dalje.Content = "Dalje";
            }
        }
    }
}
