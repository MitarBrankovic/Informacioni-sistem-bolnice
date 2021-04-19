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
    /// Interaction logic for PrepisiTerapiju.xaml
    /// </summary>
    public partial class PrepisiTerapiju : Window
    {
        private IzvrseniPregled izvrseniPregled;
        public PrepisiTerapiju(IzvrseniPregled izvrseniPregled)
        {
            InitializeComponent();
            this.izvrseniPregled = izvrseniPregled;

            TextboxPacijent.Text = izvrseniPregled.Termin.pacijent.Ime + " " + izvrseniPregled.Termin.pacijent.Prezime;

        }

        private void ZavrsiTerapiju_Click(object sender, RoutedEventArgs e)
        {
            Terapija terapija = new Terapija();
            terapija.Opis = TextboxTerapija.Text;
            izvrseniPregled.terapija = terapija;

            this.Close();
        }
    }
}
