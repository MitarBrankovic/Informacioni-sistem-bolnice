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
    /// Interaction logic for PocetniPrikaz.xaml
    /// </summary>
    public partial class PocetniPrikaz : Window
    {
        private Lekar lekar;
        public UserControl stariUserControl;
        public UserControl noviUserControl;
        private TerminiPrikaz terminiPrikaz;
        private LekoviPrikaz lekoviPrikaz;
        private PacijentiPrikaz pacijentiPrikaz;
        public PocetniPrikaz(Lekar lekar)
        {
            InitializeComponent();
            this.lekar = lekar;
        }

        private void Raspored_Click(object sender, RoutedEventArgs e)
        {
            terminiPrikaz = new TerminiPrikaz(this, ContentArea, lekar);
            ContentArea.Children.Clear();
            ContentArea.Children.Add(terminiPrikaz);
        }

        private void PregledLekova_Click(object sender, RoutedEventArgs e)
        {
            lekoviPrikaz = new LekoviPrikaz(this, lekar);
            ContentArea.Children.Clear();
            ContentArea.Children.Add(lekoviPrikaz);
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
            if (stariUserControl.Parent != null)
            {
                (stariUserControl.Parent as Panel).Children.Remove(stariUserControl);
                ContentArea.Children.Add(noviUserControl);
            }
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

        public void setUserControl(UserControl stariUserControl, UserControl noviUserControl)
        {
            this.stariUserControl = stariUserControl;
            this.noviUserControl = noviUserControl;
        }

        private void Pacijenti_Click(object sender, RoutedEventArgs e)
        {
            pacijentiPrikaz = new PacijentiPrikaz(this, ContentArea);
            ContentArea.Children.Clear();
            ContentArea.Children.Add(pacijentiPrikaz);
        }
    }
}
