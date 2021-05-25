using Model;
using Service;
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
    /// Interaction logic for KreiranjeNotifikacijaBelske.xaml
    /// </summary>
    public partial class KreiranjeNotifikacijaBelske : Window
    {
        public IzvrseniPregled IzvrseniPregled = new IzvrseniPregled();
        public Pacijent pacijent = new Pacijent();
        public KreiranjeNotifikacijaBelske(IzvrseniPregled izvrseniPregled,Pacijent pacijent)
        {
            InitializeComponent();
            OpisBeleske.Text = izvrseniPregled.Beleska;
            OpisBeleske.IsEnabled = false;
            this.IzvrseniPregled = izvrseniPregled;
            this.pacijent = pacijent;
        }



        private void otkaziButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PotvrdiButton_Click(object sender, RoutedEventArgs e)
        {
            Beleska beleska = new Beleska(OpisBeleske.Text, (DateTime)PocetakStizanjaNotifikacijeDate.SelectedDate,
               (DateTime)KrajStizanjaNotifikacijeDate.SelectedDate, pacijent.Jmbg, IzvrseniPregled.Sifra, comboBoxVreme.Text);
            BeleskaService.getInstance().DodavanjeBeleske(beleska);
            this.Close();

        }
    }
}
