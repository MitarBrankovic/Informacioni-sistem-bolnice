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
    /// Interaction logic for PrepisiLek.xaml
    /// </summary>
    public partial class PrepisiLek : Window
    {
        private IzvrseniPregled izvrseniPregled;
        public PrepisiLek(IzvrseniPregled izvrseniPregled, Pacijent pacijent)
        {
            InitializeComponent();
            this.izvrseniPregled = izvrseniPregled;
            TextboxPacijent.Text = pacijent.Ime + " " + pacijent.Prezime;
            if (izvrseniPregled.recept != null)
                TextboxRecept.Text = izvrseniPregled.recept.Lekovi;

        }

        private void ZavrsiIzdavenjeRecepta_Click(object sender, RoutedEventArgs e)
        {
            Recept recept = new Recept();
            recept.Lekovi = TextboxRecept.Text;
            izvrseniPregled.recept = recept;

            this.Close();
        }
    }
}
