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
    public partial class PocetniPrikaz : Window
    {
        private Lekar lekar;
        private UserControl trenutniUserControl;
        private TerminiPrikaz terminiPrikaz;
        private LekoviPrikaz lekoviPrikaz;
        private PacijentiPrikaz pacijentiPrikaz;
        private NalogPrikaz nalogPrikaz;
        private List<UserControl> listOfUserControls = new List<UserControl>();
        public PocetniPrikaz(Lekar lekar)
        {
            InitializeComponent();
            this.lekar = lekar;
        }

        private void Raspored_Click(object sender, RoutedEventArgs e)
        {
            terminiPrikaz = new TerminiPrikaz(this, lekar);
            ContentArea.Children.Clear();
            ContentArea.Children.Add(terminiPrikaz);
            ProveraKojiProzorJeOtvoren();
        }

        private void Pacijenti_Click(object sender, RoutedEventArgs e)
        {
            pacijentiPrikaz = new PacijentiPrikaz(this, ContentArea);
            ContentArea.Children.Clear();
            ContentArea.Children.Add(pacijentiPrikaz);
            ProveraKojiProzorJeOtvoren();
        }

        private void PregledLekova_Click(object sender, RoutedEventArgs e)
        {
            lekoviPrikaz = new LekoviPrikaz(this, lekar);
            ContentArea.Children.Clear();
            ContentArea.Children.Add(lekoviPrikaz);
            ProveraKojiProzorJeOtvoren();
        }

        private void Nalog_Click(object sender, RoutedEventArgs e)
        {
            nalogPrikaz = new NalogPrikaz(this, lekar);
            ContentArea.Children.Clear();
            ContentArea.Children.Add(nalogPrikaz);
            ProveraKojiProzorJeOtvoren();
        }

        public void DugmeVisibilityTrue()
        {
            GoBack.Visibility = Visibility.Visible;
        }

        public void DugmeVisibilityFalse()
        {
            GoBack.Visibility = Visibility.Collapsed;
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Children.Remove(listOfUserControls[listOfUserControls.Count - 1]);
            listOfUserControls.Remove(listOfUserControls[listOfUserControls.Count - 1]);
            ContentArea.Children.Add(listOfUserControls[listOfUserControls.Count - 1]);
            ProveraKojiProzorJeOtvoren();
        }
        private void ProveraKojiProzorJeOtvoren()
        {
            foreach(var i in ContentArea.Children)
            {
                if(i == terminiPrikaz)
                {
                    DugmeVisibilityFalse();
                }else if(i == lekoviPrikaz)
                {
                    DugmeVisibilityFalse();
                }else if (i == pacijentiPrikaz)
                {
                    DugmeVisibilityFalse();
                }
            }
        }

        public void GoBackButtonVisibilityTrue()
        {
            this.DugmeVisibilityTrue();
        }

        public void DodajUserControl(UserControl noviUserControl)
        {
            listOfUserControls.Add(noviUserControl);
            trenutniUserControl = noviUserControl;
        }

        
    }
}
