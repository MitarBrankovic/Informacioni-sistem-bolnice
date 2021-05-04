using Model;
using PrviProgram.Repository;
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

namespace PrviProgram.Izgled.IzgledPacijent
{
    /// <summary>
    /// Interaction logic for AnketiranjeBolnice.xaml
    /// </summary>
    public partial class AnketiranjeBolnice : Window
    {
        List<BolnicaAnketiranje> ankete = new List<BolnicaAnketiranje>();
        BolnicaAnketiranjeRepository datoteka = new BolnicaAnketiranjeRepository();
        public AnketiranjeBolnice()
        {
            InitializeComponent();
        }

      

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            BolnicaAnketiranje anketa = new BolnicaAnketiranje(DateTime.Now, ocenaBolniceComboBox.Text, zadovoljstvoHigijeneComboBox.Text, zadovoljstvoOsobljemComboBox.Text, uslugeBolniceComboBox.Text);
            this.ankete.Add(anketa);
            datoteka.UpisivanjeUFajl(this.ankete);
            this.Close();
           
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
